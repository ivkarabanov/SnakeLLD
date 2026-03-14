namespace Snake.Domain
{
    public struct Coordinates
    {
        public Coordinates()
        {            
        }

        public Coordinates(Coordinates coordinates)
        {
            X = coordinates.X; 
            Y = coordinates.Y;
        }

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
