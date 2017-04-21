using System;
using System.Collections.Generic;
using ReactiveUI;
using Xamarin.Forms;

namespace Test.Pages
{
	public partial class TestPage
	{
		public TestPage()
		{
			InitializeComponent();

			this.BindCommand(ViewModel, vm => vm.ShowAlertCommand, v => v.ShowAlertButton);
		}
	}
}
