using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public string ToolTarget;
    private Transform Spawn;
    public GameObject Brush;
    public Camera Cam;
    private Ray Ray;
    private Vector3 Offset;
    public GameObject Face;

    [SerializeField]
    private float brushSize = 0.2f;


    Tool _tool;
    Vector3 hitPoint;
    // Start is called before the first frame update
    void Start()
    {
        Offset = new Vector3(0, 0, 0.01f);
        Spawn = GameObject.FindGameObjectWithTag("Spawn").GetComponent<Transform>();
        _tool = GetComponent<Tool>();
        Brush = gameObject;
        Cam = Camera.main;
        Face = GameObject.FindGameObjectWithTag("Face");
    }

    // Update is called once per frame

    public void Update()
    {
        RaycastHit hit;

        if (Input.GetMouseButton(0))
        {
            Ray = Cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(Ray, out hit, 1000.0f))
            {
                Brush.transform.position = hit.point -Offset;
                draw(hit);
            };
        }
        else
        {
            Brush.transform.position = Spawn.position;
        }

    }

    private void draw(RaycastHit hit)
    {
        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;

        

        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.clear);
        tex.Apply();
    }
}

