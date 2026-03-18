using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Snake.Domain;
using Snake.Domain.Direction;
using Snake.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace SnakeWPF
{
    public class MainViewModel: ObservableObject
    {
        private Game _snakeOperator;

        public MainViewModel()
        {
            StartCommand = new RelayCommand(Start);
            StopCommand = new RelayCommand(Stop);
            UpCommand = new RelayCommand(MoveUp);
            DownCommand = new RelayCommand(MoveDown);
            LeftCommand = new RelayCommand(MoveLeft);
            RightCommand = new RelayCommand(MoveRight);
        }

        private List<Coordinates> _snakeParts;
        public List<Coordinates> SnakeParts
        {
            get => _snakeParts;
            set => SetProperty(ref _snakeParts, value);
        }

        private int _boxSideLength;
        public int BoxSideLength
        {
            get => _boxSideLength;
            set => SetProperty(ref _boxSideLength, value);
        }

        private int _score;
        public int Score
        {
            get => _score;
            set => SetProperty(ref _score, value);
        }

        private int _snakeLength;
        public int SnakeLength
        {
            get => _snakeLength;
            set => SetProperty(ref _snakeLength, value);
        }

        private int _initialSnakeLength = 3;
        public int InitialSnakeLength
        {
            get => _initialSnakeLength;
            set => SetProperty(ref _initialSnakeLength, value);
        }

        private int _width = 12;
        public int Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        private int _height = 12;
        public int Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        private int _widthPixels;
        public int WidthPixels
        {
            get => _widthPixels;
            set => SetProperty(ref _widthPixels, value);
        }

        private int _heightPixels;
        public int HeightPixels
        {
            get => _heightPixels;
            set => SetProperty(ref _heightPixels, value);
        }

        private int _fieldWidthPixels;
        public int FieldWidthPixels
        {
            get => _fieldWidthPixels;
            set => SetProperty(ref _fieldWidthPixels, value);
        }

        private int _fieldHeightPixels;
        public int FieldHeightPixels
        {
            get => _fieldHeightPixels;
            set => SetProperty(ref _fieldHeightPixels, value);
        }

        private bool _isGameOver;
        public bool IsGameOver
        {
            get => _isGameOver;
            set => SetProperty(ref _isGameOver, value);
        }

        public ICommand StartCommand { get; }

        private void Start()
        {
            CalculatePixelSizes();
            if (_snakeOperator == null)
            {
                _snakeOperator = new Game();                
                _snakeOperator.SnakeMoved += Recalculate;
            }
            _snakeOperator.CreateSnake(Width, Height, InitialSnakeLength);
            _snakeOperator.Launch();
            IsGameOver = false;
        }

        private void Recalculate(Snake.Domain.Snake snake)
        {
            if (snake.MoveState == MoveStates.Crashed)
            {
                IsGameOver = true;
            }

            var parts = snake.SnakeParts.Select(p => new Coordinates(p.X * _boxSideLength, p.Y * _boxSideLength));
            SnakeParts = new List<Coordinates>(parts);

            Score = _snakeOperator.Score;
            SnakeLength = _snakeOperator.SnakeLength;
        }

        public ICommand StopCommand { get; }

        private void Stop()
        {
            CalculatePixelSizes();
            if (_snakeOperator != null)
            {
                _snakeOperator.Stop();
            }            
        }

        private void MoveUp()
        {
            Rotate(Directions.Up);
        }

        private void MoveDown()
        {
            Rotate(Directions.Down);
        }

        private void MoveLeft()
        {
            Rotate(Directions.Left);
        }

        private void MoveRight()
        {
            Rotate(Directions.Right);
        }

        private void Rotate(Directions direction)
        {
            if (_snakeOperator != null)
            {
                _snakeOperator.Rotate(direction);
            }
        }

        public ICommand UpCommand { get; }
        public ICommand DownCommand { get; }
        public ICommand LeftCommand { get; }
        public ICommand RightCommand { get; }

        private void CalculatePixelSizes()
        {
            var widthSize = _widthPixels / _width;
            var heightSize = _heightPixels / _height;

            var minSize = Math.Min(widthSize, heightSize);
            BoxSideLength = minSize;

            //рассчитать размеры Canvas
            FieldHeightPixels = minSize * Height;
            FieldWidthPixels = minSize * Width;
        }
    }
}
