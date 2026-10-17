// Welcome Message
Console.WriteLine("Welcome to the Math Game!");
Console.Write("What's your name? ");
string playerName = Console.ReadLine();
Console.WriteLine($"Nice to meet you, {playerName}!");

// Main Menu
Console.WriteLine("Main Menu");
Console.WriteLine("1. Play a game");
Console.WriteLine("2. View History");
Console.WriteLine("3. Exit");
while (int.TryParse(Console.ReadLine(), out int menuChoice))
{
    switch (menuChoice)
        {
            case 1:
                GameSelection();
                break;
            case 2:
                Console.WriteLine("History available in a future update!");
                break;
            case 3:
                Console.WriteLine("Thanks for playing!");
                break;
            default:
                Console.WriteLine("The choice you selected was invalid. Please enter 1 through 3 only.");
                break;
        }
}

// Choose a game
static void GameSelection()
{
    Console.WriteLine("Enter the number of a game to play:");
    Console.WriteLine("1. Addition");
    Console.WriteLine("2. Subtraction");
    Console.WriteLine("3. Multiplication");
    Console.WriteLine("4. Division");
    if (int.TryParse(Console.ReadLine(), out int gameChoice))
    {
        switch (gameChoice)
        {
            case 1:
                Console.WriteLine("Addition selected!");
                AskQuestions('+');
                break;
            case 2:
                Console.WriteLine("Subtraction selected!");
                AskQuestions('-');
                break;
            case 3:
                Console.WriteLine("Multiplication selected!");
                AskQuestions('*');
                break;
            case 4:
                Console.WriteLine("Division selected!");
                AskQuestions('/');
                break;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
    }
    else
        Console.WriteLine("The choice you selected was invalid. Please enter 1 through 4 only.");
}

// Ask questions
static void AskQuestions(char operation)
{
    Random random = new Random();
    int count = 1;
    int score = 0;
    int randomNum = random.Next(1, 11);
    int randomNum2 = random.Next(1, 11);
    int result = 0;
    int product = 0;

    while (count < 6)
    {
        if (operation == '+')
            result = randomNum + randomNum2;
        if (operation == '-')
            result = randomNum - randomNum2;
        if (operation == '*')
            result = randomNum * randomNum2;
        if (operation == '/')
        {
            product = randomNum * randomNum2;
            result = product / randomNum2;
            randomNum = product;
        }

        Console.WriteLine($"Question {count}! What is {randomNum} {operation} {randomNum2}?");
        string answer = Console.ReadLine();
        Console.WriteLine($"Your answer is {answer}");

        if (int.TryParse(answer, out int validAnswer))
        {
            if (validAnswer == result)
            {
                score++;
                Console.WriteLine("Correct!");
            }
            else
                Console.WriteLine("That's the wrong answer.");
        }
        else
            Console.WriteLine("The answer you entered was invalid. Please enter a whole number only.");

        count++;
        randomNum = random.Next(1, 11);
        randomNum2 = random.Next(1, 11);
    }

    Console.WriteLine($"Your final score is {score}!");
}