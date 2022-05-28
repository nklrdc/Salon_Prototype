using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private float zoomDistance = 3f;
    [SerializeField]
    private float zoomInSpeed = 1.3f;
    [SerializeField]
    private float zoomOutSpeed = 1.5f;

    private Vector3 _originalPosition;
    private Vector3 _destinationPosition;

    private const float _doubleClick = 0.2f;

    private float lastClickTime;

    

    private bool isItDoubleClick = false;
    private bool isItZoomedIn = false;
    private bool isItZoomedOut = true;

    
    public GameObject Model;

    Vector3 resetRotation;

    Camera _cam;
    // Start is called before the first frame update
    void Start()
    {
        _originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        _destinationPosition = new Vector3(transform.position.x, transform.position.y, -3.5f);

        _cam = Camera.main;

        resetRotation = new Vector3(_cam.transform.eulerAngles.x, _cam.transform.eulerAngles.y, _cam.transform.eulerAngles.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        DoubleClickDetection();

        CameraZoom();
        CameraRotation();
        
       
    }

    //Double click detection for zooming in or out
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
        Vector3 cameraZoomOutDistance = new Vector3(transform.position.x, transform.position.y, transform.position.z - zoomDistance);

        if (isItDoubleClick && _cam.transform.position.z < -3.5f && isItZoomedOut)
        {
            _cam.transform.position = Vector3.Lerp(transform.position, cameraZoomDistance, zoomInSpeed * Time.deltaTime);

            if (Vector3.Distance(_cam.transform.position, _destinationPosition) < 0.1f)
            {
                _cam.transform.position = _destinationPosition;

                isItDoubleClick = false;
                isItZoomedIn = true;
                Debug.Log("Reached here");
                Debug.Log(isItZoomedIn);
            }

        }
        else if (isItZoomedIn && isItDoubleClick)
        {
            ResetCameraParameters(cameraZoomOutDistance);

            isItZoomedOut = false;



            if (Vector3.Distance(_cam.transform.position, _originalPosition) < 0.1f)
            {

                _cam.transform.position = _originalPosition;

                isItZoomedOut = true;
                isItZoomedIn = false;
                isItDoubleClick = false;
            }

        }

    }

    private void ResetCameraParameters(Vector3 cameraZoomOutDistance)
    {
        _cam.transform.position = new Vector3(0, transform.position.y, transform.position.z);
        _cam.transform.eulerAngles = resetRotation;
        _cam.transform.position = Vector3.Lerp(transform.position, cameraZoomOutDistance, zoomOutSpeed * Time.deltaTime);
    }

    void CameraRotation()
    {
        Vector3 mousePos = _cam.ScreenToViewportPoint(Input.mousePosition);
        if(Input.GetMouseButton(0))
        {
            if(mousePos.x < 0.1f && isItZoomedIn && _cam.transform.position.x > - 0.45f)
            {
                _cam.transform.RotateAround(Model.transform.position, Vector3.up, 20 * Time.deltaTime);
            }
            else if(mousePos.x > 0.85f && isItZoomedIn && _cam.transform.position.x < 0.45f)
            {
                _cam.transform.RotateAround(Model.transform.position, Vector3.up, -20 * Time.deltaTime);
            }
        }
       
    }
    public bool IsItZoomed()
    {
        return isItZoomedIn;
    }
    

}
