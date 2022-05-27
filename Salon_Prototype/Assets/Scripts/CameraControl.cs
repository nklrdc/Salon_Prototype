using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private float zoomDistance = 3f;

    private Vector3 _originalPosition;

    private const float _doubleClick = 0.2f;

    private float lastClickTime;

    private bool isItDoubleClick = false;
    private bool isItZoomedIn = false;
    private bool isItZoomedOut = true;
    Camera _cam;
    // Start is called before the first frame update
    void Start()
    {
        _originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        DoubleClickDetection();

        CameraZoom();
    }
    private void DoubleClickDetection()
    {

        


        if(Input.GetMouseButtonDown(0))
        {
            float timeSinceLastClick = Time.time - lastClickTime;
            if(timeSinceLastClick <= _doubleClick)
            {
                isItDoubleClick = true;
            }
            else
            {

            }
            lastClickTime = Time.time;
        }
    }
    private void CameraZoom()
    {
        Vector3 cameraZoomDistance = new Vector3(transform.position.x, transform.position.y, transform.position.z + zoomDistance);

        if (isItDoubleClick && _cam.transform.localPosition.z < -0.7f && isItZoomedOut)
        {
            _cam.transform.position = Vector3.Lerp(transform.position, cameraZoomDistance, 2f * Time.deltaTime);
            
            if(_cam.transform.localPosition.z >= -0.7f)
            {
                isItDoubleClick = false;
                isItZoomedIn = true;
            }
           
        }
        else if (isItZoomedIn && isItDoubleClick)
        {
            _cam.transform.position = Vector3.Lerp(transform.position, _originalPosition, 2f * Time.deltaTime);
            isItZoomedOut = false;

            if(Vector3.Distance(_cam.transform.position, _originalPosition) < 0.05f)
            {
                _cam.transform.position = _originalPosition;
                isItZoomedOut = true;
                isItDoubleClick = false;
            }
            
        }
        

        
        
        
        
    }

}
