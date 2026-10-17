// Welcome Message
Console.WriteLine("Welcome to the Math Game!");
Console.Write("What's your name? ");
string playerName = Console.ReadLine();
Console.WriteLine($"Nice to meet you, {playerName}!");

// Main Menu + Game Loop
bool gameRunning = true;
List<GameResult> history = new List<GameResult>();

while (gameRunning)
{
    Console.WriteLine("Main Menu");
    Console.WriteLine("1. Play a game");
    Console.WriteLine("2. View History");
    Console.WriteLine("3. Exit");
    string menuChoice = Console.ReadLine();

    switch (menuChoice)
    {
        case "1":
            GameSelection();
            break;
        case "2":
            foreach (GameResult result in history)
                Console.WriteLine($"{result.GameType} - {result.Score}");
            break;
        case "3":
            Console.WriteLine("Thanks for playing!");
            gameRunning = false;
            break;
        default:
            Console.WriteLine("The choice you selected was invalid. Please enter 1 through 3 only.");
            break;
    }

    // Choose a game
    void GameSelection()
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
    void AskQuestions(char operation)
    {
        Random random = new Random();
        int count = 1;
        int score = 0;
        string gameType = string.Empty;
        int randomNum = random.Next(1, 11);
        int randomNum2 = random.Next(1, 11);
        int result = 0;
        int product = 0;

        while (count < 6)
        {
            if (operation == '+')
            {
                result = randomNum + randomNum2;
                gameType = "Addition";
            }
            else if (operation == '-')
            {
                result = randomNum - randomNum2;
                gameType = "Subtraction";
            }
            else if (operation == '*')
            {
                result = randomNum * randomNum2;
                gameType = "Multiplication";
            }
            else if (operation == '/')
            {
                product = randomNum * randomNum2;
                result = product / randomNum2;
                randomNum = product;
                gameType = "Division";
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

        GameResult currentResult = new GameResult();
        currentResult.GameType = gameType;
        currentResult.Score = score;
        history.Add(currentResult);
        Console.WriteLine($"Your final score is {score}!");
    }

    Console.WriteLine("Press any key to continue");
    Console.ReadLine();
    Console.Clear();
}

class GameResult
{
    public string GameType { get; set; }
    public int Score { get; set; }
}