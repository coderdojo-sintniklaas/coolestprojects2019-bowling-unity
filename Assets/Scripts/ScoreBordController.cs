using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace
{
    public static class ScoreBordController
    {
        const string ScoreBordIp = Instellingen.scoreServerIp;

        public static async Task ToonScores(int scoreSpelerA, int scoreSpelerB, char speler)
        {
            // Zet de juiste scores op het scorebord
            Debug.Log($@"Scorebord wijzigen: A->{scoreSpelerA}, B->{scoreSpelerB}, Actief->{speler} ");

            var client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri("http://" + ScoreBordIp);

                var response = client.GetAsync($@"/zetscore?ScoreA={scoreSpelerA}&ScoreB={scoreSpelerB}&Speler={speler}").Result;
                if (!response.IsSuccessStatusCode)
                {
                    Debug.LogError("Scorebord wijzigen gaf fout: " + response.StatusCode);
                }
                else
                {
                    var antwoord = response.Content.ReadAsStringAsync().Result;
                    Debug.Log("Scorebord wijzigen, antwoord van server: " + antwoord);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                client.Dispose();                
            }
        }
    }
}