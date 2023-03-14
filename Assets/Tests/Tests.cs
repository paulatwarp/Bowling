using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

public class Tests
{
    [Test]
    public void TestExampleGame()
    {
        var rolls = new Rolls(new int[] { 8, 1, 0, 9, 2, 8, 10, 6, 3, 7, 0, 5, 2, 10, 0, 6, 2, 8, 10 });
        var scorecard = new Scorecard("Test");
        Scoring.Score(rolls, scorecard);
        var expected = new Scorecard("Test", "81,-9,2/,X,63,7-,52,X,-6,2/X", new int[] { 9, 18, 38, 57, 66, 73, 80, 96, 102, 122 });
        Assert.That(scorecard, Is.EqualTo(expected));
    }
}
