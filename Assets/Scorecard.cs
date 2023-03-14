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

    public void Display(Display display)
    {
        for(frame = 0; frame < 10; frame++)
        {
            var displayFrame = display.frames[frame];
            frames[frame].Display(displayFrame);
        }
    }
}
