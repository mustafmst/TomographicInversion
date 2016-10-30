namespace InwersjaTomograficzna.Core.DataStructures
{
    public class Cell
    {
        public readonly int xIndex;
        public readonly int yIndex;

        public readonly int leftBoarder;
        public readonly int rightBoarder;
        public readonly int upperBoarder;
        public readonly int lowerBoarder;
        public double velocity;


        public Cell(int x, int y, int left, int right, int lower, int upper)
        {
            xIndex = x;
            yIndex = y;
            leftBoarder = left;
            rightBoarder = right;
            upperBoarder = upper;
            lowerBoarder = lower;
        }

        public Cell(int x, int y, int left, int right, int lower, int upper, double vel)
        {
            xIndex = x;
            yIndex = y;
            leftBoarder = left;
            rightBoarder = right;
            upperBoarder = upper;
            lowerBoarder = lower;
            velocity = vel;
        }
    }
}
