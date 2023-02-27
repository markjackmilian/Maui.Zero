using Maui.Zero.Sample.ViewModels;

namespace Maui.Zero.Sample;

public partial class MainPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;
    int count = 0;

    public MainPage(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        InitializeComponent();
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {

        await this.Navigation.PushModalAsync(this._serviceProvider.GetService<Page1>());
        
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}