using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followChar : MonoBehaviour
{
    private Camera cam;
    public Transform lookAt;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(lookAt.position);
        
        if(transform.position != pos)
        transform.position = pos;


    }
}
