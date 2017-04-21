using System;
using System.Reactive.Subjects;
using ReactiveForms.HelperModels;
using ReactiveUI;

namespace ReactiveForms.PageModels
{
	public interface IBasePageModel
	{
		IObservable<IAlertMessage> Alerts { get; }
		IObservable<INavigationModel> NavigateToPage { get; }
		IObservable<INavigationBackModel> NavigateBack { get; }
	}

	public class BasePageModel : ReactiveObject, IBasePageModel
	{
		public BasePageModel()
		{
			_alerts = new Subject<IAlertMessage>();
			_navigateToPage = new Subject<INavigationModel>();
			_navigateBack = new Subject<INavigationBackModel>();
		}

		protected ISubject<IAlertMessage> _alerts;
		public IObservable<IAlertMessage> Alerts => _alerts;

		protected ISubject<INavigationModel> _navigateToPage;
		public IObservable<INavigationModel> NavigateToPage => _navigateToPage;

		protected ISubject<INavigationBackModel> _navigateBack;
		public IObservable<INavigationBackModel> NavigateBack => _navigateBack;
	}
}
