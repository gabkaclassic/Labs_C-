using System.ComponentModel;
using System.Text;
using Labs.Lab_4.entity;

namespace Labs.Lab_4.@event;

public class Listener<TKey>
{

    private readonly List<ListEntry> _listEntry;
    public Listener()
    {
        _listEntry = new List<ListEntry>();
    }

    public void Save(object source, MagazinesChangedEventArgs<TKey> args)
    {
        var entry = new ListEntry(args.TitleCollection, args.Type, args.TitleProperty);
        _listEntry.Add(entry);
    }

    public void Save(object? sender, PropertyChangedEventArgs e)
    {
        var edition = sender as Edition;
        var entry = new ListEntry(edition.Title, Update.Property, e.PropertyName);
        _listEntry.Add(entry);
    }
    public override string ToString()
    {
        var result = new StringBuilder($"Listener<{typeof(TKey)}>:\n");

        foreach (var entry in _listEntry)
            result.Append(entry).Append('\n');

        return result.ToString();
    }

    private class ListEntry
    {
        private string TitleCollection { get; set; }

        private Update Type { get; set; }

        private string TitleProperty { get; set; }

        public ListEntry(string titleCollection, Update type, string titleProperty)
        {
            TitleCollection = titleCollection;
            Type = type;
            TitleProperty = titleProperty;
        }

        public override string ToString()
        {
            return $"MagazinesChangedEventArgs: [ title of collection: {TitleCollection}, type : {Type}, title of property: {TitleProperty}]";
        }
    }
}
