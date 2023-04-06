public class ClockWise
{

            public List<Point2D> OrderPointsClockwise(List<Point2D> points)
            {                
                ComputeBoundingRectangle(points, out Point2D min, out Point2D max);

                Point2D[] vertices = new Point2D[]
                {
                    min,
                    new Point2D(min.X, Center.Y),
                    new Point2D(min.X, max.Y),
                    new Point2D(Center.X, max.Y),
                    max,
                    new Point2D(max.X, Center.Y),
                    new Point2D(max.X, min.Y),
                    new Point2D(Center.X, min.Y),
                };

                Triangle2D[] triangles = new Triangle2D[]
                {
                    new Triangle2D(Center, vertices[0], vertices[1]),
                    new Triangle2D(Center, vertices[1], vertices[2]),
                    new Triangle2D(Center, vertices[2], vertices[3]),
                    new Triangle2D(Center, vertices[3], vertices[4]),
                    new Triangle2D(Center, vertices[4], vertices[5]),
                    new Triangle2D(Center, vertices[5], vertices[6]),
                    new Triangle2D(Center, vertices[6], vertices[7]),
                    new Triangle2D(Center, vertices[7], vertices[0])
                };

                List<Point2D> ordered = new List<Point2D>();

                for (int i = 0; i < triangles.Length; i++)
                {
                    List<Point2D> pts = new List<Point2D>();
                    foreach (var p in points)
                    {
                        if (triangles[i].IsPointInsideTriangle(p))
                            if (!pts.Contains(p))
                                pts.Add(p);
                    }

                    ordered.AddRange(OrderPointsInTriangles(i, pts));
                }
                return RemoveDuplicates(ordered);
            }

            private List<Point2D> RemoveDuplicates(List<Point2D> points)
            {
                List<Point2D> pts = new List<Point2D>();

                foreach (var p in points)
                {
                    if (!pts.Any())
                        pts.Add(p);
                    else if(pts.Last().DistanceTo(p) > 0.01)
                        pts.Add(p);
                }

                return pts;
            }

            private List<Point2D> OrderPointsInTriangles(int index, List<Point2D> points)
            {
                switch (index)
                {
                    case 0:
                        return points.OrderBy(p => p.Y).ToList();
                    case 1:
                        return points.OrderBy(p => p.Y).ToList();
                    case 2:
                        return points.OrderBy(p => p.X).ToList();
                    case 3:
                        return points.OrderBy(p => p.X).ToList();
                    case 4:
                        return points.OrderByDescending(p => p.Y).ToList();
                    case 5:
                        return points.OrderByDescending(p => p.Y).ToList();
                    case 6:
                        return points.OrderByDescending(p => p.X).ToList();
                    case 7:
                        return points.OrderByDescending(p => p.X).ToList();

                    default:
                        return new List<Point2D>();
                }
            }
            
            private void ComputeBoundingRectangle(List<Point2D> points, out Point2D min, out Point2D max)
            {
                max = points[0];
                min = points[0];
                foreach (var p in points)
                {
                    min = Min(min, p);
                    max = Max(max, p);
                }
            }
            
            private Point2D Min(Point2D p1, Point2D p2)
            {
                return new Point2D(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y));
            }
            private Point2D Max(Point2D p1, Point2D p2)
            {
                return new Point2D(Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y));
            }
}


public class Point2D
{
    public Point2D(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X { get; set; }
    public double Y { get; set; }
    
    public double DistanceTo(Point2D p) => Math.Sqrt(Math.Pow(p.X - X, 2) + Math.Pow(p.Y - Y, 2));
}

public class Triangle2D
{
    public Triangle2D(Point2D p1, Point2D p2, Point2D p3)
    {
        P1 = p1;
        P2 = p2;
        P3 = p3;
    }

    public Point2D P1 { get; set; }
    public Point2D P2 { get; set; }
    public Point2D P3 { get; set; }

    public bool IsPointInsideTriangle(Point2D p)
    {
        double d1 = Sign(p, P1, P2);
        double d2 = Sign(p, P2, P3);
        double d3 = Sign(p, P3, P1);

        bool hasNeg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        bool hasPos = (d1 > 0) || (d2 > 0) || (d3 > 0);

        return !(hasNeg && hasPos);
    }

    private double Sign(Point2D p1, Point2D p2, Point2D p3)
    {
        return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
    }
}
