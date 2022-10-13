using System.ComponentModel;
using System.Runtime.CompilerServices;
using Labs.Annotations;
using Labs.Lab_4;

namespace Labs.Lab_2;

public class Edition : IComparable<Edition>, IComparer<Edition>, INotifyPropertyChanged
{
    protected string title;

    private DateTime date;

    private int circulation;

    public DateTime Date
    {
        get => date;
        set
        {
            date = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("date"));
        }
    }
        
    public int Circulation {
        get => circulation;
        set
        {
            if (value < 0)
                throw new InvalidDataException("Circulation value must be positive");
            circulation = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("circulation"));
        }

    }
    
    public string Title {
        get => title;
        set
        {
            if (!string.IsNullOrEmpty(value))
                title = value;
        }
    }

    public Edition(string title, DateTime date, int circulation) {

        Title = title;
        this.title = title;
        Date = date;
        Circulation = circulation;
    }

    public Edition() 
    {
        Date = DateTime.Now;
        Title = "<Empty title>";
    }

    public int CompareTo(Edition? other)
    {
        return string.Compare(Title, other?.Title, StringComparison.Ordinal);
    }

    public int Compare(Edition? x, Edition? y)
    {
        return x!.Date.CompareTo(y!.Date);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Edition other)
            return false;

        return Title.Equals(other.Title)
               && Date.ToBinary().Equals(other.Date.ToBinary())
               && Circulation.Equals(other.Circulation);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Title, Date, Circulation);
    }

    public virtual object DeepCopy()
    {
        return new Edition
        {
            Title = string.Copy(title),
            Date = date,
            Circulation = Circulation,
        };
    }

    public override string ToString()
    {
        return $"Edition: [title: {Title}, circulation: {Circulation}, date of publication: {Date.Day}.{Date.Month}.{Date.Year}]";
    }
    
    public static bool operator==(Edition edition1, Edition edition2)
    {
        return edition1.Equals(edition2);
    }
    
    public static bool operator !=(Edition edition1, Edition edition2)
    {
        return !(edition1 == edition2);
    }

    public void AddListener<TKey>(Listener<TKey> listener)
    {
        PropertyChanged += listener.Save;
    }
    
    
    public void RemoveListener<TKey>(Listener<TKey> listener)
    {
        PropertyChanged -= listener.Save;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    
}