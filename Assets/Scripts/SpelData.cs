using DefaultNamespace;
using UnityEngine;

[System.Serializable]
public class SpelData
{
 /*   
    public enum Speler
    {
        SpelerA = 'A',
        SpelerB = 'B'
    }
*/    
    public SpelerData spelerA;
    public SpelerData spelerB;

    public SpelerData actieveSpeler;
    
    public KegelData kegels;
    
    public SpelData()
    {
        spelerA = new SpelerData('A');
        spelerB = new SpelerData('B');
        kegels = new KegelData();
    }
/*
    public void ActiveerSpeler(Speler speler)
    {
        if (speler == Speler.SpelerA)
        {
            actieveSpeler = spelerA;
        }
        else
        {
            actieveSpeler = spelerB;
        }
    }
*/    
    public void WisselSpeler()
    {
        if (actieveSpeler == null)
        {
            actieveSpeler = spelerA;
        } 
        else if (actieveSpeler == spelerA)
        {
            actieveSpeler = spelerB;
        }
        else
        {
            actieveSpeler = spelerA;
        }
        
        // Reset kegels -> allemaal terug recht
        kegels.Reset();

        // Tel eentje bij huidige beurt
        actieveSpeler.huidigeBeurt += 1;
    }
     
}