namespace SimpleRPG.Items
{
    public class ActionItemSlot : ItemSlot<ActionItem>
    {
        public ActionItemSlot(ActionItem item, int count)
        {
            Item = item;
            Count = count;
        }
    }
}