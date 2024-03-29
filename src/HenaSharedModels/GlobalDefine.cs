﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hena
{
	public static class GlobalDefine
	{
		// 잘못된 DB키 
		public readonly static DBKey INVALID_DBKEY = -1;

		// 잘못된 머신 ID
		public const short INVALID_MACHINE_ID = -1;

		// 잘못된 시간 값
		public readonly static DateTime INVALID_DATETIME = DateTime.MinValue;
	}
}
