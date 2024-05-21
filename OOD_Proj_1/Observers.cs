namespace OOD_Proj_1
{
    public interface IObserver
    {
        public bool FindAndUpdateID(ulong From, ulong To);
    }
    public class Observer<T> : IObserver where T : Product
    {
        public Dictionary<ulong, T> TDict;
        public Observer(Dictionary<ulong, T> dict)
        {
            TDict = dict;
        }

        public bool FindAndUpdateID(ulong From, ulong To)
        {
            return TDict.UpdateKeyAndID(From, To);
        }
    }
}
