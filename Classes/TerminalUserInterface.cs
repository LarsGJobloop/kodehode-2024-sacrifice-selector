// This class makes use of some more advanced concepts to allow us
// to write a single method for the Request by implementing it as a
// generic method, a method where the exact Type is unkown, and
// we pass it in as an argument when calling the function
//
// A simpler alternative would be to create multiple different
// methods like:
// - string RequestString()
// - string RequestInt()
// - string RequestBool()
// - string RequestEnum()
// - ...
//
// This would work, but would also be very verbose
// Generic Methods allows us to create generalized
// versions of them instead. We still have to
// supply the logic though
// 
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics

public class TerminalUserInterface : IUserInterface
{
  public T Request<T>()
  {
    do
    {
      string? inputRaw = Console.ReadLine();

      if (inputRaw == null)
      {
        Console.WriteLine("No input detected, please try again");
        continue;
      }

      if (string.IsNullOrWhiteSpace(inputRaw))
      {
        Console.WriteLine("Input can't be empty or only whitespace, please try again");
        continue;
      }

      try
      {
        T input = Parse<T>(inputRaw);
        return input;
      }
      catch (FormatException)
      {
        Console.WriteLine($"Invalid input, please try again!");
        continue;
      }
    } while (true);
  }

  public void Send(string message)
  {
    Console.WriteLine(message);
  }

  // Trying to create a generic parser is verbose
  // We have split out the logic here so we only have
  // to think about going from an input string
  // to an output type and value.
  //
  // If you ever need to do this manually, do yourself a favor
  // and search online after solutions for the various types.

  /// <summary>
  /// Parses the input string into the specified type T.
  /// Handles special cases for booleans and enums, while using Convert.ChangeType for general primitive types.
  /// Throws a FormatException if the conversion fails.
  /// </summary>
  /// <param name="input">The raw input string from the user.</param>
  /// <typeparam name="T">The target type to convert the input to.</typeparam>
  /// <returns>The converted value of type T.</returns>
  /// <exception cref="FormatException">Thrown when the input cannot be converted to the specified type.</exception>
  private T Parse<T>(string input)
  {
    Type targetType = typeof(T);

    // Special handling for boolean values
    if (targetType == typeof(bool))
    {
      if (bool.TryParse(input, out bool boolResult))
      {
        return (T)(object)boolResult;
      }
      throw new FormatException("Invalid boolean value.");
    }

    // Special handling for enums
    if (targetType.IsEnum)
    {
      if (Enum.TryParse(targetType, input, true, out var enumResult))
      {
        return (T)enumResult;
      }

      // Provide valid options to the user
      var validValues = string.Join(", ", Enum.GetNames(targetType));
      throw new FormatException($"Invalid value for enum type {typeof(T).Name}. Valid options are: {validValues}.");
    }

    // Handle numeric and other primitive types (e.g., int, double)
    if (targetType == typeof(int))
    {
      if (int.TryParse(input, out int intResult))
      {
        return (T)(object)intResult;
      }
      throw new FormatException("Invalid integer value. Please enter a valid number.");
    }

    if (targetType == typeof(double))
    {
      if (double.TryParse(input, out double doubleResult))
      {
        return (T)(object)doubleResult;
      }
      throw new FormatException("Invalid double value. Please enter a valid number.");
    }

    // Fallback to Convert.ChangeType for other types (e.g., decimal, string)
    try
    {
      return (T)Convert.ChangeType(input, targetType);
    }
    catch (InvalidCastException)
    {
      throw new FormatException($"Cannot convert input to type {targetType.Name}.");
    }
  }
}