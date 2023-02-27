using Maui.Zero.ViewModels;

namespace Maui.Zero.Services
{
    public interface IPageResolver
    {
        /// <summary>
        /// Resolve a page
        /// </summary>
        /// <param name="previousModel"></param>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T ResolvePage<T>(ZeroBaseModel previousModel = null, object data = null) where T : Page;

        /// <summary>
        /// Resolve a page using an explicit Type
        /// </summary>
        /// <param name="pageType"></param>
        /// <param name="previousModel"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Page ResolvePage(Type pageType, ZeroBaseModel previousModel, object data);
    }
    
    class PageResolver : IPageResolver
    {
        private readonly IServiceProvider _serviceProvider;
        private static Type[] _assemblyTypes;

        public PageResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T ResolvePage<T>(ZeroBaseModel previousModel = null, object data = null) where T : Page
        {
            var page = this._serviceProvider.GetService<T>();
            if (page == null)
                throw new Exception($"Cannot resolve {typeof(T).Name}");
            var context = (ZeroBaseModel)page.BindingContext ?? this.ResolveViewModelByConvention(page);

            context.CurrentPage = page;
            context.PreviousModel = previousModel;
            Utility.Utility.InvokeReflectionPrepareModel(context, data);

            return page;
        }

        public Page ResolvePage(Type pageType, ZeroBaseModel previousModel, object data)
        {
            var page = (Page)this._serviceProvider.GetService(pageType);
            if (page == null)
                throw new Exception($"Cannot resolve {pageType.Name}");
            
            var context = (ZeroBaseModel)page.BindingContext ?? this.ResolveViewModelByConvention(page);

            context.CurrentPage = page;
            context.PreviousModel = previousModel;
            Utility.Utility.InvokeReflectionPrepareModel(context, data);

            return page;
        }

        private ZeroBaseModel ResolveViewModelByConvention(Page page)
        {
            if (_assemblyTypes == null)
            {
                var pageAssemply = page.GetType().Assembly;
                _assemblyTypes = pageAssemply.GetTypes().Where(w => w.IsClass).Where(w => !w.IsAbstract)
                    .Where(w => w.IsSubclassOf(typeof(ZeroBaseModel))).ToArray();
            }

            var viewModelName = $"{page.GetType().Name}ViewModel";
            var vmType = _assemblyTypes.Single(sd => sd.Name == viewModelName);
            var context =  (ZeroBaseModel)this._serviceProvider.GetService(vmType);
            page.BindingContext = context;
            return context;
        }
    }
}