namespace Maui.Zero;

public class ZeroApp : Application
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public ZeroApp(IServiceProvider serviceProvider,IZeroAppStarter starter)
    {
        if (starter.StartEvaluator == null)
            throw new Exception("No startup evaluator configured");
        ServiceProvider = serviceProvider;
        this.MainPage = starter.StartEvaluator.Invoke(serviceProvider);
    }

   
}

