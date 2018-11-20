using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameManager gm;
    public Vector3 cameraFollowOffset = new Vector3(0, 10, 0);
    public float zoomScale;
    public Camera followCamera;
    public float cameraTimeScale;

    private Vector3 center;
    private Vector3 playerVector;

    public void ChangeCamera(Player player)
    {
        if(followCamera == null)
        {
            followCamera = Camera.main;
        }

        followCamera.transform.position = player.transform.position + cameraFollowOffset;
    }

    private void Update()
    {
        ChangeCamera(gm.player1, gm.player2); // change camera
    }

    void ChangeCamera(Player player1, Player player2)
    {

        float zoomFactor;
        if(followCamera == null)
        {
            followCamera = Camera.main;
        }

        playerVector = (player2.transform.position - player1.transform.position);
        center = (playerVector / 2.0f) + player1.transform.position;

        zoomFactor = zoomScale * playerVector.magnitude;

        Vector3 currentposition = followCamera.transform.position;
        Vector3 newposition = center + cameraFollowOffset + (cameraFollowOffset * zoomFactor);
        followCamera.transform.position =  Vector3.Slerp(currentposition, newposition, cameraTimeScale*Time.deltaTime);
    }
}
