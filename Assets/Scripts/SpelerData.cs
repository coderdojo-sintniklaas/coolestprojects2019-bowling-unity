namespace DefaultNamespace
{
    public class SpelerData
    {
        public int huidigeBeurt = 0;
        public int aantalBeurten = Instellingen.aantalBeurtenPerSpeler;

        public char code;
        public string naam;
        public int score = 0;
        
        public ScoreData scoreData { get { return new ScoreData{naam = this.naam, score = this.score}; }}

        public SpelerData(char code)
        {
            this.code = code;
        }
    }
}