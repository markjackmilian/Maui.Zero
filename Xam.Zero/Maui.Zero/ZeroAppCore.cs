using Maui.Zero.Standard.Classes;
using Maui.Zero.ViewModels;

namespace Maui.Zero;

public class ZeroAppCore
{
    private readonly IServiceCollection _serviceCollection;
    public bool UseTransientPages { get; set; }
    public bool UseTransientViewModels { get; set; }

    internal ZeroAppCore(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
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
    public static MauiAppBuilder AddZero(this MauiAppBuilder builder, Action<ZeroAppCore> options, Func<IServiceProvider,Page> startupEvaluator)
    {
        var zeroInstance = new ZeroAppCore(builder.Services);
        options(zeroInstance);

        builder.Services.AddTransient<IZeroAppStarter>(provider => new ZeroAppStarter(startupEvaluator));
        zeroInstance.Setup();
        return builder;
    }
}