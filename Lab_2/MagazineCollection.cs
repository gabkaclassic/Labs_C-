using System.Collections;
using System.Net.Mail;
using System.Text;

namespace Lab_2;


public delegate TKey KeySelector<out TKey>(Magazine magazine); 

public class MagazineCollection <TKey>
{
    private Dictionary<TKey, Magazine> Magazines;

    private KeySelector<TKey> KeySelector { get; set; }

    public double MaxRating => Magazines.Values.Max(magazine => magazine.AverageRate);

    public IEnumerable<IGrouping<Frequency, KeyValuePair<TKey, Magazine>>> GroupByFrequency =>
        Magazines.GroupBy(p => p.Value.Frequency);

    public MagazineCollection(KeySelector<TKey> selector)
    {
        KeySelector = selector;
    }
    
    public void AddDefaults()
    {
        Magazines = new Dictionary<TKey, Magazine>();
        var magazine = new Magazine();
        Magazines.Add(KeySelector(magazine), magazine);
    }

    public void AddMagazines(params Magazine[] magazines)
    {
        foreach (var magazine in magazines)
            Magazines.Add(KeySelector(magazine), magazine);
    }

    public IEnumerable<KeyValuePair<TKey, Magazine>> FrequencyGroup(Frequency frequency) =>
        Magazines.Where(p => p.Value.Frequency == frequency);

    public override string ToString()
    {
        var result = new StringBuilder("Magazine collection:");
        foreach (var pair in Magazines)
            result.Append(pair.Key).Append(" : ").Append(pair.Value).Append('\n');

        return result.ToString();
    }
    
    public string ToShortString()
    {
        var result = new StringBuilder("Magazine collection:");
        foreach (var pair in Magazines)
            result.Append(pair.Key).Append(" : ").Append(pair.Value.ToShortString()).Append('\n');

        return result.ToString();
    }
}