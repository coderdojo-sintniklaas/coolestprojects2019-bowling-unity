using System;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpelSceneController : MonoBehaviour
{
    public TMP_Text scoreSpelerA;
    public TMP_Text scoreSpelerB;
    
    public TMP_Text naamSpelerA;
    public TMP_Text naamSpelerB;

    public TMP_Text beurtSpelerA;
    public TMP_Text beurtSpelerB;

    public TMP_Text afteller;
    public TMP_Text aftellerLabel;

    public TMP_Text scoreBeurt;
    
    public Button spelerStaatklaarKnop;

    public SpelData spelData = new SpelData();

    public float tijdTotEindeBeurt = -1.0f;                // moet eigenlijk private zijn
    public bool aftellenTotEindeBeurt = false;             // moet eigenlijk private zijn

    public bool spelActief = false;                        // moet eigenlijk private zijn

    private const string spelerKlaarTekst = "Speler en kegels staan klaar";
    private const string beurtGedaanTekst = "Beurt gedaan (geen kegels omver)";
    
    private void Start()
    {
        // Laad de spelernamen (bewaard in vorige scene)
        LaadSpelerNamenUitBestand();
        
        // Zet het scorebord op 0
        ToonScores();
        
        // Verberg afteller
        afteller.gameObject.SetActive(false);
        aftellerLabel.gameObject.SetActive(false);
        
        // Zet juiste tekst op knop
        spelerStaatklaarKnop.GetComponentInChildren<Text>().text = spelerKlaarTekst;
    }
    
    // Onderstaand ftie wordt door unity opgeroepen
    void Update()
    {
        if (aftellenTotEindeBeurt)
        {
            tijdTotEindeBeurt -= Time.deltaTime;
            if (tijdTotEindeBeurt <= 0)
            {
                BeurtGedaan();    
            }
            else
            {
                afteller.text = ((int) tijdTotEindeBeurt).ToString();
            }
        }
    }

    public void ToonScores()
    {
        // Toon scores in unity
        naamSpelerA.text = spelData.spelerA.naam;
        naamSpelerB.text = spelData.spelerB.naam;

        scoreSpelerA.text = spelData.spelerA.score.ToString();
        scoreSpelerB.text = spelData.spelerB.score.ToString();

        if (spelData.spelerA.huidigeBeurt > 0)
        {
            beurtSpelerA.gameObject.SetActive(true);
            beurtSpelerA.text = $@"Beurt {spelData.spelerA.huidigeBeurt} / {spelData.spelerA.aantalBeurten}";
        }
        else
        {
            beurtSpelerA.gameObject.SetActive(false);
        }
        if (spelData.spelerB.huidigeBeurt > 0)
        {
            beurtSpelerB.gameObject.SetActive(true);
            beurtSpelerB.text = $@"Beurt {spelData.spelerB.huidigeBeurt} / {spelData.spelerB.aantalBeurten}";
        }
        else
        {
            beurtSpelerB.gameObject.SetActive(false);
        }

        if (spelData.actieveSpeler == null)
        {
            scoreSpelerA.color = Color.red;
            scoreSpelerB.color = Color.red;
        }
        else if (spelData.actieveSpeler.code == 'A')
        {
            scoreSpelerA.color = Color.green;
            scoreSpelerB.color = Color.red;
        }
        else if (spelData.actieveSpeler.code == 'B')
        {
            scoreSpelerA.color = Color.red;
            scoreSpelerB.color = Color.green;
        }
        
        // Toon scores op scorebord
        ScoreBordController.ToonScores(
            spelData.spelerA.score, 
            spelData.spelerB.score,
            spelData.actieveSpeler?.code ?? ' '        // als actieve speler "leeg" is dan word ' ' ingevuld
        );
    }

    public void BeurtGedaan()
    {
        // Stop aftellen en verberg teller
        StopEindeTeller();
        
        // Bereken nieuwe score
        spelData.actieveSpeler.score += spelData.kegels.BerekenScore();
        
        // Toon score op scorebord
        ToonScores();

        // Zet knop voor volgende beurt terug aan
        // spelerStaatklaarKnop.interactable = true;
    }

    public void SpelerStaatKlaar()
    {
        ControleerOfEindeSpel();
        
        // Stel andere speler in als actieve speler 
        spelData.WisselSpeler();
        
        // Toon de speler namen en initialiseer het scorebord
        ToonScores();
        
        // Toon de score voor deze beurt = 0
        ToonScoreVoorBeurt();
        
        // Zorg ervoor data data van de kegels uitgelezen wordt
        spelActief = true;

        // Zet tekst van knop 
        spelerStaatklaarKnop.GetComponentInChildren<Text>().text = beurtGedaanTekst;
    }
    
    private void LaadSpelerNamenUitBestand()
    {
        // Probeer naam speler A te lezen, als het mislukt nieuw score object aanmaken
        // Zou beter zijn dat we dan teruggestuurd worden naar vorige scene
        try
        {
            var scoreSpelerA = HulpFuncties.LaadGegevensUitBestand<ScoreData>("invoerSpelerA.json");
            spelData.spelerA.naam = scoreSpelerA.naam;
        }
        catch (FileNotFoundException e)
        {
            Debug.LogError("Ophalen naam speler A mislukt: " + e.Message);
        }
        
        // Probeer naam speler A te lezen, als het mislukt nieuw score object aanmaken
        // Zou beter zijn dat we dan teruggestuurd worden naar vorige scene
        try
        {
            var scoreSpelerB = HulpFuncties.LaadGegevensUitBestand<ScoreData>("invoerSpelerB.json");
            spelData.spelerB.naam = scoreSpelerB.naam;
        }
        catch (FileNotFoundException e)
        {
            Debug.LogError("Ophalen naam speler B mislukt: " + e.Message);
        }
    }


    private void StartEindeTeller()
    {
        /*
         * Als eindebeurt teller nog niet loopt
         * - Tijd instellen tot einde beurt
         * - Activeren
         */
        if (!aftellenTotEindeBeurt)
        {   
            // Toon afteller
            afteller.gameObject.SetActive(true);
            aftellerLabel.gameObject.SetActive(true);


            tijdTotEindeBeurt = Instellingen.tijdNaWorp;
            aftellenTotEindeBeurt = true;
        }
    }

    private bool ControleerOfEindeSpel()
    {
        if ( (spelData.spelerA.huidigeBeurt >= spelData.spelerA.aantalBeurten)
            && (spelData.spelerB.huidigeBeurt >= spelData.spelerB.aantalBeurten) )
        {
            // Bewaar scores in bestanden
            var scoreSpelerA = spelData.spelerA.scoreData;
            var scoreSpelerB = spelData.spelerB.scoreData;

            HulpFuncties.BewaarGegevensInBestand(scoreSpelerA, "scoreSpelerA.json");
            HulpFuncties.BewaarGegevensInBestand(scoreSpelerB, "scoreSpelerB.json");
            if (spelData.spelerA.score > spelData.spelerB.score)
            {
                HulpFuncties.BewaarGegevensInBestand(scoreSpelerA, "scoreWinnaar.json");
            }
            else
            {
                HulpFuncties.BewaarGegevensInBestand(scoreSpelerB, "scoreWinnaar.json");
            }
              

            // Voeg toe aan historiek
            var scoreController = GameObject.FindObjectOfType<ScoreController>();
            if (scoreController != null)
            {
                scoreController.VoegScoresToeAanHistoriek(scoreSpelerA, scoreSpelerB);
            }
            
            // Laad volgende scene
            SceneManager.LoadScene("gameOverScene");

            return true;
        }
        else
        {
            return false;
        }
    }

    private void StopEindeTeller()
    {
        // Stop teller
        aftellenTotEindeBeurt = false;
        
        // Verberg afteller
        afteller.gameObject.SetActive(false);
        aftellerLabel.gameObject.SetActive(false);
        
        // Zorg ervoor dat kegels geen invloed meer hebben
        spelActief = false;
        
        // Zet tekst van knop 
        spelerStaatklaarKnop.GetComponentInChildren<Text>().text = spelerKlaarTekst;
    }

    private void ToonScoreVoorBeurt()
    {
        scoreBeurt.text = spelData.kegels.BerekenScore().ToString();
    }
    
    // Gegevens die van de micro:bit komen worden hier verwerkt
    void OnSerialLine(string line) {
        Debug.Log("Ontvangen van micro:bit: " + line);
        
        if (spelActief)
        {
        
            /*
             * de micro:bit stuurt gegevens door in volgende vorm:
             *     kegel:toestand
             *     toestand = 0 of 1 -> 1 = recht, 0 = gevallen
             * -> bvb: "2:0" wil zeggen dat kegel 2 gevallen is
             * 
             * we splitsen die string op het ":" karakter
             * dan is het eerste element (= het 0-de element want lijstjes beginnen op 0), de kegel
             * en het tweede element (= het 1e element want lijstjes beginnen op 0), de toestand
             *
             * te verbeteren:
             *  
             */
            var data = line.Replace(Environment.NewLine, "").Trim().Split(':');
            
            // als er geen 2 elementen in zitten dan is er iets mis
            if (data.Length >= 2)
            {
                spelData.kegels.UpdateKegel(int.Parse(data[0]), (data[1] == "0"));      
                
                // Toon score voor deze beurt
                ToonScoreVoorBeurt();
                
                // Teller starten om einde van de beurt af te wachten
                StartEindeTeller();
            }
            else
            {
                Debug.LogError("Micro bit stuurde meer data dan verwacht: " + line);
            }
        }
    }
    
    
    
}
