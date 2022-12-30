using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicController : MonoBehaviour
{
    [SerializeField] public Mechanic[] mechanics;
    private int _index;

    private void Awake()
    {
        _index = 0;
    }

    public Mechanic GetActiveMechanics()
    {
        return mechanics[_index];
    }
    private void Update()
    {
        GetActiveMechanics().OnHold();
        if (Input.GetMouseButtonDown(0))
            GetActiveMechanics().OnDown();
        else if (Input.GetMouseButton(0))
            GetActiveMechanics().OnDrag();
        else if (Input.GetMouseButtonUp(0))
            GetActiveMechanics().OnUp();
    }
}
