# Overview
[![Build status](https://build.appcenter.ms/v0.1/apps/f33d86fd-e112-4153-b348-75dd4a3da78d/branches/master/badge)](https://appcenter.ms)
[![Build status](https://build.appcenter.ms/v0.1/apps/b1cd9a80-419e-4515-af66-647c75796e4e/branches/master/badge)](https://appcenter.ms)

kubemob is a mobile app for Android and iOS built using [Xamarin Forms](https://docs.microsoft.com/en-us/xamarin/#pivot=platforms&panel=XamarinForms) that allows you to monitor Kubernetes clusters, akin to [Kubernetes Dashboard](https://kubernetes.io/docs/tasks/access-application-cluster/web-ui-dashboard/).


# Resources

Below are a number of resources detailing guidance and best practises related to C# and Xamarin that have been used to develop this project.

## Xamarin Forms
### Architecture
- [Enterprise Application Patterns - Bindable base](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/mvvm#updating-views-in-response-to-changes-in-the-underlying-view-model-or-model)
- [Enterprise Application Patterns - ViewModelLocator](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/mvvm#automatically-creating-a-view-model-with-a-view-model-locator)
- [Enterprise Application Patterns - Dependency Injection](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/dependency-injection)
- [Enterprise Application Patterns - Navigation Service](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/navigation#Navigating_Between_Pages)
- [Enterprise Application Patterns - Configuration](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/configuration-management)
- [Enterprise Application Patterns - Validation](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/validation)
- [Localization](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/localization?tabs=vswin)

### Performance
- [Enabling XAML Compilation](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/xaml/xamlc)
- [Using Layout compression](https://developer.xamarin.com/guides/xamarin-forms/user-interface/layouts/layout-compression/)
- [Performance best practises](https://docs.microsoft.com/en-us/xamarin/cross-platform/deploy-test/memory-perf-best-practices)
- [Listview Performance](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/listview/performance)

### UI
- [Reusable Effect Behavior](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/behaviors/reusable/effect-behavior)
- [Event to Command Behavior](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/mvvm#ui-interaction-using-commands-and-behaviors)
- [Items Control](https://docs.microsoft.com/en-us/xamarin/cross-platform/desktop/controls/wpf#itemscontrol)
- [Wrap layout](https://github.com/xamarinhq/xamu-infrastructure/blob/master/src/XamU.Infrastructure/Layout/WrapLayout.cs)

### Storage
- [Secure Storage](https://github.com/xamarin/recipes/tree/master/Recipes/xamarin-forms/General/store-credentials)

## iOS

### UI
- [Disclosure Indicator](https://montemagno.com/adding-a-disclosure-indicator-accessory-to/)

### Performance
- [Configuring the Linker](https://docs.microsoft.com/en-us/xamarin/ios/deploy-test/linker?tabs=vsmac)
- [Using LLVM code generation engine](https://docs.microsoft.com/en-us/xamarin/ios/deploy-test/compiling-for-different-devices?tabs=vsmac#Code_Generation_Engine)
- [Configuring HttpClient and TLS](https://docs.microsoft.com/en-us/xamarin/cross-platform/macios/http-stack)

## Android

### Performance
- [Configuring the Linker](https://docs.microsoft.com/en-us/xamarin/android/deploy-test/linker)
- [Configuring HttpClient and TLS](https://docs.microsoft.com/en-us/xamarin/android/app-fundamentals/http-stack?tabs=vswink)
- [Enabling Fast Renderers](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/internals/fast-renderers)


## Test
### Unit
- [NUnit Xamarin test runners](https://github.com/nunit/nunit.xamarin)

### UI
- [Xamarin.UITest](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/deploy-test/uitest-and-test-cloud?tabs=vswin)

# Future features
Features that I would like to add in the future are:

- Search
- Pull to refresh
- Create/edit/delete resources
- Caching
- App Icons / Splash screens
- Paging of lists
- Other cloud support: AWS, GCE
