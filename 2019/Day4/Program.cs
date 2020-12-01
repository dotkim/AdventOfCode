using System;
using System.Collections.Generic;

namespace Day4
{
    class Program
    {
        private static List<int> Part1()
        {
            string input = "231832-767346";
            // The value is within the range given in your puzzle input.
            Int32.TryParse(input.Split("-")[0], out int start);
            Int32.TryParse(input.Split("-")[1], out int end);

            List<int> possibleNumbers = new List<int>();

            for (int i = start; i < end; i++)
            {
                if (i.ToString().Length != 6) continue; // It is a six-digit number.

                int lastnum = 0;
                bool containsDouble = false;
                foreach (char a in i.ToString())
                {
                    Int32.TryParse(a.ToString(), out int x);
                    if (x < lastnum)
                    {
                        // Going from left to right, the digits never decrease; they only ever increase or stay the same (like 111123 or 135679).
                        lastnum = -1;
                        break;
                    }

                    if (x == lastnum)
                    {
                        // Two adjacent digits are the same (like 22 in 122345).
                        containsDouble = true;
                    }

                    lastnum = x;
                }

                if (lastnum == -1) continue;
                if (!containsDouble) continue;
                possibleNumbers.Add(i);
            }

            return possibleNumbers;
        }

        private static int Part2(List<int> pnum)
        {
            List<int> possibleNumbers = new List<int>();
            foreach (int i in pnum)
            {
                int lnum = 0;
                bool containsDouble = false;
                int dnum = 0;
                int grpCount = 0;
                int step = 0;
                foreach (char a in i.ToString())
                {
                    Int32.TryParse(a.ToString(), out int x);
                    if (x == lnum) grpCount++;
                    if (grpCount == 2 & (x == lnum & (dnum == 0))) { containsDouble = true; dnum = lnum; }
                    if (grpCount == 3 & containsDouble & dnum == x) { containsDouble = false; dnum = 0; }
                    if (x != lnum) grpCount = 1;

                    lnum = x;
                    step++;
                }
                if (containsDouble) possibleNumbers.Add(i);
            }
            return possibleNumbers.Count;
        }

        static void Main()
        {
            List<int> p1 = Part1();
            Console.WriteLine("Part 1: " + p1.Count.ToString());
            Console.WriteLine("Part 2: " + Part2(p1).ToString());
        }
    }
}