using Lab_2;

namespace Labs.Lab_2;

internal class TestCollection
{
    public List<Edition> Editions;

    public List<string> Strings;

    public Dictionary<Edition, Magazine> Magazines;

    public Dictionary<string, Magazine> MagazinesWithString;

    public TestCollection(int count)
    {
        Editions = new List<Edition>();
        Strings = new List<string>();
        Magazines = new Dictionary<Edition, Magazine>();
        MagazinesWithString = new Dictionary<string, Magazine>();
        
        for (var i = 0; i < count; i++)
        {
            var magazineInstance = GetMagazineInstance(i);
            
            Editions.Add(magazineInstance);
            Strings.Add(magazineInstance.ToString());
            Magazines.Add(magazineInstance.Edition, magazineInstance);
            MagazinesWithString.Add(magazineInstance.ToString(), magazineInstance);
        }

    }

    public bool SearchElementInEditions(Edition edition)
    {
        return Editions.Contains(edition);
    }
    
    public bool SearchElementInStrings(string str)
    {
        return Strings.Contains(str);
    }
    
    public bool SearchElementInMagazines(Magazine magazine)
    {
        return Magazines.ContainsValue(magazine);
    }

    public bool SearchKeyInMagazines(Edition edition)
    {
        return Magazines.ContainsKey(edition);
    }
    public static Magazine GetMagazineInstance(int ind)
    {

        var date = DateTime.FromBinary(ind);
        var magazine = new Magazine(ind.ToString(), (Frequency)(ind % 3), date, ind, new List<Article>(),
            new List<Person>())
        {
            Edition =
            {
                Circulation = ind,
                Date = date,
                Title = ind.ToString()
            }
        };

        return magazine;
    }
}