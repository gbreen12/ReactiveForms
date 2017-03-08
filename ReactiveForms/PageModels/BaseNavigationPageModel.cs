using System;
using System.Reactive.Subjects;
using ReactiveForms.HelperModels;

namespace ReactiveForms.PageModels
{
	public interface IBaseNavigationPageModel : IBasePageModel
	{
		IObservable<INavigationModel> NavigateToPage { get; }
		IObservable<INavigationBackModel> NavigateBack { get; }
	}

	public class BaseNavigationPageModel : BasePageModel, IBaseNavigationPageModel
	{
		public BaseNavigationPageModel()
			: base()
		{
			_navigateToPage = new Subject<INavigationModel>();
			_navigateBack = new Subject<INavigationBackModel>();
		}

		protected ISubject<INavigationModel> _navigateToPage;
		public IObservable<INavigationModel> NavigateToPage => _navigateToPage;

		protected ISubject<INavigationBackModel> _navigateBack;
		public IObservable<INavigationBackModel> NavigateBack => _navigateBack;
	}
}
