using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public Display display;

    private void Start()
    {
        var rolls = new Rolls(new int[] { 8, 1, 0, 9, 2, 8, 10, 6, 3, 7, 0, 5, 2, 10, 0, 6, 2, 8, 10 });
        var scorecard = new Scorecard("Test");
        Score(rolls, scorecard);
    }

    //code by www.exchangecore.com
    static int intLength(int integer)
    {
        int length = 1;
        if (integer / 10 > 0)
            length = 2;
        if (integer / 100 > 0)
            length = 3;
        return length;
    }

    static String scoreTwo(int totalScore, String score2)
    {
        if (intLength(totalScore) == 1)
            score2 += totalScore + "     ";
        else if (intLength(totalScore) == 2)
            score2 += totalScore + "    ";
        if (intLength(totalScore) == 3)
            score2 += totalScore + "   ";
        return score2;
    }
    void Score(Rolls rolls, Scorecard scorecard)
    {
        int frameScore = 0, prevFrame = 0, prevFrameTwo = 0, bowlOne, bowlTwo = 0, frame = 1, totalScore = 0, extraFrame;
        bool strike = false, strikeTwo = false, spare = false;
        String score2 = "", frameNum = "", line = "";

        for (; frame <= 10; frame++)
        {
            Debug.LogFormat("Please Enter your Scores for Frame {0}:", frame);
            do //bowlOne loop
            {
                Debug.Log("Bowl 1:");
                bowlOne = rolls.NextRoll();
            } while (bowlOne > 10 || bowlOne < 0); //checks for valid bowlOne input
            if (spare == true)// if previous frame was a spare add in the extra points now
            {
                prevFrame = 10 + bowlOne;
                spare = false;
                totalScore = prevFrame + totalScore;
                score2 = scoreTwo(totalScore, score2);
                scorecard.ScoreFrame(totalScore);
            }
            if (strikeTwo == true && bowlOne == 10)
            {
                prevFrameTwo = 30;
                totalScore = prevFrameTwo + totalScore;
                score2 = scoreTwo(totalScore, score2);
                scorecard.ScoreFrame(totalScore);
            }
            if (strikeTwo == true && bowlOne != 10)
            {
                strikeTwo = false;
                prevFrameTwo = 10 + 10 + bowlOne;
                totalScore = prevFrameTwo + totalScore;
                score2 = scoreTwo(totalScore, score2);
                scorecard.ScoreFrame(totalScore);
            }
            if (strike == true && bowlOne == 10)
            {
                strikeTwo = true;
                prevFrameTwo = 20;
            }

            if (bowlOne < 10) //check to make sure there wasn't a strike on first bowl
            {
                do //bowlTwo loop
                {
                    Debug.Log("Bowl 2:");
                    bowlTwo = rolls.NextRoll();
                } while (bowlTwo > (10 - bowlOne) || bowlTwo < 0);
                if (bowlOne + bowlTwo == 10)
                {
                    spare = true;
                    scorecard.Spare(bowlOne);
                }

                if (strikeTwo == true && frame == 10)
                {
                    prevFrameTwo = 10 + 10 + bowlTwo;
                    totalScore = prevFrameTwo + totalScore;
                    score2 = scoreTwo(totalScore, score2);
                    scorecard.ScoreFrame(totalScore);
                    strikeTwo = false;
                }

                if (strike == true && bowlOne != 10)
                {
                    strike = false;
                    prevFrame = 10 + bowlOne + bowlTwo;
                    totalScore = totalScore + prevFrame;
                    score2 = scoreTwo(totalScore, score2);
                    scorecard.ScoreFrame(totalScore);
                }
                if (spare != true && strike != true && strikeTwo != true)
                {
                    frameScore = bowlOne + bowlTwo;
                    totalScore = totalScore + frameScore;
                    score2 = scoreTwo(totalScore, score2);
                    scorecard.Score(bowlOne, bowlTwo);
                    scorecard.ScoreFrame(totalScore);
                }
            }
            else
            {
                strike = true;
                prevFrame = 10;
                if (frame != 10)
                    scorecard.Strike();
            }
            if (frame == 10 && strike == true)
            {
                do
                    bowlTwo = rolls.NextRoll();
                while (bowlTwo < 0 || bowlTwo > 10);

                if (strikeTwo == true)
                {
                    prevFrameTwo = 10 + 10 + bowlTwo;
                    totalScore = prevFrameTwo + totalScore;
                    score2 = scoreTwo(totalScore, score2);
                    scorecard.ScoreFrame(totalScore);
                    strikeTwo = false;
                }
            }

            if (frame == 10 && (spare == true || strike == true))
            {
                do
                    extraFrame = rolls.NextRoll();
                while (extraFrame < 0 || extraFrame > 10);
                if (strike == true)
                {
                    prevFrame = 10 + bowlTwo + extraFrame;
                    totalScore = totalScore + prevFrame;
                    score2 = scoreTwo(totalScore, score2);
                    scorecard.ScoreFrame(totalScore);
                    if (bowlTwo == 10 && extraFrame == 10)
                    {
                        scorecard.BonusStrikes();
                    }
                    else if (bowlTwo == 10)
                    {
                        scorecard.BonusStrike(extraFrame);
                    }
                    else if (bowlTwo + extraFrame == 10)
                    {
                        scorecard.BonusSpare(bowlTwo);
                    }
                    else
                    {
                        scorecard.BonusBalls(bowlTwo, extraFrame);
                    }
                }
                else
                {
                    scorecard.Spare(bowlOne);
                    if (extraFrame == 10)
                    {
                        scorecard.BonusStrike();
                    }
                    else
                    {
                        scorecard.BonusBall(extraFrame);
                    }
                    totalScore = totalScore + 10 + extraFrame;
                    score2 = scoreTwo(totalScore, score2);
                    scorecard.ScoreFrame(totalScore);
                }
            }
            scorecard.MarkNextFrame();
            frameNum += frame + "    ";
            line += "------";
        }
        scorecard.Display(display);
    }
}
