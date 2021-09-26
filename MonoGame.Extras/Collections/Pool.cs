namespace MonoGame.Extras.Collections
{
    using System;

    public class Pool<T>
        where T : class
    {
        private readonly Func<T> createItem;
        private readonly Action<T> resetItem;
        private readonly Deque<T> freeItems;
        private readonly int maximum;

        public Pool(Func<T> createItem, Action<T> resetItem, int capacity = 16, int maximum = int.MaxValue)
        {
            this.createItem = createItem;
            this.resetItem = resetItem;
            this.maximum = maximum;
            freeItems = new Deque<T>(capacity);
        }

        public Pool(Func<T> createItem, int capacity = 16, int maximum = int.MaxValue)
            : this(createItem, _ => { }, capacity, maximum)
        {
        }

        public int AvailableCount => freeItems.Count;

        public T Obtain()
        {
            if (freeItems.Count > 0)
            {
                return freeItems.Pop();
            }

            return createItem();
        }

        public void Free(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (freeItems.Count < maximum)
            {
                freeItems.AddToBack(item);
            }

            resetItem(item);
        }

        public void Clear()
        {
            freeItems.Clear();
        }
    }
}
