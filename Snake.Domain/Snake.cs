using Snake.Domain.Direction;
using Snake.Domain.Enum;

namespace Snake.Domain
{
    public sealed class Snake
    {
        private const int StepToGrow = 3;

        private int _stepNumber;
        private MoveStates _moveState;
        private Directions _currentDirection;
        private Directions _nextDirection = Directions.NotDefined;

        private Dictionary<Directions, Directions> _deniedDirections = new Dictionary<Directions, Directions>()
        {
            [Directions.Left] = Directions.Right,
            [Directions.Right] = Directions.Left,
            [Directions.Up] = Directions.Down,
            [Directions.Down] = Directions.Up
        };

        public Snake(Coordinates[] initialSnakeParts) 
        {
            _snakeParts = new(initialSnakeParts);
            _stepNumber = 0;
            _moveState = MoveStates.Wait;
            _currentDirection = Directions.Right;
        }

        private LinkedList<Coordinates> _snakeParts;
        public IEnumerable<Coordinates> SnakeParts => _snakeParts;

        public Coordinates Head => _snakeParts.LastOrDefault();
        public Directions NextDirection => _nextDirection != Directions.NotDefined ? _nextDirection : _currentDirection;

        public MoveStates MoveState => _moveState;

        public void Run()
        {
            _moveState = MoveStates.Run;
        }
        public void Stop()
        {
            _moveState = MoveStates.Wait;
        }

        public MoveStates Move(Coordinates newHead)
        {
            if (_moveState == MoveStates.Run)
            {
                _stepNumber++;
                _snakeParts.AddLast(newHead);
                MoveTail();
                _currentDirection = NextDirection;
                _nextDirection = Directions.NotDefined;
            }

            if (IsCrashed(newHead))
            {
                _moveState = MoveStates.Crashed;
            }                 

            return _moveState;
        }
        public void ChangeDirection(Directions direction)
        {
            if (_deniedDirections[_currentDirection] != direction)
                _nextDirection = direction;
        }

        private bool IsCrashed(Coordinates newHead)
        {
            bool isCrashed = false;
            foreach(var snakePart in _snakeParts.SkipLast(1))
            {
                if (snakePart == newHead) 
                { 
                    isCrashed = true; 
                    break; 
                }
            }
            return isCrashed;
        }

        /// <summary>
        /// Хвост перемещается всякий раз кроме каждого третьего шага
        /// </summary>
        private void MoveTail()
        {
            if (_stepNumber % StepToGrow != 0)
            {
                _snakeParts.RemoveFirst();
            }
        }
    }
}
