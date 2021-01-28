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
            if (ValidEntry(insertInteger))
                set[insertInteger] = true;
        } // end method InsertElement

        public IntegerSet Intersection(IntegerSet integerSet) {
            IntegerSet temp = new IntegerSet();

            for (int count = 0; count < SETSIZE; count++)
                temp.set[count] = (set[count] && integerSet.set[count]);

            return temp;
        } // end method Intersection

    }

    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
        }
    }
}
