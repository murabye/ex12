using System;
using System.Diagnostics;
using MyLib;

// что вообще понимать под перестановками и сравнениями для подразрядной сортировки?

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
            var temp = new int[arr.Length];                             // вспомогательный массив
            const int move = 8;                                         // насколько смещаемся
            const int bits = 32;                                        // битов в инте

            // в размер 2^move входят всевозможные значения для чисел с move битами
            var count = new int[1 << move];                             // массив для подсчета
            var pref = new int[1 << move];                              // массив префиксный
            
            var groups = (int)Math.Ceiling(bits / (double)move);        // кол-во групп
            const int mask = (1 << move) - 1;                           // маска для идентификации групп
            
            for (int c = 0, shift = 0; c < groups; c++, shift += move)
            {
                count = new int[count.Length];                          // очищение массива подсчета
                foreach (var t in arr)                                  // подсчет элементов в каждой группе
                    count[(t >> shift) & mask]++;
                
                pref[0] = 0;                                            // высчитывание префикса
                for (var i = 1; i < count.Length; i++)
                    pref[i] = pref[i - 1] + count[i - 1];
                
                foreach (var t in arr)                                  // формирование вспомогательного массива
                    temp[pref[(t >> shift) & mask]++] = t;
                
                temp.CopyTo(arr, 0);                                    // переносим и начинаем 
                                                                        // заново до последней группы
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
