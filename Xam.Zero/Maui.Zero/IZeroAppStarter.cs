namespace Maui.Zero;

public interface IZeroAppStarter
{
    Func<IServiceProvider,Page> StartEvaluator { get; }
}

class ZeroAppStarter : IZeroAppStarter
{
    public Func<IServiceProvider,Page> StartEvaluator { get; }

    public ZeroAppStarter(Func<IServiceProvider,Page> startEvaluator)
    {
        StartEvaluator = startEvaluator;
    }
}