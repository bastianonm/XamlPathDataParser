using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace XamlPathDataParser.DataContext.PathDataItems
{
    public class PathDataItemHorizontalLine : IPathDataItem
    {

        public double X { get; set; }

        public PathDataItemHorizontalLine()
        { }

        public PathDataItemHorizontalLine(string Data)
        {

            int index = 1;
            Point? p = null;

            X = Parser.PathDataParser.ReadDouble(Data, index, out index).Value;

        }

    }
}
