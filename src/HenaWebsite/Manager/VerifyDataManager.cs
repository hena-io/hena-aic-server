using Hena.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hena
{

	[JsonConverter(typeof(StringEnumConverter))]
    public enum VerifyState
	{
        Unverified,
        Verified,
        Expired,
    }

    public class VerifyData
    {
        public string VerifyCode { get; set; } = string.Empty;
        public string CustomVerifyCode { get; set; } = string.Empty;

        private VerifyState _verifyState = VerifyState.Unverified;
        public VerifyState VerifyState
        {
            get
            {
                if (_verifyState == VerifyState.Verified)
                    return VerifyState.Verified;

                return IsExpireTimeOver ? VerifyState.Expired : VerifyState.Unverified;
            }
        }

        // 만료상태가 됐을때 자동 삭제
        public bool AutoDestroyExpiredState { get; set; } = false;

        public DateTime CreateTime { get; internal set; } = GlobalDefine.INVALID_DATETIME;
        public DateTime ExpireTime { get { return CreateTime + ExpireLifeTime; } }
        public DateTime DestroyTime { get { return CreateTime + Lifetime; } }
        public TimeSpan ExpireLifeTime { get; set; } = TimeSpan.Zero;
        public TimeSpan Lifetime { get; set; } = TimeSpan.Zero;

        public bool IsExpireTimeOver { get { return ExpireTime <= DateTime.UtcNow; } }
        public bool IsExpired { get { return VerifyState == VerifyState.Expired; } }
        public bool IsVerified { get { return VerifyState == VerifyState.Verified; } }

        public object UserData { get; set; }

        public bool TrySetVerified()
        { 
            if (IsExpireTimeOver)
                return false;

            _verifyState = VerifyState.Verified;
            return true;
        }
    }

    public class VerifyDataManager : SingletonThreadService<VerifyDataManager>
    {
        public Dictionary<string, VerifyData> Items = new Dictionary<string, VerifyData>();

        public VerifyData NewVerifyData(TimeSpan expireLifeTime, TimeSpan lifeTime, bool destroyExpiredState = true, object userData = null)
        {
            return NewVerifyData(IDGenerator.NewVerifyCode, expireLifeTime, lifeTime, destroyExpiredState, userData);
        }

        public VerifyData NewVerifyData(string verifyCode, TimeSpan expireLifeTime, TimeSpan lifeTime, bool autoDestroyExpiredState = true, object userData = null)
        {
            var verifyData = Find(verifyCode);
            if (verifyData != null)
                return null;

            var item = new VerifyData();
            item.VerifyCode = verifyCode;
            item.CreateTime = DateTime.UtcNow;
            item.ExpireLifeTime = expireLifeTime;
            item.Lifetime = lifeTime;
            item.UserData = userData;
            item.AutoDestroyExpiredState = autoDestroyExpiredState;

            Items.Add_LockThis(item.VerifyCode, item);
            return item;
        }


        public VerifyData Find(string verifyCode)
        {
            return Items.TryGetValueEx_LockThis(verifyCode);
        }

        public void Remove(string verifyCode)
        {
            Items.Remove_LockThis(verifyCode);
        }

        public bool Contains(string verifyCode)
        {
            return Items.ContainsKey_LockThis(verifyCode);
        }


        private void Tick_UpdateDestroy(double deltaTimeSec)
        {
            DateTime utcNow = DateTime.UtcNow;
            var items = Items.ToArray_LockThis();
            foreach (var item in items)
            {
                var value = item.Value;
                if (value.DestroyTime <= utcNow 
                    || (value.AutoDestroyExpiredState && value.IsExpired))
                {
                    Items.Remove_LockThis(item.Key);
                }
            }
        }

        #region IService
        protected override void OnBeginService()
        {
            UpdateIntervalMS = 500;
        }
        protected override void OnUpdateService(double deltaTimeSec)
        {
            Tick_UpdateDestroy(deltaTimeSec);
        }
        protected override void OnEndService() { }
        #endregion // IService


    }
}
