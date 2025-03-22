using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2
{
    internal class Conway
    {
        public bool isLooped;

        private bool[,] board;
        public int mX { get; private set; }
        public int mY { get; private set; }
        public int Generation { get; private set; }

        public Conway(int x, int y)
        {
            board = new bool[x, y];
            board[1, 0] = true;
            board[2, 1] = true;
            board[0, 2] = true;
            board[1, 2] = true;
            board[2, 2] = true;
            mX = x;
            mY = y;
        }

        public int NextTick()
        {
            int[,] neighbors = new int[mX, mY];

            for (int i = 0; i < mY; i++)
                for (int j = 0; j < mX; j++)
                {
                    neighbors[j, i] = FindNeighbors(j, i);
                }

            for (int i = 0; i < mY; i++)
                for(int j = 0; j < mX; j++)
                {
                    if (board[j, i])
                    {
                        if (neighbors[j, i] < 2 || neighbors[j, i] > 3)
                            board[j, i] = false;
                    }
                    else
                    {
                        if (neighbors[j, i] == 3)
                            board[j, i] = true;
                    }
                }
            Generation++;
            return Generation;
        }

        public void Toggle(int x, int y)
        {
            int cX = Clamp(x, mX);
            int cY = Clamp(y, mY);
            if (board[cX, cY])
                board[cX, cY] = false;
            else
                board[cX, cY] = true;
        }

        public bool[,] GetBoard()
        {
            return board;
        }

        private int FindNeighbors(int x, int y)
        {
            int num = 0;

            for (int i = y - 1; i <= y + 1; i++)
                for (int j = x - 1; j <= x + 1; j++)
                    if (!(i == y && j == x))
                    {
                        if (isLooped)
                        {
                            if (board[Clamp(j, mX), Clamp(i, mY)])
                                num++;
                        }
                        else
                        {
                            int nCx = j % mX;
                            int nCy = i % mY;
                            if (!(nCx < 0 || nCx > mX || nCy < 0 || nCy > mY))
                                if (board[nCx, nCy])
                                    num++;
                        }
                    }

            return num;
        }

        private int Clamp(int num, int max)
        {
            if(num < 0)
                num = max + (num % max);

            if(num >= max)
                num %= max;

            
            return num;
        }

    }
}
