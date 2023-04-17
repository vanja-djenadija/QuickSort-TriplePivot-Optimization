using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort
{
    public class Sort
    {
        static int maxDepth = Environment.ProcessorCount;
        static int THRESHOLD = 2048;

        public static void QuickSortSingle(int[] niz, int begin, int end)
        {
            if (begin < end)
            {
                int pivot = PartitionOnePivot(niz, begin, end);
                QuickSortSingle(niz, begin, pivot - 1);
                QuickSortSingle(niz, pivot + 1, end);
            }
        }

        private static int PartitionOnePivot(int[] array, int begin, int end)
        {
            int i = begin, j = end;
            int pivot = array[begin];
            while (i < j)
            {
                while (array[i] <= pivot && i < j)
                    i++;
                while (array[j] > pivot)
                    j--;
                if (i < j)
                {
                    Swap(ref array[i], ref array[j]);
                }
            }
            array[begin] = array[j];
            array[j] = pivot;
            return j;
        }


        /** https://www.geeksforgeeks.org/dual-pivot-quicksort/ */
        public static void QuickSortDual(int[] arr, int low, int high)
        {
            if (low < high)
            {

                (int left, int right) = DualPivotPartition(arr, low, high);

                QuickSortDual(arr, low, left - 1);
                QuickSortDual(arr, left + 1, right - 1);
                QuickSortDual(arr, right + 1, high);
            }
        }

        private static (int, int) DualPivotPartition(int[] arr, int low, int high)
        {
            if (arr[low] > arr[high])
                Swap(ref arr[low], ref arr[high]);

            int j = low + 1;
            int g = high - 1;
            int k = low + 1;
            int p = arr[low];
            int q = arr[high];

            while (k <= g)
            {

                if (arr[k] < p)
                {
                    Swap(ref arr[k], ref arr[j]);
                    j++;
                }

                else if (arr[k] >= q)
                {
                    while (arr[g] > q && k < g)
                        g--;

                    Swap(ref arr[k], ref arr[g]);
                    g--;

                    if (arr[k] < p)
                    {
                        Swap(ref arr[k], ref arr[j]);
                        j++;
                    }
                }
                k++;
            }
            j--;
            g++;

            Swap(ref arr[low], ref arr[j]);
            Swap(ref arr[high], ref arr[g]);

            return (j, g);
        }

        public static void QuickSortTriple(int[] array, int begin, int end)
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

        private static (int, int, int) PartitionTriplePivot(int[] array, int begin, int end)
        {
            // Sorting increasingly
            if (array[begin] > array[end])
                Swap(ref array[begin], ref array[end]);

            if (array[begin] > array[begin + 1])
                Swap(ref array[begin], ref array[begin + 1]);

            if (array[begin + 1] > array[end])
                Swap(ref array[begin + 1], ref array[end]);

            int a = begin + 2, b = begin + 2;
            int c = end - 1, d = end - 1;
            int p = array[begin], q = array[begin + 1], r = array[end];

            while (b <= c)
            {
                while (array[b] < q && b <= c)
                {
                    if (array[b] < p)
                    {
                        Swap(ref array[a], ref array[b]);
                        a = a + 1;
                    }
                    b = b + 1;
                }

                while (array[c] > q && b <= c)
                {
                    if (array[c] > r)
                    {
                        Swap(ref array[c], ref array[d]);
                        d = d - 1;
                    }
                    c = c - 1;
                }

                if (b <= c)
                {
                    if (array[b] > r)
                    {
                        if (array[c] < p)
                        {
                            Swap(ref array[b], ref array[a]);
                            Swap(ref array[a], ref array[c]);
                            a = a + 1;
                        }
                        else
                        {
                            Swap(ref array[b], ref array[c]);
                        }
                        Swap(ref array[c], ref array[d]);
                        b = b + 1;
                        c = c - 1;
                        d = d - 1;
                    }
                    else
                    {
                        if (array[c] < p)
                        {
                            Swap(ref array[b], ref array[a]);
                            Swap(ref array[a], ref array[c]);
                            a = a + 1;
                        }
                        else
                        {
                            Swap(ref array[b], ref array[c]);
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
            Swap(ref array[begin + 1], ref array[a]);
            Swap(ref array[a], ref array[b]);
            a = a - 1;
            Swap(ref array[begin], ref array[a]);
            Swap(ref array[end], ref array[d]);

            return (a, b, d);
        }

        private static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
        public static void ParallelSinglePivotQuickSort(int[] array)
        {
            ParallelSinglePivot(array, 0, array.Length - 1);
        }

        private static void ParallelSinglePivot(int[] arr, int low, int high)
        {

            if (low < high)
            {

                if (high - low < THRESHOLD)
                {
                    QuickSortSingle(arr, low, high);
                }
                else
                {
                    int pivot = PartitionOnePivot(arr, low, high);

                    Parallel.Invoke(
                    () => ParallelSinglePivot(arr, low, pivot - 1),
                    () => ParallelSinglePivot(arr, pivot + 1, high));

                }
            }
        }

        public static void ParallelDualPivotQuickSort(int[] array)
        {
            ParallelDualPivot(array, 0, array.Length - 1);
        }

        private static void ParallelDualPivot(int[] arr, int low, int high)
        {
            if (low < high)
            {

                if (high - low < THRESHOLD)
                {
                    QuickSortDual(arr, low, high);
                }
                else
                {
                    (int left, int right) = DualPivotPartition(arr, low, high);

                    Parallel.Invoke(
                    () => ParallelDualPivot(arr, low, left - 1),
                    () => ParallelDualPivot(arr, left + 1, right - 1),
                    () => ParallelDualPivot(arr, right + 1, high));
                }

            }
        }
        public static void ParallelTriplePivotQuickSort(int[] array)
        {
            ParallelTriplePivot(array, 0, array.Length - 1);
        }

        private static void ParallelTriplePivot(int[] arr, int low, int high)
        {
            if (low < high)
            {

                if (high - low < THRESHOLD)
                {
                    QuickSortTriple(arr, low, high);
                }
                else
                {
                    (int a, int b, int d) = PartitionTriplePivot(arr, low, high);

                    Parallel.Invoke(
                   () => ParallelTriplePivot(arr, low, a - 1),
                   () => ParallelTriplePivot(arr, a + 1, b - 1),
                   () => ParallelTriplePivot(arr, b + 1, d - 1),
                   () => ParallelTriplePivot(arr, d + 1, high));

                }
            }
        }
    }
}

