using System;
using System.Collections.Generic;
using System.Text;

namespace Supplementler.Model
{
    public class SupplementlerMenuModel
    {
        public string MenuName { get; set; }
        public List<SupplementlerMenuContentModel> MenuContent { get; set; }
    }

    public class SupplementlerMenuContentModel
    {
        public int MenuContentOrder { get; set; }
        public string MenuContentName { get; set; }
        public string MenuContentUrl { get; set; }
    }
}
