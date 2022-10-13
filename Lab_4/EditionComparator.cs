using Labs.Lab_2;

namespace Lab_2;

public class EditionComparator : IComparer<Edition>
{
    public int Compare(Edition? x, Edition? y)
    {
        return x!.Circulation.CompareTo(y!.Circulation);
    }
}