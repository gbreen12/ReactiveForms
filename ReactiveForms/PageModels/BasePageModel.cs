using System;
using System.Reactive.Subjects;
using ReactiveForms.HelperModels;
using ReactiveUI;

namespace ReactiveForms.PageModels
{
	public interface IBasePageModel
	{
		IObservable<IAlertMessage> Alerts { get; }
	}

	public class BasePageModel : ReactiveObject, IBasePageModel
	{
		public BasePageModel()
		{
			_alerts = new Subject<IAlertMessage>();
		}

		protected ISubject<IAlertMessage> _alerts;
		public IObservable<IAlertMessage> Alerts => _alerts;
	}
}
