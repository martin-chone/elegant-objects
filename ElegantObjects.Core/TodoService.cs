namespace ElegantObjects.Core
{
    public class TodoService
    {
        private readonly TodoRepository todoRepository;

        public TodoService(TodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        public List<Todo> todos()
        {
            return todoRepository.getAll();
        }

        public List<Todo> add(String title, String author)
        {
            if (todoRepository.find(title) is not null)
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
        
            todoRepository.save(Todo.builder()
                    .title(title)
                    .isDone(false)
                    .author(author)
                    .createdAt(DateTime.Now)
                    .build());

            return todoRepository.getAll();
        }

        public void done(String title)
        {
            var todo = todoRepository.find(title);

            if (todo == null)
            {
                throw new InvalidOperationException($"'{title}' does not exist");
            }

            if (todo.getIsDone())
            {
                throw new InvalidOperationException($"'{title}' is already done");
            }

            todo.setDone(true);
            todoRepository.save(todo);
        }

        public List<Todo> add(String author, String title, String idGroup)
        {
            if (todoRepository.find(title) is not null)
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

            todoRepository.save(Todo.builder()
                    .title(title)
                    .isDone(false)
                    .author(author)
                    .createdAt(DateTime.Now)
                    .idGroup(idGroup)
                    .build());

            return todoRepository.getAll();
        }

    }
}
