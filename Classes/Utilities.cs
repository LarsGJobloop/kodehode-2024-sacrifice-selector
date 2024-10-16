public class Utilities
{
  /// <summary>
  /// Randomly selects a specified number of items from the provided list.
  /// </summary>
  /// <typeparam name="T">The type of items in the list.</typeparam>
  /// <param name="list">The list from which to select items.</param>
  /// <param name="numberOfItems">The number of items to select.</param>
  /// <returns>A list of randomly selected items.</returns>
  /// <exception cref="ArgumentException">Thrown if the number of items requested exceeds the size of the list.</exception>
  public static List<T> RandomSlice<T>(List<T> list, int numberOfItems)
  {
    if (numberOfItems > list.Count)
    {
      throw new ArgumentException("Cannot select more items than available in the list.", nameof(numberOfItems));
    }

    // Use a HashSet to store unique random indices
    // https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1?view=net-8.0
    HashSet<int> selectedIndexes = new HashSet<int>(numberOfItems);

    // Random is an simple Pseudo Random Number Generator (PRNG)
    // https://learn.microsoft.com/en-us/dotnet/api/system.random?view=net-8.0
    var random = new Random();

    // Select unique random indexes
    while (selectedIndexes.Count < numberOfItems)
    {
      int newIndex = random.Next(list.Count);
      selectedIndexes.Add(newIndex);
    }

    // Use the selected indexes to build the result list
    List<T> selectedItems = new List<T>(numberOfItems);
    foreach (int index in selectedIndexes)
    {
      selectedItems.Add(list[index]);
    }

    return selectedItems;
  }
}