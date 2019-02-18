using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using DefaultNamespace;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreController : MonoBehaviour
{
    
    public List<ScoreData> scores { get; set; }
    private string NaamScoreBestand = @"scores.json";
    
    public ScoreData scoreSpelerA = new ScoreData();
    public ScoreData scoreSpelerB = new ScoreData();
    
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
    }

    public void ActiveerSpelerB()
    {
        ScoreBordController.ActiveerSpeler('B').Wait();
    }
    
    public void UpdateScoreA(int score)
    {
        scoreSpelerA.score += score;
        ToonScoresOpBord();
    }
    
    public void UpdateScoreB(int score)
    {
        scoreSpelerB.score += score;
        ToonScoresOpBord();
    }

    void OnApplicationQuit()
    {
        // Bij afsluiten van de applicatie, punten bewaren
        BewaarScoreHistoriekInBestand(); 
    }

    public void VoegScoresToeAanHistoriek()
    {
        scores.Add(scoreSpelerA);
        scores.Add(scoreSpelerB);
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
        ScoreBordController.ToonScores(scoreSpelerA, scoreSpelerB)
    }

}
