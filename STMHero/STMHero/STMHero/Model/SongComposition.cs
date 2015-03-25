using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STMHero.Model
{
    public class SongManager
    {
        public string name { get; set; }
        public string desription { get; set; }
        public string songPath { get; set; }
        public List<Button> songComposition = new List<Button>();
        public SongManager()
        {
            loadSong();
        }
        private void loadSong()
        {
            //todo
            //wczytywanie listy z pliku lel
        }
    }
}
