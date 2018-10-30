using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public Tile tilePrefab;
    public int sizeX;
    public int sizeY;
    public float tilePadding;

    public Tile[,] tiles;

    private void Start()
    {
        GenerateMap();
        Wall wall = new Wall();
//        tiles[1, 2].CreateWall(Tile.Direction.North);
  //      tiles[1, 2].CreateWall(Tile.Direction.South);
    //    tiles[1, 2].CreateWall(Tile.Direction.East);
      //  tiles[1, 2].CreateWall(Tile.Direction.West);
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
                    if (i == 0)
                    {
                        tiles[i, j].CreateWall(Tile.Direction.West);
                    }
                    if (i == sizeX - 1)
                    {
                        tiles[i, j].CreateWall(Tile.Direction.East);
                    }
                    if (j == 0)
                    {
                        tiles[i, j].CreateWall(Tile.Direction.South);
                    }
                    if (j == sizeY - 1)
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

                //matrix[x,y] = Mathf.RoundToInt( distanceToCenter);
            }
        }
        return matrix;
    }
}
