using System;
using System.Reactive;

namespace ReactiveForms.HelperModels
{
	public interface INavigationBackModel
	{
		int CountToGoBack { get; set; }
		bool Animated { get; set; }
		bool IsModal { get; set; }
		IObserver<Unit> ToBeCompleted { get; set; }
	}

	public class NavigationBackModel : INavigationBackModel
	{
		public NavigationBackModel(IObserver<Unit> toBeCompleted = null, bool animated = true, bool isModal = false, int countToGoBack = 0)
		{
			CountToGoBack = countToGoBack;
			ToBeCompleted = toBeCompleted;
			IsModal = isModal;
			Animated = animated;
		}

		public int CountToGoBack { get; set; }
		public bool Animated { get; set; }
		public bool IsModal { get; set; }
		public IObserver<Unit> ToBeCompleted { get; set; }
	}
}
