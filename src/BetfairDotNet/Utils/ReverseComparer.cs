namespace BetfairDotNet.Utils;


internal class ReverseComparer : IComparer<double> {
    public int Compare(double x, double y) {
        return y.CompareTo(x);
    }
}
