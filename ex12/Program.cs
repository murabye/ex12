using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex12
{
    internal class Program
    {
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
                    if (arr[j] < arr[min])
                        min = j;
                
                arr[i] += arr[min];                     // перестановка arr[i] и arr[min]
                arr[min] = arr[i] - arr[min];           // без использования дополнительной
                arr[i] -= arr[min];                     // переменной
            }
        }


    }
}
