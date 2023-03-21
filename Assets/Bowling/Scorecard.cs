using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

public class Scorecard
{
    string name;
    string marks;
    int [] scores;
    int currentFrame;
    int total;

    public Scorecard(string name)
    {
        this.name = name;
        marks = "";
        scores = new int[10];
        currentFrame = 0;
        total = 0;
    }

    public Scorecard(string name, string marks, int[] scores)
    {
        this.name = name;
        this.marks = marks;
        this.scores = scores;
        currentFrame = 0;
        total = scores[9];
    }

    char ScoreToMark(int score)
    {
        return "-123456789"[score];
    }

    void SetMarks(string marks)
    {
        for (int i = 0; i < marks.Length; ++i)
        {
            this.marks += marks[i].ToString();
        }
    }

    public void MarkOpen(int first, int second)
    {
        SetMarks($"{ScoreToMark(first)}{ScoreToMark(second)}");
    }

    public void MarkSpare(int first)
    {
        SetMarks($"{ScoreToMark(first)}/");
    }

    public void MarkStrike()
    {
        SetMarks("X");
    }

    public void MarkBonusBall(int bonus)
    {
        SetMarks($"{ScoreToMark(bonus)}");
    }

    public void ScoreFrame(int score)
    {
        total += score;
        scores[currentFrame] = total;
        currentFrame++;
    }

    string ListMarks()
    {
        return new string (marks);
    }

    string ListScores()
    {
        return string.Join(" ", scores);
    }

    public override string ToString()
    {
        return $"Scorecard {name} marks {ListMarks()} scores {ListScores()}";
    }

    public override bool Equals(object obj)
    {
        return (obj is Scorecard)? Equals(obj as Scorecard) : base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public bool Equals(Scorecard other)
    {
        return name == other.name && Enumerable.SequenceEqual(marks, other.marks) && Enumerable.SequenceEqual(scores, other.scores);
    }

    public void Display(Display display)
    {
        display.player.text = name;
        int mark = 0;
        for (int frame = 1; frame <= 10; frame++)
        {
            var displayFrame = display.frames[frame - 1];
            int markCount = 2;
            int boxIndex = 0;
            if (frame < 10 && marks[mark] == 'X')
            {
                markCount = 1;
                boxIndex = 1;
            }
            else if (frame == 10 && (marks[mark] == 'X' || marks[mark + 1] == '/'))
            {
                markCount = 3;
            }
            for (int i = 0; i < markCount; ++i)
            {
                displayFrame.marks[boxIndex + i].text = marks[mark + i].ToString();
            }
            mark += markCount;
            displayFrame.subTotal.text = scores[frame - 1].ToString();
        }
    }
}
