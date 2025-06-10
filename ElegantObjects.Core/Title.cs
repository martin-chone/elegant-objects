namespace ElegantObjects.Core
{
    public record Title
    {
        public string Value { get; }

        public Title(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Title is mandatory");
            }

            Value = value;
        }
    }
}