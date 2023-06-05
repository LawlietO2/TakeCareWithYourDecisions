using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneHandler : MonoBehaviour
{
    public GameObject player;
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
    public Camera cam4;
    private GameObject cv2;
    private GameObject cv3;
    private GameObject cv4;
    public bool _activateCam3;
    public bool _activateCam4;
    public Global gb; 
    public WorkStationStatusManager WorkStationStatusManager;
    // Start is called before the first frame update
    void Start()
    {
        gb = GameObject.Find("GlobalSystem").GetComponent<Global>();  
        _activateCam3 = false;
        player = GameObject.Find("XR Leap Rig");
        cam1 = GameObject.Find("Main Camera").GetComponent<Camera>();
        cam2 = GameObject.Find("Main Camera_2").GetComponent<Camera>();
        cam3 = GameObject.Find("Main Camera_3").GetComponent<Camera>();
        cam4 = GameObject.Find("Main Camera_4").GetComponent<Camera>();
        WorkStationStatusManager = GameObject.Find("UI_Objects").GetComponent<WorkStationStatusManager>();
        cv2  = GameObject.Find("Canvas2");
        cv3  = GameObject.Find("Canvas3");
        cv4  = GameObject.Find("Canvas4");
       // GameObject.Find("Main Camera_3").SetActive(false);
        cv3.SetActive(false);
        cam3.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_activateCam3){
         // Debug.Log("ATIVA CAM 3"); 
          cam1.enabled = false;
          cam2.enabled = false;
          cam3.enabled = true;
          cam4.enabled = false;
          //GameObject.Find("Main Camera_3").SetActive(true);
          cv2.SetActive(true);
          cv3.SetActive(true);
          cv4.SetActive(true);
          player.transform.position = new Vector3(-78.50f,7.51f,12.8f);
          _activateCam3 = false;
          gb.state = State.quarantine;
        }

        if(_activateCam4){
          //Debug.Log("ATIVA CAM 4"); 
          cam1.enabled = false;
          cam2.enabled = false;
          cam3.enabled = false;
          cam4.enabled = true;
          //GameObject.Find("Main Camera_3").SetActive(true);
          cv2.SetActive(true);
          cv3.SetActive(true);
          cv4.SetActive(true);
          player.transform.position = new Vector3(-78.50f,7.51f,12.8f);
          _activateCam4 = false;
          gb.state = State.laboratory;
        }
    }
}
