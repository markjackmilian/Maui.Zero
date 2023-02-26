namespace Maui.Zero;

public interface IZeroAppStarter
{
    Func<Page> StartEvaluator { get; }
}

class ZeroAppStarter : IZeroAppStarter
{
    public Func<Page> StartEvaluator { get; }

    public ZeroAppStarter(Func<Page> startEvaluator)
    {
        StartEvaluator = startEvaluator;
    }
}