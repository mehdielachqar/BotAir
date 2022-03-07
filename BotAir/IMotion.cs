using System;
using System.Collections.Generic;
using System.Text;

namespace BotAir
{
    public interface IMotion
    {
        void Rotate(int degree);

        bool Move(int distance);
    }
}
