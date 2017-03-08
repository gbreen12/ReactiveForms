using System;
using System.Reactive.Linq;
using ReactiveForms.PageModels;
using ReactiveUI;
using Xamarin.Forms;

namespace ReactiveForms.Pages
{
	public class BasePage<TV, TVM> : ContentPage, IViewFor<TVM> where TVM : class, IBasePageModel
	{
		public BasePage()
		{
			if (Device.OS == TargetPlatform.iOS)
				// iOS specific padding to move page out of status bar.
				Padding = new Thickness(0, 20, 0, 0);

			this.WhenAnyObservable(v => v.ViewModel.Alerts)
				.ObserveOn(RxApp.MainThreadScheduler)
				.Subscribe(async message =>
			{
				await this.DisplayMessage(message);
			});
		}

		/***********************************************************************************************************
			ReactiveUI boilerplate
		 *********************************************************************************************************/
		public static readonly BindableProperty ViewModelProperty =
		   BindableProperty.Create(
			   nameof(ViewModel),
			   typeof(TVM),
			   typeof(TV));

		public TVM ViewModel
		{
			get { return (TVM)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}

		object IViewFor.ViewModel
		{
			get { return ViewModel; }
			set { ViewModel = (TVM)value; }
		}
	}
}

