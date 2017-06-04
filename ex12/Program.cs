using System;
using System.Collections.Generic;
using System.Diagnostics;
using MyLib;

namespace ex12
{
    delegate void sort(int[] arr);         // делегат для сортировки

    internal class Program
    {
        static int countChange = 0;                                
        static int countEqual = 0;                                 
        static Random rand = new Random();

        private static void Main()
        {
            var scs = new sort(SimpleChoiseSort);
            var msd = new sort(RadixSort);

            while (true)
            {
                var length = Ask.Num("Введите длину массива: ");
                var arr = Generate(length);                         // генерация массива
                Console.WriteLine(String.Join(", ", arr));          // выписать массив

                var simple = (int[])arr.Clone();
                var radix = (int[])arr.Clone();

                // несортированный массив
                Try(simple, scs);
                Try(radix, msd);

                // сортированный массив
                Try(simple, scs);
                Try(radix, msd);

                // развернуть
                Array.Reverse(simple);
                Array.Reverse(radix);
                // сортированный в обратном порядке
                Try(simple, scs);
                Try(radix, msd);

                OC.Stay();
            }

        }

        static void Try(int[] arr, sort method)
        {
            var time = Stopwatch.StartNew();                    // подготовка: 
            countChange = 0;                                    // обнуление счетчиков операций
            countEqual = 0;                                     // и времени

            Console.WriteLine();
            Console.WriteLine("Массив:");
            Console.WriteLine(String.Join(", ", arr));

            Console.WriteLine("Результат: ");
            method(arr);                                        // сортируем тута
            Console.WriteLine(String.Join(", ", arr));

            Console.WriteLine("Затрачено {0} тиков, {1} сравнений, {2} перессылок",
                time.ElapsedTicks, countEqual, countChange);
            time.Reset();
        }

        static void SimpleChoiseSort(int[] arr)
        {
            // сортировка простым выбором

            var length = arr.Length;                    // количество проходов по массиву
            for (var i = 0; i < length - 1; i++)
            {
                var min = i;                            // номер минимального

                for (var j = i + 1; j < length; j++)    // поиск минимума
                {
                    countEqual++;
                    if (arr[j] < arr[min])
                        min = j;
                }

                if (i != min)
                {
                    countChange++; // что считать здесь перестановкой?
                    var temp = arr[min];
                    arr[min] = arr[i];
                    arr[i] = temp;
                }
            }
        }
        static void RadixSort(int[] arr)
        {
            // our helper array 
            int[] t = new int[arr.Length];

            // number of bits our group will be long 
            int r = 4; // try to set this also to 2, 8 or 16 to see if it is 
            // quicker or not 

            // number of bits of a C# int 
            int b = 32;

            // counting and prefix arrays
            // (note dimensions 2^r which is the number of all possible values of a 
            // r-bit number) 
            int[] count = new int[1 << r];
            int[] pref = new int[1 << r];

            // number of groups 
            int groups = (int)Math.Ceiling((double)b / (double)r);

            // the mask to identify groups 
            int mask = (1 << r) - 1;

            // the algorithm: 
            for (int c = 0, shift = 0; c < groups; c++, shift += r)
            {
                // reset count array 
                for (int j = 0; j < count.Length; j++)
                    count[j] = 0;

                // counting elements of the c-th group 
                for (int i = 0; i < arr.Length; i++)
                    count[(arr[i] >> shift) & mask]++;

                // calculating prefixes 
                pref[0] = 0;
                for (int i = 1; i < count.Length; i++)
                    pref[i] = pref[i - 1] + count[i - 1];

                // from a[] to t[] elements ordered by c-th group 
                for (int i = 0; i < arr.Length; i++)
                    t[pref[(arr[i] >> shift) & mask]++] = arr[i];

                // a[]=t[] and start again until the last group 
                t.CopyTo(arr, 0);
            }
            // a is sorted 




        }

       

        static int[] Generate(int length)
        {
            // генератор массива заданной длины

            var ans = new int[length];

            for (var i = 0; i < length; i++)
                ans[i] = rand.Next(0, 100);

            return ans;
        }
    }
}
