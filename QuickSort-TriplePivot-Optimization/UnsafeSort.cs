using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort
{
    public class UnsafeSort
    {
        public unsafe static void QuickSortSingle(int* niz, int begin, int end)
        {
            if (begin < end)
            {
                int pivot = PartitionOnePivot(niz, begin, end);
                QuickSortSingle(niz, begin, pivot - 1);
                QuickSortSingle(niz, pivot + 1, end);
            }
        }

        private unsafe static int PartitionOnePivot(int* array, int begin, int end)
        {
            int i = begin, j = end;
            int pivot = *(array + begin);
            while (i < j)
            {
                while (*(array + i) <= pivot && i < j) i++;
                while (*(array + j) > pivot) j--;
                if (i < j)
                {
                    Swap(array + i, array + j);
                }
            }
            *(array + begin) = *(array + j);
            *(array + j) = pivot;
            return j;
        }

        public unsafe static void QuickSortDual(int* arr, int low, int high)
        {
            if (low < high)
            {

                (int left, int right) = DualPivotPartition(arr, low, high);

                QuickSortDual(arr, low, left - 1);
                QuickSortDual(arr, left + 1, right - 1);
                QuickSortDual(arr, right + 1, high);
            }
        }

        private unsafe static (int, int) DualPivotPartition(int* arr, int low, int high)
        {
            if (arr[low] > arr[high])
                Swap(arr + low,arr + high);

            int j = low + 1;
            int g = high - 1;
            int k = low + 1;
            int p = arr[low];
            int q = arr[high];

            while (k <= g)
            {

                if (arr[k] < p)
                {
                    Swap(arr + k, arr + j);
                    j++;
                }

                else if (arr[k] >= q)
                {
                    while (arr[g] > q && k < g)
                        g--;

                    Swap(arr + k, arr + g);
                    g--;

                    if (arr[k] < p)
                    {
                        Swap(arr + k,arr +j);
                        j++;
                    }
                }
                k++;
            }
            j--;
            g++;

            Swap(arr + low ,arr +j);
            Swap(arr +high, arr + g);

            return (j, g);
        }

        public unsafe static void QuickSortTriple(int* array, int begin, int end)
        {
            if (begin < end)
            {
                (int a, int b, int d) = PartitionTriplePivot(array, begin, end);

                QuickSortTriple(array, begin, a - 1);
                QuickSortTriple(array, a + 1, b - 1);
                QuickSortTriple(array, b + 1, d - 1);
                QuickSortTriple(array, d + 1, end);
            }
        }

        private unsafe static (int, int, int) PartitionTriplePivot(int* array, int begin, int end)
        {
            // Sorting increasingly
            if (*(array + begin) > *(array + end))
                Swap(array + begin, array + end);

            if (*(array + begin) > *(array + begin + 1))
                Swap(array + begin, array + begin +1);

            if (*(array + begin + 1) > *(array + end))
                Swap(array + begin + 1, array + end);

            int a = begin + 2, b = begin + 2;
            int c = end - 1, d = end - 1;
            int p = *(array + begin), q = *(array + begin + 1), r = *(array + end);

            while (b <= c)
            {
                while (*(array + b) < q && b <= c)
                {
                    if (*(array + b) < p)
                    {
                        Swap(array + a, array + b);
                        a = a + 1;
                    }
                    b = b + 1;
                }

                while (*(array + c) > q && b <= c)
                {
                    if (*(array + c) > r)
                    {
                        Swap(array + c, array + d);
                        d = d - 1;
                    }
                    c = c - 1;
                }

                if (b <= c)
                {
                    if (*(array + b) > r)
                    {
                        if (*(array + c) < p)
                        {
                            Swap(array + b, array + a);
                            Swap(array + a, array + c);
                            a = a + 1;
                        }
                        else
                        {
                            Swap(array + b, array + c);
                        }
                        Swap(array + c, array + d);
                        b = b + 1;
                        c = c - 1;
                        d = d - 1;
                    }
                    else
                    {
                        if (*(array + c) < p)
                        {
                            Swap(array + b, array + a);
                            Swap(array + a, array + c);
                            a = a + 1;
                        }
                        else
                        {
                            Swap((array + b), array + c);
                        }
                        b = b + 1;
                        c = c - 1;
                    }
                }
            }
            a = a - 1;
            b = b - 1;
            c = c + 1;
            d = d + 1;
            Swap(array + begin+ 1, array + a);
            Swap(array + a, array + b);
            a = a - 1;
            Swap(array + begin, array + a);
            Swap(array + d, array + d);

            return (a, b, d);
        }

        private unsafe static void Swap( int* a,  int* b)
        {
            int temp = *a;
            *a = *b;
            *b = temp;
        }
    }
}
