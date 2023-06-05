using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowDocument : MonoBehaviour
{
    private Camera cam;
    public Transform lookAt;
    private Vector3 positionBkp;
    public bool Blue = false;
    public bool Red = false;
    public bool Magenta = false;
    private WorkStationStatusManager WorkStationStatusManager;

    void Start()
    {
        cam = Camera.main;
        positionBkp = this.transform.position;
        WorkStationStatusManager = GameObject.Find("UI_Objects").GetComponent<WorkStationStatusManager>();
    }

    void Update()
    {
        if(Blue == true && WorkStationStatusManager.Blue == true && WorkStationStatusManager.isInScanningRoom == false)
        {
            Vector3 pos = cam.WorldToScreenPoint(lookAt.position);
            
            if(transform.position != pos)
            transform.position = pos;    
        }
        else if(Red == true && WorkStationStatusManager.Red == true && WorkStationStatusManager.isInScanningRoom == false)
        {
            Vector3 pos = cam.WorldToScreenPoint(lookAt.position);
            
            if(transform.position != pos)
            transform.position = pos;    
        }
        else if(Magenta == true && WorkStationStatusManager.Magenta == true && WorkStationStatusManager.isInScanningRoom == false)
        {
            Vector3 pos = cam.WorldToScreenPoint(lookAt.position);
            
            if(transform.position != pos)
            transform.position = pos;    
        }
        else
        {
          transform.position = positionBkp;
        }

    }
}
