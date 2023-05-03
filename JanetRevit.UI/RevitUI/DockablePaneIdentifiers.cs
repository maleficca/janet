using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JanetRevit.UI.Properties;

namespace JanetRevit.UI.RevitUI
{
    public class DockablePaneIdentifiers
    {
        public static Guid GetPaneIdentifier()
        {
            return new Guid(Resources.DockablePaneGUID);
        }
    }
}
