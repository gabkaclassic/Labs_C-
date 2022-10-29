using System.Text;
using Labs.Lab_4.entity;
using Labs.Lab_4.@event;

namespace Labs.Lab_4.collection;


public delegate TKey KeySelector<out TKey>(Magazine magazine); 

public class MagazineCollection <TKey> where TKey : notnull
{
    private Dictionary<TKey, Magazine> Magazines;
    public string TitleCollection { get; set; }

    private Listener<TKey> listener;
    private KeySelector<TKey> KeySelector { get; set; }
    public event MagazinesChangedHandler<TKey> MagazinesChanged;
    public double MaxRating => Magazines.Values.Max(magazine => magazine.AverageRate);
    public IEnumerable<IGrouping<Frequency, KeyValuePair<TKey, Magazine>>> GroupByFrequency =>
        Magazines.GroupBy(p => p.Value.Frequency);

    public MagazineCollection(KeySelector<TKey> selector, Listener<TKey> listener)
    {
        KeySelector = selector;
        this.listener = listener;
        MagazinesChanged += this.listener.Save;
        Magazines = new Dictionary<TKey, Magazine>();
    }
    public void AddDefaults()
    {
        var magazine = new Magazine();
        var key = KeySelector(magazine);
        magazine.AddListener(listener);
        Magazines.Add(key, magazine);
        MagazinesChanged(magazine, new MagazinesChangedEventArgs<TKey>(TitleCollection, Update.Add, "Magazines", key));
    }

    public void AddMagazines(params Magazine[] magazines)
    {
        foreach (var magazine in magazines) {
            var key = KeySelector(magazine);
            magazine.AddListener(listener);
            Magazines.Add(key, magazine);
            MagazinesChanged(magazine, new MagazinesChangedEventArgs<TKey>(TitleCollection, Update.Add, "Magazines", key));
        }
    }

    public IEnumerable<KeyValuePair<TKey, Magazine>> FrequencyGroup(Frequency frequency) =>
        Magazines.Where(p => p.Value.Frequency == frequency);

    public bool Replace(Magazine mold, Magazine mnew)
    {
        var result = Magazines.ContainsValue(mold);
        
        var key = KeySelector(mold);
        mold.RemoveListener(listener);
        Magazines.Remove(key);
        Magazines.Add(key, mnew);
        mnew.AddListener(listener);
        MagazinesChanged(mnew, new MagazinesChangedEventArgs<TKey>(TitleCollection, Update.Replace, "Magazines", key));
        
        return result;
    }

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

    protected virtual void OnMagazinesChanged(object source, MagazinesChangedEventArgs<TKey> args)
    {
        MagazinesChanged?.Invoke(source, args);
    }
}

public delegate void MagazinesChangedHandler<TKey>
    (object source, MagazinesChangedEventArgs<TKey> args);