using System;
using UnityEngine;

/// <summary>
/// For card data and card operations.
/// </summary>


[RequireComponent(typeof(Collider))]
public class Card : MonoBehaviour, IDrag
{
    public bool IsDraggable { get; private set; } = true;

    public bool Dragging { get; set; }

    private Vector3 _dragOriginPosition;  

    // Unused for the moment.
    public void OnPointerEnter(Vector3 position) { }
    public void OnPointerExit(Vector3 position)  { }

    /// <summary> Drag begins. </summary>
    /// <param name="position">Mouse position.</param>
    public void OnBeginDrag(Vector3 position)
    {
        _dragOriginPosition = transform.position; 
        // We raise the card to the height indicated by 'position'.
        transform.position = new Vector3(transform.position.x,
            position.y,
            transform.position.z);
    }
    
  
    /// <summary>A drag is being made. </summary>
    /// <param name="deltaPosition"> Mouse offset position. </param>
    /// <param name="droppable">Object on which a drop may be made, or null.</param>
    public void OnDrag(Vector3 deltaPosition, IDrop droppable)
    { 
        deltaPosition.y = 0.0f;
        // We move the card.
        transform.position += deltaPosition;
    }

    /// <summary> The drag operation is completed. </summary>
    /// <param name="position">Mouse position.</param>
    /// <param name="droppable">Object on which a drop may be made, or null.</param>
    public void OnEndDrag(Vector3 position, IDrop droppable)
    {
        if (droppable is {IsDroppable: true} && droppable.AcceptDrop(this) == true)
        {
            transform.position = new Vector3(transform.position.x,
                position.y,
                transform.position.z);
        } 
        else
        {
            transform.position = _dragOriginPosition;
        }
    }
  
    private void OnEnable()
    {
        _dragOriginPosition = transform.position;
    }
}
