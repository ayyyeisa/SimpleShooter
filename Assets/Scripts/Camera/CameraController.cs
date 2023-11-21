/*****************************************************************************
// File Name : CameraController.cs
// Author : Isa Luluquisin
// Creation Date : November 18, 2023
//
// Brief Description : This controls the camera's behavior, making sure it follows 
                        the player and does not go out of bounds.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("References the player")]
    [SerializeField] Transform player;

    //camera's bounds
    private Bounds cameraBounds;

    //refers to the game's camera
    private Camera camera;

    //game bounds
    private Bounds gameBounds;

    private void Awake()
    {
        //sets the camera being referred to as the main camera
        camera = Camera.main;
    }

    private void Start()
    {
        //height and width are set to the camera's height and width
        var height = camera.orthographicSize;
        var width = height * camera.aspect;

        //the min and max width and height are set to based on the game's bounds and camera's bounds
        var minX = Globals.GameBounds.min.x + width;
        var maxX = Globals.GameBounds.extents.x - width;

        var minY = Globals.GameBounds.min.y + height;
        var maxY = Globals.GameBounds.extents.y - height;

        //creates a new bound object for the camera
        cameraBounds = new Bounds();
        //sets the camera's boundaries to the min and max x and y variables previously found
        cameraBounds.SetMinMax(
            new Vector3(minX, minY, -10f),
            new Vector3(maxX, maxY, -10f)
            );
    }

    private void Update()
    {
        //changes the position of the camera 
        transform.position = GetCameraBounds();
    }

    /// <summary>
    /// ensures that the camera follows the player, but the camera is limited to the game's bounds.
    /// The camera should stop moving when it "collides" with the edge of the background
    /// </summary>
    /// <returns>a vector of the where the camera should be</returns>
    private Vector3 GetCameraBounds()
    {
        return new Vector3(
            Mathf.Clamp(player.transform.position.x, cameraBounds.min.x, cameraBounds.max.x),
            Mathf.Clamp(player.position.y, cameraBounds.min.y, cameraBounds.max.y),
            transform.position.z
            );
    }
}
