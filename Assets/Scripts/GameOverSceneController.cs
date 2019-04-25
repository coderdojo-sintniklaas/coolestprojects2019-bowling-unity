using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSceneController : MonoBehaviour
{
    public TMP_Text naamWinnaar;
    
    // Start is called before the first frame update
    void Start()
    {
        var scoreWinaar = HulpFuncties.LaadGegevensUitBestand<ScoreData>("scoreWinnaar.json");

        naamWinnaar.text = scoreWinaar.naam;
    }

    public void StartOpnieuw()
    {
        // Laad start scene
        SceneManager.LoadScene("startScene");
    }
}
