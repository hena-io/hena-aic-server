using System;
using System.Threading.Tasks;

namespace HenaTestCode
{
	class Program
	{
		static void Main(string[] args)
		{
			Task.Run(async () =>
			{
				await HenaLibrary.HenaLibrary.Run();
				await HenaSharedModels.HenaSharedModels.Run();
				await HenaDatabaseLibrary.HenaDatabaseLibrary.Run();
			});

			Console.WriteLine("Press any key");
			Console.ReadKey();
		}
	}
}
