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

        public List<Todo> Add(Title title, Author author)
        {
            if (_todoRepository.Find(title) is not null)
            {
                throw new InvalidOperationException($"'{title.Value}' already exist");
            }

            var todo = Todo.Create(title, author);
            _todoRepository.Save(todo);

            return _todoRepository.GetAll();
        }

        public void Done(Title title)
        {
            var todo = _todoRepository.Find(title);

            if (todo == null)
            {
                throw new InvalidOperationException($"'{title.Value}' does not exist");
            }

            if (todo.Completed)
            {
                throw new InvalidOperationException($"'{title.Value}' is already done");
            }

            todo.Complete(DateTime.Now);

            _todoRepository.Save(todo);
        }

        public List<Todo> Add(Author author, Title title, string idGroup)
        {
            if (_todoRepository.Find(title) is not null)
            {
                throw new InvalidOperationException($"'{title.Value}' already exists");
            }

            var todo = Todo.CreateWithGroup(title, author, idGroup);
            _todoRepository.Save(todo);

            return _todoRepository.GetAll();
        }

    }
}
