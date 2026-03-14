
namespace Snake.Domain
{
    public sealed class GameInfo
    {
        public GameInfo(int areaWidth, int areaLength, int initialSnakeLength)
        {
            AreaWidth = areaWidth;
            AreaLength = areaLength;
            InitialSnakeLength = initialSnakeLength;
            Reset();
        }

        public int AreaWidth { get; set; }
        public int AreaLength { get; set; }
        public int Score { get; set; }
        public int SnakeLength { get; set; }
        public int InitialSnakeLength { get; set; }

        public void Reset()
        {
            Score = 0;
            SnakeLength = InitialSnakeLength;
        }
    }
}
