using System;
using System.Reactive;
using System.Reactive.Subjects;
using ReactiveForms.HelperModels;
using ReactiveForms.PageModels;
using ReactiveUI;

namespace Test.PageModels
{
	public interface ITestPageModel : IBasePageModel
	{
		ReactiveCommand<Unit, Unit> ShowAlertCommand { get; }
	}

	public class TestPageModel : BasePageModel, ITestPageModel
	{
		public TestPageModel()
		{
			ShowAlertCommand = ReactiveCommand.CreateFromObservable(() => 
			{
				var response = new AsyncSubject<Unit>();
				_alerts.OnNext(new AlertMessage("Alert", "An alert") 
				{
					CancelCommand = ReactiveCommand.CreateFromObservable(() => 
					{
						response.OnNext(Unit.Default);
						response.OnCompleted();
						return response;
					})
				});
				return response;
			});
		}

		private ReactiveCommand<Unit, Unit> _showAlertCommand;
		public ReactiveCommand<Unit, Unit> ShowAlertCommand
		{
			get { return _showAlertCommand; }
			set { this.RaiseAndSetIfChanged(ref _showAlertCommand, value); }
		}
	}
}
