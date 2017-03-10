# ReactiveForms

Reactive Forms is a set of extensions built on Xamarin Forms and ReactiveUI. There is also a dependency on Splat (1.6.2) for page navigation.

## Setup

### BasePage

Reactive Forms comes with a BasePage that abstracts some common code for setting up a page. The base page takes two arguments: The current page and the page model interface that the page uses. The interface must inherit from IBasePageModel and the page model must extend BasePageModel.

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

Any page that extends the bast page can easily show an alert. A simple alert can be shown by using the following code:

```
_alerts.OnNext(new AlertMessage("Title", "Message to show"));
```

This will show a default alert with "OK" as the only option. This can be changed by changing the `Cancel` attribute, or by passing in a different string for cancel in the constructor. You can also customize functionality by setting the `AcceptCommand` and/or `CancelCommand`.

