namespace Labs.Lab_4.@event;

public class MagazinesChangedEventArgs<TKey> : EventArgs
{

    public string TitleCollection { get; set; }

    public Update Type { get; set; }

    public string TitleProperty { get; set; }

    public TKey Key { get; set; }

    public MagazinesChangedEventArgs(string titleCollection, Update type, string titleProperty, TKey key)
    {
        TitleCollection = titleCollection;
        Type = type;
        TitleProperty = titleProperty;
        Key = key;
    }

    public override string ToString()
    {
        return $"MagazinesChangedEventArgs: [ title of collection: {TitleCollection}, type : {Type}, title of property: {TitleProperty}, key: {Key}]";
    }
}