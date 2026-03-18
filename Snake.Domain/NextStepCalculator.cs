using Snake.Domain.Direction;

namespace Snake.Domain
{
    public sealed class NextStepCalculator
    {
        private readonly int _width;
        private readonly int _length;

        public NextStepCalculator(int width, int length) 
        {
            _width = width;
            _length = length;
        }

        public Coordinates[] GetInitialCoordinates(int initialSize) 
        {
            if (initialSize > _width - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(initialSize));
            }

            var coordinates = new Coordinates[initialSize];

            int initialY = (_length - 1) / 2;
            int xShift = _width / 2 - initialSize / 2;

            for(int i = 0; i < initialSize; i++)
            {
                int x = i + xShift;
                coordinates[i] = new Coordinates(x, initialY);
            }

            return coordinates;
        }

        public Coordinates GetNewHead(Coordinates headCoordinates, Directions direction)
        {
            Coordinates nextStepCoordinates = new Coordinates(headCoordinates);
            switch (direction)
            {
                case Directions.Down:
                    nextStepCoordinates.Y += 1;
                    nextStepCoordinates.Y = nextStepCoordinates.Y >= _length ? 0 : nextStepCoordinates.Y;
                    break;
                case Directions.Up:
                    nextStepCoordinates.Y -= 1;
                    nextStepCoordinates.Y = nextStepCoordinates.Y < 0 ? _length - 1 : nextStepCoordinates.Y;
                    break;
                case Directions.Right:
                    nextStepCoordinates.X += 1;
                    nextStepCoordinates.X = nextStepCoordinates.X >= _width ? 0 : nextStepCoordinates.X;
                    break;
                case Directions.Left:
                    nextStepCoordinates.X -= 1;
                    nextStepCoordinates.X = nextStepCoordinates.X < 0 ? _width - 1 : nextStepCoordinates.X;
                    break;
            }

            return nextStepCoordinates;
        }
    }
}
