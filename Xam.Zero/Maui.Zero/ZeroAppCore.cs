using Maui.Zero.Classes;
using Maui.Zero.Services;
using Maui.Zero.ViewModels;

namespace Maui.Zero;

public class ZeroAppCore
{
    private readonly IServiceCollection _serviceCollection;
    public bool UseTransientPages { get; set; }
    public bool UseTransientViewModels { get; set; }

    internal ZeroAppCore(IServiceCollection serviceCollection)
    {
        this._serviceCollection = serviceCollection;
        this.UseTransientPages = true;
        this.UseTransientViewModels = true;
    }


    public void Setup()
    {
        this._serviceCollection.AddSingleton<IShellService, ShellService>();
        
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
            type => type is { IsClass: true, IsAbstract: false } && type.IsSubclassOf(typeof(ContentPage)),
            this.UseTransientPages);
    }
    
    /// <summary>
    /// Register all shells
    /// </summary>
    private void RegisterShells()
    {
        this._serviceCollection.RegisterMany(
            type => type is { IsClass: true, IsAbstract: false } && type.IsSubclassOf(typeof(Shell)),
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

public static class ZeroAppBuilder
{
    public static MauiAppBuilder AddZero(this MauiAppBuilder builder, Action<ZeroAppCore> options = null)
    {
        var zeroInstance = new ZeroAppCore(builder.Services);
        options?.Invoke(zeroInstance);

        builder.Services.AddSingleton<IPageResolver, PageResolver>();
        zeroInstance.Setup();
        return builder;
    }
}