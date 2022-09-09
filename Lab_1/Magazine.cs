using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_1
{
    class Magazine
    {
        private string title;
        public string Title {
            get => title;
            set
            {
                if (string.IsNullOrEmpty(value))
                    title = value;
            }
        }

        private Frequency frequency;
        public Frequency Frequency { get; set; }

        private DateTime date;
        public DateTime Date { get; set; }
        private int circulation;
        public int Circulation {
            get => circulation;
            set {
                circulation = Math.Max(0, value);
            }
        }

        public bool this[int index] {
            get => this.Frequency.Equals(index);
        }

        private Article[] articlesList;
        public Article[] ArticlesList { get; set; }

        public double AverageRate { 
            get {
                var sum = 0.0;

                foreach (var article in ArticlesList)
                    sum += article.Rate;

                return sum/ArticlesList.Length;
            } 
        }

        public Magazine() {
            Frequency = Frequency.Yearly;
            Date = DateTime.Now;
            ArticlesList = new Article[]{ new Article() };
            Title = "<Empty title>";
        }

        public Magazine(string title, Frequency frequency, DateTime date, int circulation, Article[] articles) {

            Title = title;
            Frequency = frequency;
            Date = date;
            Circulation = circulation;
            ArticlesList = articles;
        }

        public void AddArticles(Article[] articles) {
            var len = articles.Length + ArticlesList.Length;
            var newArticles = new Article[len];

            for (var i = 0; i < len; i++) {
                if (i < articles.Length)
                    newArticles[i] = articles[i];
                else
                    newArticles[i] = ArticlesList[i - articles.Length];
            }

            ArticlesList = newArticles;
        }

        private static string FrequencyName(Frequency frequency) {

            if (frequency.Equals(Frequency.Monthly))
                return "Monthly";
            else if (frequency.Equals(Frequency.Yearly))
                return "Yearly";
            else
                return "Wheekly";
        }
        public override string ToString()
        {
            var result = new StringBuilder($"Title: {Title}, Frequency: {FrequencyName(Frequency)}, date of publication: {Date.Day}.{Date.Month}.{Date.Year}, circulation: {Circulation}, articles: \n");
            foreach (var article in ArticlesList)
                result.Append(article).Append('\n');
            return result.ToString();
        }

        public virtual string ToShortString()
        {
            return $"Title: {Title}, Frequency: {FrequencyName(Frequency)}, date of publication: {Date.Day}.{Date.Month}.{Date.Year}, circulation: {Circulation}, average rating of articles: {AverageRate}";
        }
    }
}
