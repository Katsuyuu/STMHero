using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STMHero.Model
{
     [Serializable]
    public class GameResult
    {
        public string Name { get; set; }
        public uint TotalCorrectClicks { get;  set; }
        public uint CurretScore { get;  set; }
        public uint CurretCombo { get; set; }
        public uint MaxCombo { get;set; }
        public uint Points;
        public bool newResult;

        public GameResult()
        {
            TotalCorrectClicks = 0;
            CurretCombo = 0;
            CurretScore = 0;
            MaxCombo = 0;
            Points = 10;
        }
        internal void AddClick()
        {
            TotalCorrectClicks++;
        }

        public void AddCombo()
        {
            CurretCombo++;
        }
        internal void LostCombo()
        {
            CurretCombo = 0;
        }
        internal void AddPoints(uint points)
        {
            CurretScore += points;
        }
        internal void SetNewHighScore(uint CurretCombo)
        {
            MaxCombo = CurretCombo;
        }

    }
}
