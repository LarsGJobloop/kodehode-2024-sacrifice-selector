/// <summary>
/// Interface for handling user input and output operations. 
/// Used to abstract the communication with the user, allowing different implementations (e.g., terminal, GUI, etc.).
/// </summary>
public interface IUserInterface
{
  /// <summary>
  /// Requests input from the user of the specified type.
  /// </summary>
  /// <typeparam name="T">The type of input expected from the user.</typeparam>
  /// <returns>
  /// The input provided by the user, cast to the specified type T.
  /// </returns>
  public T Request<T>();

  /// <summary>
  /// Sends a message to the user.
  /// </summary>
  /// <param name="message">The message string to be displayed to the user.</param>
  public void Send(string message);
}