using UnityEngine;
using UnityEditor;

namespace DefaultNamespace
{
    public static class Instellingen
    {
        /*
         * Geen best practice om alle instellingen op deze manier te doen
         * dat maakt de code niet herbruikbaar buiten dit project
         * (tenzij daar uiteraard ook een static Instellingen klasse bestaat)
         */
       
        
        public const string scoreServerIp = "192.168.1.102";
        public static float tijdNaWorp = 3.0f;
        public static int aantalBeurtenPerSpeler = 3;
        public static int bonusPuntenAlsAlleKegelsGevallen = 5;
    }
}