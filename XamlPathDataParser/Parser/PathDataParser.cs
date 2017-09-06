using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using XamlPathDataParser.DataContext.PathDataItems;

namespace XamlPathDataParser.Parser
{
    public class PathDataParser
    {

        //  https://docs.microsoft.com/en-us/dotnet/framework/wpf/graphics-multimedia/path-markup-syntax

        public static IEnumerable<IPathDataItem> ParseData(string DataString)
        {
            string mydata = DataString.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " ");
            mydata = Regex.Replace(mydata, @"\s+", " ");

            string[] mydataArr = Regex.Replace(mydata, @"(F[0-1]|[MLHVCQSTAZ])", "\n$1", RegexOptions.IgnoreCase).Split('\n');

            string mydataItem = "";
            char command = ' ';

            foreach (var _mydataItem in mydataArr)
            {
                mydataItem = _mydataItem.ToUpper().Trim();

                if (string.IsNullOrWhiteSpace(mydataItem))
                    continue;

                if (mydataItem == "F0" || mydataItem == "F1")
                {
                    // sot supported so far
                    yield return new PathDataItemFillRule(mydataItem);
                    continue;
                }

                command = mydataItem[0];

                if (command == 'M')
                {
                    yield return new PathDataItemMove(mydataItem);
                }
                else if (command == 'L')
                {
                    yield return new PathDataItemLine(mydataItem);
                }
                else if (command == 'H')
                {
                    yield return new PathDataItemHorizontalLine(mydataItem);
                }
                else if (command == 'V')
                {
                    yield return new PathDataItemVerticalLine(mydataItem);
                }
                else if (command == 'C')
                {
                    yield return new PathDataItemCubicBezierCurve(mydataItem);
                }
                else if (command == 'Q')
                {
                    yield return new PathDataItemQuadraticBezierCurve(mydataItem);
                }
                else if (command == 'S')
                {
                    yield return new PathDataItemSmoothCubicBezierCurve(mydataItem);
                }
                else if (command == 'T')
                {
                    yield return new PathDataItemSmoothQuadraticBezierCurve(mydataItem);
                }
                else if (command == 'A')
                {
                    yield return new PathDataItemEllipticalArc(mydataItem);
                }
                else if (command == 'Z')
                {
                    yield return new PathDataItemClose();
                }
            }


        }

        internal static Point? ReadPoint(string DataString, int index)
        {
            int endIndex = 0;
            return ReadPoint(DataString, index, out endIndex);
        }

        internal static Point? ReadPoint(string DataString, int index, out int endIndex)
        {
            string dataString = DataString.Substring(index);

            endIndex = index;

            var m = Regex.Match(dataString, @"([0-9\.]+)[, ]+([0-9\.]+)");
            if (m.Success)
            {
                endIndex = index + m.Index + m.Length;
                return new Point(
                    double.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture),
                    double.Parse(m.Groups[2].Value, CultureInfo.InvariantCulture)
                    );
            }

            return null;

        }

        internal static Size? ReadSize(string DataString, int index, out int endIndex)
        {
            string dataString = DataString.Substring(index);

            endIndex = index;

            var m = Regex.Match(dataString, @"([0-9\.]+)[, ]+([0-9\.]+)");
            if (m.Success)
            {
                endIndex = index + m.Index + m.Length;
                return new Size(
                    double.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture),
                    double.Parse(m.Groups[2].Value, CultureInfo.InvariantCulture)
                    );
            }

            return null;

        }


        internal static int? ReadInt(string DataString, int index, out int endIndex)
        {
            string dataString = DataString.Substring(index);

            endIndex = index;

            var m = Regex.Match(dataString, @"([0-9]+)");
            if (m.Success)
            {
                endIndex = index + m.Index + m.Length;
                return int.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture);
            }

            return null;

        }

        internal static double? ReadDouble(string DataString, int index, out int endIndex)
        {
            string dataString = DataString.Substring(index);

            endIndex = index;

            var m = Regex.Match(dataString, @"([0-9\.]+)");
            if (m.Success)
            {
                endIndex = index + m.Index + m.Length;
                return double.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture);

            }

            return null;

        }

    }
}
