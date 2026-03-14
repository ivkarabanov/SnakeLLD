using Snake.Domain.Direction;
using System;
using System.Linq;
using System.Windows.Threading;

namespace Snake.Domain
{
    public sealed class SnakeOperator
    {
        private Snake _snake;
        private DispatcherTimer _dispatcherTimer;
        private GameInfo _gameInfo;
        private NextStepCalculator _nextStepCalulator;

        //public Snake Snake => _snake;
        //public GameInfo Game => _gameInfo;

        public event Action<GameInfo, Snake> SnakeMoved;

        public void CreateSnake(int width, int length, int initialSnakeLength)
        {
            //задать GameInfo
            _gameInfo = new GameInfo(width, length, initialSnakeLength);

            //рассчитать начальные координаты змейки
            _nextStepCalulator = new NextStepCalculator(width, length);
            var initialCoordinates = _nextStepCalulator.GetInitialCoordinates(initialSnakeLength);

            //создать змейку
            _snake = new Snake(initialCoordinates);

            if (_dispatcherTimer == null)
            {
                _dispatcherTimer = new DispatcherTimer();
                _dispatcherTimer.Tick += new EventHandler(MoveSnake);
                _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(600);
            }
        }

        public void Launch()
        {
            if (_snake is null || _dispatcherTimer is null)
            {
                return;
            }

            _snake.Run();
            _dispatcherTimer.Start();            
        }

        private void MoveSnake(object sender, EventArgs e)
        {
            //запуск Move
            var nextHead = _nextStepCalulator.Calculate(_snake.Head, _snake.NextDirection);
            var snakeState = _snake.Move(nextHead);

            //обновление счета и длины змеи
            _gameInfo.SnakeLength = _snake.SnakeParts.Count();
            if (snakeState == Enum.MoveStates.Run)
            {
                _gameInfo.Score++;
            }

            SnakeMoved(_gameInfo, _snake);
        }

        public void Rotate(Directions direction)
        {
            if (_snake != null)
            {
                _snake.ChangeDirection(direction);
            }
        }

        public void Stop()
        {
            if (_dispatcherTimer != null) 
            {
                _dispatcherTimer.Stop(); 
            }
            if (_snake != null)
            {
                _snake.Stop();
            }
        }
    }
}
