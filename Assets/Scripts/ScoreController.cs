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
    
    public ScoreData scoreSpelerA = new ScoreData();
    public ScoreData scoreSpelerB = new ScoreData();
    public ScoreData actieveSpelerScore;
    
    void Start()
    {      
        // Bij start van gameobject, punten laden
        LaadScoreHistoriekUitBestand();
        LaadSpelerNamenUitBestand();
    }

    public void InitialiseerScores(string naamSpelerA, string naamSpelerB)
    {
        scoreSpelerA.naam  = naamSpelerA;
        scoreSpelerA.score = 0;
        scoreSpelerB.naam  = naamSpelerB;
        scoreSpelerB.score = 0;

        ToonScoresOpBord();
    }

    public void ActiveerSpelerA()
    {
        ScoreBordController.ActiveerSpeler('A').Wait();
        actieveSpelerScore = scoreSpelerA;
    }

    public void ActiveerSpelerB()
    {
        ScoreBordController.ActiveerSpeler('B').Wait();
        actieveSpelerScore = scoreSpelerB;
    }
    
    public void UpdateScore(int score)
    {
        actieveSpelerScore.score += score;
        ToonScoresOpBord();
    }

    void OnDestroy()
    {
        // Bij afsluiten van de applicatie, punten bewaren
        BewaarScoreHistoriekInBestand(); 
    }

    public void VoegScoresToeAanHistoriek()
    {
        scores.Add(scoreSpelerA);
        scores.Add(scoreSpelerB);
    }

    public void Test()
    {
        scores.Add(new ScoreData(){naam="tim", score = new Random().Next(100)});
    }

    private void LaadSpelerNamenUitBestand()
    {
        // Probeer naam speler A te lezen, als het mislukt nieuw score object aanmaken
        // Zou beter zijn dat we dan teruggestuurd worden naar vorige scene
        try
        {
            scoreSpelerA = HulpFuncties.LaadGegevensUitBestand<ScoreData>("invoerSpelerA.json");
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e);
            scoreSpelerA = new ScoreData();
        }
        
        // Probeer naam speler A te lezen, als het mislukt nieuw score object aanmaken
        // Zou beter zijn dat we dan teruggestuurd worden naar vorige scene
        try
        {
            scoreSpelerB = HulpFuncties.LaadGegevensUitBestand<ScoreData>("invoerSpelerB.json");
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e);
            scoreSpelerB = new ScoreData();
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
    
    private void BewaarScoreHistoriekInBestand()
    {
        // Bewaar de historiek van alle scores in een bestand
        HulpFuncties.BewaarGegevensInBestand(scores, NaamScoreBestand);
    }

    private async void ToonScoresOpBord()
    {
        ScoreBordController.ToonScores(scoreSpelerA, scoreSpelerB);
    }

}
