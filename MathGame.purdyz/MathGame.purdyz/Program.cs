Console.WriteLine("Welcome to the Math Game!");
Console.Write("What's your name? ");
string playerName = Console.ReadLine();
Console.WriteLine($"Nice to meet you, {playerName}!");

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
            ViewHistory();
            break;
        case "3":
            Console.WriteLine("Thanks for playing!");
            gameRunning = false;
            break;
        default:
            Console.WriteLine("The choice you selected was invalid. Please enter 1 through 3 only.");
            break;
    }

    Console.WriteLine("Press any key to continue");
    Console.ReadLine();
    Console.Clear();
}

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
                PlayRound("Addition", '+');
                break;
            case 2:
                Console.WriteLine("Subtraction selected!");
                PlayRound("Subtraction", '-');
                break;
            case 3:
                Console.WriteLine("Multiplication selected!");
                PlayRound("Multiplication", '*');
                break;
            case 4:
                Console.WriteLine("Division selected!");
                PlayRound("Division", '/');
                break;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
    }
    else
        Console.WriteLine("The choice you selected was invalid. Please enter 1 through 4 only.");
}

void PlayRound(string operation, char operand)
{
    Random random = new Random();
    int count = 1;
    int score = 0;
    int randomNum;
    int randomNum2;
    int product;
    int result;

    while (count < 6)
    {
        randomNum = random.Next(1, 11);
        randomNum2 = random.Next(1, 11);

        if (operation == "Division")
            randomNum = randomNum * randomNum2;

        result = CalculateResult(operation, randomNum, randomNum2);

        Console.WriteLine($"Question {count}! What is {randomNum} {operand} {randomNum2}?");
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
    }

    GameResult currentResult = new GameResult();
    currentResult.GameType = operation;
    currentResult.Score = score;
    history.Add(currentResult);
    Console.WriteLine($"Your final score is {score}!");
}

static int CalculateResult(string operation, int randomNum, int randomNum2)
{
    if (operation == "Addition")
        return randomNum + randomNum2;
    else if (operation == "Subtraction")
        return randomNum - randomNum2;
    else if (operation == "Multiplication")
        return randomNum * randomNum2;
    else
        return randomNum / randomNum2;
}

void ViewHistory()
{
    if (history.Count > 0)
    {
        foreach (GameResult result in history)
            Console.WriteLine($"{result.GameType} - {result.Score}");
    }
    else
        Console.WriteLine("No scores to display");
}

class GameResult
{
    public string GameType { get; set; }
    public int Score { get; set; }
}