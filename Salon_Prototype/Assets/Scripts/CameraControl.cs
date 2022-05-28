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
    private float lerpSpeed = 1.3f;

    private Vector3 _originalPosition;

    private const float _doubleClick = 0.2f;

    private float lastClickTime;

    

    private bool isItDoubleClick = false;
    private bool isItZoomedIn = false;
    private bool isItZoomedOut = true;

    public Transform CameraHolder;
    public GameObject Model;

    Vector3 resetRotation;

    Camera _cam;
    // Start is called before the first frame update
    void Start()
    {
        _originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
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
            _cam.transform.position = Vector3.Lerp(transform.position, cameraZoomDistance, lerpSpeed * Time.deltaTime);

            if (_cam.transform.localPosition.z >= -0.7f)
            {
                isItDoubleClick = false;
                isItZoomedIn = true;
            }

        }
        else if (isItZoomedIn && isItDoubleClick)
        {
            _cam.transform.position = Vector3.Lerp(transform.position, _originalPosition, lerpSpeed * Time.deltaTime);
            isItZoomedOut = false;
            _cam.transform.eulerAngles = resetRotation;

            if (Vector3.Distance(_cam.transform.position, _originalPosition) < 0.05f)
            {
                _cam.transform.position = _originalPosition;
                
                isItZoomedOut = true;
                isItDoubleClick = false;
            }

        }

    }   
    void CameraRotation()
    {
        Vector3 mousePos = _cam.ScreenToViewportPoint(Input.mousePosition);
        if(Input.GetMouseButton(0))
        {
            if(mousePos.x < 0.3f && isItZoomedIn && _cam.transform.position.x > - 0.45f)
            {
                _cam.transform.RotateAround(Model.transform.position, Vector3.up, 20 * Time.deltaTime);
            }
            else if(mousePos.x > 0.6f && isItZoomedIn && _cam.transform.position.x < 0.45f)
            {
                _cam.transform.RotateAround(Model.transform.position, Vector3.up, -20 * Time.deltaTime);
            }
        }
       
    }
    

}
