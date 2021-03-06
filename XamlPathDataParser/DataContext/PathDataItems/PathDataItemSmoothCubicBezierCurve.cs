﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace XamlPathDataParser.DataContext.PathDataItems
{
    public class PathDataItemSmoothCubicBezierCurve :IPathDataItem
    {
        public Point ControlPoint { get; set; }
        public Point EndPoint { get; set; }

        public PathDataItemSmoothCubicBezierCurve()
        { }

        public PathDataItemSmoothCubicBezierCurve(string Data)
        {
            int index = 1;

            ControlPoint = Parser.PathDataParser.ReadPoint(Data, index, out index).Value;

            EndPoint = Parser.PathDataParser.ReadPoint(Data, index, out index).Value;
        }


    }
}
