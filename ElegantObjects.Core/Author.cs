namespace ElegantObjects.Core
{
    public record Author
    {
        public string Name { get; }

        public Author(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Author is mandatory");
            }

            Name = name;
        }
    }
}