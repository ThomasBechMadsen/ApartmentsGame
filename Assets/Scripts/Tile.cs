using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public enum Direction {North, South, East, West};
    public Wall wallPrefab;
    public GameObject pillarPrefab;

    public Map map;
    public Wall north;
    public Wall south;
    public Wall east;
    public Wall west;
    public GameObject northWest;
    public GameObject northEast;
    public GameObject southEast;
    public GameObject southWest;
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
            CreatePillars(dir);
        }
        return wall;
    }

    Wall CreateNorthWall()
    {
        Wall wall = Instantiate(wallPrefab, transform.position + new Vector3(0, wallPrefab.transform.position.y, 0.5f + map.tilePadding / 2), Quaternion.identity, transform);
        wall.transform.LookAt(transform.position + Vector3.up * wallPrefab.transform.position.y);
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
        Wall wall = Instantiate(wallPrefab, transform.position + new Vector3(0.5f + map.tilePadding / 2, wallPrefab.transform.position.y, 0), Quaternion.identity, transform);
        wall.transform.LookAt(transform.position + Vector3.up * wallPrefab.transform.position.y);
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
        Wall wall = Instantiate(wallPrefab, transform.position + new Vector3(0, wallPrefab.transform.position.y, -0.5f - map.tilePadding / 2), Quaternion.identity, transform);
        wall.transform.LookAt(transform.position + Vector3.up * wallPrefab.transform.position.y);
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
        Wall wall = Instantiate(wallPrefab, transform.position + new Vector3(-0.5f - map.tilePadding / 2, wallPrefab.transform.position.y, 0), Quaternion.identity, transform);
        wall.transform.LookAt(transform.position + Vector3.up * wallPrefab.transform.position.y);
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

    public void CreatePillars(Direction dir)
    {
        switch (dir)
        {
            case Direction.North:
                if (!northWest)
                {
                    CreateNortWest();
                }
                if (!northEast)
                {
                    CreateNorthEast();
                }
                break;
            case Direction.East:
                if (!northEast)
                {
                    CreateNorthEast();
                }
                if (!southEast)
                {
                    CreateSouthEast();
                }
                break;
            case Direction.South:
                if (!southWest)
                {
                    CreateSouthWest();
                }
                if (!southEast)
                {
                    CreateSouthEast();
                }
                break;
            case Direction.West:
                if (!northWest)
                {
                    CreateNortWest();
                }
                if (!southWest)
                {
                    CreateSouthWest();
                }
                break;
        }
    }

    GameObject CreateNortWest()
    {
        GameObject pillar = Instantiate(pillarPrefab, transform.position + new Vector3(-0.5f - map.tilePadding / 2, pillarPrefab.transform.position.y, 0.5f + map.tilePadding / 2), Quaternion.identity, transform);
        northWest = pillar;

        //Inform neighbours
        Tile left = map.getTile(mapPosition.x - 1, mapPosition.y);
        Tile up = map.getTile(mapPosition.x, mapPosition.y + 1);
        Tile adjacent = map.getTile(mapPosition.x - 1, mapPosition.y + 1);
        if (left)
        {
            left.northEast = pillar;
        }
        if (up)
        {
            up.southWest = pillar;
        }
        if (adjacent)
        {
            adjacent.southEast = pillar;
        }

        return pillar;
    }

    GameObject CreateNorthEast()
    {
        GameObject pillar = Instantiate(pillarPrefab, transform.position + new Vector3(0.5f + map.tilePadding / 2, pillarPrefab.transform.position.y, 0.5f + map.tilePadding / 2), Quaternion.identity, transform);
        northEast = pillar;

        //Inform neighbours
        Tile right = map.getTile(mapPosition.x + 1, mapPosition.y);
        Tile up = map.getTile(mapPosition.x, mapPosition.y + 1);
        Tile adjacent = map.getTile(mapPosition.x + 1, mapPosition.y + 1);
        if (right)
        {
            right.northWest = pillar;
        }
        if (up)
        {
            up.southEast = pillar;
        }
        if (adjacent)
        {
            adjacent.southWest = pillar;
        }

        return pillar;
    }

    GameObject CreateSouthWest()
    {
        GameObject pillar = Instantiate(pillarPrefab, transform.position + new Vector3(-0.5f - map.tilePadding / 2, pillarPrefab.transform.position.y, -0.5f - map.tilePadding / 2), Quaternion.identity, transform);
        southWest = pillar;

        //Inform neighbours
        Tile left = map.getTile(mapPosition.x - 1, mapPosition.y);
        Tile down = map.getTile(mapPosition.x, mapPosition.y - 1);
        Tile adjacent = map.getTile(mapPosition.x - 1, mapPosition.y - 1);
        if (left)
        {
            left.southEast = pillar;
        }
        if (down)
        {
            down.northWest = pillar;
        }
        if (adjacent)
        {
            adjacent.northEast = pillar;
        }

        return pillar;
    }

    GameObject CreateSouthEast()
    {
        GameObject pillar = Instantiate(pillarPrefab, transform.position + new Vector3(0.5f + map.tilePadding / 2, pillarPrefab.transform.position.y, -0.5f - map.tilePadding / 2), Quaternion.identity, transform);
        southEast = pillar;

        //Inform neighbours
        Tile right = map.getTile(mapPosition.x + 1, mapPosition.y);
        Tile down = map.getTile(mapPosition.x, mapPosition.y - 1);
        Tile adjacent = map.getTile(mapPosition.x + 1, mapPosition.y - 1);
        if (right)
        {
            right.southWest = pillar;
        }
        if (down)
        {
            down.northEast = pillar;
        }
        if (adjacent)
        {
            adjacent.northWest = pillar;
        }

        return pillar;
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
        GameManager.instance.ResetMoves();
    }

}
