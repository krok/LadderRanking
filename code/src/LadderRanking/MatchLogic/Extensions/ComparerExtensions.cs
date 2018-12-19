using System;
using System.Collections.Generic;

namespace LadderRanking.MatchLogic.Extensions
{
    public static class ComparerExtensions
    {
        // Reverses the comparer by wrapping it in a ComparerReverser instance.
        public static IComparer<T> Reverse<T>(this IComparer<T> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return new ComparerReverser<T>(comparer);
        }


        // A generic comparer that reverses the action of a wrapped comparer.
        private sealed class ComparerReverser<T> : IComparer<T>
        {
            private readonly IComparer<T> _wrappedComparer;

            // Initializes an instance of a ComparerReverser that takes a wrapped comparer
            // and returns the inverse of the comparison.
            public ComparerReverser(IComparer<T> wrappedComparer)
            {
                _wrappedComparer = wrappedComparer ?? throw new ArgumentNullException(nameof(wrappedComparer));
            }

            // Compares two objects and returns a value indicating whether 
            // one is less than, equal to, or greater than the other.
            public int Compare(T x, T y)
            {
                // to reverse compare, just invert the operands....
                return _wrappedComparer.Compare(y, x);
            }
        }
    }
}