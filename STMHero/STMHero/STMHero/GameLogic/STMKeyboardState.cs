using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace STMHero.GameLogic
{
    public struct STMKeyboardState
    {
        public bool A ;
        public bool S ;
        public bool D ;
        public bool F ;

        
        public bool IsKeyDown(Keys key)
        {
            switch (key)
            {
                case Keys.A:
                    return A;
                case Keys.S:
                    return S;
                case Keys.D:
                    return D;
                case Keys.F:
                    return F;
                default:
                    return false;
            }
        }
    }
}
