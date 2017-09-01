using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace XamlPathDataParser.DataContext.PathDataItems
{
    public class PathDataItemVerticalLine : IPathDataItem
    {

        public double Y { get; set; }

        public PathDataItemVerticalLine()
        { }

        public PathDataItemVerticalLine(string Data)
        {
            int index = 1;
            Point? p = null;

            Y = Parser.PathDataParser.ReadDouble(Data, index, out index).Value;

        }

    }
}
