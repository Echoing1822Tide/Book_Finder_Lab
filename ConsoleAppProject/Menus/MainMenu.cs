using ConsoleHelpers;

namespace ConsoleAppProject.Menus;

public class MainMenu
{
    //private variables for various menus

    


    //TODO: Inject Menu Dependencies
    public MainMenu()
    {
    }

    private void ShowFormattedMessages()
    {
        var prompt = "What is your name?";
        var response = InputHelpers.GetInputAsString(prompt);
        Console.WriteLine(OutputHelpers.BoxedMessage(response, '*'));
        Console.WriteLine(OutputHelpers.BoxedMessage(response, '-'));
        string tooLong = "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf";
        Console.WriteLine(OutputHelpers.BoxedMessage(tooLong, '-'));
        Console.WriteLine(new string('~', 40));

        Console.WriteLine(OutputHelpers.BoxedMessageWithTitle("Welcome", response));
    }

    private void ShowInputHelpers()
    {
        string prompt = "Please enter your favorite constant";
        double min = 0;
        double max = 100;
        
        double result = InputHelpers.GetInputAsDouble(prompt, min, max, true);
        Console.WriteLine($"You entered {result}");

        prompt = "Guess a number between 1 and 7";
        int intMin = 1;
        int intMax = 7;
        int intResult = InputHelpers.GetInputAsInt(prompt, intMin, intMax, true);
        Console.WriteLine($"You guessed {intResult}");

        prompt = "Would you like to continue?";
        bool boolResult = InputHelpers.GetInputAsBool(prompt, true);
        Console.WriteLine($"continue: {(boolResult? "yes" : "no")}");

        var prompt2 = "IS C# the best language?";
        bool boolResult2 = InputHelpers.GetInputAsBool(prompt2);
        Console.WriteLine($"give up: {(boolResult2? "yes" : "no")}");

        // String method
        var prompt3 = "What is your name?";
        string stringResult = InputHelpers.GetInputAsString(prompt3);
        Console.WriteLine($"Hello {stringResult}!");

        var prompt4 = "Enter your name again";
        string stringResult2 = InputHelpers.GetInputAsString(prompt4, true);
        Console.WriteLine($"Hello again {stringResult2}!");
    }

    public async Task ShowAsync()
    {
        bool shouldContinue = true;
        while (shouldContinue)
        {
            Console.Clear();

            string[] menuOptions = GetMenuOptions();

            var menuText = MenuGenerator.GenerateMenu("Main Menu", "Please select an operation", menuOptions, 40);

            // Show menu and get user choice
            int choice = InputHelpers.GetInputAsInt(menuText, confirm: true, min: 1, max: menuOptions.Length);

            try
            {
                shouldContinue = await HandleMenuChoiceAsync(choice);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }

    //handle user choice
    private async Task<bool> HandleMenuChoiceAsync(int choice)
    {
        switch (choice)
        {
            case 1:
                ShowFormattedMessages();
                break;
            case 2:
                ShowInputHelpers();
                break;
            default:
                return false;
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        return true;
    }

    //get menu options
    private string[] GetMenuOptions()
    {
        return new string[] {
            "Show formatted Messages",
            "Show Input Helpers",
            "Exit"
        };
    }
}
