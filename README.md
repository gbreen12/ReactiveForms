# ReactiveForms

Reactive Forms is a set of extensions built on Xamarin Forms and ReactiveUI. There is also a dependency on Splat (1.6.2) for page navigation.

## Setup

### BasePage

Reactive Forms comes with a `BasePage` that abstracts some common code for setting up a page. The base page takes two arguments: The current page and the page model interface that the page uses. The interface must inherit from `IBasePageModel` and the page model must extend `BasePageModel`.

Example:

FirstPage.xaml
```
<?xml version="1.0" encoding="UTF-8"?>
<rf:BasePage 
	xmlns="http://xamarin.com/schemas/2014/forms" 
	xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" 
	x:Class="NugetTest.Pages.FirstPage"
	xmlns:rf="clr-namespace:ReactiveForms.Pages;assembly=ReactiveForms"
	xmlns:pages="clr-namespace:NugetTest.Pages;assebmly=NugetTest"
	xmlns:pageModels="clr-namespace:NugetTest.PageModels;assembly=NugetTest"
	x:TypeArguments="pages:FirstPage,pageModels:IFirstPageModel">
	<Button Text="Push Me" x:Name="ChangePageButton" />
</rf:BasePage>
```

FirstPageModel.cs
```
public interface IFirstPageModel : IBasePageModel
{
}

public class FirstPageModel : BasePageModel, IFirstPageModel
{
	public FirstPageModel()
		: base()
	{
	}
}
```

### Show Alert

Any page that extends the bast page can easily show an alert. A simple alert can be shown by using the following code in the view model:

```
_alerts.OnNext(new AlertMessage("Title", "Message to show"));
```

This will show a default alert with "OK" as the only option. This can be changed by changing the `Cancel` attribute, or by passing in a different string for cancel in the constructor. You can also customize functionality by setting the `AcceptCommand` and/or `CancelCommand`.

The following example shows how to use the `AcceptCommand` and `CancelCommand`:

```
public interface IFirstPageModel : IBasePageModel
{
	ReactiveCommand<Unit, Unit> ShowAlertCommand { get; }
}

public class FirstPageModel : BasePageModel, IFirstPageModel
{
	public FirstPageModel()
		: base()
	{
		ShowAlertCommand = ReactiveCommand.CreateFromObservable(() => {
			var response = new AsyncSubject<Unit>();

			_alerts.OnNext(new AlertMessage("Title", "Message to show", "Cancel", "Accept"){
					AcceptCommand = ReactiveCommand.CreateFromObservable(() => {
						// Do work
						response.OnNext(Unit.Default);
						response.OnCompleted();
					}),
					CancelCommand = ReactiveCommand.CreateFromObservable(() => {
						// Do work
						response.OnNext(Unit.Default);
						response.OnCompleted();
					})
				});

			return response;
		});
	}

	public ReactiveCommand<Unit, Unit> ShowAlertCommand { get; private set; }
}

```

### Page Navigation

Reactive Forms allows for simple page navigation. If a page extends the `BaseNavigationPage` and the page model and page model interface extend `BaseNavigationPageModel` and `IBaseNavigationPageModel` respectively, two observables are exposed: `_navigateToPage` and `_navigateBack`.  To use page navigation, do the following:

FirstPage.xaml
```
<?xml version="1.0" encoding="UTF-8"?>
<rf:BaseNavigationPage 
	xmlns="http://xamarin.com/schemas/2014/forms" 
	xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" 
	x:Class="NugetTest.Pages.FirstPage"
	xmlns:rf="clr-namespace:ReactiveForms.Pages;assembly=ReactiveForms"
	xmlns:pages="clr-namespace:NugetTest.Pages;assebmly=NugetTest"
	xmlns:pageModels="clr-namespace:NugetTest.PageModels;assembly=NugetTest"
	x:TypeArguments="pages:FirstPage,pageModels:IFirstPageModel">
	<Button Text="Push Me" x:Name="ChangePageButton" />
</rf:BaseNavigationPage>
```

FirstPageModel.cs
```
public interface IFirstPageModel : IBaseNavigationPageModel
{
	ReactiveCommand<Unit, Unit> NavigateToNextPageCommand { get; }
	ReactiveCommand<Unit, Unit> NavigateBackCommand { get; }
}

public class FirstPageModel : BaseNavigationPageModel, IFirstPageModel
{
	public FirstPageModel()
		: base()
	{
		NavigateToNextPageCommand = ReactiveCommand.CreateFromObservable(() => {
			var response = new AsyncSubject<Unit>();

			var pageModel = new SecondPageModel();
			_navigateToPage.OnNext(new NavigationModel(pageModel, response));

			return response;
		});

		NavigateBackCommand = ReactiveCommand.CreateFromObservable(() => {
			var response = new AsyncSubject<Unit>();

			_navigateBack.OnNext(new NavigationBackModel(response));

			return response;
		});
	}

	public ReactiveCommand<Unit, Unit> NavigateToNextPageCommand { get; private set; }
	public ReactiveCommand<Unit, Unit> NavigateBackCommand { get; private set; }
}
```

Two other pieces of setup must happen before you can use the navigation.

1. The page model of the page you are navigating to must inherit from `IPageModelNavigateTo`
2. You must setup the `Splat` connection like: `Locator.CurrentMutable.Register(() => new SecondPage(), typeof(SecondPageModel));`

### NavigationModel

The navigation model allows the user to setup some fields:

1. `Model`: The page model you want to navigate to. Must inherit from `IPageModelNavigateTo`
2. `ToBoCompleted`: If navigating within a command, this is the subject to be completed from the command
3. `Animated`: Whether the page navigation should be animated
4. `IsModel`: Whether the page should be used as a modal

### NavigationBackModel

The navigation back model also has some optional setup fields

1. `ToBeCompleted`: If navigating within a command, this is the subject to be completed from the command
2. `Animated`: Whether the page navigation should be animated
3. `IsModal`: Whether the page should be used as a modal
4. `CountToGoBack`: If you want to go back multiple pages at the same time, you can use this field

