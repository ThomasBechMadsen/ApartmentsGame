using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    public Vector2Int mapPosition;
    public Material playerColour;
    public int Moves { get; set; }
    public int Points { get; set; }
    
    /*public static Player Instantiate(Player prefab, Vector3 position, Quaternion rotation, Transform parent, Map map, Vector2Int mapPosition)
    {
        Player newPlayer = Instantiate<Player>(prefab, position, rotation, parent);
        newPlayer.mapPosition = mapPosition;
        return newPlayer;
    }*/

    void Start()
    {
        GetComponent<Renderer>().material = playerColour;
    }


}
