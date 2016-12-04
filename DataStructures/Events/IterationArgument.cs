namespace InwersjaTomograficzna.Core.DataStructures.Events
{
    public delegate void IterationEventHandler(IterationArgument e);
    public class IterationArgument
    {
        public int Value
        {
            get;
            set;
        }

    }
}