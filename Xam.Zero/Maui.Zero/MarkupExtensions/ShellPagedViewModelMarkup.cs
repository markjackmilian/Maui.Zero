using Maui.Zero.ViewModels;

namespace Maui.Zero.MarkupExtensions
{
    /// <summary>
    /// Resolve ViewModel from XAML
    /// pass Page too 
    /// </summary>
    public class ShellPagedViewModelMarkup : ViewModelMarkup
    {
        public Page Page { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var baseModel = (ZeroBaseModel)ZeroApp.ServiceProvider.GetService(this.ViewModel);
            if (baseModel == default)
                throw new Exception($"Cannot resolve {this.ViewModel.Name}");
            
            baseModel.CurrentPage = this.Page;
            
            Utility.Utility.InvokeReflectionPrepareModel(baseModel, null);
            return baseModel;
        }
    }
}