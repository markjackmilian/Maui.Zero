using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Maui.Zero.Classes;

static class ServiceCollectionHelper
{
    /// <summary>
    /// Register many services
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="filter"></param>
    /// <param name="isDefaultTransient"></param>
    public static void RegisterMany(this IServiceCollection serviceCollection,Func<Type, bool> filter, bool isDefaultTransient)
    {
        var types = AppDomain.CurrentDomain.GetAssemblies()
            // .Where(w => !w.FullName.StartsWith("JetBrains")) // kludge fix error in rider
            .SelectMany(s => s.GetTypes());

        var models = types.Where(filter.Invoke).ToArray();


        foreach (var model in models)
        {
            var transient = model.GetCustomAttribute<TransientAttribute>();
            var isTransient = isDefaultTransient || transient != null;

            if (isTransient)
                serviceCollection.AddTransient(model);
            else
                serviceCollection.AddSingleton(model);
        }
    }
}