using Microsoft.Xna.Framework.Input;
using STMHero.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STMHero.GameLogic
{
    public interface IGame
    {
        bool End { get; }
        GameResult Results { get; set; }
        void Update();
        void Update(KeyboardState keyboardState);
        void Draw();
    }
}
