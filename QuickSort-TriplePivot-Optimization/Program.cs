
using System.Diagnostics;


/*fixed (int* firstElement = &singlePivot[0])
    UnsafeSort.QuickSortSingle(firstElement, 0, singlePivot.Length - 1);*/
namespace QuickSort
{
    public class Program
    {

        private static int[] singlePivot;
        private static int[] parallelSinglePivot;
        private static int[] dualPivot;
        private static int[] parallelDualPivot;
        private static int[] triplePivot;
        private static int[] parallelTriplePivot;

        static void Main(string[] args)
        {
            Console.WriteLine(String.Format("{0,-15} {1,-20} {2,-10} \n", "Algorithm", "Number of elements", "Time in ms"));

            Perform(200_000);
            Perform(500_000);
            Perform(1_000_000);
            Perform(10_000_000);
            Perform(50_000_000);
            Perform(100_000_000);
            Perform(200_000_000);
            //Perform(300_000_000);
            //Perform(400_000_000);
            //Perform(500_000_000);
        }


        public unsafe static void Perform(long number)
        {

            MakeArrays(number);

            Stopwatch s;
            // single pivot
            {
                s = Stopwatch.StartNew();
                Sort.QuickSortSingle(singlePivot, 0, singlePivot.Length - 1);
                s.Stop();
                Console.WriteLine(String.Format("{0,-20} {1,-20} {2,-10} ", "Single Pivot", number, s.ElapsedMilliseconds));

            }

            // dual pivot
            {
                s = Stopwatch.StartNew();
                Sort.QuickSortDual(dualPivot, 0, dualPivot.Length - 1);
                s.Stop();
                Console.WriteLine(String.Format("{0,-20} {1,-20} {2,-10} ", "Dual Pivot", number, s.ElapsedMilliseconds));
            }

            // triple pivot
            {
                s = Stopwatch.StartNew();
                Sort.QuickSortTriple(triplePivot, 0, triplePivot.Length - 1);
                s.Stop();
                Console.WriteLine(String.Format("{0,-20} {1,-20} {2,-10} ", "Triple Pivot", number, s.ElapsedMilliseconds));
            }

            // parallel single pivot 
            {
                s = Stopwatch.StartNew();
                Sort.ParallelSinglePivotQuickSort(parallelSinglePivot);
                s.Stop();
                Console.WriteLine(String.Format("{0,-20} {1,-20} {2,-10} ", "Par Single Pivot", number, s.ElapsedMilliseconds));
            }
            // parallel dual pivot
            {
                s = Stopwatch.StartNew();
                Sort.ParallelDualPivotQuickSort(parallelDualPivot);
                s.Stop();
                Console.WriteLine(String.Format("{0,-20} {1,-20} {2,-10} ", "Par Dual Pivot", number, s.ElapsedMilliseconds));
            }
            // parallel triple pivot 
            {
                s = Stopwatch.StartNew();
                Sort.ParallelTriplePivotQuickSort(parallelTriplePivot);
                s.Stop();
                Console.WriteLine(String.Format("{0,-20} {1,-20} {2,-10} ", "Par Triple Pivot", number, s.ElapsedMilliseconds));
            }

            TestSort();

            Console.WriteLine();
        }

        private static void MakeArrays(long number)
        {
            Random random = new Random();
            List<int> list = new List<int>();
            for (int i = 0; i < number; i++)
                list.Add(random.Next());


            singlePivot = list.ToArray();
            parallelSinglePivot = list.ToArray();
            dualPivot = list.ToArray();
            parallelDualPivot = list.ToArray();
            triplePivot = list.ToArray();
            parallelTriplePivot = list.ToArray();
            list.Clear();
        }

        private static void TestSort()
        {
            if (!(singlePivot.SequenceEqual(dualPivot) && singlePivot.SequenceEqual(triplePivot) &&
                singlePivot.SequenceEqual(parallelSinglePivot) && singlePivot.SequenceEqual(parallelDualPivot)
                && singlePivot.SequenceEqual(parallelTriplePivot)))
                Console.WriteLine("Nizovi nisu sortirani.");
        }
    }
}

