using System.Timers; //https://gist.github.com/SarahBourgeois/680b6e05947de3ed4370ff7dce5828fb

namespace typespeed;

class Program
{
    static System.Timers.Timer timer;
    static int score = 0;
    
    static void Main(string[] args)
    {
        Console.Write("Choose the difficulty (easy, medium or hard): ");
        string difficulty = Console.ReadLine();
        int difficultyNum = 2;

        switch (difficulty) {
            case "easy":
                difficultyNum = 3;
                break;
            case "medium":
                break;
            case "hard":
                difficultyNum = 1;
                break;
            default:
                Console.WriteLine("That's not a difficulty level! Medium picked automatically.");
                break;
        }

        timer = new System.Timers.Timer(TimeSpan.FromSeconds(difficultyNum).TotalMilliseconds);
        
        GameState.fillList();

		timer.AutoReset = true;
		timer.Elapsed += new System.Timers.ElapsedEventHandler(callEverySecond);
		timer.Start();

        string keys = "";
        bool canClearKeys = false;
        
        while (true) {
            keys += (Console.ReadKey(true).Key).ToString().ToLower();

            for (int i = 0; i < 29; i++) {
                if (keys.Contains(GameState.state[i].Replace(" ", string.Empty)) && GameState.state[i] != "") {
                    canClearKeys = true;
                    GameState.state[i] = "";
                    score++;
                }
            }

            if (canClearKeys) {
                keys = "";
                canClearKeys = false;
            }
        }
    }

    static void callEverySecond(object sender, ElapsedEventArgs e) {
		Console.Clear();

        if (GameState.state[28] != "") {
            timer.Stop();
            Console.WriteLine("YOU LOST! The word that killed you was '" + GameState.state[28].Replace(" ", string.Empty) + "'.");
            Console.WriteLine("You have managed to clear " + score + " words.");
            return;
        }

        GameState.state = GameState.state.Prepend("").ToList();
        GameState.state.RemoveAt(29);

        GameState.addNewWord();
        
        Console.ForegroundColor = ConsoleColor.Green;
        
        for (int i = 0; i < 10; i++) {
            Console.WriteLine(GameState.state[i]);
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        
        for (int i = 10; i < 20; i++) {
            Console.WriteLine(GameState.state[i]);
        }

        Console.ForegroundColor = ConsoleColor.Red;
        
        for (int i = 20; i < 29; i++) {
            Console.WriteLine(GameState.state[i]);
        }

        Console.ForegroundColor = ConsoleColor.White;

        Console.Write("-----------------------------------------------------------------------------------------------------------------------");
	}
}
