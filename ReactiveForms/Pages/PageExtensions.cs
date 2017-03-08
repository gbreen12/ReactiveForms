using System;
using System.Threading.Tasks;
using ReactiveForms.HelperModels;
using ReactiveUI;
using Splat;
using Xamarin.Forms;

namespace ReactiveForms.Pages
{
	public static class PageExtensions
	{
		public static async Task NavigateToPageForPageModel(this Page contentPage, INavigationModel navigationModel)
		{
			try
			{
				var page = Locator.Current.GetService(navigationModel.Model.GetType()) as Page;

				var pageCasted = (IViewFor)page;
				if (pageCasted != null) pageCasted.ViewModel = navigationModel.Model;

				if (navigationModel.IsModal)
					await contentPage.Navigation.PushModalAsync(page, navigationModel.Animated);
				else
					await contentPage.Navigation.PushAsync(page, navigationModel.Animated);
			}
			catch (Exception e)
			{
				await contentPage.DisplayAlert("Error", $"There was an error navigating to a new page. {e}", "OK");
			}
			finally
			{
				navigationModel?.ToBeCompleted.OnCompleted();
			}
		}

		public static async Task NavigateBack(this Page contentPage, INavigationBackModel model)
		{
			int i = 0;
			while (i < model.CountToGoBack && contentPage.Navigation.NavigationStack.Count > 1)
			{
				var index = contentPage.Navigation.NavigationStack.Count - 2;
				var pageToRemove = contentPage.Navigation.NavigationStack[index];
				contentPage.Navigation.RemovePage(pageToRemove);
				i++;
			}

			if (model.IsModal)
				await contentPage.Navigation.PopModalAsync(model.Animated);
			else
				await contentPage.Navigation.PopAsync(model.Animated);

			if (model.ToBeCompleted != null)
			{
				model.ToBeCompleted.OnNext(new System.Reactive.Unit());
				model.ToBeCompleted.OnCompleted();
			}
		}

		public static async Task DisplayMessage(this Page page, IAlertMessage messageToDisplay)
		{
			if (!string.IsNullOrEmpty(messageToDisplay.Accept) && messageToDisplay.AcceptCommand != null)
			{
				var response = await page.DisplayAlert(
					messageToDisplay.Title,
					messageToDisplay.Message,
					messageToDisplay.Accept,
					messageToDisplay.Cancel);

				if (response)
					messageToDisplay.AcceptCommand.Execute();
				else
					messageToDisplay.CancelCommand?.Execute();
			}
			else
			{
				await page.DisplayAlert(
					messageToDisplay.Title,
					messageToDisplay.Message,
					messageToDisplay.Cancel);

				messageToDisplay.CancelCommand?.Execute();
			}
		}
	}
}
