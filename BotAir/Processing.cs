using System;
using System.Collections.Generic;

namespace BotAir
{
    public class Processing
    {
        private IMotion motion;
        private ZoneState[,] grid;

        public Processing(int width, int height, IMotion motion)
        {
            this.motion = motion;
            this.grid = new ZoneState[height, width];
            for (int i = 0; i < width; i++)
            {
                grid[0, i] = grid[height - 1, i] = ZoneState.Obstructed;
            }
            for (int i = 0; i < height; i++)
            {
                grid[i, 0] = grid[i, width - 1] = ZoneState.Obstructed;
            }
            grid[1, 1] = ZoneState.Free;
        }
        public async IEnumerable<ZoneState> Scan()
        {
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    bool acc = motion.Move(1);
                    if (acc)
                    {
                        grid[i, j] = 1;
                    }
                    else
                    {
                        grid[i, j] = -1;
                        motion.Rotate(-90);
                    }
                }
                motion.Rotate(-90);
            }
            for (int i = height - 1; i > 1; i--)
            {
                for (int j = width - 1; j > 1; j--)
                {
                    bool acc = motion.Move(1);
                    if (acc)
                    {
                        grid[i, j] = 1;
                    }
                    else
                    {
                        grid[i, j] = -1;
                        motion.Rotate(-90);
                    }
                }
                motion.Rotate(-90);
            }
            return Array.Empty<ZoneState>();
        }
    }
}
