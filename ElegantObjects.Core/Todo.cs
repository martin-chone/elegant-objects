namespace ElegantObjects.Core
{
    public class Todo
    {
        private string? title;
        private string? description;
        private bool isDone;
        private string? author;
        private DateTime? createdAt;
        private DateTime? doneAt;
        private string? idGroup;

        private Todo(TodoBuilder builder)
        {
            title = builder.GetTitle();
            description = builder.GetDescription();
            isDone = builder.GetIsDone();
            author = builder.GetAuthor();
            createdAt = builder.GetCreatedAt();
            doneAt = builder.GetDoneAt();
            idGroup = builder.GetIdGroup();
        }

        public static ITodoTitleBuilder Builder()
        {
            return new TodoBuilder();
        }

        public string? GetTitle()
        {
            return title;
        }

        public void SetTitle(string? title)
        {
            this.title = title;
        }

        public bool GetIsDone()
        {
            return isDone;
        }

        public void SetDone(bool done)
        {
            isDone = done;
        }

        public string? GetAuthor()
        {
            return author;
        }

        public void SetAuthor(string? author)
        {
            this.author = author;
        }

        public DateTime? GetCreatedAt()
        {
            return createdAt;
        }

        public void SetCreatedAt(DateTime? createdAt)
        {
            this.createdAt = createdAt;
        }

        public string? GetIdGroup()
        {
            return idGroup;
        }

        public void SetIdGroup(string? idGroup)
        {
            this.idGroup = idGroup;
        }

        public bool Equals(Object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is null || obj.GetType() != this.GetType()) return false;

            var that = (Todo)obj;
            return string.Equals(this.title, that.title) &&
                   this.isDone == that.isDone;
        }

        public DateTime? GetDoneAt()
        {
            return doneAt;
        }

        public void SetDoneAt(DateTime doneAt)
        {
            this.doneAt = doneAt;
        }

        public string? GetDescription()
        {
            return description;
        }

        public void SetDescription(string? description)
        {
            this.description = description;
        }

        public int HashCode()
        {
            return System.HashCode.Combine<string, bool>(title, isDone);
        }

        public string Tostring()
        {
            return "Todo{" +
                    "title='" + title + '\'' +
                    ", description='" + description + '\'' +
                    ", isDone=" + isDone +
                    ", author='" + author + '\'' +
                    ", createdAt=" + createdAt +
                    ", doneAt=" + doneAt +
                    ", idGroup='" + idGroup + '\'' +
                    '}';
        }

        public interface ITodoTitleBuilder
        {
            ITodoIsDoneBuilder Title(string? title);
        }

        public interface ITodoIsDoneBuilder
        {
            ITodoAuthorBuilder IsDone(bool isDone);
        }

        public interface ITodoAuthorBuilder
        {
            ITodoCreatedAtBuilder Author(string? author);
        }

        public interface ITodoCreatedAtBuilder
        {
            ITodoOptionalBuilder CreatedAt(DateTime? createdAt);
        }

        public interface ITodoOptionalBuilder
        {
            ITodoOptionalBuilder Description(string? description);
            ITodoOptionalBuilder DoneAt(DateTime? doneAt);
            ITodoOptionalBuilder IdGroup(string? idGroup);
            Todo Build();
        }

        private sealed class TodoBuilder : ITodoTitleBuilder,
                                           ITodoIsDoneBuilder,
                                           ITodoAuthorBuilder,
                                           ITodoCreatedAtBuilder,
                                           ITodoOptionalBuilder
        {

            private string? _title;
            private string? _description;
            private bool _isDone;
            private string? _author;
            private DateTime? _createdAt;
            private DateTime? _doneAt;
            private string? _idGroup;

            public string? GetTitle()
            {
                return _title;
            }

            public bool GetIsDone()
            {
                return _isDone;
            }

            public string? GetDescription()
            {
                return _description;
            }

            public string? GetAuthor()
            {
                return _author;
            }

            public DateTime? GetCreatedAt()
            {
                return _createdAt;
            }

            public DateTime? GetDoneAt()
            {
                return _doneAt;
            }

            public string? GetIdGroup()
            {
                return _idGroup;
            }

            public ITodoIsDoneBuilder Title(string? title)
            {
                _title = title;

                return this;
            }

            public ITodoAuthorBuilder IsDone(bool isDone)
            {
                _isDone = isDone;

                return this;
            }

            public ITodoCreatedAtBuilder Author(string? author)
            {
                _author = author;

                return this;
            }

            public ITodoOptionalBuilder CreatedAt(DateTime? createdAt)
            {
                _createdAt = createdAt;

                return this;
            }

            public ITodoOptionalBuilder DoneAt(DateTime? doneAt)
            {
                _doneAt = doneAt;

                return this;
            }

            public ITodoOptionalBuilder IdGroup(string? idGroup)
            {
                _idGroup = idGroup;

                return this;
            }

            public ITodoOptionalBuilder Description(string? description)
            {
                _description = description;

                return this;
            }

            public Todo Build()
            {
                return new Todo(this);
            }
        }

    }
}
