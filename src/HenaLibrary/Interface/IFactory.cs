using System;

namespace Hena
{
	public interface IFactory
	{
		object New();
		RT New<RT>() where RT : class;
	}
}