using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public GameObject ToolMenu;

    CameraControl _camControl;
    // Start is called before the first frame update
    void Start()
    {
        ToolMenu.SetActive(false);
        _camControl = FindObjectOfType<CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_camControl.IsItZoomed())
        {
            ToolMenu.SetActive(true);
        }
        else
        {
            ToolMenu.SetActive(false);
        }
    }
}
