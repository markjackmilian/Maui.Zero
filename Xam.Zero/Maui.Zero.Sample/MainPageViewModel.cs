using System.Windows.Input;
using Maui.Zero.ViewModels;
using mvvm.zcommand.ZCommand;

namespace Maui.Zero.Sample;

public class MainPageViewModel : ZeroBaseModel
{
    public ICommand GoToPage1Command { get; private set; }
    
    public MainPageViewModel()
    {
        this.GoToPage1Command = ZeroCommand.On(this)
            .WithExecute((o, context) => this.PushModal<Page1>())
            .Build();
    }
}