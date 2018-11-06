using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public Tile tilePrefab;
    public int sizeX;
    public int sizeY;
    public float tilePadding;
    public Player player1;
    public Player player2;

    public Tile[,] tiles;

    private void Start()
    {
        GenerateMap();
        //SetPlayerStartPosition();
        Wall wall = new Wall();
        //tiles[1, 2].CreateWall(Tile.Direction.North);
        //tiles[1, 2].CreateWall(Tile.Direction.South);
        //tiles[1, 2].CreateWall(Tile.Direction.East);
        //tiles[1, 2].CreateWall(Tile.Direction.West);
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
                    tiles[i, j] = Tile.Instantiate(tilePrefab, new Vector3(i + tilePadding * i, 0, j + tilePadding * j), Quaternion.Euler(90, 0, 0), transform, this, new Vector2Int(i, j));
                    if (i == 0 || i - 1 >= 0 && map[i - 1, j] == 1)
                    {
                        tiles[i, j].CreateWall(Tile.Direction.West);
                    }
                    if (i == sizeX - 1 || i + 1 < sizeX && map[i + 1, j] == 1)
                    {
                        tiles[i, j].CreateWall(Tile.Direction.East);
                    }
                    if (j == 0 || j - 1 >= 0 && map[i, j - 1] == 1)
                    {
                        tiles[i, j].CreateWall(Tile.Direction.South);
                    }
                    if (j == sizeY - 1 || j + 1 < sizeY && map[i, j + 1] == 1)
                    {
                        tiles[i, j].CreateWall(Tile.Direction.North);
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

        int newNoise = Random.Range(0, 10000);
        //int newNoise = 3;

        int centerX = Mathf.RoundToInt(n / 2);
        int centerY = Mathf.RoundToInt(m / 2);

        for(int x = 0; x < n; x++)
        {
            for(int y = 0; y < m; y++)
            {
                float distanceX = (centerX - x) * (centerX - x);
                float distanceY = (centerY - y) * (centerY - y);

                float distanceToCenter =  Mathf.Sqrt(distanceX + distanceY);

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

        // insert player1
        while(true)
        {
            if (tiles[centerX, centerY] != null)
            {
                player1.transform.position = new Vector3(centerX, 0.5f, centerY);
                player1.mapPosition = new Vector2Int(centerX, centerY);
                break;
            }
            else
            {
                // TODO 
                if(true)
                {

                }
                 
            }
        }

        int player1PositionX = player1.mapPosition.x + 2;
        int player2PositionY = player1.mapPosition.y + 2;

        while(true)
        {
            if (tiles[player1PositionX, player2PositionY] != null)
            {
                player2.transform.position = new Vector3(player1PositionX, 0.5f, player2PositionY);
                player2.mapPosition = new Vector2Int(player1PositionX, player2PositionY);
                break;
            }
            else
            {
                // TODO
                if(true)
                {

                }
            }
        }



    }
}
