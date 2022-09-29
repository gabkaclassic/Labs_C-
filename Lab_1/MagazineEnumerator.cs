using System.Collections;

namespace Lab_1;

public class MagazineEnumerator : IEnumerator
{

    private readonly ArrayList _list;
    private int _index;
    private readonly IEnumerable<Person> _authors;

    public MagazineEnumerator(ICollection list, IEnumerable<Person> authors)
    {
        _list = new ArrayList();
        _list.AddRange(list);
        _index = 0;
        _authors = authors;
    }

    public bool MoveNext() => _index < _list.Count && CheckPerson(_index);

    private bool CheckPerson(int i) => !_authors.Contains((Person)_list[i]!);
    
    public void Reset()
    {
        _index = 0;
    }

    public object Current
    {
        get
        {
            for(var i = _index; i < _list.Count; i++)
                if (CheckPerson(i))
                {
                    _index = i;
                    break;
                }

            return _list[_index++]!;
        }
    }


    public void Dispose()
    {
        throw new NotImplementedException();
    }
}