namespace InwersjaTomograficzna.Core.RayDensity.DataStructures
{
    public class Cell
    {
        public readonly int xIndex;
        public readonly int yIndex;

        public readonly int leftBoarder;
        public readonly int rightBoarder;
        public readonly int upperBoarder;
        public readonly int lowerBoarder;

        public Cell(int x, int y, int left, int right, int lower, int upper)
        {
            xIndex = x;
            yIndex = y;
            leftBoarder = left;
            rightBoarder = right;
            upperBoarder = upper;
            lowerBoarder = lower;
        }
    }
}
