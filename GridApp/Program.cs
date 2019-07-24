using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Test.GridApp
{
    class Program
    {
        //Returns Available Combinations
        public static int iAvailComb = 0;
        static void Main(string[] args)
        {
            //*****For testing use arrSample
            /*
            int[][] arrSample = new int[5][]{new int[] { 1, 2, 3, 4, 5 },
                    new int[] {6, 7, 8, 9, 1},
                    new int[] {2, 3, 4, 5, 6},
                    new int[] {7, 8, 119, 1, 0},
                    new int[] {9, 6, 4, 2, 3}};
                    */

            //Using grid from RMS test
            //I coded it such that the array can be put in a text file and read from there, easier to change and test
            string filename = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\arrayinput.txt";
            int[,] inputSquare = readInput(filename);

            //convert into a Jagged array
            int[][] arrSample = inputSquare.ToJaggedArray();

            //Assumes that the array is not jagged and has equal rows and columns
            int iArrLen = arrSample.Length;

            var result = findProduct(arrSample, iArrLen);
            //Shows available combinations
            Console.WriteLine("Possible Combinations : " + iAvailComb);
            //Shows max product
            Console.WriteLine("Max Product : " + result);
            Console.ReadLine();
        }
        
        /// <summary>
        /// Find product and available combinations
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static decimal findProduct(int[][] arr, int length)
        {
            int product = 0, result;
            //3 as per question
            int numsToMultiply = 3;
            
            // iterate through the rows. 
            for (int i = 0; i < length; i++)
            {
                // iterate through the columns. 
                for (int j = 0; j < length; j++)
                {
                    // evaluate the maximum product in horizontal row. 
                    if ((j - numsToMultiply) >= 0)
                    {
                        iAvailComb++;
                           result = arr[i][j] * arr[i][j - 1] *
                                arr[i][j - 2] * arr[i][j - 3];

                        product = Math.Max(product, result);
                    }

                    // evaluate the maximum product  in vertical row. 
                    if ((i - numsToMultiply) >= 0)
                    {
                        iAvailComb++;
                        result = arr[i][j] * arr[i - 1][j] *
                                arr[i - 2][j] * arr[i - 3][j];


                        product = Math.Max(product, result);
                    }

                    // evaluate the maximum product in diagonal and anti - diagonal 
                    if ((i - numsToMultiply) >= 0 && (j - numsToMultiply) >= 0)
                    {
                        iAvailComb++;
                        result = arr[i][j] * arr[i - 1][j - 1] *
                                arr[i - 2][j - 2] * arr[i - 3][j - 3];


                        product = Math.Max(product, result);
                    }
                }
            }

            return product;
        }

        private static int[,] readInput(string filename)
        {
            int lines = 0;
            string line;
            string[] linePieces;

            StreamReader r = new StreamReader(filename);
            while (r.ReadLine() != null)
            {
                lines++;
            }

            int[,] inputSquare = new int[lines, lines];
            r.BaseStream.Seek(0, SeekOrigin.Begin);

            int j = 0;
            while ((line = r.ReadLine()) != null)
            {
                linePieces = line.Split(' ');
                for (int i = 0; i < linePieces.Length; i++)
                {
                    inputSquare[j, i] = int.Parse(linePieces[i]);
                }
                j++;
            }

            r.Close();

            return inputSquare;
        }

    }

    internal static class ExtensionMethods
    {
        internal static T[][] ToJaggedArray<T>(this T[,] twoDimensionalArray)
        {
            int rowsFirstIndex = twoDimensionalArray.GetLowerBound(0);
            int rowsLastIndex = twoDimensionalArray.GetUpperBound(0);
            int numberOfRows = rowsLastIndex + 1;

            int columnsFirstIndex = twoDimensionalArray.GetLowerBound(1);
            int columnsLastIndex = twoDimensionalArray.GetUpperBound(1);
            int numberOfColumns = columnsLastIndex + 1;

            T[][] jaggedArray = new T[numberOfRows][];
            for (int i = rowsFirstIndex; i <= rowsLastIndex; i++)
            {
                jaggedArray[i] = new T[numberOfColumns];

                for (int j = columnsFirstIndex; j <= columnsLastIndex; j++)
                {
                    jaggedArray[i][j] = twoDimensionalArray[i, j];
                }
            }
            return jaggedArray;
        }
    }
}
