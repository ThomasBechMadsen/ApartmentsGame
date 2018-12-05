using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {
    
    public int sizeX;
    public int sizeY;
    public bool hasAI;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}