using Supplementler.Interface;
using Supplementler.Model;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace Supplementler.Web.Providers
{
    public class PluginProvider : IPluginProvider
    {
        public PluginProvider()
        {
            Compose();
        }

        [ImportMany]
        public IEnumerable<IPlugin> Services { get; private set; }

        private void Compose()
        {
            string filePath = $"{Directory.GetParent(Directory.GetCurrentDirectory()).FullName}\\DLLs\\";
            var assemblies = new List<Assembly>() { typeof(Program).GetTypeInfo().Assembly };
            var pluginAssemblies = Directory.GetFiles(filePath, "*.Plugin.dll", SearchOption.AllDirectories)
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                .Where(s => s.GetTypes().Where(p => typeof(IPlugin).IsAssignableFrom(p)).Any())
                .Distinct();

            assemblies.AddRange(pluginAssemblies);
            var configuration = new ContainerConfiguration().WithAssemblies(assemblies);
            using (var container = configuration.CreateContainer())
            {
                Services = container.GetExports<IPlugin>();
            }
        }

        public IEnumerable<PluginItem> GetAllDatasFromPlugins()
        {
            var pluginItems = Services.Select(service =>
                                         new PluginItem
                                         {
                                             PluginId = service.GetType().ToString(),
                                             PluginValue = service.ReadFromRedis()
                                         })
                .AsEnumerable()
                .Select(s => s)
                .ToList();

            return pluginItems;
        }
    }
}
