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

    public Vector2Int mapPosition;

    public static Tile Instantiate(Tile prefab, Vector3 position, Quaternion rotation, Transform parent, Map map, Vector2Int mapPosition)
    {
        Tile newTile = Instantiate<Tile>(prefab, position, rotation, parent);
        newTile.map = map;
        newTile.mapPosition = mapPosition;
        return newTile;
    }

    public Wall CreateWall(Direction dir, Player creator)
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
            if (creator != null && wall.back.north && wall.back.south && wall.back.east && wall.back.west)
            {
                ClaimTile(wall.back, creator);
            }
        }
        
        return wall;
    }

    Wall CreateNorthWall()
    {
        Wall wall = Instantiate(wallPrefab, transform.position + new Vector3(0, 0, 0.5f + map.tilePadding / 2), Quaternion.identity, transform);
        wall.transform.LookAt(transform.position + Vector3.forward);
        north = wall;

        int posX = mapPosition.x;
        int posY = mapPosition.y + 1;
        if (posY < map.sizeY)
        {
            Tile backTile = map.tiles[posX, posY];
            if (backTile)
            {
                wall.back = backTile;
                backTile.south = wall;
            }
        }
        return wall;
    }
    
    Wall CreateEastWall()
    {
        Wall wall = Instantiate(wallPrefab, transform.position + new Vector3(0.5f + map.tilePadding / 2, 0, 0), Quaternion.identity, transform);
        wall.transform.LookAt(transform.position + Vector3.right);
        east = wall;

        int posX = mapPosition.x + 1;
        int posY = mapPosition.y;
        if (posX < map.sizeX)
        {
            Tile backTile = map.tiles[posX, posY];
            if (backTile)
            {
                wall.back = backTile;
                backTile.west = wall;
            }
        }
        return wall;
    }

    Wall CreateSouthWall()
    {
        Wall wall = Instantiate(wallPrefab, transform.position + new Vector3(0, 0, -0.5f - map.tilePadding / 2), Quaternion.identity, transform);
        wall.transform.LookAt(transform.position + -Vector3.forward);
        south = wall;
        
        int posX = mapPosition.x;
        int posY = mapPosition.y - 1;
        if (posY >= 0)
        {
            Tile backTile = map.tiles[posX, posY];
            if (backTile)
            {
                wall.back = backTile;
                backTile.north = wall;
            }
        }
        return wall;
    }

    Wall CreateWestWall()
    {
        Wall wall = Instantiate(wallPrefab, transform.position + new Vector3(-0.5f - map.tilePadding / 2, 0, 0), Quaternion.identity, transform);
        wall.transform.LookAt(transform.position + -Vector3.right);
        west = wall;
        
        int posX = mapPosition.x - 1;
        int posY = mapPosition.y;
        if (posX >= 0) {
            Tile backTile = map.tiles[posX, posY];
            if (backTile)
            {
                wall.back = backTile;
                backTile.east = wall;
            }
        }
        return wall;
    }

    public bool DestroyWall(Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                if (north && north.destructable)
                {
                    Destroy(north.gameObject);
                    if (mapPosition.y + 1 < map.sizeY)
                    {
                        map.tiles[mapPosition.x, mapPosition.y + 1].south = null;
                    }
                    return true;
                }
                break;
            case Direction.East:
                if (east && east.destructable)
                {
                    Destroy(east.gameObject);
                    if (mapPosition.x + 1 < map.sizeX)
                    {
                        map.tiles[mapPosition.x + 1, mapPosition.y].west = null;
                    }
                    return true;
                }
                break;
            case Direction.South:
                if (south && south.destructable)
                {
                    Destroy(south.gameObject);
                    if (mapPosition.y - 1 > 0)
                    {
                        map.tiles[mapPosition.x, mapPosition.y - 1].north = null;
                    }
                    return true;
                }
                break;
            case Direction.West:
                if (west && west.destructable)
                {
                    Destroy(west.gameObject);
                    if (mapPosition.x - 1 > 0)
                    {
                        map.tiles[mapPosition.x - 1, mapPosition.y].east = null;
                    }
                    return true;
                }
                break;
        }
        return false;
    }

    public static void ClaimTile(Tile tile, Player player)
    {
        player.Points++;
        tile.GetComponent<Renderer>().material = player.playerColour;
        tile.north.destructable = false;
        tile.east.destructable = false;
        tile.south.destructable = false;
        tile.west.destructable = false;
        tile.map.unclaimedTiles--;
        tile.map.gm.ResetMoves();
    }

}
