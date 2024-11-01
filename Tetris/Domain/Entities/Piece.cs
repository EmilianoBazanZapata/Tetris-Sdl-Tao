using Domain.Core;


namespace Domain.Entities
{
    public class Piece
    {
        public int[,] Shape { get; set; }
        public (int x, int y) Position { get; set; }
        public Image Image { get; set; }
        public Image Icon { get; set; }

        public Piece(int[,] shape, Image image, Image icon)
        {
            Shape = shape;
            Position = (0, 0);
            Image = image;
            Icon = icon;
        }
    }
}