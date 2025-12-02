using System.Text;

namespace ConsoleHelpers;

public class OutputHelpers
{
    //Format a string to in a box
    //*******************************
    //* SOME STRING HERE            *
    //*******************************
    //or
    //-------------------------------
    //| SOME STRING HERE            |
    //-------------------------------
    public static string BoxedMessage(string message, char borderChar, int lineLength = 80)
    {
        string separatorString = new string(borderChar, lineLength);

        StringBuilder sb = new StringBuilder();
        //border top
        sb.AppendLine(separatorString);

        //left indicator 
        var borderIndicator = borderChar == '*' ? "*" : "|";
        sb.Append(borderIndicator);
        //message + spaces? 
        var actualDataString = $" {message}".PadRight(lineLength - 2, ' ');
        sb.Append($"{actualDataString}");
        //right indicator
        sb.AppendLine(borderIndicator);
        //border bottom
        sb.AppendLine(separatorString);
        //return the formatted message;
        return sb.ToString();
    }
    
    //output a string to the console in a box with a header
    //*******************************
    //* Header goes here            *
    //*******************************
    //*-----------------------------*
    //* SOME STRING HERE            *
    //*-----------------------------*
    
    public static string BoxedMessageWithTitle(string title, string message, int lineLength=80)
    {
        string separatorDashes = $"*{new string('-', lineLength - 2)}*";
        string separatorStars = new string('*', lineLength);
        
        StringBuilder sb = new StringBuilder();

        sb.Append(BoxedMessage(title, '*'));
        
        //message top
        sb.AppendLine(separatorDashes);
        //message indicator + message + spaces + message indicator
        var actualDataString = $"* {message}".PadRight(lineLength - 1, ' ');
        sb.AppendLine($"{actualDataString}*");
        //message bottom
        sb.AppendLine(separatorDashes);
        sb.AppendLine(separatorStars);

        return sb.ToString();
    }

    //output a string to the console in a box with a header
    //*******************************
    //* Header goes here            *
    //*******************************
    //*-----------------------------*
    //* SOME STRING HERE            *
    //*-----------------------------*
    //*-----------------------------*
    //* SOME STRING HERE            *
    //*-----------------------------*
    //*-----------------------------*
    //* SOME STRING HERE            *
    //*-----------------------------*
    //*-----------------------------*
    //* SOME STRING HERE            *
    //*-----------------------------*
    //*******************************
    public static string BoxedArrayWithTitle(string title, string[] items, int lineLength=80)
    {
        string separatorDashes = $"*{new string('-', lineLength - 2)}*";
        string separatorStars = new string('*', lineLength);
        
        StringBuilder sb = new StringBuilder();

        sb.Append(BoxedMessage(title, '*'));
        
        //** each item needs to print here
        int i = 0; 
        foreach(var item in items)
        {
            i++;
            //message top
            //sb.AppendLine(separatorDashes);
            //message indicator + message + spaces + message indicator
            var actualDataString = $"* {item}".PadRight(lineLength - 1, ' ');
            sb.AppendLine($"{actualDataString}*");
            //message bottom
            if (i < items.Length)
            {
                sb.AppendLine(separatorDashes);
            }
        }

        //close the box
        sb.AppendLine(separatorStars);

        return sb.ToString();

    }
    //output a list to the console in a box

    //output a list to the console in a box with a header
    
}
