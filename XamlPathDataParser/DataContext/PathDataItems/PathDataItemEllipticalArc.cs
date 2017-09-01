using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace XamlPathDataParser.DataContext.PathDataItems
{
    public class PathDataItemEllipticalArc : IPathDataItem
    {

        public Size size { get; set; }

        /// <summary>
        /// The rotation of the ellipse, in degrees.
        /// </summary>
        public double rotationAngle { get; set; }

        /// <summary>
        /// Set to 1 if the angle of the arc should be 180 degrees or greater; otherwise, set to 0.
        /// </summary>
        public bool isLargeArc { get; set; }

        /// <summary>
        /// Set to 1 if the arc is drawn in a positive-angle direction; otherwise, set to 0.
        /// </summary>
        public int sweepDirection { get; set; }

        /// <summary>
        /// The point to which the arc is drawn.
        /// </summary>
        public Point EndPoint { get; set; }


        public PathDataItemEllipticalArc()
        { }

        public PathDataItemEllipticalArc(string Data)
        {

            int index = 1;

            size = Parser.PathDataParser.ReadSize(Data, index, out index).Value;

            rotationAngle = Parser.PathDataParser.ReadDouble(Data, index, out index).Value;

            isLargeArc = Parser.PathDataParser.ReadInt(Data, index, out index) == 1;

            sweepDirection = Parser.PathDataParser.ReadInt(Data, index, out index).Value;

            EndPoint = Parser.PathDataParser.ReadPoint(Data, index, out index).Value;

        }

    }
}
