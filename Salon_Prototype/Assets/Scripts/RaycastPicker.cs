using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RaycastPicker
{
    public static GameObject RaycastPickerObj()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        if (Physics.Raycast(ray, out _hit))
        {
            return _hit.collider.gameObject;
        }
        return null;
    }
}

