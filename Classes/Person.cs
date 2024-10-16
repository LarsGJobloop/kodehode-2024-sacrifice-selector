public class Person
{
  // Making this a property allow us to make it
  // immutable after it's been created
  // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties
  public string Name { get; private set; }

  public Person(string name)
  {
    if (string.IsNullOrWhiteSpace(name))
    {
      throw new ArgumentException("Name cannot be null or empty", nameof(name));
    }
    Name = name;
  }

  public override string ToString()
  {
    return Name;
  }
}
