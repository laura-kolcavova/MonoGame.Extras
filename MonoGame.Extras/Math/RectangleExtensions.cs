namespace MonoGame.Extras.Math
{
    using Microsoft.Xna.Framework;

    public static class RectangleExtensions
    {
        public static bool Intersects(this Rectangle bounds, Point point)
        {
            return
                bounds.Right > point.X &&
                bounds.Left < point.X &&
                bounds.Bottom > point.Y &&
                bounds.Top < point.Y;
        }

        public static Rectangle Transform(this Rectangle rectangle, Matrix matrix)
        {
            // Get all four corners in local space
            Vector2 leftTop = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 rightTop = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
            Vector2 rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

            // Transform all four corners into work space
            Vector2.Transform(ref leftTop, ref matrix, out leftTop);
            Vector2.Transform(ref rightTop, ref matrix, out rightTop);
            Vector2.Transform(ref leftBottom, ref matrix, out leftBottom);
            Vector2.Transform(ref rightBottom, ref matrix, out rightBottom);

            // Find the minimum and maximum extents of the rectangle in world space
            var min = Vector2
                 .Min(Vector2.Min(leftTop, rightTop), Vector2.Min(leftBottom, rightBottom))
                 .ToPoint();

            var max = Vector2
                 .Max(Vector2.Max(leftTop, rightTop), Vector2.Max(leftBottom, rightBottom))
                 .ToPoint();

            // Return that as a rectangle
            return new Rectangle(min.X, min.Y, max.X - min.X, max.Y - min.Y);
        }
    }
}
