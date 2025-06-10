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

        /// <summary>
        /// Finds a Todo by its title.
        /// </summary>
        /// <param name="title">The title of the Todo to search for.</param>
        /// <returns>The Todo with the given title, or <c>null</c> if not found.</returns>
        public virtual Todo? Find(Title title)
        {
            return _todos.FirstOrDefault(t => t.Title.Equals(title));
        }

    }
}
