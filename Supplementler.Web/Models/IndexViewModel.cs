using Supplementler.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Supplementler.Web
{
    public class IndexViewModel
    {
        public List<SupplementlerMenuModel> MenuList { get; set; }
        public List<SupplementlerWidgetModel> WidgetList { get; set; }
    }
}
