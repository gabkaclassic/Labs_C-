
using System.Collections;

namespace Lab_1
{
    class App
    {
        public static void Main(string[] args)
        {
            var d = DateTime.Now;
            var edition1 = new Edition("Some edition number one", d, 12);
            var edition2 = new Edition("Some edition number one", d, 12);
            
            // Console.WriteLine(edition1 == edition2);
            // Console.WriteLine(edition1.Equals(edition2));

            // try
            // {
            //     edition1.Circulation = -1;
            // }
            // catch (InvalidDataException e)
            // {
            //     Console.WriteLine(e.Message);
            // }

            var editors = new ArrayList();
            editors.Add(new Person());
            editors.Add(new Person("Ivan", "Ivanovich", DateTime.Now));
            var articles = new ArrayList();
            articles.Add(new Article((Person)editors[1], "Article", 2.5));
            var magazine = new Magazine
            {
                Title = "Title...",
                Circulation = 10,
                Date = DateTime.MinValue,
                Editors = editors,
                ArticlesList = articles,
                Frequency = Frequency.Weekly
            };
            // Console.WriteLine(magazine);
            // Console.WriteLine(magazine.Edition);
            // var magazine1 = (Magazine)magazine.DeepCopy();
            // magazine.Frequency = Frequency.Monthly;
            // Console.WriteLine("Changed original: ");
            // Console.WriteLine(magazine);
            // Console.WriteLine("Copy: ");
            // Console.WriteLine(magazine1);

            // Console.WriteLine("With rating:");
            // foreach (var article in magazine.RatingMoreThan(-1.0))
            //     Console.WriteLine(article);
            // Console.WriteLine("Contains string: ");
            // foreach (var article in magazine.ContainsString("ic"))
            //     Console.WriteLine(article);

            // Console.WriteLine("Only authors:");
            // foreach (var person in magazine)
            //     Console.WriteLine(person);
        }


    }
}

