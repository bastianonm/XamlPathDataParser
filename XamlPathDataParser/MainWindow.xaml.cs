using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using XamlPathDataParser.DataContext.PathDataItems;

namespace XamlPathDataParser
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DataContext.PathDataDataContext dc = new DataContext.PathDataDataContext();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (sender as TextBox);

            tb.Background = Brushes.White;

            try
            {

            string ParsedDataText = tb.Text;

            var ParsedDataItems = Parser.PathDataParser.ParseData(ParsedDataText);

            if (ParsedDataItems != null)
            {

                if (dc.PathDataItems == null)
                {
                    dc.PathDataItems = new System.Collections.ObjectModel.ObservableCollection<IPathDataItem>();
                }
                else
                {
                    dc.PathDataItems.Clear();
                }

                foreach (var item in ParsedDataItems)
                {
                    dc.PathDataItems.Add(item);
                }
            }

            ListBoxItems.ItemsSource = null;
            ListBoxItems.ItemsSource = dc.PathDataItems;

            FirstPath.Data = Geometry.Parse(ParsedDataText);

            DrawSecondPath(ParsedDataItems);
            }
            catch (Exception)
            {
                tb.Background = new SolidColorBrush(Color.FromArgb(20,255,0,0));
            }

        }


        private void DrawSecondPath(IEnumerable<IPathDataItem> ParsedDataItems, int hightlitedIndex = -1)
        {

            HilightedPath.Visibility = Visibility.Hidden;

            var pathGeometryFigure = new PathFigure();
            var pathGeometryFigures = new PathFigureCollection();
            pathGeometryFigures.Add(pathGeometryFigure);

            SecondPathGeometry.Figures = pathGeometryFigures;

            var index = -1;

            Point LastPoint = new Point(0, 0);

            foreach (var item in ParsedDataItems)
            {
                index++;
                if (item is PathDataItemFillRule)
                {
                    if ((item as PathDataItemFillRule).FillRule == 0)
                    {
                        SecondPathGeometry.FillRule = FillRule.EvenOdd;
                    }
                    else
                    {
                        SecondPathGeometry.FillRule = FillRule.Nonzero;
                    }                 
                }
                else if (item is PathDataItemClose)
                {
                    pathGeometryFigure = new PathFigure();
                    pathGeometryFigures.Add(pathGeometryFigure);
                }
                else if (item is PathDataItemMove)
                {
                    pathGeometryFigure.StartPoint = (item as PathDataItemMove).StartPoint;
                    LastPoint = pathGeometryFigure.StartPoint;
                }
                else
                {

                    if (index == hightlitedIndex)
                    {
                        var dummyPoint = new Point();
                        var s2 = getPAthSegment(item, ref dummyPoint);
                        HilightedPath.Visibility = Visibility.Visible;
                        var myFigure = new PathFigure();
                        myFigure.IsClosed = false;
                        myFigure.IsFilled = false;
                        HilightedPath.Data = new PathGeometry(new PathFigureCollection() { myFigure });
                        myFigure.StartPoint = LastPoint;
                        myFigure.Segments.Add(s2);

                    }

                    var s = getPAthSegment(item, ref LastPoint);


                    if (s != null)
                    {
                        pathGeometryFigure.Segments.Add(s);
                    }
                }

            }

        }


        private PathSegment getPAthSegment(IPathDataItem item, ref Point LastPoint)
        {

            if (item is PathDataItemCubicBezierCurve)
            {

                var _item = item as PathDataItemCubicBezierCurve;

                LastPoint = _item.EndPoint;

                return new BezierSegment()
                {
                    Point1 = _item.ControlPoint1,
                    Point2 = _item.ControlPoint2,
                    Point3 = _item.EndPoint
                };

            }
            else if (item is PathDataItemEllipticalArc)
            {

                var _item = item as PathDataItemEllipticalArc;

                LastPoint = _item.EndPoint;

                return new ArcSegment()
                {
                    Size = _item.size,
                    RotationAngle = _item.rotationAngle,
                    IsLargeArc = _item.isLargeArc,
                    SweepDirection = (SweepDirection)_item.sweepDirection,
                    Point = _item.EndPoint,
                };

            }
            else if (item is PathDataItemHorizontalLine)
            {

                LastPoint = new Point((item as PathDataItemHorizontalLine).X, LastPoint.Y);

                return new LineSegment() { Point = LastPoint };
            }
            else if (item is PathDataItemLine)
            {
                foreach (var point in (item as PathDataItemLine).EndPoints)
                {
                    LastPoint = point;

                    return new LineSegment() { Point = point, };
                }
            }

            else if (item is PathDataItemQuadraticBezierCurve)
            {
                var _item = item as PathDataItemQuadraticBezierCurve;
                LastPoint = _item.EndPoint;

                return new QuadraticBezierSegment() { Point1 = _item.ControlPoint, Point2 = _item.EndPoint };

            }
            else if (item is PathDataItemSmoothCubicBezierCurve)
            {
                var _item = item as PathDataItemSmoothCubicBezierCurve;
                LastPoint = _item.EndPoint;

                return new PolyQuadraticBezierSegment() { Points = new PointCollection { _item.ControlPoint, _item.EndPoint } };

            }
            else if (item is PathDataItemSmoothQuadraticBezierCurve)
            {
                var _item = item as PathDataItemSmoothQuadraticBezierCurve;
                LastPoint = _item.EndPoint;

                return new PolyQuadraticBezierSegment() { Points = new PointCollection { _item.ControlPoint, _item.EndPoint } };

            }
            else if (item is PathDataItemVerticalLine)
            {

                LastPoint = new Point(LastPoint.X, (item as PathDataItemVerticalLine).Y);

                return new LineSegment() { Point = LastPoint };
            }

            return null;
        }


        const double scaleStep = 0.2;

        private void DrawingCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {

            if (e.Delta > 0)
            {
                DrawingCanvasScaleTransform.ScaleX += scaleStep;
                DrawingCanvasScaleTransform.ScaleY += scaleStep;
            }
            else
            {
                DrawingCanvasScaleTransform.ScaleX -= scaleStep;
                DrawingCanvasScaleTransform.ScaleY -= scaleStep;
            }

            e.Handled = true;

        }

        private void ListBoxItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = (sender as ListBox).SelectedIndex;

            var ParsedDataItems = (sender as ListBox).ItemsSource as IEnumerable<IPathDataItem>;

            DrawSecondPath(ParsedDataItems, index);

        }

    }
}
