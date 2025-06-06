namespace ElegantObjects.Core
{
    public class TodoRepository
    {
        private List<Todo> todos = new List<Todo>();

        public virtual void Save(Todo todo)
        {
            todos.Add(todo);
        }

        public virtual List<Todo> GetAll()
        {
            return todos;
        }

        public virtual Todo? Find(String desc)
        {
            return null;
        }

    }
}
