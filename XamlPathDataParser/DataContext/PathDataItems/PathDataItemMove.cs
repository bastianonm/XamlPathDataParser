using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace XamlPathDataParser.DataContext.PathDataItems
{
    public class PathDataItemMove : IPathDataItem
    {

        public Point StartPoint { get; set; }

        public PathDataItemMove()
        { }

        public PathDataItemMove(string Data)
        {
            var point = Parser.PathDataParser.ReadPoint(Data, 1);
            if (point != null)
            {
                StartPoint = point.Value;
            }
        }


    }
}
