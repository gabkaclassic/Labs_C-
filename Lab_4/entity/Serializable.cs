namespace Labs.Lab_4.entity;

public interface Serializable<T>
{
    T DeepCopy();

    bool Save(string filename);

    bool Load(string filename);

    bool AddFromConsole();
}