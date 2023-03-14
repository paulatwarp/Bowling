using System;

public class FrameScore
{
    string[] marks;
    string subTotal;

    public FrameScore()
    {
        marks = new string[3] { string.Empty, string.Empty, string.Empty };
        subTotal = "";
    }

    public string Mark(int roll)
    {
        return roll == 0 ? "-" : roll.ToString();
    }

    public void Spare(int first)
    {
        marks[0] = Mark(first);
        marks[1] = "/";
    }

    public void Score(int first, int second)
    {
        marks[0] = Mark(first);
        marks[1] = Mark(second);
    }

    public void Strike()
    {
        marks[0] = "";
        marks[1] = "X";
    }

    public void BonusStrikes()
    {
        marks[1] = "X";
        marks[2] = "X";
    }

    public void BonusStrike(int bonus)
    {
        marks[1] = "X";
        marks[2] = Mark(bonus);
    }

    public void BonusSpare(int first)
    {
        marks[1] = Mark(first);
        marks[2] = "/";
    }

    public void BonusBalls(int first, int second)
    {
        marks[1] = Mark(first);
        marks[2] = Mark(second);
    }

    public void BonusStrike()
    {
        marks[2] = "X";
    }

    public void BonusBall(int first)
    {
        marks[2] = Mark(first);
    }

    public void Score(int score)
    {
        subTotal = score.ToString();
    }

    public void Display(DisplayFrame frame)
    {
        frame.mark1.text = marks[0].ToString();
        frame.mark2.text = marks[1].ToString();
        frame.mark3.text = marks[2].ToString();
        frame.subTotal.text = subTotal;
    }
}
