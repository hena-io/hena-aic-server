using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlakeGen;

namespace Hena
{
	public static class IDGenerator
	{
		public const short INVALID_MACHINE_ID = -1;
		public static short MachineId { get; private set; } = INVALID_MACHINE_ID;	// Limit ( 0 ~ 1023 )

		private static Id64Generator _id64Gen_UserId = null;			// 유저 ID
		private static Id64Generator _id64Gen_AppId = null;				// 앱 ID
		private static Id64Generator _id64Gen_AdUnitId = null;			// 광고 단위 ID
		private static Id64Generator _id64Gen_CampaignId = null;		// 캠페인 ID
		private static Id64Generator _id64Gen_AdDesignId = null;		// 광고 디자인 ID
		private static Id64Generator _id64Gen_AdResourceId = null;		// 광고 리소스 ID
		private static IdStringGeneratorWrapper _idGen_VerifyCode = null;   // 인증코드

		// 절대 변경하지 말 것!! 
		private const long _idMask = 30450829522678L;

		private static long GenCustomMask(long index)
		{
			return (index ^ _idMask);
		}

		public static bool IsValidMachineId(short machineId)
		{
			return machineId >= 0 && machineId < 1024;
		}

		public static bool SetMachineId(short machineId)
		{
			if (IsValidMachineId(machineId) == false)
				return false;

			MachineId = machineId;
			
			// 마스크 Index 순서 바꾸지 말것!!
			_id64Gen_UserId = new Id64Generator(machineId, 0, GenCustomMask(1));
			_id64Gen_AppId = new Id64Generator(machineId, 0, GenCustomMask(2));
			_id64Gen_AdUnitId = new Id64Generator(machineId, 0, GenCustomMask(3));
			_id64Gen_CampaignId = new Id64Generator(machineId, 0, GenCustomMask(4));
			_id64Gen_AdDesignId = new Id64Generator(machineId, 0, GenCustomMask(5));
			_id64Gen_AdResourceId = new Id64Generator(machineId, 0, GenCustomMask(6));

			_idGen_VerifyCode = new IdStringGeneratorWrapper(new Id64Generator(machineId, 0, GenCustomMask(5)), IdStringGeneratorWrapper.Base32, "V");
			return true;
		}

		// 사용하기전에 반드시 위에서 설정되어야 한다.
		// {91DEF2F4-6ED3-498A-8BA8-2F5E6E2AED28}
		// {BEB5A302-46D4-4209-9ECA-F9B4DD442E6E}
		public static long NewUserId { get { return _id64Gen_UserId.GenerateId(); } }
		public static long NewAppId { get { return _id64Gen_AppId.GenerateId(); } }
		public static long NewAdUnitId { get { return _id64Gen_AdUnitId.GenerateId(); } }
		public static long NewCampaignId { get { return _id64Gen_CampaignId.GenerateId(); } }
		public static long NewAdDesignId { get { return _id64Gen_AdDesignId.GenerateId(); } }
		public static long NewAdResourceId { get { return _id64Gen_AdResourceId.GenerateId(); } }
		public static string NewVerifyCode { get { return _idGen_VerifyCode.GenerateId(); } }

	}
}
