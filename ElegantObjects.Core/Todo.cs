namespace ElegantObjects.Core
{
    public class Todo
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsDone { get; set; }
        public string? Author { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DoneAt { get; set; }
        public string? IdGroup { get; set; }

        private Todo(TodoBuilder builder)
        {
            Title = builder.Title;
            Description = builder.Description;
            IsDone = builder.IsDone;
            Author = builder.Author;
            CreatedAt = builder.CreatedAt;
            DoneAt = builder.DoneAt;
            IdGroup = builder.IdGroup;
        }

        public static ITodoTitleBuilder Builder()
        {
            return new TodoBuilder();
        }

        public bool Equals(Object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is null || obj.GetType() != this.GetType()) return false;

            var that = (Todo)obj;
            return string.Equals(this.Title, that.Title) &&
                   this.IsDone == that.IsDone;
        }

        public int HashCode()
        {
            return System.HashCode.Combine<string, bool>(Title, IsDone);
        }

        public string Tostring()
        {
            return "Todo{" +
                    "title='" + Title + '\'' +
                    ", description='" + Description + '\'' +
                    ", isDone=" + IsDone +
                    ", author='" + Author + '\'' +
                    ", createdAt=" + CreatedAt +
                    ", doneAt=" + DoneAt +
                    ", idGroup='" + IdGroup + '\'' +
                    '}';
        }

        public interface ITodoTitleBuilder
        {
            ITodoIsDoneBuilder TitleOf(string? title);
        }

        public interface ITodoIsDoneBuilder
        {
            ITodoAuthorBuilder IsDoneAs(bool isDone);
        }

        public interface ITodoAuthorBuilder
        {
            ITodoCreatedAtBuilder AuthorOf(string? author);
        }

        public interface ITodoCreatedAtBuilder
        {
            ITodoOptionalBuilder CreatedAtDate(DateTime? createdAt);
        }

        public interface ITodoOptionalBuilder
        {
            ITodoOptionalBuilder DescriptionOf(string? description);
            ITodoOptionalBuilder DoneAtDate(DateTime? doneAt);
            ITodoOptionalBuilder WithGroup(string? idGroup);
            Todo Build();
        }

        private sealed class TodoBuilder : ITodoTitleBuilder,
                                           ITodoIsDoneBuilder,
                                           ITodoAuthorBuilder,
                                           ITodoCreatedAtBuilder,
                                           ITodoOptionalBuilder
        {

            public string? Title { get; private set; }
            public string? Description { get; private set; }
            public bool IsDone { get; private set; }
            public string? Author { get; private set; }
            public DateTime? CreatedAt { get; private set; }
            public DateTime? DoneAt { get; private set; }
            public string? IdGroup { get; private set; }

            public ITodoIsDoneBuilder TitleOf(string? title)
            {
                Title = title;

                return this;
            }

            public ITodoAuthorBuilder IsDoneAs(bool isDone)
            {
                IsDone = isDone;

                return this;
            }

            public ITodoCreatedAtBuilder AuthorOf(string? author)
            {
                Author = author;

                return this;
            }

            public ITodoOptionalBuilder CreatedAtDate(DateTime? createdAt)
            {
                CreatedAt = createdAt;

                return this;
            }

            public ITodoOptionalBuilder DoneAtDate(DateTime? doneAt)
            {
                DoneAt = doneAt;

                return this;
            }

            public ITodoOptionalBuilder WithGroup(string? idGroup)
            {
                IdGroup = idGroup;

                return this;
            }

            public ITodoOptionalBuilder DescriptionOf(string? description)
            {
                Description = description;

                return this;
            }

            public Todo Build()
            {
                return new Todo(this);
            }
        }

    }
}
