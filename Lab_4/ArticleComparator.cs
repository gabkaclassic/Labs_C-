namespace Lab_2;

public class ArticleComparator : IComparer<Article>
{
    public int Compare(Article? x, Article? y)
    {
        return x!.Rating.CompareTo(y!.Rating);
    }
}