using System;

public class Scoring
{
    static void MarkRolls(int [] rolls, Scorecard scorecard)
    {
        if (rolls[0] == 10)
        {
            scorecard.MarkStrike();
            if (rolls[1] == 10)
            {
                scorecard.MarkStrike();
            }
            else
            {
                scorecard.MarkBonusBall(rolls[1]);
            }
        }
        else if (rolls[0] + rolls[1] == 10)
        {
            scorecard.MarkSpare(rolls[0]);
        }
        else
        {
            scorecard.MarkOpen(rolls[0], rolls[1]);
        }
    }

    public static void Score(Rolls rolls, Scorecard scorecard)
    {
        int previousStrikesToCount = 0;
        int previousSparesToCount = 0;

        for (int frame = 1; frame <= 10; frame++)
        {
            bool lastFrame = frame == 10;
            int bowl1 = rolls.NextRoll();
            int bowl2 = 0;
            int bowl3 = 0;
            bool isStrike = bowl1 == 10;
            if (lastFrame || !isStrike)
            {
                bowl2 = rolls.NextRoll();
            }
            bool isSpare = !isStrike && bowl1 + bowl2 == 10;
            if (lastFrame && (isStrike || isSpare))
            {
                bowl3 = rolls.NextRoll();
            }

            if (previousSparesToCount > 0) // if previous frame was a spare add in the extra points now
            {
                ScoreSpare(bowl1, scorecard);
            }

            if (previousStrikesToCount > 1)
            {
                ScoreStrike(new int [] { 10, bowl1 }, scorecard);
            }

            if (!isStrike && previousStrikesToCount > 0)
            {
                ScoreStrike(new int[] { bowl1, bowl2 }, scorecard);
            }

            if (isStrike)
            {
                scorecard.MarkStrike();
            }
            else if (isSpare)
            {
                scorecard.MarkSpare(bowl1);
            }
            else
            {
                scorecard.MarkOpen(bowl1, bowl2);
            }

            if (lastFrame)
            {
                if (isStrike)
                {
                    MarkRolls(new int[] { bowl2, bowl3 }, scorecard);
                }
                else if (isSpare)
                {
                    if (bowl3 == 10)
                    {
                        scorecard.MarkStrike();
                    }
                    else
                    {
                        scorecard.MarkBonusBall(bowl3);
                    }
                }
            }

            if (lastFrame && isStrike && previousStrikesToCount > 0)
            {
                ScoreStrike(new int[] { bowl1, bowl2 }, scorecard);
            }

            if (lastFrame || (!isStrike && !isSpare))
            {
                scorecard.ScoreFrame(bowl1 + bowl2 + bowl3);
            }

            previousStrikesToCount = isStrike? previousStrikesToCount + 1 : 0;
            previousSparesToCount = isSpare? 1 : 0;
        }
    }

    static void ScoreSpare(int bowl1, Scorecard scorecard)
    {
        scorecard.ScoreFrame(10 + bowl1);
    }

    static void ScoreStrike(int [] bowl, Scorecard scorecard)
    {
        scorecard.ScoreFrame(10 + bowl[0] + bowl[1]);
    }
}
