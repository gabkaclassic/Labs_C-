
using Labs.Lab_4.entity;

namespace Labs
{
    public delegate void task(object obj);
    internal static class App
    {
        public static void Main(string[] args)
        {
            var mag = new Magazine()
            {
                Frequency = Frequency.Weekly,
                Circulation = 123,
                Title = "asasasdsaas",
                Articles = new List<Article>()
            };
            mag.Articles.Add(new Article(new Person("Ivan", "Koval", DateTime.Today), "Title", 45.5));
            Console.WriteLine(mag.Save("./file.bin"));
            
            Console.WriteLine($"Original: {mag}");
            Console.WriteLine("-----------------");
            Console.WriteLine($"Copy: {mag.DeepCopy()}");
            Console.WriteLine("-----------------");

            Console.WriteLine("Enter filename for load: ");
            var filename = Console.ReadLine();
            while (!mag.Load(filename)) 
                filename = Console.ReadLine();
            
            Console.WriteLine($"Loaded object: {mag}");
            Console.WriteLine("-----------------");

            mag.AddFromConsole();
            Console.WriteLine("-----------------");
            Console.WriteLine("Enter filename for save: ");
            filename = Console.ReadLine();
            while (!mag.Save(filename))  
                filename = Console.ReadLine();
            
            Console.WriteLine("-----------------");
            Magazine.Load(filename, mag);
            Console.WriteLine($"Static load: {mag}");
            Console.WriteLine("-----------------");
            mag.AddFromConsole();
            Console.WriteLine("-----------------");
            Magazine.Save(filename, mag);
        }

        
    }
}

