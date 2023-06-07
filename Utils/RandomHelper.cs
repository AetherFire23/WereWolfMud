namespace WereWolfMud.Utils
{
    public static class RandomHelper
    {
        private static Random _random = new Random();

        public static T GetRandomFrom<T>(List<T> list)
        {
            int randomInteger = _random.Next(0, list.Count);
            T randomObject = list[randomInteger];
            return randomObject;
        }

        public static T GetRandomEnumValue<T>() where T : Enum
        {
            var values = Extensions.GetAllEnumValues<T>();
            return GetRandomFrom(values);
        }
    }
}
