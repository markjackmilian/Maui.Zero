namespace Maui.Zero.Services;

public interface IShellService
{
    void SwitchContainer<TShell>() where TShell : Shell;
}

internal class ShellService : IShellService
{
    private readonly IServiceProvider _serviceProvider;

    public ShellService(IServiceProvider serviceProvider)
    {
        this._serviceProvider = serviceProvider;
    }
    
    public void SwitchContainer<TShell>() where TShell : Shell
    {
        var shell = this._serviceProvider.GetService<TShell>();
        if (shell == null)
            throw new Exception($"Cannot switch to shell of type: {typeof(TShell)}. Have you register it?");

        Application.Current.MainPage = shell;
    }
}