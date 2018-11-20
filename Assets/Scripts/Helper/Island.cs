using System;
using System.Collections.Generic;
namespace Application
{
    public class Island
    {
        readonly bool[,] visited;
        readonly int[] dx = { -1, 0, 1, 0 };
        readonly int[] dy = { 0, 1, 0, -1 };
        int max_area = 0;

        public int Row
        {
            get;
            private set;
        }

        public int Col
        {
            get;
            private set;
        }

        public int[,] Map
        {
            get;
            private set;
        }

        public Island(int row, int col, int[,] map)
        {
            Row = row;
            Col = col;
            visited = new bool[row, col];
            Map = map;
        }


        void Dfs(int u, int v)
        {
            if (!visited[u, v])
            {
                visited[u, v] = true;
                max_area++;
            }

            // search for 4 neighbours
            for (int i = 0; i < 4; i++)
            {
                int x = u + dx[i];
                int y = v + dy[i];

                if(x < 0 || x >= Row || y < 0 || y >= Col)
                {
                    continue;
                }

                if(visited[x,y] || Map[x, y] == 1 )
                {
                    continue;
                }

                visited[x, y] = true;
                max_area++;
                Dfs(x, y);
            }

        }

        public int[,] FindLargestIsland()
        {
            int answer = 0;
            int bigx = -1;
            int bigy = -1;
            List<int> list = new List<int>();

            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    if (Map[i, j] == 0 && !visited[i, j])
                    {

                        Dfs(i, j);

                        list.Add(max_area);

                        if(answer < max_area)
                        {
                            // remove prev island
                            if(bigx != -1 && bigy != -1)
                            {
                                RemoveIsland(bigx, bigy, new bool[Row, Col]);
                            }

                            bigx = i;
                            bigy = j; 

                            answer = max_area;
                        }
                        else if(answer != 0)
                        {
                           RemoveIsland(i, j, new bool[Row, Col]);
                        }
                        max_area = 0;
                    }
                }
            }

            return Map;
        }

        void RemoveIsland(int u, int v, bool[,] visited)
        {
            if (!visited[u, v])
            {
                visited[u, v] = true;
                Map[u, v] = 1;
            }

            // search for 4 neighbours
            for (int i = 0; i < 4; i++)
            {
                int x = u + dx[i];
                int y = v + dy[i];

                if (x < 0 || x >= Row || y < 0 || y >= Col)
                {
                    continue;
                }

                if (visited[x, y] || Map[x, y] == 1)
                {
                    continue;
                }

                visited[x, y] = true;
                Map[x, y] = 1;
                RemoveIsland(x, y, visited);
            }
        }
    }
}
