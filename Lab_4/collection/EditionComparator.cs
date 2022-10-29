using Labs.Lab_4.entity;

namespace Labs.Lab_4.collection;

public class EditionComparator : IComparer<Edition>
{
    public int Compare(Edition? x, Edition? y)
    {
        return x!.Circulation.CompareTo(y!.Circulation);
    }
}