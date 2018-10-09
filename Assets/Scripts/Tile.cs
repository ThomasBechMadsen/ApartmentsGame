using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public enum Direction {North, South, East, West};
    public Wall wallPrefab;

    public Map map;
    public Wall north;
    public Wall south;
    public Wall east;
    public Wall west;

    public Wall CreateWall(Direction dir)
    {
        Wall wall = null;

        switch (dir)
        {
            case Direction.North:
                if (!north) {
                    wall = CreateNorthWall();
                }
                break;
            case Direction.East:
                if (!east)
                {
                    wall = CreateEastWall();
                }
                break;
            case Direction.South:
                if (!south)
                {
                    wall = CreateSouthWall();
                }
                break;
            case Direction.West:
                if (!west)
                {
                    wall = CreateWestWall();
                }
                break;
        }
        if (wall)
        {
            wall.front = this;
        }
        return wall;
    }

    Wall CreateNorthWall()
    {
        Wall wall = Instantiate<Wall>(wallPrefab, transform);
        north = wall;
        wall.transform.position = transform.position + new Vector3(0, 0, 0.5f);
        wall.transform.LookAt(transform.position + Vector3.forward);

        int posX = GetMatrixPositionX();
        int posY = GetMatrixPositionY() + 1;
        if (posY < map.sizeY)
        {
            Tile backTile = map.tiles[posX, posY];
            if (backTile)
            {
                wall.back = backTile;
            }
        }
        return wall;
    }
    
    Wall CreateEastWall()
    {
        Wall wall = Instantiate<Wall>(wallPrefab, transform);
        east = wall;
        wall.transform.position = transform.position + new Vector3(0.5f, 0, 0);
        wall.transform.LookAt(transform.position + Vector3.right);

        int posX = GetMatrixPositionX() + 1;
        int posY = GetMatrixPositionY();
        if (posX < map.sizeX)
        {
            Tile backTile = map.tiles[posX, posY];
            if (backTile)
            {
                wall.back = backTile;
            }
        }
        return wall;
    }

    Wall CreateSouthWall()
    {
        Wall wall = Instantiate<Wall>(wallPrefab, transform);
        south = wall;
        wall.transform.position = transform.position + new Vector3(0, 0, -0.5f);
        wall.transform.LookAt(transform.position + -Vector3.forward);

        int posX = GetMatrixPositionX();
        int posY = GetMatrixPositionY() - 1;
        if (posY >= 0)
        {
            Tile backTile = map.tiles[posX, posY];
            if (backTile)
            {
                wall.back = backTile;
            }
        }
        return wall;
    }

    Wall CreateWestWall()
    {
        Wall wall = Instantiate<Wall>(wallPrefab, transform);
        west = wall;
        wall.transform.position = transform.position + new Vector3(-0.5f, 0, 0);
        wall.transform.LookAt(transform.position + -Vector3.right);

        int posX = GetMatrixPositionX() - 1;
        int posY = GetMatrixPositionY();
        if (posX >= 0) {
            Tile backTile = map.tiles[posX, posY];
            if (backTile)
            {
                wall.back = backTile;
            }
        }
        return wall;
    }

    int GetMatrixPositionX()
    {
        return Mathf.RoundToInt(transform.position.x);
    }

    int GetMatrixPositionY()
    {
        return Mathf.RoundToInt(transform.position.z);
    }
}
