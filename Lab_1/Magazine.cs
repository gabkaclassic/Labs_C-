using System.Collections;
using System.Globalization;
using System.Text;

namespace Lab_1
{
    internal class Magazine : Edition, IRateAndCopy, IEnumerable
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

        public bool this[Frequency index]
        {
            get => Frequency.Equals(index);
        }

        public double Rating { get; }

        private ArrayList articlesList;
        public ArrayList ArticlesList { get; set; }
        
        public ArrayList Editors { get; set; }


        public double AverageRate { 
            get
            {
                var sum = ArticlesList.Cast<object>().Sum(article => ((Article)article).Rating);

                return sum/ArticlesList.Count;
            } 
        }

        public Magazine() : base()
        {
            Frequency = Frequency.Yearly;
            ArticlesList = new ArrayList();
            Editors = new ArrayList();
        }

        public Magazine(string title, Frequency frequency, DateTime date, int circulation, ArrayList articles, ArrayList editors) {

            Title = title;
            Frequency = frequency;
            Date = date;
            Circulation = circulation;
            ArticlesList = articles;
            Editors = editors;
        }

        public void AddArticles(params Article[] articles)
        {
            ArticlesList.InsertRange(0, articles);
        }

        public void AddEditors(params Person[] editors)
        {
            ArticlesList.InsertRange(0, editors);
        }

        public IEnumerable<Article> RatingMoreThan(double rating)
        {
            foreach (var article in ArticlesList)
                if (article is Article a && a.Rating > rating)
                    yield return a;
        }

        public IEnumerable<Article> ContainsString(string str)
        {
            foreach (var article in ArticlesList)
                if (article is Article a && a.Title.Contains(str))
                    yield return a;
        }
        public override string ToString()
        {
            var result = new StringBuilder($"Title: {Title}, Frequency: {Frequency}, date of publication: {Date.Day}.{Date.Month}.{Date.Year}, circulation: {Circulation}, articles: \n");
            foreach (var article in ArticlesList)
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
            var authors = ArticlesList.ToArray().Cast<Article>().Select(article => article.Author);
            return new MagazineEnumerator(Editors, authors);
        }
        

        public override bool Equals(object? obj)
        {
            if (obj is not Magazine other)
                return false;
            
            return Title.Equals(other.Title)
                     && Frequency.Equals(other.Frequency)
                     && Date.Equals(other.Date)
                     && Circulation.Equals(other.Circulation)
                     && articlesList.ToArray().SequenceEqual(other.ArticlesList.ToArray());
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
                ArticlesList = new ArrayList(),
                Editors = new ArrayList()
            };
            instance.ArticlesList.InsertRange(0, ArticlesList);
            instance.Editors.InsertRange(0, Editors);

            return instance;
        }
    }
}
