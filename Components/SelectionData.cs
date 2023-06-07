namespace WereWolfMud.Components
{
    public class SelectionData<T>
    {
        public T Data { get; set; }
        public bool IsSelected { get; set; }
        public SelectionData(T data)
        {
            Data = data;
        }


        public bool AreEqual(SelectionData<T> otherData)
        {
            return EqualityComparer<T>.Default.Equals(Data, otherData.Data);
        }

        public bool AreEqual(T otherData)
        {
            return EqualityComparer<T>.Default.Equals(Data, otherData);
        }
    }
}
