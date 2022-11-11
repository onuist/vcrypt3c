using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Accord.Math;


namespace Matrix_Calculator_F2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Matrix A:");
            int[,] matr_A = EnterMatrix();
            Console.WriteLine("Matrix B:");
            int[,] matr_B = EnterMatrix();
            Console.WriteLine("Matrix C:");
            int[,] matr_C = EnterMatrix();

            //int[,] matr_A =
            //{
            //    {0, 1, 1, 1 },
            //    {1, 0, 1, 1 },
            //    {0, 0, 0, 1 },
            //    {0, 0, 1, 0 }
            //};
            //int[,] matr_B =
            //{
            //    { 1 },
            //    { 0 },
            //    { 0 },
            //    { 0 }
            //};
            //int[,] matr_C = { { 0, 0, 0, 1 } };

            int[,] matr_K = new int[0, matr_A.GetLength(0)];
            int[,] matr_C_temp = new int[matr_C.GetLength(0), matr_A.GetLength(1)];
            for (int i = 0; i < matr_C.GetLength(0); i++)
                for (int j = 0; j < matr_C.GetLength(1); j++)
                    matr_C_temp[i, j] = matr_C[i, j];
            for (int i = 0; i < matr_A.GetLength(0); i++)
            {
                matr_K = AddRowsToMatr(matr_K, matr_C_temp);
                matr_C_temp = MultiplyMatrix(matr_C_temp, matr_A);
            }
            Console.WriteLine("Matrix K:");
            PrintMatrix(matr_K);
            Console.WriteLine("\r\n");

            Console.WriteLine("Matrix T:");
            int[,] matr_T = GetLinearlyIndependentVectors(matr_K);
            PrintMatrix(matr_T);
            Console.WriteLine("\r\n");

            Console.WriteLine("Matrix Q:");
            int[,] matr_Q = new int[matr_T.GetLength(0), matr_T.GetLength(0)];
            for (int i = 0; i < matr_Q.GetLength(0); i++)
                for (int j = 0; j < matr_Q.GetLength(1); j++)
                    matr_Q[i, j] = matr_T[i, matr_T.GetLength(1) - matr_T.GetLength(0) + j];
            PrintMatrix(matr_Q);
            Console.WriteLine("\r\n");

            Console.WriteLine("Matrix Q-1:");
            double[,] matr_Q_d = new double[matr_Q.GetLength(0), matr_Q.GetLength(1)];
            for (int i = 0; i < matr_Q.GetLength(0); i++)
                for (int j = 0; j < matr_Q.GetLength(1); j++)
                    matr_Q_d[i, j] = matr_Q[i, j];
            double[,] matr_Q_1_d = Matrix.Inverse(matr_Q_d);
            int[,] matr_Q_1 = new int[matr_Q.GetLength(0), matr_Q.GetLength(1)];
            for (int i = 0; i < matr_Q.GetLength(0); i++)
                for (int j = 0; j < matr_Q.GetLength(1); j++)
                    matr_Q_1[i, j] = Math.Abs((int)matr_Q_1_d[i, j] % 2);
            PrintMatrix(matr_Q_1);
            Console.WriteLine("\r\n");

            Console.WriteLine("Matrix R:");
            int[,] matr_R = new int[matr_A.GetLength(0), matr_T.GetLength(0)];
            int dlt = matr_R.GetLength(0) - matr_R.GetLength(1);
            for (int i = 0; i < matr_Q_1.GetLength(0); i++)
                for (int j = 0; j < matr_Q_1.GetLength(1); j++)
                    matr_R[i + dlt, j] = matr_Q_1[i, j];
            PrintMatrix(matr_R);
            Console.WriteLine("\r\n");

            Console.WriteLine("Matrix T * A:");
            int[,] matr_A_cap = MultiplyMatrix(matr_T, matr_A);
            PrintMatrix(matr_A_cap);
            Console.WriteLine("\r\nMatrix A^ = TAR:");
            matr_A_cap = MultiplyMatrix(matr_A_cap, matr_R);
            PrintMatrix(matr_A_cap);
            Console.WriteLine("\r\n");

            Console.WriteLine("\r\nMatrix B^ = TB:");
            int[,] matr_B_cap = MultiplyMatrix(matr_T, matr_B);
            PrintMatrix(matr_B_cap);
            Console.WriteLine("\r\n");

            Console.WriteLine("\r\nMatrix C^ = CR:");
            int[,] matr_C_cap = MultiplyMatrix(matr_C, matr_R);
            PrintMatrix(matr_C_cap);
            Console.WriteLine("\r\n");

            Console.ReadLine();
        }

        private static int[,] GetLinearlyIndependentVectors(int[,] matr_K)
        {
            List<Vector> input_vectors = new List<Vector>();
            List<Vector> result_vectors = new List<Vector>();
            for (int i = 0; i < matr_K.GetLength(0); i++)
            {
                input_vectors.Add(new Vector(matr_K.GetLength(1)));
                for (int j = 0; j < matr_K.GetLength(1); j++)
                    input_vectors[i][j] = matr_K[i, j];
            }
            result_vectors.Add(input_vectors[0]);


            for(int i = 1; i < input_vectors.Count; i++)  
                if (LinearlyIndependent(input_vectors[i], result_vectors))
                    result_vectors.Add(input_vectors[i]);


            int[,] result_array = new int[result_vectors.Count, matr_K.GetLength(1)];
            for (int i = 0; i < result_vectors.Count; i++)
                for (int j = 0; j < matr_K.GetLength(1); j++)
                    result_array[i, j] = result_vectors[i][j];

            return result_array;
        }

        private static bool LinearlyIndependent(Vector item, List<Vector> result_vectors)
        {
            List<Vector> tmp_result_vectors = new List<Vector>();
            foreach (var v in result_vectors)
                tmp_result_vectors.Add(v);


            tmp_result_vectors.Add(item);
            double count = Math.Pow(2, tmp_result_vectors.Count);
            bool flag = true;
            for (int i = 1; i <= count - 1; i++)
            {
                string str = Convert.ToString(i, 2).PadLeft(tmp_result_vectors.Count, '0');
                List<Vector> tmp_list = new List<Vector>();
                for (int j = 0; j < str.Length; j++)
                {
                    if (str[j] == '1')
                    {
                        tmp_list.Add(tmp_result_vectors[j]);
                    }
                }
                Vector tmp_vector = new Vector(item.Length);
                foreach (Vector v in tmp_list)
                    tmp_vector += v;
                if(tmp_vector.EqualsZero)
                    return false;
            }
            return flag;
        }
        private static int[,] AddRowsToMatr(int[,] matr_K, int[,] matr_C_temp)
        {
            int rows = matr_K.GetLength(0) + matr_C_temp.GetLength(0);
            int[,] new_Matr = new int[rows, matr_K.GetLength(1)];
            for (int i = 0; i < matr_K.GetLength(0); i++)
                for (int j = 0; j < matr_K.GetLength(1); j++)
                    new_Matr[i, j] = matr_K[i, j];

            for (int i = 0; i < matr_C_temp.GetLength(0); i++)
                for (int j = 0; j < matr_K.GetLength(1); j++)
                    new_Matr[i + matr_K.GetLength(0), j] = matr_C_temp[i, j];

            return new_Matr;
        }

        private static int[,] EnterMatrix()
        {
            Console.Write("Enter first dimension: ");
            int d_1 = int.Parse(Console.ReadLine());
            Console.Write("Enter second dimension: ");
            int d_2 = int.Parse(Console.ReadLine());
            Console.WriteLine("\r\nEnter matrix: \r\n");

            int[,] matr_A = new int[d_1, d_2];
            for (int i = 0; i < d_1; i++)
            {
                string[] tmp = Console.ReadLine().Split(' ');
                for (int j = 0; j < d_2; j++)
                    matr_A[i, j] = int.Parse(tmp[j]);
            }
            Console.WriteLine("\r\n");
            return matr_A;
        }

        private static void PrintMatrix(int[,] CA)
        {
            for (int i = 0; i < CA.GetLength(0); i++)
            {
                Console.Write("| ");
                for (int j = 0; j < CA.GetLength(1); j++)
                    Console.Write(CA[i, j] + " ");
                Console.Write("|\r\n");
            }
        }

        public static int[,] MultiplyMatrix(int[,] A, int[,] B)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);
            int rB = B.GetLength(0);
            int cB = B.GetLength(1);

            if (cA != rB)
            {
                throw new Exception("Matrixes can't be multiplied!!");
            }
            else
            {
                int temp = 0;
                int[,] kHasil = new int[rA, cB];

                for (int i = 0; i < rA; i++)
                {
                    for (int j = 0; j < cB; j++)
                    {
                        temp = 0;
                        for (int k = 0; k < cA; k++)
                        {
                            temp += A[i, k] * B[k, j];
                        }
                        kHasil[i, j] = temp % 2;
                    }
                }
                return kHasil;
            }
        }
    }
}
