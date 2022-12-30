using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Accept draggable objects.
/// </summary>
public class Floor : MonoBehaviour, IDrop
{

    public bool IsDroppable => true;

    // We accept all IDrags.
    public bool AcceptDrop(IDrag drag) => true;

    public void OnDrop(IDrag drag) { }
}

