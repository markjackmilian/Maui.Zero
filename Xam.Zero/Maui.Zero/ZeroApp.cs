namespace Maui.Zero;

public class ZeroApp : Application
{
    public ZeroApp(IZeroAppStarter starter)
    {
        if (starter.StartEvaluator == null)
            throw new Exception("No startup evaluator configured");

        this.MainPage = starter.StartEvaluator.Invoke();
    }

   
}

