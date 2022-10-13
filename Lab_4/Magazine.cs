using System.Collections;
using System.Text;
using Lab_2;
using Labs.Lab_4;

namespace Labs.Lab_2
{
    public sealed class Magazine : Edition, IRateAndCopy, IEnumerable
    {

        private Frequency frequency;
        public Frequency Frequency { get; set; }

        public Edition Edition
        {
            get =>
                new()
                {
                    Circulation = Circulation,
                    Date = Date,
                    Title = Title
                };
            set
            {
                Circulation = value.Circulation;
                Date = value.Date;
                Title = value.Title;
            }
        }

        public bool this[Frequency index] => Frequency.Equals(index);

        public double Rating { get; }

        public List<Article> Articles { get; set; }
        
        public List<Person> Editors { get; set; }


        public double AverageRate { 
            get
            {
                var sum = Articles.Cast<object>().Sum(article => ((Article)article).Rating);

                return sum/Articles.Count;
            } 
        }

        public Magazine() : base()
        {
            Frequency = Frequency.Yearly;
            Articles = new List<Article>();
            Editors = new List<Person>();
        }

        public Magazine(string title, global::Lab_2.Frequency frequency, DateTime date, int circulation, List<Article> articles, List<Person> editors) {

            Title = title;
            Frequency = frequency;
            Date = date;
            Circulation = circulation;
            Articles = articles;
            Editors = editors;
        }

        public void AddArticles(params Article[] articles)
        {
            Articles.AddRange(articles);
        }

        public void AddEditors(params Person[] editors)
        {
            Editors.AddRange(editors);
        }

        public List<Article> SortByTitle()
        {
            return Articles = Articles.OrderBy(article => article.Title).ToList();
        }
        
        public List<Article> SortByAuthorLastname()
        {
            return Articles = Articles.OrderBy(article => article.Author.Lastname).ToList();
        }
        
        public List<Article> SortByRating()
        {
            Articles.Sort((a1, a2) => new ArticleComparator().Compare(a1, a2));
            
            return Articles;
        }

        public IEnumerable<Article> RatingMoreThan(double rating)
        {
            foreach (var article in Articles)
                if (article.Rating > rating)
                    yield return article;
        }

        public IEnumerable<Article> ContainsString(string str)
        {
            foreach (var article in Articles)
                if (article.Title.Contains(str))
                    yield return article;
        }
        public override string ToString()
        {
            var result = new StringBuilder($"Title: {Title}, Frequency: {Frequency}, date of publication: {Date.Day}.{Date.Month}.{Date.Year}, circulation: {Circulation}, articles: \n");
            foreach (var article in Articles)
                result.Append(article).Append('\n');
            result.Append("editors: \n");
            foreach (var editor in Editors)
                result.Append(editor).Append('\n');

            return result.ToString();
        }
        public string ToShortString()
        {
            return $"Title: {Title}, Frequency: {Frequency}, date of publication: {Date.Day}.{Date.Month}.{Date.Year}, circulation: {Circulation}, average rating of articles: {AverageRate}";
        }

        public IEnumerator GetEnumerator()
        {
            var authors = Articles.ToArray().Cast<Article>().Select(article => article.Author);
            return new MagazineEnumerator(Editors, authors);
        }
        

        public override bool Equals(object? obj)
        {
            if (obj is not Magazine other)
                return false;
            
            return Title.Equals(other.Title)
                     && Frequency == other.Frequency
                     && Date.ToBinary().Equals(other.Date.ToBinary())
                     && Circulation.Equals(other.Circulation)
                     && Articles.ToArray().SequenceEqual(other.Articles.ToArray());
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Date, Frequency, Circulation, AverageRate);
        }

        public override object DeepCopy()
        {
            var instance = new Magazine
            {
                Title = string.Copy(title),
                Date = Date,
                Circulation = Circulation,
                Articles = new List<Article>(),
                Editors = new List<Person>()
            };
            instance.Articles.InsertRange(0, Articles);
            instance.Editors.InsertRange(0, Editors);

            return instance;
        }
    }
}
