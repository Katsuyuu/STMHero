using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
namespace STMHero.Model
{
    [Serializable]
    public class SongManager
    {
        public string name { get; set; }
        public string desription { get; set; }
        public Song curretSong { get; set; }
        public List<Button> songComposition = new List<Button>();      
 
    }
}
