using System;
using ReactiveForms.PageModels;

namespace Test.PageModels
{
	public interface ITestPageModel : IBasePageModel
	{
		
	}

	public class TestPageModel : BasePageModel, ITestPageModel
	{
		public TestPageModel()
		{
		}
	}
}
