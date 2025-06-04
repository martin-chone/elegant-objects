namespace ElegantObjects.Core
{
    public class TodoRepository
    {
        private List<Todo> todos = new List<Todo>();

        public virtual void save(Todo todo)
        {
            todos.Add(todo);
        }

        public virtual List<Todo> getAll()
        {
            return todos;
        }

        public virtual Todo? find(String desc)
        {
            return null;
        }

    }
}
