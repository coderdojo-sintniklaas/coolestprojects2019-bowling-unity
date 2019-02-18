using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreItemViewController : MonoBehaviour
{
    public TMP_Text NaamText;
    public TMP_Text ScoreText;

    private ScoreData _scoreData;
    
    public void ZetScoreData(ScoreData scoreData)
    {
        // Bewaar de info in een lokaal object
        _scoreData = scoreData;
        
        // Vul de tekstvelden met de juiste info
        NaamText.text = _scoreData.naam;
        ScoreText.text = _scoreData.score.ToString();
    }

}
