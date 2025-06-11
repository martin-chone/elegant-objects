namespace ElegantObjects.Core.Tests
{
    public static class AuthorFixture
    {
        public static Author From(string name)
        {
            return new Author(name);
        }

        public static Author JohnDoe()
        {
            return From("John Doe");
        }
    }
}
