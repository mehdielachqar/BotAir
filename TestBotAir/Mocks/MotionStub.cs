using System;
using System.Linq;
using BotAir;

namespace TestBotAir.Mocks
{
    public class MotionStub : IMotion
    {
        private readonly int[] 
            dx = { 1, 0, -1,  0 },
            dy = { 0, 1,  0, -1 };
        private int x, y;
        private int direction;
        private bool[,] grid;

        public MotionStub(string map)
        {
            var lines = map.Split("\n");
            var init = false;
            var width = lines.Max(line => line.Length);

            grid = new bool[width, lines.Length];
            direction = 0;
            for (var y = 0; y<lines.Length; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    switch (lines[y][x])
                    {
                        case '>':
                            if (init)
                            {
                                throw new ArgumentException("Map must have only one '>'");
                            }
                            this.x = x;
                            this.y = y;
                            init = true;
                            goto case ' ';
                        case ' ': grid[x, y] = true; break;
                        case '*': grid[x, y] = false; break;
                        default:
                            throw new ArgumentOutOfRangeException("Unknown character pattern");
                    }
                }
                for(var x=lines[y].Length; x<width; x++)
                {
                    grid[x, y] = true;
                }
            }
            if(!init)
            {
                throw new ArgumentException("Map must have at a '>'");
            }
        }

        public bool Move(int distance)
        {
            if(distance<=0)
            {
                throw new ArgumentOutOfRangeException(nameof(distance), "distance must be strictly positive");
            }
            var newx = x;
            var newy = y;

            for(int i=0; i<distance; i++)
            {
                newx += dx[direction];
                newy += dy[direction];

                try
                {
                    if (!grid[newx, newy])
                    {
                        return false;
                    }
                }
                catch(IndexOutOfRangeException e)
                {
                    throw new InvalidOperationException("Distance too big", e);
                }
            }
            x = newx;
            y = newy;
            return true;
        }

        public void Rotate(int angle)
        {
            var abs = Math.Abs(angle);

            if (abs >= 360)
            {
                throw new NotSupportedException("Can only rotate with angles less than 360°");
            }
            if (abs%90 != 0)
            {
                throw new NotSupportedException("MotionStub cannot handle angles other than multiple of 90°");
            }
            direction = (direction+4+angle/90)%4;
        }

    }
}
