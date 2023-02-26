using Maui.Zero.Classes;
using Microsoft.Extensions.DependencyInjection;

namespace Maui.Zero;

public class ZeroApp
{
    private readonly IServiceCollection _serviceCollection;
    private readonly Type _shellType;
    private readonly Type _pageType;
    public bool UseTransientPages { get; set; }
    public bool UseTransientViewModels { get; set; }

    internal ZeroApp(IServiceCollection serviceCollection, Type shellType, Type pageType)
    {
        _serviceCollection = serviceCollection;
        _shellType = shellType;
        _pageType = pageType;
        this.UseTransientPages = true;
        this.UseTransientViewModels = true;
    }


    public void Setup()
    {
        this.RegisterModels();
        this.RegisterPages();
        this.RegisterShells();
    }

    /// <summary>
    /// Register all pages
    /// </summary>
    private void RegisterPages()
    {
        this._serviceCollection.RegisterMany(
            type => type is { IsClass: true, IsAbstract: false } && type.IsSubclassOf(this._pageType),
            this.UseTransientPages);
    }
    
    /// <summary>
    /// Register all shells
    /// </summary>
    private void RegisterShells()
    {
        this._serviceCollection.RegisterMany(
            type => type is { IsClass: true, IsAbstract: false } && type.IsSubclassOf(this._shellType),
            this.UseTransientPages);
    }

    /// <summary>
    /// Register all ViewModels
    /// </summary>
    private void RegisterModels()
    {
        this._serviceCollection.RegisterMany(
            type => type is { IsClass: true, IsAbstract: false } && type.IsSubclassOf(typeof(ZeroBaseModel)),
            this.UseTransientViewModels);
    }
}

internal class ZeroBaseModel
{
}

public static class ZeroAppBuilder
{
    public static void Setup(this IServiceCollection services, Type shellBaseType,Type pageBaseType, Action<ZeroApp> options)
    {
        var zeroInstance = new ZeroApp(services, shellBaseType,pageBaseType);
        options(zeroInstance);

        zeroInstance.Setup();
    }
}