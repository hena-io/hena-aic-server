using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hena.DB
{
	public enum DBType
	{
		None,
		Hena_AIC_Config,		// AIC 설정 DB
		Hena_AIC_Service,       // AIC 서비스 DB
		Hena_AIC_Report,        // AIC 리포트 DB
		Hena_AIC_Log,			// AIC 로그 DB
		Max,
	}

	public abstract class DBQuery : DBQueryBase
	{
		public abstract DBType DBType { get; }
	}

	public abstract class DBQuery<T_IN> : DBQuery
		where T_IN : DBQueryBase.IIN, new()
	{
		public T_IN IN { get; protected set; } = new T_IN();

		public override IIN GetInData() { return IN; }
		public override IOUT GetOutData() { return null; }
	}

	public abstract class DBQuery<T_IN, T_OUT> : DBQuery
		where T_IN : DBQueryBase.IIN, new()
		where T_OUT : DBQueryBase.IOUT, new()
	{
		public T_IN IN { get; protected set; } = new T_IN();
		public T_OUT OUT { get; protected set; } = new T_OUT();

		public override IIN GetInData() { return IN; }
		public override IOUT GetOutData() { return OUT; }
	}
}