using System;
using System.Reactive.Linq;
using ReactiveForms.PageModels;
using ReactiveUI;
using Xamarin.Forms;

namespace ReactiveForms.Pages
{
	public class BaseNavigationPage<TV, TVM> : BasePage<TV, TVM> where TVM : class, IBaseNavigationPageModel
	{
		public BaseNavigationPage()
		{
			this.WhenAnyObservable(p => p.ViewModel.NavigateToPage)
				.ObserveOn(RxApp.MainThreadScheduler)
				.Subscribe(async model =>
				{
					await this.NavigateToPageForPageModel(model);
				});

			this.WhenAnyObservable(p => p.ViewModel.NavigateBack)
				.ObserveOn(RxApp.MainThreadScheduler)
				.Subscribe(async model =>
				{
					await this.NavigateBack(model);
				});
		}
	}
}

