using Characters;
internal class Program
{
    static void Main()
    {
        List<Character> aliveChar = new List<Character>(); //список живых персонажей
        List<Character> deadChar = new List<Character>(); //список мёртвых персонажей
        Character playChar = new Character(); //объект для игры
        playChar.Play(aliveChar, deadChar);
    }
}