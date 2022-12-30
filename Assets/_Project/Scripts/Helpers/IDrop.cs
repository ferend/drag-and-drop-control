using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Accept draggable objects.
/// </summary>
public interface IDrop
{
    public bool IsDroppable { get; }

    public bool AcceptDrop(IDrag drag);

    public void OnDrop(IDrag drag);
}

