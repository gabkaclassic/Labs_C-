
using System.Diagnostics;
using Lab_2;
using Labs.Lab_2;
using Labs.Lab_4;

namespace Lab_1
{
    public delegate void task(object obj);
    internal static class App
    {
        public static void Main(string[] args)
        {
           lab_4();
        }

        private static void lab_4()
        {
            var listener = new Listener<string>();
            var collection1 = new MagazineCollection<string>(m => m.Title, listener);
            collection1.TitleCollection = "Collection #1";
            var collection2 = new MagazineCollection<string>(m => m.Title, listener);
            collection2.TitleCollection = "Collection #2";
            var magazine1 = new Magazine
            {
                Date = DateTime.UnixEpoch
            };
            var magazine2 = new Magazine
            {
                Date = DateTime.UnixEpoch,
                Frequency = Frequency.Monthly
            };
            collection1.AddDefaults();
            collection2.AddMagazines(magazine1);
            magazine1.Frequency = Frequency.Weekly;
            collection1.Replace(magazine1, magazine2);
            magazine1.Circulation = 993;
            magazine2.Circulation = 8;

            Console.WriteLine(listener.ToString());

        }
    }
}

