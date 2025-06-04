namespace ElegantObjects.Core.Tests
{
    public static class TodoFixture
    {
        public static Todo defaultTodo()
        {
            return Todo.builder()
                    .title("todos")
                    .isDone(false)
                    .author("John Doe")
                    .createdAt(new DateTime(2025, 4, 23, 10, 25, 0))
                    .build();
        }

        public static Todo withTitle(String title)
        {
            return Todo.builder()
                    .title(title)
                    .isDone(false)
                    .author("John Doe")
                    .createdAt(new DateTime(2025, 4, 23, 10, 25, 0))
                    .build();
        }

        public static Todo withTitleAndCreatedAt(String title, DateTime createdAt)
        {
            return Todo.builder().title(title).isDone(false).author("John Doe").createdAt(createdAt).build();
        }

        public static Todo withTitleAndDone(String title)
        {
            return Todo.builder()
                    .title(title)
                    .isDone(true)
                    .author("John Doe")
                    .createdAt(new DateTime(2025, 4, 23, 10, 25, 0))
                    .doneAt(new DateTime(2025, 4, 23, 10, 25, 0))
                    .build();
        }

        public static Todo withGroupdId(String idGroup)
        {
            return Todo.builder()
                    .title("todos")
                    .isDone(false)
                    .author("John Doe")
                    .createdAt(new DateTime(2025, 4, 23, 10, 25, 0))
                    .idGroup(idGroup)
                    .build();
        }

    }
}
