using System;
using System.Reactive;
using ReactiveUI;

namespace ReactiveForms.HelperModels
{
	public interface IAlertMessage
	{
		string Message { get; set; }
		string Title { get; set; }
		string Cancel { get; set; }
		string Accept { get; set; }
		ReactiveCommand<Unit, Unit> AcceptCommand { get; set; }
		ReactiveCommand<Unit, Unit> CancelCommand { get; set; }
	}

	public class AlertMessage : IAlertMessage
	{
		public AlertMessage(string title, string message, string cancel = "OK", string accept = null)
		{
			Title = title;
			Message = message;
			Cancel = cancel;
			Accept = accept;
		}

		public string Message { get; set; }
		public string Title { get; set; }
		public string Cancel { get; set; }
		public string Accept { get; set; }

		public ReactiveCommand<Unit, Unit> AcceptCommand { get; set; }
		public ReactiveCommand<Unit, Unit> CancelCommand { get; set; }
	}
}
