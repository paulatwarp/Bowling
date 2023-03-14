using System;
using System.Diagnostics;
using System.Text;

public class Scorecard
{
    string name;
    FrameScore[] frames;
    int mark;
    int frame;

    public Scorecard(string name)
    {
        this.name = name;
        frames = new FrameScore[10];
        for (frame = 0; frame < frames.Length; frame++)
        {
            frames[frame] = new FrameScore();
        }
        frame = 0;
        mark = 0;
    }

    public Scorecard(string name, string marks, int[] scores)
    {
        this.name = name;
        string[] split = marks.Split(',');
        int length = scores.Length;
        Debug.Assert(split.Length == length);
        frames = new FrameScore[length];
        for (int i = 0; i < length; ++i)
        {
            frames[i] = new FrameScore(split[i], scores[i]);
        }
    }

    public void Spare(int first)
    {
        frames[mark].Spare(first);
    }

    public void Score(int first, int second)
    {
        frames[mark].Score(first, second);
    }

    public void Strike()
    {
        frames[mark].Strike();
    }

    public void BonusStrikes()
    {
        frames[mark].BonusStrikes();
    }

    public void BonusStrike(int bonus)
    {
        frames[mark].BonusStrike(bonus);
    }

    public void BonusSpare(int first)
    {
        frames[mark].BonusSpare(first);
    }

    public void BonusBalls(int first, int second)
    {
        frames[mark].BonusBalls(first, second);
    }

    public void BonusStrike()
    {
        frames[mark].BonusStrike();
    }

    public void BonusBall(int first)
    {
        frames[mark].BonusBall(first);
    }

    public void MarkNextFrame()
    {
        mark++;
    }

    public void ScoreFrame(int score)
    {
        frames[frame].Score(score);
        frame++;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append("Scorecard {name}");
        for (int frame = 0; frame < frames.Length; ++frame)
        {
            builder.Append($" frame {frame} {frames[frame]}");
        }
        return builder.ToString();
    }

    public override bool Equals(object obj)
    {
        return (obj is Scorecard)? this == obj as Scorecard : base.Equals(obj);
    }

    public static bool operator ==(Scorecard a, Scorecard b)
    {
        bool equal = a.name == b.name && a.frames.Length == b.frames.Length;
        for (int i = 0; i < a.frames.Length; ++i)
        {
            equal = equal && a.frames[i] == b.frames[i];
        }
        return equal;
    }

    public static bool operator !=(Scorecard a, Scorecard b)
    {
        return !(a == b);
    }

    public void Display(Display display)
    {
        display.player.text = name;
        for(frame = 0; frame < 10; frame++)
        {
            var displayFrame = display.frames[frame];
            frames[frame].Display(displayFrame);
        }
    }

    public bool Compare(string marks, int[] scores)
    {
        string[] split = marks.Split(',');
        bool equal = true;
        for (frame = 0; frame < 10; frame++)
        {
            equal = equal && frames[frame].Compare(split[frame], scores[frame]);
        }
        return equal;
    }
}
