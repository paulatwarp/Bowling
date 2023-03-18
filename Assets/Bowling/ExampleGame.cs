using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleGame : MonoBehaviour
{
    public Display display;
    void Start()
    {
        var rolls = new Rolls(new int[] { 8, 1, 0, 9, 2, 8, 10, 6, 3, 7, 0, 5, 2, 10, 0, 6, 2, 8, 10 });
        var scorecard = new Scorecard("Test");
        Scoring.Score(rolls, scorecard);
        scorecard.Display(display);
    }
}
