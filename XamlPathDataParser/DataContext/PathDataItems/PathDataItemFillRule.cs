using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace XamlPathDataParser.DataContext.PathDataItems
{
    public class PathDataItemFillRule : IPathDataItem
    {

        public int FillRule { get; set; }

        public PathDataItemFillRule()
        { }

        public PathDataItemFillRule(string Data)
        {
          
            if ((Data+"").ToUpper() == "F1")
            {
                FillRule = 1;
            }
            else
            {
                FillRule = 0;
            }
        }


    }
}
