using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    public class MathMatrix<T>
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public long Size { get; private set; }

        private Dictionary<long, T> _cells = new Dictionary<long, T>();

        public MathMatrix(int w, int h)
        {
            this.Width = w;
            this.Height = h;
            this.Size = w * h;
        }

        public bool IsCellEmpty(int row, int col)
        {
            long index = row * Width + col;
            return _cells.ContainsKey(index);
        }

        public T this[int row, int col]
        {
            get
            {
                long index = row * Width + col;
                T result;
                _cells.TryGetValue(index, out result);
                return result;
            }
            set
            {
                long index = row * Width + col;
                _cells[index] = value;
            }
        }

        public MathMatrix<T> Transpose()
        {
            MathMatrix<T> result = new MathMatrix<T>(Height, Width);

            foreach(var cell in _cells)
            {
                int row = (int)cell.Key / Width;
                int col = (int)cell.Key % Width;

                result[col, row] = cell.Value;
            }

            return result;
        }

        public List<T> GetAllValues()
        {
            return _cells.Select(c => c.Value).ToList();
        }
    }
}
