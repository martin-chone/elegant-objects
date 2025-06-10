namespace ElegantObjects.Core.Tests
{
    public static class TodoFixture
    {

        private static Title _title = new Title("todos");
        private static Author _author = new Author("John Doe");

        public static Todo DefaultTodo()
        {
            return Todo.Builder()
                    .TitleOf(_title)
                    .IsDoneAs(false)
                    .AuthorOf(_author)
                    .CreatedAtDate(new DateTime(2025, 4, 23, 10, 25, 0))
                    .Build();
        }

        public static Todo WithTitle(Title title)
        {
            return Todo.Builder()
                    .TitleOf(title)
                    .IsDoneAs(false)
                    .AuthorOf(_author)
                    .CreatedAtDate(new DateTime(2025, 4, 23, 10, 25, 0))
                    .Build();
        }

        public static Todo WithTitleAndCreatedAt(Title title, DateTime createdAt)
        {
            return Todo.Builder()
                .TitleOf(title)
                .IsDoneAs(false)
                .AuthorOf(_author)
                .CreatedAtDate(createdAt)
                .Build();
        }

        public static Todo WithTitleAndDone(Title title)
        {
            return Todo.Builder()
                    .TitleOf(title)
                    .IsDoneAs(true)
                    .AuthorOf(_author)
                    .CreatedAtDate(new DateTime(2025, 4, 23, 10, 25, 0))
                    .DoneAtDate(new DateTime(2025, 4, 23, 10, 25, 0))
                    .Build();
        }

        public static Todo WithGroupdId(string? idGroup)
        {
            return Todo.Builder()
                    .TitleOf(_title)
                    .IsDoneAs(false)
                    .AuthorOf(_author)
                    .CreatedAtDate(new DateTime(2025, 4, 23, 10, 25, 0))
                    .WithGroup(idGroup)
                    .Build();
        }

    }
}
