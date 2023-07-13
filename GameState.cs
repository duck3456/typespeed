namespace typespeed;

class GameState
{
    public static List<string> state = new List<string>();

    public static void fillList() {
        for (int i = 0; i < 30; i++) {
            state.Add("");
        }
    }

    public static void addNewWord() {
        FallingWord word = new FallingWord();
        
        state[0] = word.text;
    }
}