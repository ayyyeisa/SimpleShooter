/*****************************************************************************
// File Name : SetBounds.cs
// Author : Isa Luluquisin
// Creation Date : November 18, 2023
//
// Brief Description : This sets the bounds of the gameobject it is attached to.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBounds : MonoBehaviour
{
    /// <summary>
    /// Sets the game's bounds when awake. Neither the player nor the camera can 
    /// be outside of the bounds
    /// </summary>
    private void Awake()
    {
        //gets the game object's children's renderers
        var renderers = gameObject.GetComponentsInChildren<Renderer>();
        //stores the bounds of the renderers in an array
        var bounds = renderers[0].bounds;
        for (var i = 1; i < renderers.Length; ++i)
        {
            bounds.Encapsulate(renderers[i].bounds);
        }
        //sets the game's bounds to the array
        Globals.GameBounds = bounds;
    }
}
