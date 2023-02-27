using Maui.Zero.ViewModels;

namespace Maui.Zero.Sample;

public class Page1ViewModel : ZeroBaseModel
{
    public Page1ViewModel()
    {
        this.TestText = "Prova da binding";
    }

    public string TestText { get; set; }

    protected override void PrepareModel(object data)
    {
        var t = 5;
        base.PrepareModel(data);
    }
}