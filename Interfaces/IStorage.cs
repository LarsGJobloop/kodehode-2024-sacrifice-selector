/// <summary>
/// Interface for persistence operations. 
/// Provides methods for saving and loading string data to/from a storage medium (e.g., file, database, etc.).
/// </summary>
public interface IStorage
{
  /// <summary>
  /// Saves the given data string to the storage medium.
  /// </summary>
  /// <param name="data">The string data to be saved.</param>
  /// <exception cref="Exception">Thrown if unable to save to storage.</exception>
  public void Save(string data);

  /// <summary>
  /// Loads data from the storage medium.
  /// </summary>
  /// <returns>
  /// The string data loaded from the storage medium, or null if no data is found.
  /// </returns>
  /// <exception cref="Exception">Thrown if unable to load from storage.</exception>
  public string? Load();
}
