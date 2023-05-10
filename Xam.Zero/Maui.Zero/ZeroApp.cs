namespace Maui.Zero;

public class ZeroApp : Application
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public ZeroApp(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

   
}

