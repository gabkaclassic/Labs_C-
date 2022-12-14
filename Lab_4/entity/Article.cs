
using Lab_2;

namespace Labs.Lab_4.entity
{
    [Serializable]
    public sealed class Article : IRateAndCopy, IComparer<Article>, IComparable<Article>
    {
        public Person Author { get; set; }
        public string Title { get; set; }

        public double Rating { get; set; }

        public Article() {
            Title = "<Empty title>";
            Rating = 0.0;
            Author = new Person();
        }

        public Article(Person author, string title, double rating) {
            Author = author;
            Title = title;
            Rating = rating;
        }

        public override string ToString()
        {
            return $"Article: [Title: {Title}, Rating: {Rating}, Author: {Author}]";
        }

        public int Compare(Article? x, Article? y)
        {
            return string.Compare(x!.Author.Lastname, y!.Author.Lastname, StringComparison.Ordinal);
        }

        public int CompareTo(Article? other)
        {
            return string.CompareOrdinal(Title, other!.Title);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Article other)
                return false;

            return Title.Equals(other.Title)
                   && Rating.Equals(other.Rating)
                   && Author.Equals(other.Author);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Rating, Author);
        }

        public object DeepCopy()
        {
            return new Article
            {
                Title = string.Copy(Title),
                Rating = Rating,
                Author = (Person)Author.DeepCopy()
            };
        }
    }
}
