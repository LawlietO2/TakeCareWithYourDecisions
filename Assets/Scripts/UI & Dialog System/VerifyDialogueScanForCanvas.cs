using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifyDialogueScanForCanvas : MonoBehaviour
{
    public bool isInScanningRoomForCanvas;  
    private Camera cam;
    public  GameObject NPC_Scan;
    private Vector3 positionBkp;
    
    void Start()
    {
        cam = Camera.main;
        positionBkp = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isInScanningRoomForCanvas)
        {
           NPC_Scan = GameObject.Find("BalaoDeFala");
           Vector3 pos = cam.WorldToScreenPoint(NPC_Scan.transform.position);
            
            if(transform.position != pos)
            transform.position = pos;
        }
        else
        {
          transform.position = positionBkp;
        }

    }

}
