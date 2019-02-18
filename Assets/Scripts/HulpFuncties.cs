using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using Object = System.Object;

namespace DefaultNamespace
{
    public static class HulpFuncties
    {

        public static void BewaarGegevensInBestand(Object gegevensObject, string bestandsNaam)
        {
            // Volledig pad samenstellen
            var volledigPad = Path.Combine(Application.dataPath, bestandsNaam);
            
            // Object serializeren naar bestand
            using (var file = File.CreateText(volledigPad))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, gegevensObject);
            }
        }
        
        public static T LaadGegevensUitBestand<T>(string bestandsNaam)
        {
            // Volledig pad samenstellen
            var volledigPad = Path.Combine(Application.dataPath, bestandsNaam);
            
            if (File.Exists(volledigPad))
            {
                using (var file = File.OpenText(volledigPad))
                {
                    var serializer = new JsonSerializer();
                    return (T)serializer.Deserialize(file, typeof(T));
                }
            }
            else
            {
                throw new FileNotFoundException("Bestand niet gevonden");
            }
        }
    }
}