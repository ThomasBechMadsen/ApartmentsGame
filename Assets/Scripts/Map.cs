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
        tiles[1, 2].CreateWall(Tile.Direction.North);
        tiles[1, 2].CreateWall(Tile.Direction.South);
        tiles[1, 2].CreateWall(Tile.Direction.East);
        tiles[1, 2].CreateWall(Tile.Direction.West);
    }

    void GenerateMap()
    {
        tiles = new Tile[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                tiles[i, j] = Tile.Instantiate(tilePrefab, new Vector3(i + tilePadding * i, 0, j + tilePadding * j), Quaternion.Euler(90, 0, 0), transform, this, new Vector2Int(i, j));
                if (i == 0)
                {
                    tiles[i, j].CreateWall(Tile.Direction.West);
                }
                if (i == sizeX-1)
                {
                    tiles[i, j].CreateWall(Tile.Direction.East);
                }
                if (j == 0)
                {
                    tiles[i, j].CreateWall(Tile.Direction.South);
                }
                if (j == sizeY-1)
                {
                    tiles[i, j].CreateWall(Tile.Direction.North);
                }
            }
        }
    }
}
