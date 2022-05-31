using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    /*public GameObject Tool_1;
    public GameObject Tool_2;
    public GameObject Tool_3;
    public GameObject Tool_4;*/
    public Transform SpawnLocation;

    CameraControl _camControl;
    private GameObject _currentTool;
    // Start is called before the first frame update
    
    bool _isToolSpawned = false;
    void Start()
    {
        _camControl = FindObjectOfType<CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {
        ResetToolOnZoomOut();
    }
    public void SpawnTool(GameObject tool)
    {
        ResetTool();
        

        if(tool != null && !_isToolSpawned)
        {
            GameObject tmpTool = Instantiate(tool);
            tmpTool.transform.position = SpawnLocation.position;

            _currentTool = tmpTool;
            _isToolSpawned = true;
            
        }
            }
    private void ResetTool()
    {
        if (_isToolSpawned)
        {
            
            Destroy(_currentTool.gameObject);

        }
        _isToolSpawned = false;
    }
    private void ResetToolOnZoomOut()
    {
        if (!_camControl.IsItZoomed() && _isToolSpawned)
        {
            
            Destroy(_currentTool.gameObject);
        }
        else
        {
            return;
        }
        _isToolSpawned = false;
    }

  
}
