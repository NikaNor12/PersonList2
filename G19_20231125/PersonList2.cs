using System.Collections;

namespace G19_20231125
{
    public class PersonList2 : List<Person>, IList<Person>
    {
        private List<Person> _list;
        private HashSet<int> _ids;

        string[] _items = new string?[5];
        public PersonList2()
        {
            _list = new List<Person>();
            _ids = new HashSet<int>();
        }

        public void Add(Person item)
        {
            if (!_ids.Add(item.Id))
            {
                throw new ArgumentException("Id is not unique");
            }

            _list.Add(item);
        }

        public void Save(string filePath)
        {
            using (BinaryWriter writer = new(new FileStream(filePath, FileMode.Create)))
            {
                foreach (var person in this)
                {
                    writer.Write(person.Id);
                    writer.Write(person.Firstname);
                    writer.Write(person.Lastname);
                    writer.Write(person.BirthDate.ToBinary());
                    writer.Write((byte)person.Gender);
                }
            }
        }

        public new void Load(string filePath)
        {
            this.Clear();
            this._ids.Clear();
            using (BinaryReader reader = new(new FileStream(filePath, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    Person person = new Person
                    {
                        Id = reader.ReadInt32(),
                        Firstname = reader.ReadString(),
                        Lastname = reader.ReadString(),
                        BirthDate = DateTime.FromBinary(reader.ReadInt64()),
                        Gender = (GenderType)reader.ReadByte()
                    };

                    this.Add(person);
                }
            }
        }

        public Person this[int index]
        {
            get
            {
                return _list[index];
            }
            set
            {
                _list[index] = value;
            }
        }

        public int Count => _list.Count;
        public bool IsReadOnly => throw new NotImplementedException();

        public void Clear()
        {
            _list = null;
            _ids = null;
        }

        public bool Contains(Person item)
        {
            HashSet<int> ids = _ids;

            while (ids.Contains(item.Id))
            {
                return true;
            }

            return false;
        }

        public void CopyTo(Person[] array, int arrayIndex)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                array.SetValue(_list[i], arrayIndex++);
            }
        }

        public IEnumerator<Person> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public int IndexOf(Person item) //mushaobs
        {
            return IndexOf(item, 0);
        }
        public int IndexOf(Person value, int startIndex) //mushaobs
        {
            for (int i = startIndex; i < _list.Count; i++)
            {
                if (_list[i].Equals(value))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, Person item)
        {
            foreach (var person in this)
            {
                if (person.Id == item.Id)
                {
                    throw new ArgumentException("Id is not unique");
                }
            }

            for (int i = _list.Count - 1; i > index; i--)
            {
                _list[i] = _list[i - 1];
            }

            _list[index] = item;
        }

        public void InsertRange(int index, Person[] items)
        {
            foreach (var item in items)
            {
                if (_ids.Contains(item.Id))
                {
                    throw new ArgumentException("Id is not unique");
                }

                _list.Insert(index++, item);
            }
        }

        public bool Remove(Person item)
        {
            int index = IndexOf(item);

            if (index != -1)
            {
                _list.RemoveAt(index);
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            for (int i = index; i < _list.Count - 1; i++)
            {
                _list[i] = _list[i + 1];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MyListEnumerator(_items);
        }
        public class MyListEnumerator : IEnumerator
        {
            private object[] _items;
            private int _index;
            public MyListEnumerator(object[] items)
            {
                _items = items;
                _index = -1;
            }

            public bool MoveNext()
            {
                if (_index + 1 < _items.Length)
                {
                    _index++;
                    return true;
                }
                return false;
            }
            public void Reset()
            {
                _index = -1;
            }
            public object Current
            {
                get { return _items[_index]; }
            }
        }
    }
}
