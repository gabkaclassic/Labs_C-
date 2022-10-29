using Labs.Lab_4.entity;

namespace Labs.Lab_4.collection;

public class ArticleComparator : IComparer<Article>
{
    public int Compare(Article? x, Article? y)
    {
        return x!.Rating.CompareTo(y!.Rating);
    }
}