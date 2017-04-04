using Test.PageModels;
using Test.Pages;
using Xamarin.Forms;

namespace Test
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			var viewModel = new TestPageModel();
			MainPage = new TestPage() { ViewModel = viewModel };
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
