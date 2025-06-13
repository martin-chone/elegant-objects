namespace ElegantObjects.Core
{
    public class Todo
    {
        public Title Title { get; }
        public string? Description { get; }
        public bool Completed { get; }
        public Author Author { get; }
        public DateTime CreatedAt { get; }
        public DateTime? CompletedAt { get; }
        public string? IdGroup { get; }

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

        public static Todo CreateWithGroup(Title title, Author author, DateTime createdAt, string idGroup)
        {
            return new Todo(title, null, false, author, null, createdAt, idGroup);
        }

        public static Todo Create(Title title, Author author, DateTime createdAt)
        {
            return new Todo(title, null, false, author, null, createdAt, null);
        }

        public Todo Complete(DateTime completedAt) 
        {
            return new Todo(Title, Description, true, Author, completedAt, CreatedAt, IdGroup);
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
            return $"{Title.Value} - {(Completed? "Done" : "Pending")} - {Author}";
        }
    }
}
