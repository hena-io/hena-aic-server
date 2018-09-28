using System;

namespace Hena
{
	public interface IService
	{
		void StartService();
		void UpdateService(double deltaTimeSec);
		void StopService();
	}
}
