namespace ElegantObjects.Core
{
    public class TodoRepository
    {
        private List<Todo> _todos = new List<Todo>();

        public virtual void Save(Todo todo)
        {
            _todos.Add(todo);
        }

        public virtual List<Todo> GetAll()
        {
            return _todos;
        }

        public virtual Todo? Find(string? title)
        {
            return null;
        }

    }
}
