using System;

namespace Lab_2 {

    public class IntegerSet {

        private const int SETSIZE = 101;
        private bool[] set;

        // parameterless constructor, creates an empty set
        public IntegerSet() {
            set = new bool[SETSIZE];
        } // end parameterless constructor

        // constructor creates a set from array of integers
        public IntegerSet(int[] array)
        : this() {
            for (int i = 0; i < array.Length; i++)
                InsertElement(array[i]);
        } // end constructor

        public void InsertElement(int insertInteger) {
            if (insertInteger >= 0 && insertInteger <= 100)
                set[insertInteger] = true;
        } // end method InsertElement

        public void DeleteElement(int insertInteger) {
            if (insertInteger >= 0 && insertInteger <= 100)
                set[insertInteger] = false;
        } // end method DeleteElement

        public IntegerSet Union(IntegerSet integerSet) {
            IntegerSet temp = new IntegerSet();

            for (int count = 0; count < SETSIZE; count++)
                temp.set[count] = (set[count] || integerSet.set[count]);

            return temp;
        } // end method Union

        public IntegerSet Intersection(IntegerSet integerSet) {
            IntegerSet temp = new IntegerSet();

            for (int count = 0; count < SETSIZE; count++)
                temp.set[count] = (set[count] && integerSet.set[count]);

            return temp;
        } // end method Intersection

        public String ToString() {
            if (set.Length == 0 || set == null) return "---";
            string result = "{ ";
            for (int i = 0; i < set.Length; i++) {
                if (set[i] == true)
                    result += i + " ";
            }
            result += "}";
            return result;
        }

        public bool IsEqualTo(IntegerSet integerSet) {
            if (set.Length != integerSet.set.Length) return false;
            for (int i = 0; i < integerSet.set.Length; i++) if (set[i].CompareTo(integerSet.set[i]) != 0) return false;
            return true;
        }

    }

    class Program {
        static IntegerSet InputSet() {
            IntegerSet result = new IntegerSet();
            while (true) {
                Console.Write("Enter number (-1 to end): ");
                int inputNum = Convert.ToInt32(Console.ReadLine());
                if (inputNum != -1) result.InsertElement(inputNum);
                else return result;
            }
        }
        static void Main(string[] args) {
            // initialize two sets
            Console.WriteLine("Input Set A");
            IntegerSet set1 = InputSet();
            Console.WriteLine("\nInput Set B");
            IntegerSet set2 = InputSet();

            IntegerSet union = set1.Union(set2);
            IntegerSet intersection = set1.Intersection(set2);

            // prepare output
            Console.WriteLine("\nSet A contains elements:");
            Console.WriteLine(set1.ToString());
            Console.WriteLine("\nSet B contains elements:");
            Console.WriteLine(set2.ToString());
            Console.WriteLine(
            "\nUnion of Set A and Set B contains elements:");
            Console.WriteLine(union.ToString());
            Console.WriteLine(
            "\nIntersection of Set A and Set B contains elements:");
            Console.WriteLine(intersection.ToString());

            // test whether two sets are equal
            if (set1.IsEqualTo(set2))
                Console.WriteLine("\nSet A is equal to set B");
            else
                Console.WriteLine("\nSet A is not equal to set B");

            // test insert and delete
            Console.WriteLine("\nInserting 77 into set A...");
            set1.InsertElement(77);
            Console.WriteLine("\nSet A now contains elements:");
            Console.WriteLine(set1.ToString());

            Console.WriteLine("\nDeleting 77 from set A...");
            set1.DeleteElement(77);
            Console.WriteLine("\nSet A now contains elements:");
            Console.WriteLine(set1.ToString());

            // test constructor
            int[] intArray = { 25, 67, 2, 9, 99, 105, 45, -5, 100, 1 };
            IntegerSet set3 = new IntegerSet(intArray);

            Console.WriteLine("\nNew Set contains elements:");
            Console.WriteLine(set3.ToString());
        }
    }
}
