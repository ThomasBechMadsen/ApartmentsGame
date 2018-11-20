using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public GameManager gm;
    public Tile tilePrefab;
    public int sizeX;
    public int sizeY;
    public float tilePadding;

    public int unclaimedTiles = 0;


    public Tile[,] tiles;

    private void Start()
    {
        GenerateMap();
        SetPlayerStartPosition();
    }

    void GenerateMap()
    {
        tiles = new Tile[sizeX, sizeY];
        int[,] map = GenerateMatrix(sizeX, sizeY);

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                if (map[i, j] == 0)
                {
                    unclaimedTiles++;
                    tiles[i, j] = Tile.Instantiate(tilePrefab, new Vector3(i + tilePadding * i, 0, j + tilePadding * j), Quaternion.Euler(90, 0, 0), transform, this, new Vector2Int(i, j));
                    if (i == 0 || i - 1 >= 0 && map[i - 1, j] == 1)
                    {
                        tiles[i, j].CreateWall(Tile.Direction.West, null).destructable = false;
                    }
                    if (i == sizeX - 1 || i + 1 < sizeX && map[i + 1, j] == 1)
                    {
                        tiles[i, j].CreateWall(Tile.Direction.East, null).destructable = false;
                    }
                    if (j == 0 || j - 1 >= 0 && map[i, j - 1] == 1)
                    {
                        tiles[i, j].CreateWall(Tile.Direction.South, null).destructable = false;
                    }
                    if (j == sizeY - 1 || j + 1 < sizeY && map[i, j + 1] == 1)
                    {
                        tiles[i, j].CreateWall(Tile.Direction.North, null).destructable = false; ;
                    }
                          
                }
            }
        }
    }

    private int[,] GenerateMatrix(int n, int m)
    {
        int[,] matrix = new int[n, m];

        Vector2 shift = new Vector2(0, 0);
        float zoom = 0.1f;

        if (n <= 5 || m <= 5)
        {
            zoom = 0.001f;
        }

        int newNoise = Random.Range(0, 10000);
        //int newNoise = 3;

        int centerX = Mathf.RoundToInt(n / 2);
        int centerY = Mathf.RoundToInt(m / 2);

        for (int x = 0; x < n; x++)
        {
            for (int y = 0; y < m; y++)
            {
                float distanceX = (centerX - x) * (centerX - x);
                float distanceY = (centerY - y) * (centerY - y);

                float distanceToCenter = Mathf.Sqrt(distanceX + distanceY);

                distanceToCenter = distanceToCenter / n;

                Vector2 pos = zoom * (new Vector2(x, y)) + shift;
                float noise = Mathf.PerlinNoise(pos.x + newNoise, pos.y + newNoise);

                if (noise < distanceToCenter)
                {
                    matrix[x, y] = 1;
                }
                else
                {
                    matrix[x, y] = 0;
                }
            }
        }


        matrix = new Application.Island(n, m, matrix).FindLargestIsland();
        return matrix;
    }



    private void SetPlayerStartPosition()
    {
        int centerX = Mathf.RoundToInt(sizeX / 2);
        int centerY = Mathf.RoundToInt(sizeY / 2);




        Vector2Int player1position = SetPlayer(centerX, centerY, new bool[sizeX, sizeY], new Vector2Int());

        // insert player1
        gm.player1.transform.position = new Vector3(player1position.x + tilePadding*player1position.x, 0.5f, player1position.y + tilePadding*player1position.y);
        gm.player1.mapPosition = player1position;


        Vector2Int player2position = SetPlayer(centerX, centerY, new bool[sizeX, sizeY], player1position);

        // insert player2
        gm.player2.transform.position = new Vector3(player2position.x + tilePadding * player2position.x,  0.5f, player2position.y + tilePadding * player2position.y);
        gm.player2.mapPosition = player2position;



    }

    Vector2Int SetPlayer(int u, int v, bool[,] visited, Vector2Int playerPosition)
    {   
        int[] dx = {0,  -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dy = {0,  -1, 0, 1, -1, 1, -1, 0, 1 };

        if (!visited[u, v])
        {
            visited[u, v] = true;
        }

        // search for 4 neighbours
        for (int i = 0; i < 9; i++)
        {
            int x = u + dx[i];
            int y = v + dy[i];

            if (x < 0 || x >= sizeX || y < 0 || y >= sizeY)
            {
                continue;
            }

            if (visited[x, y] || tiles[x, y] == null)
            {
                continue;
            }

            visited[x, y] = true;

            if (tiles[x, y] && (playerPosition.x != x && playerPosition.y != y))
            {
                return new Vector2Int(x, y);
            }
            else
            {
                return SetPlayer(x, y, visited, playerPosition);
            }
        }

        return new Vector2Int();
    }
}
