public static class Extensions
{
    //Оборачивает экзмепляр в массив
    public static T[] WrapToArray<T>(this T source)
    {
        return new T[] { source };
    }
}