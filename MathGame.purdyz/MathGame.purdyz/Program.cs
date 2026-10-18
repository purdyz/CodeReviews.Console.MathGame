using System.Diagnostics;

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
    Console.WriteLine("5. Random");

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
            case 5:
                Console.WriteLine("Random selected!");
                PlayRound("Random", ' ');
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
    int difficulty = ChooseDifficulty();
    int count = 1;
    int score = 0;
    int randomNum;
    int randomNum2;
    int product;
    int result;
    bool randomGame = operation == "Random" ? true : false;
    Stopwatch timer = new Stopwatch();

    while (count < 6)
    {
        randomNum = GenerateRandomNumber(difficulty);
        randomNum2 = GenerateRandomNumber(difficulty);

        if (randomGame)
        {
            string[] operationArray = new string[] { "Addition", "Subtraction", "Multiplication", "Division" };
            char[] operandArray = new char[] { '+', '-', '*', '/' };
            Random random = new Random();
            int randomOperation = random.Next(0, 4);
            operation = operationArray[randomOperation];
            operand = operandArray[randomOperation];
        }

        if (operation == "Division")
            randomNum = CalculateResult("Multiplication", randomNum, randomNum2);

        result = CalculateResult(operation, randomNum, randomNum2);

        Console.WriteLine($"Question {count}! What is {randomNum} {operand} {randomNum2}?");
        timer.Start();
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

    timer.Stop();

    GameResult currentResult = new GameResult();
    currentResult.GameType = randomGame ? "Random" : operation;
    currentResult.Score = score;
    currentResult.Difficulty = difficulty;
    currentResult.Time = timer.Elapsed;
    history.Add(currentResult);
    Console.WriteLine($"Your final score is {score}! It took you {currentResult.Time}!");
}

static int ChooseDifficulty()
{
    Console.WriteLine("Choose a difficulty level");
    Console.WriteLine("1. Easy");
    Console.WriteLine("2. Medium");
    Console.WriteLine("3. Hard");

    if (int.TryParse(Console.ReadLine(), out int difficultyChoice))
    {
        switch (difficultyChoice)
        {
            case 1:
                return 1;
            case 2:
                return 2;
            case 3:
                return 3;
            default:
                Console.WriteLine("Invalid choice. Defaulting to easy mode.");
                return 1;
        }
    }
    else
    {
        Console.WriteLine("The choice you selected was invalid. Defaulting to easy mode.");
        return 1;
    }
}

static int GenerateRandomNumber(int difficulty)
{
    Random random = new Random();

    if (difficulty == 3)
        return random.Next(1, 1001);
    if (difficulty == 2)
        return random.Next(1, 101);

    return random.Next(1, 11);
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
            Console.WriteLine($"{result.GameType} - {result.Score} - {result.Difficulty} - {result.Time}");
    }
    else
        Console.WriteLine("No scores to display");
}

class GameResult
{
    public string GameType { get; set; }
    public int Score { get; set; }
    public int Difficulty { get; set; }
    public TimeSpan Time { get; set; }
}