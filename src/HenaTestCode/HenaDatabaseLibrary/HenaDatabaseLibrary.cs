using Hena;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HenaTestCode.HenaDatabaseLibrary
{
	public static class HenaDatabaseLibrary
	{
		public static async Task Run()
		{
			Test_IdGenerator();
			await Task.Yield();
		}

		public static void Test_IdGenerator()
		{
			Console.WriteLine($"START ID GENERATOR");
			IDGenerator.SetMachineId(1);
			HashSet<long> hsLong = new HashSet<long>();
			for( int i = 0; i < 100000; ++i)
			{
				var newAppId = IDGenerator.NewAppId;
				var newAppUnitId = IDGenerator.NewAdUnitId;
				if ( hsLong.Contains(newAppId) )
				{
					Console.WriteLine($"APP ID - {newAppId}(CONTAINS)");
				}
				hsLong.Add(newAppId);
			}
			Console.WriteLine($"END ID GENERATOR");
		}
	}
}
