/// <summary>
/// Manages the selection of volunteers for sacrifice and maintains the state of the application.
/// The state is persisted using the provided IStorage implementation.
/// </summary>
public class SacrificeSelector
{
  private readonly State state;
  private readonly IStorage storage;

  /// <summary>
  /// Initializes a new instance of the SacrificeSelector class, loading any saved state from storage.
  /// </summary>
  /// <param name="persistence">The storage mechanism used to persist the state.</param>
  public SacrificeSelector(IStorage persistence)
  {
    state = new State();
    storage = persistence;

    // Load initial state
    string? loadedStateString = storage.Load();
    if (loadedStateString != null)
    {
      state.FromString(loadedStateString);
    }
  }

  /// <summary>
  /// Adds a new volunteer to the list and persists the updated state.
  /// </summary>
  /// <param name="name">The name of the new volunteer.</param>
  public void AddName(string name)
  {
    state.volunteers.Add(new Person(name));
    storage.Save(state.ToString());
  }

  /// <summary>
  /// Selects a specified number of random volunteers for sacrifice and persists the updated state.
  /// </summary>
  /// <param name="numberOfNames">The number of volunteers to select for sacrifice.</param>
  /// <returns>An array of names of the selected volunteers.</returns>
  /// <exception cref="ArgumentException">Thrown when the number of volunteers requested exceeds the available number.</exception>
  public string[] SelectRandomSacrifices(int numberOfNames)
  {
    if (numberOfNames > state.volunteers.Count)
    {
      throw new ArgumentException("Too many sacrifices requested! Gather more volunteers!");
    }

    List<Person> selectedVolunteers = Utilities.RandomSlice(state.volunteers, numberOfNames);
    state.sacrificed.AddRange(selectedVolunteers);
    storage.Save(state.ToString());

    // Return the names of the lucky ones as an array
    return selectedVolunteers
      .Select(sacrifice => sacrifice.Name)
      .ToArray();
  }
}
