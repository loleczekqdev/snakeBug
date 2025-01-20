using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace Snake
{
    public static class Images
    {
        public readonly static ImageSource Empty = LoadImage("empty-cell.png");
        public readonly static ImageSource Food = LoadImage("food.png");
        public readonly static ImageSource SnakeHead = LoadImage("snake-head");
        public readonly static ImageSource SnakeBody = LoadImage("snake-body.png");
        public readonly static ImageSource DeadSnakeHead = LoadImage("snake-head-dead.png");
        public readonly static ImageSource DeadSnakeBody = LoadImage("snake-body-dead.png");

        public static ImageSource LoadImage(string fileName)
        {
            return new BitmapImage(new Uri($"Assets/{fileName}", UriKind.Relative));
        }
    }
}

