using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace XamlPathDataParser.DataContext.PathDataItems
{
    public class PathDataItemQuadraticBezierCurve :IPathDataItem 
    {
        public Point ControlPoint { get; set; }
        public Point EndPoint { get; set; }

        public PathDataItemQuadraticBezierCurve()
        { }

        public PathDataItemQuadraticBezierCurve(string Data)
        {

            int index = 1;

            ControlPoint = Parser.PathDataParser.ReadPoint(Data, index, out index).Value;

            EndPoint = Parser.PathDataParser.ReadPoint(Data, index, out index).Value;

        }

    }
}
