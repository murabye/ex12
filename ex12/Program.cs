using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ex12
{
    internal class Program
    {
        static int countEqual = 0, countChange = 0;

        private static void Main()
        {

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

                countChange++;                          // что считать здесь перестановкой?
                arr[i] += arr[min];                     
                arr[min] = arr[i] - arr[min];
                arr[i] -= arr[min];                     
            }
        }

        static void MSD(int[] arr)
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
    }
}
