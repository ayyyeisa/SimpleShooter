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
    private void Awake()
    {
        var renderers = gameObject.GetComponentsInChildren<Renderer>();
        var bounds = renderers[0].bounds;
        for (var i = 1; i < renderers.Length; ++i)
        {
            bounds.Encapsulate(renderers[i].bounds);
        }
        Globals.GameBounds = bounds;
    }
}
