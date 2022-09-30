
using System.Collections;
using Lab_2;

namespace Lab_1
{
    internal static class App
    {
        public static void Main(string[] args)
        {
           // lab_2();
           lab_3();
        }

        private static void lab_2()
        {
            var d = DateTime.Now;
            var edition1 = new Edition("Some edition number one", d, 12);
            var edition2 = new Edition("Some edition number one", d, 12);
            
            Console.WriteLine(edition1 == edition2);
            Console.WriteLine(edition1.Equals(edition2));

            try
            {
                edition1.Circulation = -1;
            }
            catch (InvalidDataException e)
            {
                Console.WriteLine(e.Message);
            }

            var editors = new ArrayList
            {
                new Person(),
                new Person("Ivan", "Ivanovich", DateTime.Now)
            };
            var articles = new ArrayList { new Article(((Person)editors[1]!)!, "Article", 2.5) };
            var magazine = new Magazine
            {
                Title = "Title...",
                Circulation = 10,
                Date = DateTime.MinValue,
                Editors = editors,
                ArticlesList = articles,
                Frequency = Frequency.Weekly
            };
            Console.WriteLine(magazine);
            Console.WriteLine(magazine.Edition);
            var magazine1 = (Magazine)magazine.DeepCopy();
            magazine.Frequency = Frequency.Monthly;
            Console.WriteLine("Changed original: ");
            Console.WriteLine(magazine);
            Console.WriteLine("Copy: ");
            Console.WriteLine(magazine1);

            Console.WriteLine("With rating:");
            foreach (var article in magazine.RatingMoreThan(-1.0))
                Console.WriteLine(article);
            Console.WriteLine("Contains string: ");
            foreach (var article in magazine.ContainsString("ic"))
                Console.WriteLine(article);

            Console.WriteLine("Only authors:");
            foreach (var person in magazine)
                Console.WriteLine(person);
        }

        private static void lab_3()
        {
            var p1 = new Lab_2.Person("Roman", "Sobaka", DateTime.MaxValue);
            var p2 = new Lab_2.Person("Ivan", "Lopata", DateTime.MinValue);
            var p3 = new Lab_2.Person("Redis", "Redisov", DateTime.Now);
            var p4 = new Lab_2.Person();
            var editors1 = new List<Lab_2.Person>();
            editors1.Add(p1);
            editors1.Add(p3);
            var editors2 = new List<Lab_2.Person>();
            editors1.Add(p2);
            editors1.Add(p4);
            var articles1 = new List<Lab_2.Article>
            {
                new Lab_2.Article(),
                new Lab_2.Article(p2, "Article #1", 12.2),
                new Lab_2.Article(p1, "Blabla bla", 8.2),
                new Lab_2.Article(p4, "History of world", 2.1)
            };
            var articles2 = new List<Lab_2.Article>
            {
                new Lab_2.Article(p2, "Figma", 8.7),
                new Lab_2.Article(p2, "Article #1123", 0.7),
                new Lab_2.Article(p1, "My minds", 8.4)
            };
            var magazine1 = new Lab_2.Magazine("Some magazine", Lab_2.Frequency.Weekly, DateTime.MinValue.Add(DateTime.Now.TimeOfDay), 8, articles1, editors1);
            var magazine2 = new Lab_2.Magazine("Famous magazine", Lab_2.Frequency.Yearly, DateTime.MaxValue, 8, articles2, editors2);

            Console.WriteLine("Sorted by title of article:");
            foreach (var article in magazine1.SortByTitle())
                Console.WriteLine(article);
            Console.WriteLine("---------------------");
            Console.WriteLine("Sorted by last name of author of article:");
            foreach (var article in magazine1.SortByAuthorLastname())
                Console.WriteLine(article);
            Console.WriteLine("---------------------");
            Console.WriteLine("Sorted by rating of article:");
            foreach (var article in magazine1.SortByRating())
                Console.WriteLine(article);
            Console.WriteLine("---------------------");
            var collection = new MagazineCollection<string>(magazine => magazine.Title);
            collection.AddDefaults();
            collection.AddMagazines(magazine1, magazine2);

            Console.WriteLine("Max rating:");
            Console.WriteLine(collection.MaxRating);
            Console.WriteLine("---------------------");
            Console.WriteLine("With weekly frequency:");
            foreach (var magazine in collection.FrequencyGroup(Lab_2.Frequency.Weekly))
                Console.WriteLine(magazine);
            Console.WriteLine("---------------------");
            Console.WriteLine("Grouping by frequency:");

            foreach (var group in collection.GroupByFrequency)
            {
                
                var key = group.Key;
                foreach (var value in group)
                    Console.WriteLine(key + " : " + value.Value.ToShortString() + "\n");
                
            }

        }
    }
}

