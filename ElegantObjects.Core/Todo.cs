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
            title = builder.getTitle();
            description = builder.getDescription();
            isDone = builder.getIsDone();
            author = builder.getAuthor();
            createdAt = builder.getCreatedAt();
            doneAt = builder.getDoneAt();
            idGroup = builder.getIdGroup();
        }

        public static ITodoTitleBuilder builder()
        {
            return new TodoBuilder();
        }

        public string? getTitle()
        {
            return title;
        }

        public void setTitle(string? title)
        {
            this.title = title;
        }

        public bool getIsDone()
        {
            return isDone;
        }

        public void setDone(bool done)
        {
            isDone = done;
        }

        public string? getAuthor()
        {
            return author;
        }

        public void setAuthor(string? author)
        {
            this.author = author;
        }

        public DateTime? getCreatedAt()
        {
            return createdAt;
        }

        public void setCreatedAt(DateTime? createdAt)
        {
            this.createdAt = createdAt;
        }

        public string? getIdGroup()
        {
            return idGroup;
        }

        public void setIdGroup(string? idGroup)
        {
            this.idGroup = idGroup;
        }

        public bool equals(Object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is null || obj.GetType() != this.GetType()) return false;

            var that = (Todo)obj;
            return string.Equals(this.title, that.title) &&
                   this.isDone == that.isDone;
        }

        public DateTime? getDoneAt()
        {
            return doneAt;
        }

        public void setDoneAt(DateTime doneAt)
        {
            this.doneAt = doneAt;
        }

        public string? getDescription()
        {
            return description;
        }

        public void setDescription(string? description)
        {
            this.description = description;
        }

        public int hashCode()
        {
            return HashCode.Combine(title, isDone);
        }

        public string tostring()
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
            ITodoIsDoneBuilder title(string? title);
        }

        public interface ITodoIsDoneBuilder
        {
            ITodoAuthorBuilder isDone(bool isDone);
        }

        public interface ITodoAuthorBuilder
        {
            ITodoCreatedAtBuilder author(string? author);
        }

        public interface ITodoCreatedAtBuilder
        {
            ITodoOptionalBuilder createdAt(DateTime? createdAt);
        }

        public interface ITodoOptionalBuilder
        {
            ITodoOptionalBuilder description(string? description);
            ITodoOptionalBuilder doneAt(DateTime? doneAt);
            ITodoOptionalBuilder idGroup(string? idGroup);
            Todo build();
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

            public string? getTitle()
            {
                return _title;
            }

            public bool getIsDone()
            {
                return _isDone;
            }

            public string? getDescription()
            {
                return _description;
            }

            public string? getAuthor()
            {
                return _author;
            }

            public DateTime? getCreatedAt()
            {
                return _createdAt;
            }

            public DateTime? getDoneAt()
            {
                return _doneAt;
            }

            public string? getIdGroup()
            {
                return _idGroup;
            }

            public ITodoIsDoneBuilder title(string? title)
            {
                _title = title;

                return this;
            }

            public ITodoAuthorBuilder isDone(bool isDone)
            {
                _isDone = isDone;

                return this;
            }

            public ITodoCreatedAtBuilder author(string? author)
            {
                _author = author;

                return this;
            }

            public ITodoOptionalBuilder createdAt(DateTime? createdAt)
            {
                _createdAt = createdAt;

                return this;
            }

            public ITodoOptionalBuilder doneAt(DateTime? doneAt)
            {
                _doneAt = doneAt;

                return this;
            }

            public ITodoOptionalBuilder idGroup(string? idGroup)
            {
                _idGroup = idGroup;

                return this;
            }

            public ITodoOptionalBuilder description(string? description)
            {
                _description = description;

                return this;
            }

            public Todo build()
            {
                return new Todo(this);
            }
        }

    }
}
