using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class HighScoresViewController : MonoBehaviour
{
    const string NaamScoreBestand = @"scores.json";
    
    public GameObject prefab; // Dit is de prefab waarmee de lijst opgevuld gaat worden
    public List<ScoreData> scores { get; set; }

    private void Start()
    {
        LaadScoreHistoriekUitBestand();        
        Populate();
    }

    private void Populate()
    {
        GameObject nieuwObj; // Maak GameObject instantie

        foreach (var score in scores)
        {
            // Maak voor elke score een nieuwe instantie aan van de "prefab"
            nieuwObj = (GameObject)Instantiate(prefab, transform);
			
            // Zoek de controller die verantwoordelijk is om de tekst velden op te vullen en roep de functie aan die dat uitvoert
            nieuwObj.GetComponent<HighScoreItemViewController>().ZetScoreData(score);
        }
    }
    
    private void LaadScoreHistoriekUitBestand()
    {
        // Probeer de historiek van alle scores te laden
        try
        {
            scores = HulpFuncties.LaadGegevensUitBestand < List<ScoreData> >(NaamScoreBestand);
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e);
            scores = new List<ScoreData>();
        }
    }

}
