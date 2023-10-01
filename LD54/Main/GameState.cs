using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD54.Main
{
    internal abstract class GameState
    {
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update(double elapsed);
        public abstract void Draw(float deltaTime);
    }
}
