using System;
using System.Collections.Generic;
using System.Diagnostics;
using MyLib;

namespace ex12
{
    delegate void sort(int countEq, int countCh, int[] arr);         // делегат для сортировки

    internal class Program
    {
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
            var countChange = 0;                                // подготовка: 
            var countEqual = 0;                                 // обнуление счетчиков операций
            var time = Stopwatch.StartNew();                    // и времени

            Console.WriteLine("Массив:");
            Console.WriteLine(String.Join(", ", arr));

            Console.WriteLine("Результат: ");
            method(countEqual, countChange, arr);               // сортируем тута
            Console.WriteLine(String.Join(", ", arr));

            Console.WriteLine("Затрачено {0} тиков, {1} сравнений, {2} перессылок",
                time.ElapsedTicks, countEqual, countChange);
            time.Reset();
        }

        static void SimpleChoiseSort(int countEqual, int countChange, int[] arr)
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

                countChange++;                          // что считать здесь перестановкой?
                arr[i] += arr[min];                     
                arr[min] = arr[i] - arr[min];
                arr[i] -= arr[min];                     
            }
        }
        static void RadixSort(int countEqual, int countChange, int[] arr)
        {
            // разобраться получше!

            int i, j;
            var temp = new int[arr.Length];

            for (var shift = 31; shift > -1; --shift)       // от последнего к первому разряду
            {
                j = 0;
                for (i = 0; i < arr.Length; ++i)
                {
                    countEqual++;
                    countChange++;                          // спросить считается ли по временному массиву движуха
                    var move = (arr[i] << shift) >= 0;      // результат смещения бита (деления на 2) >= нуля?
                    if (shift == 0 ? !move : move)          // перемещаем 0-ой в начало
                        arr[i - j] = arr[i];
                    else                                    // перемещаем 1-ый во временный массив
                        temp[j++] = temp[i];
                }

                Array.Copy(temp, 0, arr, arr.Length - j, j);    // можно ли?
            }


        }

        static int[] Generate(int length)
        {
            // генератор массива заданной длины

            var ans = new int[length];

            for (var i = 0; i < length; i++)
                ans[i] = rand.Next();

            return ans;
        }
    }
}
