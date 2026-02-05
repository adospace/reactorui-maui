
namespace MauiReactor.HotReload;

internal interface ITypeLoaderEventConsumer
{
    void OnAssemblyChanged();
}


//internal interface INewComponentLoadedEventConsumer
//{
//    void OnNewComponentVersionLoaded(object component);
//}

//internal interface INewThemeLoadedEventConsumer
//{
//    void OnNewThemeVersionLoaded(object theme);
//}
