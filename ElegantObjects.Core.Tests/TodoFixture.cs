namespace ElegantObjects.Core.Tests
{
    public static class TodoFixture
    {
        
        public static Todo WithTitleAndCreatedAt(string title, DateTime createdAt)
        {
            return new Todo(TitleFixture.From(title), AuthorFixture.JohnDoe(), createdAt);
        }
    }
}
