﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Hena
{
	[JsonConverter(typeof(DBKeyStringConverter))]
    public struct DBKey : IJSONSerializable, IConvertible
	{
		public readonly static DBKey Default = new DBKey(-1);

        private int _high;
        private int _low;

        public int High
        {
            get { return _high; }
            set { _high = value; }
        }
        public int Low
        {
            get { return _low; }
            set { _low = value; }
        }

		public string ToHex()
		{
			return Value.ToString("X");
		}

		public long FromHex(string hexValue)
		{
			long value = long.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
			return value;
		}

		public long Value
        {
            // -------------------------------------------------------------------
            // [WARNING]
            // javascript에서 64bit 지원 안함( 53bit까지만 지원 ) 
            // signalR통신시 누락되는 부분때문에 set Property는 함수로만 지원함.
            // 절대로 Property를 통해 값이 세팅되게 하지 말것!!!!
            // -------------------------------------------------------------------
            get { return DoubleInt2Long(High, Low); }
        }

        public string ValueString
        {
            get { return Value.ToString(); }
        }

        public void SetValue(long value)
        {
            Long2DoubleInt(value, out _high, out _low);
        }
        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        const long LOW_MASK = ((1L << 32) - 1);
        static void Long2DoubleInt(long value, out int high, out int low)
        {
            high = (int)(value >> 32);
            low = (int)(value & LOW_MASK);
        }

        static long DoubleInt2Long(int high, int low)
        {
            return (long)(((ulong)high << 32) | (uint)low);
        }

		#region Operator
		public static implicit operator long(DBKey v)
        {
            return v.Value;
        }

		public static implicit operator JToken(DBKey v)
		{
			return new JValue(v.Value);
		}

		public static implicit operator string(DBKey v)
        {
            return v.Value.ToString();
        }

        public static implicit operator DBKey(long v)
        {
            return new DBKey(v);
        }

		public static implicit operator DBKey(int v)
        {
            return new DBKey(v);
        }

        public static implicit operator DBKey(string v)
        {
            return new DBKey(long.Parse(v));
        }

		public static implicit operator DBKey(JToken v)
		{
			return new DBKey(v.AsLong());
		}

		public static bool operator ==(DBKey lhs, DBKey rhs)
        {
            return lhs.Value == rhs.Value;
        }
        public static bool operator !=(DBKey lhs, DBKey rhs)
        {
            return lhs.Value != rhs.Value;
        }
		#endregion	// Operator

		public DBKey(long value)
        {
            Long2DoubleInt(value, out _high, out _low);
        }

        public DBKey(string value)
        {
            long outValue = 0;
            long.TryParse(value, out outValue);
            Long2DoubleInt(outValue, out _high, out _low);
        }

		public bool IsValid()
		{
			return Value > 0;
		}

		public static bool CheckValidations(params DBKey[] items)
		{
			foreach( var it in items )
			{
				if (it.IsValid() == false)
					return false;
			}
			return true;
		}

        #region IJSONSerializable
        public JToken ToJSON()
		{
			return new JValue(Value);
		}

		public bool FromJSON(JToken token)
		{
			if (token == null)
				return false;

			SetValue(token.AsLong());
			return true;
		}
		#endregion // IJSONSerializable

		#region IConvertible
		TypeCode IConvertible.GetTypeCode()
		{
			return TypeCode.Object;
		}

		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(Value);
		}

		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(Value);
		}

		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(Value);
		}

		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(Value);
		}

		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(Value);
		}

		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(Value);
		}

		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(Value);
		}

		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(Value);
		}

		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(Value);
		}

		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(Value);
		}

		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(Value);
		}

		string IConvertible.ToString(IFormatProvider provider)
		{
			return Convert.ToString(Value);
		}

		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			return Convert.ChangeType(Value, conversionType);
		}

		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(Value);
		}

		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(Value);
		}

		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(Value);
		}
		#endregion // IConvertible
	}
}
