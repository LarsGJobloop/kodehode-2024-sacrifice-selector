/// <summary>
/// A simple implementation of IStorage that persists data to a text file.
/// </summary>
public class TextFileStorage : IStorage
{
  private readonly string fullPath;

  /// <summary>
  /// Initializes a new instance of the TextFileStorage class.
  /// </summary>
  /// <param name="rootPath">The directory in which the file will be stored.</param>
  /// <param name="filePath">The name of the file (default is "file-storage.txt").</param>
  public TextFileStorage(string rootPath, string filePath = "file-storage.txt")
  {
    fullPath = Path.Combine(rootPath, filePath);
  }

  // There is a wide range of error types that might happen when working with Input/Output
  // Here we are aggregating some of the most common ones into
  // a single generic Exception type, to support the IStorage interface contract

  /// <summary>
  /// Saves the provided data to the text file.
  /// </summary>
  /// <param name="data">The data to save to the file.</param>
  /// <exception cref="Exception">Thrown if the file cannot be written to.</exception>
  public void Save(string data)
  {
    try
    {
      File.WriteAllText(fullPath, data);
    }
    catch (UnauthorizedAccessException ex)
    {
      throw new Exception($"Permission denied when trying to write to {fullPath}: {ex.Message}");
    }
    catch (IOException ex)
    {
      throw new Exception($"IO error when writing to {fullPath}: {ex.Message}");
    }
    catch (Exception ex)
    {
      // For other exceptions, preserve the original exception and stack trace
      throw new Exception($"An error occurred when writing to {fullPath}: {ex.Message}", ex);
    }
  }

  /// <summary>
  /// Loads the data from the text file.
  /// </summary>
  /// <returns>The contents of the file as a string, or null if the file does not exist.</returns>
  /// <exception cref="Exception">Thrown if the file cannot be read.</exception>
  public string? Load()
  {
    try
    {
      return File.ReadAllText(fullPath);
    }
    catch (FileNotFoundException)
    {
      // Return null if the file doesn't exist, as this is a valid state
      return null;
    }
    catch (UnauthorizedAccessException ex)
    {
      throw new Exception($"Permission denied when trying to read from {fullPath}: {ex.Message}");
    }
    catch (IOException ex)
    {
      throw new Exception($"IO error when reading from {fullPath}: {ex.Message}");
    }
    catch (Exception ex)
    {
      throw new Exception($"An error occurred when reading from {fullPath}: {ex.Message}", ex);
    }
  }
}