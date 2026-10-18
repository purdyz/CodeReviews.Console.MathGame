using System.Diagnostics;
using MathGame.purdyz;

WelcomePlayer();
bool gameRunning = true;
List<GameResult> history = new List<GameResult>();
Random random = new Random();

MainGame();

void WelcomePlayer()
{
    Console.WriteLine("Welcome to the Math Game!");
    Console.Write("What's your name? ");
    string? playerName = Console.ReadLine();
    Console.WriteLine($"Nice to meet you, {playerName}!");
    Console.WriteLine();
}

void MainGame()
{
    while (gameRunning)
    {
        Console.WriteLine("Main Menu");
        Console.WriteLine("1. Play a game");
        Console.WriteLine("2. View History");
        Console.WriteLine("3. Exit");
        string? menuChoice = Console.ReadLine();

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

        Console.WriteLine("Press any key to return to main menu.");
        Console.ReadLine();
        Console.Clear();
    }
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
                Console.WriteLine("Invalid choice.");
                break;
        }
    }
    else
    {
        Console.WriteLine("The choice you selected was invalid. Defaulting to random mode.");
        PlayRound("Random", ' ');
    }
}

void PlayRound(string operation, char operand)
{
    string difficulty = ChooseDifficulty();
    int roundNumber = 1;
    int maxRounds = 5;
    int score = 0;
    int randomNum;
    int randomNum2;
    int result;
    bool randomGame = operation == "Random" ? true : false;
    Stopwatch timer = new Stopwatch();

    while (roundNumber <= maxRounds)
    {
        randomNum = GenerateRandomNumber(difficulty);
        randomNum2 = GenerateRandomNumber(difficulty);

        if (randomGame)
        {
            string[] operationArray = new string[] { "Addition", "Subtraction", "Multiplication", "Division" };
            char[] operandArray = new char[] { '+', '-', '*', '/' };
            int randomOperation = random.Next(0, 4);
            operation = operationArray[randomOperation];
            operand = operandArray[randomOperation];
        }

        if (operation == "Division")
            randomNum = Calculate("Multiplication", randomNum, randomNum2);

        result = Calculate(operation, randomNum, randomNum2);

        Console.WriteLine($"Question {roundNumber}! What is {randomNum} {operand} {randomNum2}?");
        timer.Start();
        string? answer = Console.ReadLine();
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
            Console.WriteLine("The answer you entered was invalid. Please enter a whole number only. Marking as incorrect!");

        roundNumber++;
    }

    timer.Stop();
    RecordResults(randomGame, operation, score, difficulty, timer);
    Console.WriteLine($"Your final score is {score}! It took you {timer.Elapsed.ToString(@"mm\:ss")}!");
}

static string ChooseDifficulty()
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
                return "Easy";
            case 2:
                return "Medium";
            case 3:
                return "Hard";
            default:
                Console.WriteLine("Invalid choice. Defaulting to easy mode.");
                return "Easy";
        }
    }
    else
    {
        Console.WriteLine("The choice you selected was invalid. Defaulting to easy mode.");
        return "Easy";
    }
}

int GenerateRandomNumber(string difficulty)
{
    if (difficulty.Equals("Hard"))
        return random.Next(1, 1001);
    if (difficulty.Equals("Medium"))
        return random.Next(1, 101);

    return random.Next(1, 11);
}

void RecordResults(bool randomGame, string operation, int score, string difficulty, Stopwatch timer)
{
    GameResult currentResult = new GameResult();
    currentResult.GameType = randomGame ? "Random" : operation;
    currentResult.Score = score;
    currentResult.Difficulty = difficulty;
    currentResult.Time = timer.Elapsed.ToString(@"mm\:ss");
    history.Add(currentResult);
}

static int Calculate(string operation, int randomNum, int randomNum2)
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
        Console.WriteLine($"{"Game Type", -15} {"Score", -6} {"Difficulty", -12} {"Time Elapsed", -12}");
        foreach (GameResult result in history)
            Console.WriteLine($"{result.GameType, -15} {result.Score, -6} {result.Difficulty, -12} {result.Time, -12}");
    }
    else
        Console.WriteLine("No scores to display");
}