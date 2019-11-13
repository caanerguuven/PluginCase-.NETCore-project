using Supplementler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supplementler.Web.Providers
{
    public interface IPluginProvider
    {
        IEnumerable<PluginItem> GetAllDatasFromPlugins();
    }
}
