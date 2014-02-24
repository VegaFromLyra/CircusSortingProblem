using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircusSortingProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person(1, 13);
            Person p2 = new Person(2, 14);
            Person p3 = new Person(3, 10);
            Person p4 = new Person(4, 11);
            Person p5 = new Person(5, 12);

            List<Person> input = new List<Person>();
            input.Add(p1);
            input.Add(p2);
            input.Add(p3);
            input.Add(p4);
            input.Add(p5);

            var result = GetLongestSequence(input);

            Console.WriteLine("The output is");

            foreach (Person p in result)
            {
                Console.WriteLine("{0}, {1}", p.Height, p.Weight);
            }
        }

        static List<Person> GetLongestSequence(List<Person> people)
        {
            // sort the items according 
            // to height
            people.Sort();

            // Now get longest subsequence according to weight
            return GetLongestIncreasingSubsequence(people);
        }

        // Array: 13, 14, 10, 11, 12
        // Longest ending with A[0]: 13
        // Longest ending with A[1]: 13, 14
        // Longest ending with A[2]: 10
        // Longest ending with A[3]: 10, 11
        // Longest ending with A[4]: 10, 11, 12
        static List<Person> GetLongestIncreasingSubsequence(List<Person> people)
        {
            List<Person>[] solutions = new List<Person>[people.Count];

            // Create an array of list of persons
            // At index i of array is the max subsequence seen so far
            GetLongestIncreasingSubsequence(people, solutions, 0);

            List<Person> bestSequence = null;

            for (int i = 0; i < people.Count; i++)
            {
                bestSequence = GetMaxSequence(bestSequence, solutions[i]);
            }

            return bestSequence;
        }

        // Compares the two input sequences and returns the one
        // that has greater lengths
        static List<Person> GetMaxSequence(List<Person> sequence1, List<Person> sequence2)
        {
            if (sequence1 == null)
            {
                return sequence2;
            }

            if (sequence2 == null)
            {
                return sequence1;
            }

            return sequence1.Count > sequence2.Count ? sequence1 : sequence2;
        }

        static void GetLongestIncreasingSubsequence(List<Person> input,
                                                    List<Person>[] solutions,
                                                    int currentIndex)
        {
            if (currentIndex >= input.Count || currentIndex < 0)
            {
                return;
            }

            List<Person> bestSequence = null;

            Person currentPerson = input[currentIndex];

            for (int i = 0; i < currentIndex; i++)
            {
                if (input[i].IsBefore(currentPerson))
                {
                    bestSequence = GetMaxSequence(bestSequence, solutions[i]);
                }
            }

            List<Person> newSolution = new List<Person>();

            if (bestSequence != null)
            {
                newSolution.AddRange(bestSequence);
            }

            newSolution.Add(currentPerson);

            solutions[currentIndex] = newSolution;

            GetLongestIncreasingSubsequence(input, solutions, currentIndex + 1);
        }

    }

    class Person : IComparable
    {
        public Person(int height, int weight)
        {
            Height = height;
            Weight = weight;
        }

        public int Height { get; set; }

        public int Weight { get; set; }

        public int CompareTo(object other)
        {
            Person otherPerson = other as Person;

            if (Height.Equals(otherPerson.Height))
            {
                return Weight.CompareTo(otherPerson.Weight);
            }

            return Height.CompareTo(otherPerson.Height);
        }

        public bool IsBefore(Person other)
        {
            if (Height < other.Height && Weight < other.Weight)
            {
                return true;
            }

            return false;
        }

    }
}
