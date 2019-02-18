using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace
{
    public static class ScoreBordController
    {
        const string ScoreBordIp = @"192.168.1.1";

        public static async Task ToonScores(ScoreData scoreSpelerA, ScoreData scoreSpelerB)
        {
            // Zet de juiste scores op het scorebord
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://" + ScoreBordIp);

            var response = await client.GetAsync($@"ScoreA={scoreSpelerA.score}&ScoreB={scoreSpelerB.score}");
            if (!response.IsSuccessStatusCode)
            {
                Debug.Log("Scorebord wijzigen gaf fout: " + response.StatusCode);
            }
            client.Dispose();
        }

        public static async Task ActiveerSpeler(char speler)
        {
            // Toont welke speler actief is op het scorebord
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://" + ScoreBordIp);

            var response = await client.GetAsync($@"Speler={speler}");
            if (!response.IsSuccessStatusCode)
            {
                Debug.Log("Scorebord wijzigen gaf fout: " + response.StatusCode);
            }
            client.Dispose();  
        }
    }
}