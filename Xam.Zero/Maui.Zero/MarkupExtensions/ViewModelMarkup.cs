namespace Maui.Zero.MarkupExtensions
{
    /// <summary>
    /// Resolve viewmodel from XAML
    /// </summary>
    public class ViewModelMarkup : IMarkupExtension
    {
        public Type ViewModel { get; set; }
        
        public virtual object ProvideValue (IServiceProvider serviceProvider)
        {
            var vm = ZeroApp.ServiceProvider.GetService(this.ViewModel);

            if (vm == default)
                throw new Exception($"Cannot resolve {this.ViewModel.Name}");

            return vm;
        }
    }
}