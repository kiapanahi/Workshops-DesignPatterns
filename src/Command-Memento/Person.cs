namespace CommandMemento
{
    public class Person
    {
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Name => $"{FirstName} {LastName}";
        public string Email => $"{FirstName.ToLowerInvariant()}@fanap.plus";
    }
}