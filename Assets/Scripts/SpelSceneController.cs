using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpelSceneController : MonoBehaviour
{
    public TMP_Text ScoreSpelerA;
    public TMP_Text ScoreSpelerB;


    public void ToonScores(int scoreA, int scoreB)
    {
        ScoreSpelerA.text = scoreA.ToString();
        ScoreSpelerB.text = scoreB.ToString();
    }

    public void TestScores()
    {
        ScoreSpelerA.text = "1";
        ScoreSpelerB.text = "2";
       
    }
    
}
