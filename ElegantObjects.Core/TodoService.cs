namespace ElegantObjects.Core
{
    public class TodoService
    {
        private readonly TodoRepository _todoRepository;

        public TodoService(TodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public List<Todo> Todos()
        {
            return _todoRepository.GetAll();
        }

        public List<Todo> Add(string title, string author)
        {
            if (_todoRepository.Find(title) is not null)
            {
                throw new InvalidOperationException($"'{title}' already exist");
            }

            _todoRepository.Save(Todo.Builder()
                    .TitleOf(title)
                    .IsDoneAs(false)
                    .AuthorOf(author)
                    .CreatedAtDate(DateTime.Now)
                    .Build());

            return _todoRepository.GetAll();
        }

        public void Done(string title)
        {
            var todo = _todoRepository.Find(title);

            if (todo == null)
            {
                throw new InvalidOperationException($"'{title}' does not exist");
            }

            if (todo.IsDone)
            {
                throw new InvalidOperationException($"'{title}' is already done");
            }

            todo.IsDone = true;
            _todoRepository.Save(todo);
        }

        public List<Todo> Add(string author, string title, string? idGroup)
        {
            if (_todoRepository.Find(title) is not null)
            {
                throw new InvalidOperationException($"'{title}' already exists");
            }

            _todoRepository.Save(Todo.Builder()
                    .TitleOf(title)
                    .IsDoneAs(false)
                    .AuthorOf(author)
                    .CreatedAtDate(DateTime.Now)
                    .WithGroup(idGroup)
                    .Build());

            return _todoRepository.GetAll();
        }

    }
}
