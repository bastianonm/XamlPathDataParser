using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace XamlPathDataParser.DataContext.PathDataItems
{
    public class PathDataItemLine : IPathDataItem
    {

        public List<Point> EndPoints { get; set; }

        public PathDataItemLine()
        { }

        public PathDataItemLine(string Data)
        {

            EndPoints = new List<Point>();

            int index = 1;
            Point? p = null;

            do
            {
                p = Parser.PathDataParser.ReadPoint(Data, index, out index);

                if (p == null)
                {
                    break;
                }
                else
                {
                    EndPoints.Add(p.Value);
                }

            }
            while (true);

        }

    }
}
