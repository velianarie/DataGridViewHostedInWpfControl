namespace DataGridViewHostedInWpfControl.BusinessObjects
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class TenorStrikeRates : IEnumerable<TenorStrikeRate>
    {
        private readonly SortedList<Tuple<string, double>, TenorStrikeRate> internalData;

        public TenorStrikeRates()
        {
            internalData = new SortedList<Tuple<string, double>, TenorStrikeRate>();
        }

        public List<string> UniqueSortedTenors
        {
            get { return internalData.Values.Select(x => x.Tenor).Distinct().ToList(); }
        }

        public List<double> UniqueSortedStrikes
        {
            get { return internalData.Values.Select(x => x.Strike).Distinct().ToList(); }
        }

        public void Add(TenorStrikeRate toBeAddedItem)
        {
            Tuple<string, double> key = new Tuple<string, double>(toBeAddedItem.Tenor, toBeAddedItem.Strike);
            if (internalData.ContainsKey(key))
            {
                internalData.Remove(key);
            }

            internalData.Add(key, toBeAddedItem);
        }

        public TenorStrikeRate Find(string tenor, double strike)
        {
            return internalData.Values.FirstOrDefault(x => IsTenorEqual(x.Tenor, tenor) && IsDoubleEqual(x.Strike, strike));
        }

        public IEnumerator<TenorStrikeRate> GetEnumerator()
        {
            IEnumerator<TenorStrikeRate> iterator = internalData.Values.GetEnumerator();
            while (iterator.MoveNext())
            {
                yield return iterator.Current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof(TenorStrikeRates))
                return false;

            TenorStrikeRates other = (TenorStrikeRates)obj;
            return IsAllItemInListEqual(internalData.Values, other.internalData.Values);
        }
        
        private bool IsAllItemInListEqual(IList<TenorStrikeRate> thisList, IList<TenorStrikeRate> otherList)
        {
            bool isEqual = thisList.Count.Equals(otherList.Count);
            for (int index = 0; index < otherList.Count && isEqual; index++)
            {
                TenorStrikeRate thisItem = thisList[index];
                TenorStrikeRate otherItem = otherList[index];
                isEqual = IsTenorEqual(thisItem.Tenor, otherItem.Tenor) 
                    && IsDoubleEqual(thisItem.Strike, otherItem.Strike)
                    && IsDoubleEqual(thisItem.Rate, otherItem.Rate);
            }

            return isEqual;
        }

        private bool IsDoubleEqual(double one, double two)
        {
            if (double.IsNaN(one) && double.IsNaN(two))
            {
                return true;
            }

            return Math.Abs(one - two) < 1e-15;
        }

        private bool IsTenorEqual(string one, string two)
        {
            return one.Equals(two, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
