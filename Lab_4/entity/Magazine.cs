using System.Collections;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Lab_2;
using Labs.Lab_4.collection;

namespace Labs.Lab_4.entity
{
    [Serializable]
    public sealed class Magazine : Edition, IRateAndCopy, IEnumerable, Serializable<Magazine>
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

        public Magazine(string title, Frequency frequency, DateTime date, int circulation, List<Article> articles, List<Person> editors) {

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

        private Magazine Copy()
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

        public bool Save(string filename)
        {
            
            if(!File.Exists(filename))
                Console.WriteLine("Creating new file...");
            
            var fileStream = new FileStream(filename, FileMode.Append, FileAccess.Write);

            var stream = new MemoryStream();
            
            try
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                fileStream.Write(stream.GetBuffer());
            }
            catch
            {
                return false;
            }
            finally
            {
                stream.Close();
                fileStream.Close();
            }

            return true;
        }

        public bool Load(string filename)
        {

            if (!File.Exists(filename))
            {
                Console.WriteLine("File doesn't exists");
                
                return false;
            }

            var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            
            try
            {

                var formatter = new BinaryFormatter();
                var instance = (Magazine)formatter.Deserialize(fileStream);
                Define(instance);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error of load");
                return false;
            }
            finally
            {
                fileStream.Close();
            }

            return true;
        }

        public bool AddFromConsole()
        {
            Console.WriteLine("Please, enter data for creating new object (Article, data format: <Title>;<Rating>;<Author firstname>;<Author lastname>; <Author birthday date in format dd.MM.yyyy>): ");
            var data = Console.ReadLine().Split(";");
            var article = new Article();

            try
            {
                article.Title = data[0];
                article.Rating = double.Parse(data[1]);
                article.Author.Firstname = data[2];
                article.Author.Lastname = data[3];
                article.Author.Birthday = DateTime.ParseExact(data[4], "dd.MM.yyyy", null);
                
                Articles.Add(article);
            }
            catch(Exception e)
            {
                Console.WriteLine("Invalid input data");
                
                return false;
            }

            return true;
        }

        public Magazine DeepCopy()
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Seek(0, SeekOrigin.Begin);
            
            return (Magazine)formatter.Deserialize(stream);
        }

        public static bool Save(string filename, Magazine obj)
        {
            return obj.Save(filename);
        }

        public static bool Load(string filename, Magazine obj)
        {
            return obj.Load(filename);
        }

        private void Define(Magazine other)
        {
            Frequency = other.Frequency;
            Articles = new List<Article>(other.Articles);
            Editors = new List<Person>(other.Editors);
            Edition = other.Edition;
            Circulation = other.Circulation;
            Date = other.Date;
            Title = other.Title;
        }
    }
}
