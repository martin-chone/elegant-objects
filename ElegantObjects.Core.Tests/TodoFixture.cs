namespace ElegantObjects.Core.Tests
{
    public static class TodoFixture
    {
        
        public static Todo WithTitleAndCreatedAt(string title, DateTime createdAt)
        {
            return Todo.Create(TitleFixture.From(title), AuthorFixture.JohnDoe(), createdAt);
        }
    }
}
