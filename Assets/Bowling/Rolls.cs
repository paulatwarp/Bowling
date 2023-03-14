public class Rolls
{
    int[] rolls;
    int index;

    public Rolls(int[] rolls)
    {
        this.rolls = rolls;
    }

    public int NextRoll()
    {
        int roll = rolls[index];
        index++;
        return roll;
    }
}
