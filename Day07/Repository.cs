using System;

namespace ExamSystem.Generics
{

    public class Repository<T> where T : ICloneable, IComparable<T>
    {
        private T[] _items;
        private int _count;
        private const int DefaultCapacity = 8;

        public int Count => _count;

        public Repository()
        {
            _items = new T[DefaultCapacity];
        }

        public void Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (_count == _items.Length) Resize();
            _items[_count++] = item;
        }

        public bool Remove(T item)
        {
            if (item == null) return false;
            for (int i = 0; i < _count; i++)
            {
                if (_items[i].CompareTo(item) == 0)
                {
                    // Shift left
                    for (int j = i; j < _count - 1; j++)
                        _items[j] = _items[j + 1];
                    _items[--_count] = default!;
                    return true;
                }
            }
            return false;
        }

        public void Sort()
        {
            // Insertion sort (works well for small arrays and preserves T[])
            for (int i = 1; i < _count; i++)
            {
                T key = _items[i];
                int j = i - 1;
                while (j >= 0 && _items[j].CompareTo(key) > 0)
                {
                    _items[j + 1] = _items[j];
                    j--;
                }
                _items[j + 1] = key;
            }
        }

        public T[] GetAll()
        {
            T[] result = new T[_count];
            Array.Copy(_items, result, _count);
            return result;
        }

        private void Resize()
        {
            T[] bigger = new T[_items.Length * 2];
            Array.Copy(_items, bigger, _count);
            _items = bigger;
        }
    }
}
