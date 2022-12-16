using System;

namespace SimpleRPG.Items
{
    public class ItemSlot<T> where T : InventoryItem
    {
        private T item;
        private int count;
        public ItemSlot()
        {
        }
        public ItemSlot(T item, int count)
        {
            this.item = item;
            this.count = count;
        }

        public T Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
                if (item == null)
                    count = 0;
            }
        }

        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                if (value < 0) throw new InvalidOperationException("Items count can't be less zero");
                count = value;
                if (count == 0)
                    item = null;
            }
        }
    }
}