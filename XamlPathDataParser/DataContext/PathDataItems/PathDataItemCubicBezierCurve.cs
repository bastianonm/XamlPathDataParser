using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace XamlPathDataParser.DataContext.PathDataItems
{
    public class PathDataItemCubicBezierCurve : IPathDataItem
    {

        public Point ControlPoint1 { get; set; }
        public Point ControlPoint2 { get; set; }
        public Point EndPoint { get; set; }

        public PathDataItemCubicBezierCurve()
        { }

        public PathDataItemCubicBezierCurve(string Data)
        {
            int index = 1;

            ControlPoint1 = Parser.PathDataParser.ReadPoint(Data, index, out index).Value;

            ControlPoint2 = Parser.PathDataParser.ReadPoint(Data, index, out index).Value;

            EndPoint = Parser.PathDataParser.ReadPoint(Data, index, out index).Value;

        }

    }
}
