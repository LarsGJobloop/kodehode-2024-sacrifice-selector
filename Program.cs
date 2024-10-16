internal class Program
{
  // Default number of names to select if no number is provided via command line
  private static readonly int defaultNumber = 2;

  // Storage backend and user interface dependencies are static
  // This allows for changing storage or UI implementations in the future without modifying much code.
  // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/static
  private static readonly IStorage storage = new TextFileStorage("./");
  private static readonly IUserInterface ui = new TerminalUserInterface();

  // The SacrificeSelector uses storage to manage the names and selections
  private static readonly SacrificeSelector selector = new SacrificeSelector(storage);

  /// <summary>
  /// The Main entry point for the application. It handles command-line arguments, 
  /// such as selecting the number of names and adding new names.
  /// </summary>
  /// <param name="args">Command-line arguments passed to the program.</param>
  private static void Main(string[] args)
  {
    // Number of names to select defaults to 'defaultNumber', but can be overridden by a command-line argument.
    int numberOfNames = defaultNumber;

    // Parse command-line arguments to determine behavior
    foreach (var arg in args)
    {
      // Check if the argument is a number and set the number of sacrifices accordingly
      if (int.TryParse(arg, out int result))
      {
        numberOfNames = result;
        continue;
      }

      // Check for flags to add names via the command line (-a or --add-names)
      switch (arg)
      {
        case "-a":
        case "--add-names":
          AddNames();
          return; // Exit after adding names, helps with automation
      }
    }

    // If no special flags were provided, proceed with selecting names
    SelectSacrifices(numberOfNames);
  }

  /// <summary>
  /// Allows the user to add names to the list via the terminal.
  /// The user can enter names until they decide to quit by entering 'q' or 'Q'.
  /// </summary>
  private static void AddNames()
  {
    ui.Send("Please enter names for volunteers. Type (q/Q) to stop.");

    // Loop to collect names until the user types 'q' or 'Q' to stop
    while (true)
    {
      // Request a name from the user
      string newName = ui.Request<string>();

      // If the user types 'q' or 'Q', break the loop and stop adding names
      if (newName.Equals("q", StringComparison.OrdinalIgnoreCase))
      {
        break;
      }

      // Add the new name to the storage via SacrificeSelector
      selector.AddName(newName);
    }
  }

  /// <summary>
  /// Attempts to select a specified number of random "sacrifices" (names) from the list.
  /// If there are not enough names, it catches the error and prompts the user to add more.
  /// </summary>
  /// <param name="numberOfNames">The number of names to select.</param>
  private static void SelectSacrifices(int numberOfNames)
  {
    try
    {
      // Attempt to select the specified number of random names
      string[] selectedNames = selector.SelectRandomSacrifices(numberOfNames);

      // Output each selected name to the user interface (Console in this case)
      foreach (string name in selectedNames)
      {
        ui.Send(name);
      }
    }
    catch (ArgumentException ex)
    {
      // If there are not enough names to select from, inform the user and suggest adding names
      ui.Send(ex.Message);
      ui.Send("You need to add more names. Try running the program with the '-a' or '--add-names' flag to add names.");
    }
  }
}
