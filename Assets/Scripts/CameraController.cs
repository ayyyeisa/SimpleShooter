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
    [SerializeField] Transform player;
    private Bounds cameraBounds;
    private Camera camera;

    private Bounds gameBounds;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Start()
    {
        var height = camera.orthographicSize;
        var width = height * camera.aspect;

        var minX = Globals.GameBounds.min.x + width;
        var maxX = Globals.GameBounds.extents.x - width;

        var minY = Globals.GameBounds.min.y + height;
        var maxY = Globals.GameBounds.extents.y - height;

        cameraBounds = new Bounds();
        cameraBounds.SetMinMax(
            new Vector3(minX, minY, -10f),
            new Vector3(maxX, maxY, -10f)
            );
    }

    private void Update()
    {
        transform.position = GetCameraBounds();
    }

    private Vector3 GetCameraBounds()
    {
        return new Vector3(
            Mathf.Clamp(player.transform.position.x, cameraBounds.min.x, cameraBounds.max.x),
            Mathf.Clamp(player.position.y, cameraBounds.min.y, cameraBounds.max.y),
            transform.position.z
            );
    }
}
