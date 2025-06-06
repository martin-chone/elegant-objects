namespace ElegantObjects.Core.Tests
{
    public static class TodoFixture
    {
        public static Todo DefaultTodo()
        {
            return Todo.Builder()
                    .Title("todos")
                    .IsDone(false)
                    .Author("John Doe")
                    .CreatedAt(new DateTime(2025, 4, 23, 10, 25, 0))
                    .Build();
        }

        public static Todo WithTitle(String title)
        {
            return Todo.Builder()
                    .Title(title)
                    .IsDone(false)
                    .Author("John Doe")
                    .CreatedAt(new DateTime(2025, 4, 23, 10, 25, 0))
                    .Build();
        }

        public static Todo WithTitleAndCreatedAt(String title, DateTime createdAt)
        {
            return Todo.Builder()
                .Title(title)
                .IsDone(false)
                .Author("John Doe")
                .CreatedAt(createdAt)
                .Build();
        }

        public static Todo WithTitleAndDone(String title)
        {
            return Todo.Builder()
                    .Title(title)
                    .IsDone(true)
                    .Author("John Doe")
                    .CreatedAt(new DateTime(2025, 4, 23, 10, 25, 0))
                    .DoneAt(new DateTime(2025, 4, 23, 10, 25, 0))
                    .Build();
        }

        public static Todo WithGroupdId(String idGroup)
        {
            return Todo.Builder()
                    .Title("todos")
                    .IsDone(false)
                    .Author("John Doe")
                    .CreatedAt(new DateTime(2025, 4, 23, 10, 25, 0))
                    .IdGroup(idGroup)
                    .Build();
        }

    }
}
