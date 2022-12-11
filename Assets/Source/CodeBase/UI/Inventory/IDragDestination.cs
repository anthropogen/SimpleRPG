namespace SimpleRPG.UI
{
    public interface IDragDestination<T>
    {
        int MaxAcceptable(T item);
        void AddItem(T item, int count);
    }
}