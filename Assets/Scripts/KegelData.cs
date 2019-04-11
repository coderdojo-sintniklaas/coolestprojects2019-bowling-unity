using System.Collections.Generic;
using System.Resources;

namespace DefaultNamespace
{
    public class Kegel
    {
        public int nummer;
        public int score;
        public bool gevallen;
    }
    
    public class KegelData
    {
        public List<Kegel> kegels;

        public KegelData()
        {
            // Maak lijst aan om kegels bij te houden
            kegels = new List<Kegel>();
            
            for (var i = 1; i < 7; i++)
            {
                // Maak nieuwe kegel toe
                var kegel = new Kegel();
                kegel.nummer = i;
                kegel.score = (i / 2) + 1;
                kegel.gevallen = false;

                // Voeg kegel toe aan lijst
                kegels.Add(kegel);
            }
        }

        public void Reset()
        {
            // zet alle kegels terug recht
            
            foreach (var kegel in kegels)
            {
                kegel.gevallen = false;
            }

        }

        public void UpdateKegel(int nummer, bool gevallen)
        {
            // -1 want lijst start bij 0
            kegels[nummer - 1].gevallen = gevallen;
        }

        public int BerekenScore()
        {
            // Totale score =
            // som van de punten van de individuele gevallen kegels
            // + 5 bonus punten, enkel als alle kegels gevallen zijn
            var score = 0;
            var aantalGevallen = 0;
            
            foreach (var kegel in kegels)
            {
                if (kegel.gevallen)
                {
                    score += kegel.score;
                    aantalGevallen += 1;
                }
            }

            // Bereken bonus indien nodig
            if (aantalGevallen == kegels.Count)
            {
                score += Instellingen.bonusPuntenAlsAlleKegelsGevallen;
            }

            return score;
        }
    }
}