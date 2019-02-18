using System.Collections;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NaamIngaveSceneController : MonoBehaviour
{

    public TMP_InputField InvoerNaamSpelerA;
    public TMP_InputField InvoerNaamSpelerB;

    public void TerugKnopKlik()
    {
        SceneManager.LoadScene("StartScene");
    }
    
    public void VerderKnopKlik()
    {
        var spelerA = new ScoreData {naam = InvoerNaamSpelerA.text};

        var spelerB = new ScoreData {naam = InvoerNaamSpelerB.text};

        // Bewaar gegevens in bestanden
        HulpFuncties.BewaarGegevensInBestand(spelerA, "invoerSpelerA.json");
        HulpFuncties.BewaarGegevensInBestand(spelerA, "invoerSpelerA.json");

        // Laad volgende scene
        SceneManager.LoadScene("spelScene");
    }
}
