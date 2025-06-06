namespace ElegantObjects.Core.Tests
{
    public static class TodoFixture
    {
        public static Todo DefaultTodo()
        {
            return Todo.Builder()
                    .TitleOf("todos")
                    .IsDoneAs(false)
                    .AuthorOf("John Doe")
                    .CreatedAtDate(new DateTime(2025, 4, 23, 10, 25, 0))
                    .Build();
        }

        public static Todo WithTitle(String title)
        {
            return Todo.Builder()
                    .TitleOf(title)
                    .IsDoneAs(false)
                    .AuthorOf("John Doe")
                    .CreatedAtDate(new DateTime(2025, 4, 23, 10, 25, 0))
                    .Build();
        }

        public static Todo WithTitleAndCreatedAt(String title, DateTime createdAt)
        {
            return Todo.Builder()
                .TitleOf(title)
                .IsDoneAs(false)
                .AuthorOf("John Doe")
                .CreatedAtDate(createdAt)
                .Build();
        }

        public static Todo WithTitleAndDone(String title)
        {
            return Todo.Builder()
                    .TitleOf(title)
                    .IsDoneAs(true)
                    .AuthorOf("John Doe")
                    .CreatedAtDate(new DateTime(2025, 4, 23, 10, 25, 0))
                    .DoneAtDate(new DateTime(2025, 4, 23, 10, 25, 0))
                    .Build();
        }

        public static Todo WithGroupdId(String idGroup)
        {
            return Todo.Builder()
                    .TitleOf("todos")
                    .IsDoneAs(false)
                    .AuthorOf("John Doe")
                    .CreatedAtDate(new DateTime(2025, 4, 23, 10, 25, 0))
                    .WithGroup(idGroup)
                    .Build();
        }

    }
}
