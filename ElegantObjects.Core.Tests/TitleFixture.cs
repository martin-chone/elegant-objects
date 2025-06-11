namespace ElegantObjects.Core.Tests
{
    public static class TitleFixture
    {
        public static Title From(String value)
        {
            return new Title(value);
        }

        public static Title Todos()
        {
            return From("todos");
        }

    }
}
