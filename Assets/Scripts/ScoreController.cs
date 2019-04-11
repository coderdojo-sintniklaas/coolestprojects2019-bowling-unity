using System;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using UnityEngine;
using Random = System.Random;

public class ScoreController : MonoBehaviour
{
    const string NaamScoreBestand = @"scores.json";
    
    public List<ScoreData> scores { get; set; }
 
    void Start()
    {      
        // Bij start van gameobject, punten laden
        LaadScoreHistoriekUitBestand();
    }
 
    
    /*
     * OK - serialiseerbaar object met settings: ip scorebord, tijd na worp
     * visualiseren: beurt x van x
     * visualiseren; actieve speler (groen, rood=actief)
     * 
     * OK - Knop voor start van beurt
     * Toon scores -> juiste speler actief zetten
     * Eerste worp -> indien minstens 1 kegel gevallen, 3 seconden (instelling) nadien is beurt gedaan
     * Toon score
     *
     * Terug knop voor start van beurt 
     * 
     * 3 worpen per speler per spel
     * dan game over
     * 
     */

    void OnDestroy()
    {
        // Bij afsluiten van de applicatie, punten bewaren
        BewaarScoreHistoriekInBestand(); 
    }

    public void VoegScoresToeAanHistoriek(ScoreData scoreSpelerA, ScoreData scoreSpelerB)
    {
        scores.Add(scoreSpelerA);
        scores.Add(scoreSpelerB);
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
    
    private void BewaarScoreHistoriekInBestand()
    {
        // Bewaar de historiek van alle scores in een bestand
        HulpFuncties.BewaarGegevensInBestand(scores, NaamScoreBestand);
    }

}
