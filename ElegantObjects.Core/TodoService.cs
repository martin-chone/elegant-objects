namespace ElegantObjects.Core
{
    public class TodoService
    {
        private readonly TodoRepository todoRepository;

        public TodoService(TodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        public List<Todo> Todos()
        {
            return todoRepository.GetAll();
        }

        public List<Todo> Add(String title, String author)
        {
            if (todoRepository.Find(title) is not null)
            {
                throw new InvalidOperationException($"'{title}' already exist");
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title is mandatory");
            }

            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("Author is mandatory");
            }
        
            todoRepository.Save(Todo.Builder()
                    .Title(title)
                    .IsDone(false)
                    .Author(author)
                    .CreatedAt(DateTime.Now)
                    .Build());

            return todoRepository.GetAll();
        }

        public void Done(String title)
        {
            var todo = todoRepository.Find(title);

            if (todo == null)
            {
                throw new InvalidOperationException($"'{title}' does not exist");
            }

            if (todo.GetIsDone())
            {
                throw new InvalidOperationException($"'{title}' is already done");
            }

            todo.SetDone(true);
            todoRepository.Save(todo);
        }

        public List<Todo> Add(String author, String title, String idGroup)
        {
            if (todoRepository.Find(title) is not null)
            {
                throw new InvalidOperationException($"'{title}' already exists");
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title is mandatory");
            }

            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("Author is mandatory");
            }

            todoRepository.Save(Todo.Builder()
                    .Title(title)
                    .IsDone(false)
                    .Author(author)
                    .CreatedAt(DateTime.Now)
                    .IdGroup(idGroup)
                    .Build());

            return todoRepository.GetAll();
        }

    }
}
