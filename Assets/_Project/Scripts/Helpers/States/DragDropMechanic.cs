using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DragDropMechanic : Mechanic
{
    private float _timer;
    [SerializeField] protected float triggerInterval;
    protected abstract void Action();

    public override void OnDrag()
    {

        _timer += Time.deltaTime;
        if (_timer >= triggerInterval)
        {
            _timer = 0;
        }
    }

    public override void OnHold()
    {
        Action();
    }

    public override void OnUp()
    {
        _timer = 0f;
    }
}
