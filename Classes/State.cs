public class State
{
  public List<Person> volunteers;
  public List<Person> sacrificed;

  private char listSeperator = '\n';
  private char entrySeperator = ',';

  public State()
  {
    volunteers = new List<Person>();
    sacrificed = new List<Person>();
  }

  // The two following methods are for translating the current state
  // to and from another format. It's usually refered to as
  // Serialization and Deserialization or
  // Marshalling and Unmarshalling
  //
  // We are using a simple string format here, since the Person object
  // can be recunstructed just by it's name, for more complex
  // statefull objects a more advanced tactic would be required.
  //
  // Most of the time you would be using a Serialization library
  // for this rather then reinvent the "wheel".
  // https://learn.microsoft.com/en-us/dotnet/standard/serialization/

  /// <summary>
  /// Converts the state to a string format for saving.
  /// </summary>
  /// <returns>A string representing the state of volunteers and sacrifices.</returns>
  public override string ToString()
  {
    // Use ternary operator to conditionally assign a value based on a boolean
    // Useful when you want a default value
    // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator
    string volunteersString = volunteers.Count > 0
      ? string.Join(entrySeperator, volunteers.Select(volunteer => volunteer.Name))
      : string.Empty;

    string sacrificedString = sacrificed.Count > 0
      ? string.Join(entrySeperator, sacrificed.Select(volunteer => volunteer.Name))
      : string.Empty;

    return $"{volunteersString}{listSeperator}{sacrificedString}";
  }

  /// <summary>
  /// Restores the state from a string representation.
  /// </summary>
  /// <param name="raw">The raw string to deserialize into state.</param>
  public void FromString(string raw)
  {
    var listStrings = raw.Split(listSeperator);

    // We are rebuilding the state, so clear out the old one
    volunteers.Clear();
    sacrificed.Clear();

    if (string.IsNullOrWhiteSpace(raw))
    {
      return;
    }

    // There might be no stored data, hence the conditionals
    if (listStrings.Length > 0 && !string.IsNullOrWhiteSpace(listStrings[0]))
    {
      foreach (var volunteer in listStrings[0].Split(entrySeperator))
      {
        var trimmedName = volunteer.Trim();
        if (!string.IsNullOrEmpty(trimmedName))
        {
          volunteers.Add(new Person(trimmedName));
        }
      }
    }

    if (listStrings.Length > 1 && !string.IsNullOrWhiteSpace(listStrings[1]))
    {
      foreach (var sacrificedPerson in listStrings[1].Split(entrySeperator))
      {
        var trimmedName = sacrificedPerson.Trim();
        if (!string.IsNullOrEmpty(trimmedName))
        {
          sacrificed.Add(new Person(trimmedName));
        }
      }
    }
  }
}
