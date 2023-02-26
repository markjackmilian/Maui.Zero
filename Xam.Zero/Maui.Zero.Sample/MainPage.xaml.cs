using Maui.Zero.Sample.ViewModels;

namespace Maui.Zero.Sample;

public partial class MainPage : ContentPage
{
    private readonly ProvaModel _model;
    int count = 0;

    public MainPage(ProvaModel model)
    {
        _model = model;
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}