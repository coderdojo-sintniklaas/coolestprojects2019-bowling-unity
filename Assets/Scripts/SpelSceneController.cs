using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpelSceneController : MonoBehaviour
{
    public TMP_Text scoreSpelerA;
    public TMP_Text scoreSpelerB;


    public void ToonScores(int scoreA, int scoreB)
    {
        scoreSpelerA.text = scoreA.ToString();
        scoreSpelerB.text = scoreB.ToString();
    }

    public void TestScores()
    {
        scoreSpelerA.text = "1";
        scoreSpelerB.text = "2";
    }
    
}
