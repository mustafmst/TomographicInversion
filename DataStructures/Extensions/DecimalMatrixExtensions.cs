using DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class DecimalMatrixExtensions
    {

        public static MathMatrix<decimal> ConvertResultToVelociti(this MathMatrix<decimal> m)
        {
            var result = new MathMatrix<decimal>(m.Width, m.Height);
            for (int i = 0; i < result.Height; i++)
            {
                result[i, 0] = m[i, 0] == 0 ? 0 : 1 / m[i, 0];
            }
            return result;
        }

        public static decimal ColumnSum(this MathMatrix<decimal> matrix, int col)
        {
            decimal res = 0;
            for (int i = 0; i < matrix.Height; i++)
            {
                res += matrix[i, col];
            }
            return res;
        }

        public static decimal RowSum(this MathMatrix<decimal> matrix, int row)
        {
            decimal res = 0;
            for (int i = 0; i < matrix.Width; i++)
            {
                res += matrix[row, i];
            }
            return res;
        }

        public static MathMatrix<decimal> Multiply(this MathMatrix<decimal> m1, MathMatrix<decimal> m2)
        {
            if (m1.Width != m2.Height) throw new Exception("Macierze muszą się zgadzać aby je mnożyć!!!");
            MathMatrix<decimal> result = new MathMatrix<decimal>(m2.Width, m1.Height);

            for(int i = 0; i < result.Width; i++)
            {
                for(int j = 0; j<result.Height; j++)
                {
                    decimal res = 0;

                    for(int z=0; z< m1.Width; z++)
                    {
                        res += m1[j, z] * m2[z, i];
                    }
                    if (res != 0) result[j, i] = res;
                }
            }

            return result;
        }

        public static MathMatrix<decimal> Add(this MathMatrix<decimal> m1, MathMatrix<decimal> m2)
        {
            if(m1.Height != m2.Height || m1.Width != m2.Width) throw new Exception("Macierze muszą się zgadzać aby je dodawać!!!");
            MathMatrix<decimal> result = new MathMatrix<decimal>(m1.Width, m1.Height);

            for(int i = 0; i < result.Height; i++)
            {
                for(int j = 0; j < result.Width; j++)
                {
                    var res = m1[i, j] + m2[i, j];
                    if (res != 0) result[i, j] = res;
                }
            }

            return result;
        }

        public static MathMatrix<decimal> Subtract(this MathMatrix<decimal> m1, MathMatrix<decimal> m2)
        {
            if (m1.Height != m2.Height || m1.Width != m2.Width) throw new Exception("Macierze muszą się zgadzać aby je dodawać!!!");
            MathMatrix<decimal> result = new MathMatrix<decimal>(m1.Width, m1.Height);

            for (int i = 0; i < result.Height; i++)
            {
                for (int j = 0; j < result.Width; j++)
                {
                    var res = m1[i, j] - m2[i, j];
                    if (res != 0) result[i, j] = res;
                }
            }

            return result;
        }

        public static void PrinttoFile(this MathMatrix<decimal> m1, string fileName)
        {
            var writer = new StreamWriter(fileName);

            for (int i = 0; i < m1.Height; i++)
            {
                string line = "";
                for (int j = 0; j < m1.Width; j++)
                {
                    line += m1[i, j] + "\t";
                }
                writer.WriteLine(line);
            }

            writer.Close();
        }

        public static decimal AverageStatisticError(this MathMatrix<decimal> m1, MathMatrix<decimal> m2)
        {
            List<decimal> errorsList = new List<decimal>();
            if (m1.Height == m2.Height && m1.Width == m2.Width)
            {
                for(int i = 0; i < m1.Height; i++)
                {
                    for (int j = 0; j < m1.Width; j++)
                    {
                        errorsList.Add(Math.Abs(m2[i, j] - m1[i, j]));
                    }
                }
            }
            var error = errorsList.Count == 0 ? 0 : errorsList.Average()*100/m1.GetAllValues().Average();
            return error;
        }

        public static string GetMatrixHash(this MathMatrix<decimal> m1)
        {
            if (m1.Width!=1) { throw new Exception("Wrong value exception"); }
            string tmp = "[";
            for(int i = 0; i < m1.Height; i++)
            {
                tmp += string.Format("\t{0},", m1[i, 0]);
            }
            tmp += "]";
            return string.Format("{0:X}", tmp.GetHashCode());
        }
    }
}
