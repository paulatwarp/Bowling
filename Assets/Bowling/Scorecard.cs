using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

public class Scorecard
{
    string name;
    char [] marks;
    int currentMark;
    int [] scores;
    int currentFrame;

    public Scorecard(string name)
    {
        this.name = name;
        marks = new char[21];
        scores = new int[10];
        currentFrame = 0;
        currentMark = 0;
    }

    public Scorecard(string name, string marks, int[] scores)
    {
        this.name = name;
        this.marks = marks.ToCharArray();
        this.scores = scores;
        currentMark = 0;
        currentFrame = 0;
    }

    char ScoreToMark(int score)
    {
        return "-123456789"[score];
    }

    void SetMarks(char [] marks)
    {
        for (int i = 0; i < marks.Length; ++i)
        {
            this.marks[currentMark + i] = marks[i];
        }
    }

    public void MarkSpare(int first)
    {
        SetMarks(new char[] { ScoreToMark(first), '/', ' ' });
    }

    public void MarkOpen(int first, int second)
    {
        SetMarks(new char[] { ScoreToMark(first), ScoreToMark(second), ' ' });
    }

    public void MarkStrike()
    {
        SetMarks(new char[] { 'X', ' ', ' ' });
    }

    public void MarkBonusStrikes()
    {
        SetMarks(new char[] { 'X', 'X', 'X' });
    }

    public void MarkBonusStrike(int bonus)
    {
        SetMarks(new char[] { 'X', 'X', ScoreToMark(bonus) });
    }

    public void MarkBonusSpare(int first)
    {
        SetMarks(new char[] { 'X', ScoreToMark(first), '/' });
    }

    public void MarkBonusBalls(int first, int second)
    {
        SetMarks(new char[] { 'X', ScoreToMark(first), ScoreToMark(second) });
    }

    public void MarkBonusStrike()
    {
        currentMark += 2;
        SetMarks(new char[] { 'X' });
    }

    public void MarkBonusBall(int first)
    {
        currentMark += 2;
        SetMarks(new char[] { ScoreToMark(first) });
    }

    public void MarkNextFrame()
    {
        currentMark += 2;
    }

    public void ScoreFrame(int score)
    {
        scores[currentFrame] = score;
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
        for (int frame = 0; frame < 10; frame++)
        {
            var displayFrame = display.frames[frame];
            for (int i = 0; i < displayFrame.marks.Length; ++i)
            {
                displayFrame.marks[i].text = marks[mark + i].ToString();
            }
            mark += 2;
            displayFrame.subTotal.text = scores[frame].ToString();
        }
    }
}
