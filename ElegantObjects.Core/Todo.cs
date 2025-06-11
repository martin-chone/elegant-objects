namespace ElegantObjects.Core
{
    public class Todo
    {
        public Title Title { get; private set; }
        public string? Description { get; private set; }
        public bool Completed { get; private set; }
        public Author Author { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public string? IdGroup { get; private set; }

        private Todo(Title title, string? description, bool completed, Author author, DateTime? completedAt, DateTime createdAt, string? idGroup)
        {
            Title = title;
            Description = description;
            Completed = completed;
            Author = author;
            CompletedAt = completedAt;
            CreatedAt = createdAt;
            IdGroup = idGroup;
        }

        private Todo(Title title, Author author, DateTime createdAt, string idGroup)
            : this(title, null, false, author, null, createdAt, idGroup) { }

        public static Todo CreateWithGroup(Title title, Author author, string idGroup)
        {
            return new Todo(title, author, DateTime.Now, idGroup);
        }

        public Todo(Title title, Author author, DateTime createdAt)
            : this(title, null, false, author, null, createdAt, null) { }

        public static Todo Create(Title title, Author author)
        {
            return new Todo(title, author, DateTime.Now);
        }

        public void Complete(DateTime completedAt) 
        {
            Completed = true;
            CompletedAt = completedAt;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is null || obj.GetType() != this.GetType()) return false;

            var that = (Todo)obj;
            return string.Equals(Title, that.Title) &&
                   Completed == that.Completed;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine<string, bool>(Title.Value, Completed);
        }

        public override string ToString()
        {
            return "Todo{" +
                    "title='" + Title + '\'' +
                    ", description='" + Description + '\'' +
                    ", isDone=" + Completed +
                    ", author='" + Author + '\'' +
                    ", createdAt=" + CreatedAt +
                    ", doneAt=" + CompletedAt +
                    ", idGroup='" + IdGroup + '\'' +
                    '}';
        }
    }
}
