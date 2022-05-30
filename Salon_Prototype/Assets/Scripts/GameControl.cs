using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject Tool_1;
    public GameObject Tool_2;
    public GameObject Tool_3;
    public GameObject Tool_4;
    public Transform SpawnLocation;

    private GameObject _currentTool;
    // Start is called before the first frame update
    int _toolCount;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnTool_1(GameObject tool)
    {
        ResetTool();
        if(tool != null && _toolCount == 0)
        {
            GameObject tmpTool = Instantiate(tool);
            tmpTool.transform.position = SpawnLocation.position;

            _currentTool = tmpTool;
            _toolCount++;
            
        }
        
    }
    private void ResetTool()
    {
        if(_toolCount > 0)
        {
            Destroy(_currentTool.gameObject);

        }
        _toolCount = 0;
    }

  
}
