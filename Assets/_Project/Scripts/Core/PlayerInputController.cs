using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : DragDropMechanic
{
    
    [SerializeField] private LayerMask raycastMask;
    private float _dragSpeed = 1.0f;
    [SerializeField, Range(0.0f, 10.0f)]
    private float _height = 1.0f;
    private Vector2 _cardSize;
        
    private IDrag _currentDrag;
    
    private IDrag _possibleDrag;
    
    private Transform _currentDragTransform; 
    
    private const int HitsCount = 5;
    
    private readonly RaycastHit[] _raycastHits = new RaycastHit[5];
    
    private readonly RaycastHit[] _cardHits = new RaycastHit[4];

    private Ray _mouseRay;

    private Vector3 _oldMouseWorldPosition;

    
    protected override void Action()
    {
        DragAndDropCard();
    }
    

    private void DragAndDropCard()
     {
         if (_currentDrag == null)
         {
             IDrag draggable = DetectDraggable();

             if (Input.GetMouseButtonDown(0) == true)
             {
                 // Is there an IDrag object under the mouse pointer?
                 if (draggable != null)
                 {
                     _currentDrag = draggable;
                     //currentDragTransform = hit;
                     _oldMouseWorldPosition = MousePositionToWorldPoint();
                     Cursor.visible = false;
                     Cursor.lockState = CursorLockMode.Confined;

                     // The drag operation begins.
                     _currentDrag.Dragging = true;
                     _currentDrag.OnBeginDrag(new Vector3(_raycastHits[0].point.x, _raycastHits[0].point.y + _height, _raycastHits[0].point.z));
                 }
             }
             else
             { 
                 if (draggable != null && _possibleDrag == null)
                 {
                     _possibleDrag = draggable;
                     _possibleDrag.OnPointerEnter(_raycastHits[0].point);
                 }

                 // We are leaving an IDrag?
                 if (draggable == null && _possibleDrag != null)
                 {
                     _possibleDrag.OnPointerExit(_raycastHits[0].point);
                     _possibleDrag = null;
                     ResetCursor();
                 }
             }
         }
         else
         {
             IDrop droppable = DetectDroppable();

             if (Input.GetMouseButton(0) == true)
             {
                 Vector3 mouseWorldPosition = MousePositionToWorldPoint();
                 Vector3 offset = (mouseWorldPosition - _oldMouseWorldPosition) * _dragSpeed;
                 _currentDrag.OnDrag(offset, droppable);
                 _oldMouseWorldPosition = mouseWorldPosition;
             }
             else if (Input.GetMouseButtonUp(0) == true)
             {

                 _currentDrag.Dragging = false;
                 _currentDrag.OnEndDrag(_raycastHits[0].point, droppable);
                 _currentDrag = null;
                 _currentDragTransform = null;
                 // We return the mouse icon to its normal state.
                 Cursor.visible = true;
                 Cursor.lockState = CursorLockMode.None;
             }
         }
     }
     
    private Vector3 MousePositionToWorldPoint()
    {
        Vector3 mousePosition = Input.mousePosition;
        if (Camera.main.orthographic == false)
            mousePosition.z = 10.0f;

        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
    
    private void ResetCursor() => Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    
    /// <summary>
    /// Returns the Transform of the object closest to the origin
    /// of the ray.
    /// </summary>
    /// <returns>Transform or null if there is no impact.</returns>
    private Transform MouseRaycast()
    {
        Transform hit = null;
        if (Physics.RaycastNonAlloc(_mouseRay,
                _raycastHits,
                Camera.main.farClipPlane,
                raycastMask) > 0)
        {
            System.Array.Sort(_raycastHits,
                (x, y) => x.distance.CompareTo(y.distance));
            hit = _raycastHits[0].transform;
        }

        return hit;
    }  
    
    public IDrag DetectDraggable()
    {
        IDrag draggable = null;

        _mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Transform hit = MouseRaycast();
        if (hit != null)
        {
            draggable = hit.GetComponent<IDrag>();
            if (draggable is { IsDraggable: true })
                _currentDragTransform = hit;
            else
                draggable = null;
        }

        return draggable;
    }
    
    private IDrop DetectDroppable()
    {
        IDrop droppable = null;
    
        // The four corners of the card.
        Vector3 cardPosition = _currentDragTransform.position;
        Vector2 halfCardSize = _cardSize * 0.5f;
        Vector3[] cardConner =
        {
            new(cardPosition.x + halfCardSize.x, cardPosition.y, cardPosition.z - halfCardSize.y),
            new(cardPosition.x + halfCardSize.x, cardPosition.y, cardPosition.z + halfCardSize.y),
            new(cardPosition.x - halfCardSize.x, cardPosition.y, cardPosition.z - halfCardSize.y),
            new(cardPosition.x - halfCardSize.x, cardPosition.y, cardPosition.z + halfCardSize.y)
        };

        int cardHitIndex = 0;
        Array.Clear(_cardHits, 0, _cardHits.Length);

        // We launch the four rays.
        for (int i = 0; i < cardConner.Length; ++i)
        {
            Ray ray = new(cardConner[i], Vector3.down);

            int hits = Physics.RaycastNonAlloc(ray, _raycastHits, Camera.main.farClipPlane, raycastMask);
            if (hits > 0)
            {
                // We order the impacts by distance from the origin of the ray.
                Array.Sort(_raycastHits, (x, y) => x.transform != null ? x.distance.CompareTo(y.distance) : -1);

                // We are only interested in the closest one.
                _cardHits[cardHitIndex++] = _raycastHits[0];
            }
        }

        if (cardHitIndex > 0)
        {
            // We are looking for the nearest possible IDrop.
            Array.Sort(_cardHits, (x, y) => x.transform != null ? x.distance.CompareTo(y.distance) : -1);

            if (_cardHits[0].transform != null)
                droppable = _cardHits[0].transform.GetComponent<IDrop>();
        }

        return droppable;
    }
    
    private void OnEnable()
    {
        _possibleDrag = null;
        _currentDrag = null;
    
        ResetCursor();
    }
}
