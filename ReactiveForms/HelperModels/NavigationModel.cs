using System;
using System.Reactive;
using System.Reactive.Subjects;

namespace ReactiveForms.HelperModels
{
	public interface INavigationModel
	{
		IPageModelNavigateTo Model { get; set; }
		bool Animated { get; set; }
		bool IsModal { get; set; }
		ISubject<Unit> ToBeCompleted { get; set; }
	}

	public class NavigationModel : INavigationModel
	{
		public NavigationModel(IPageModelNavigateTo model, ISubject<System.Reactive.Unit> toBeCompleted = null, bool animated = true, bool isModal = false)
		{
			Model = model;
			ToBeCompleted = toBeCompleted;
			Animated = animated;
			IsModal = isModal;
		}

		public IPageModelNavigateTo Model { get; set; }
		public bool Animated { get; set; }
		public bool IsModal { get; set; }
		public ISubject<Unit> ToBeCompleted { get; set; }
	}
}
