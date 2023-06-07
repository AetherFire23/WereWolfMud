namespace WereWolfMud.Utils
{
    public static class Extensions
    {
        public static List<T> GetAllEnumValues<T>()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Type T must be an enum");
            }

            return new List<T>(Enum.GetValues(typeof(T)) as IEnumerable<T>);
        }
    }
}
