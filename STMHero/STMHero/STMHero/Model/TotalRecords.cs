using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STMHero.Model
{

    [Serializable]
    public class TotalRecords
    {
        public List<GameResult> results;
        public void SetList(List<GameResult> result)
        {
            results = result;
        }
    }
}
