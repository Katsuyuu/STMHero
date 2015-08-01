using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STMHero.Model;

namespace STMHero.Controler
{


    public class StateClass
    {
        public GameState gameState;
        public MenuState menuState;
        public MoreState moreState;

        public StateClass()
        {
            gameState = GameState.LOGO;
            menuState = MenuState.PLAY;
            moreState = MoreState.NAME;
        }
    }
}
