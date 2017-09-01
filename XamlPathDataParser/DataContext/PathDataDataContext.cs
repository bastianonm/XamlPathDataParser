using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamlPathDataParser.DataContext.PathDataItems;

namespace XamlPathDataParser.DataContext
{
    public class PathDataDataContext
    {
        public ObservableCollection<IPathDataItem> PathDataItems { get; set; }

    }
}
