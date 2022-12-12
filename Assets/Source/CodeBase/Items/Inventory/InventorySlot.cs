using System;

namespace SimpleRPG.Items
{
    [Serializable]
    public class InventorySlot
    {
        private InventoryItem item;
        private int count;

        public InventoryItem Item
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