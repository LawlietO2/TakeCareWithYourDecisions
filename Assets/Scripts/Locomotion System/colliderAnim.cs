using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderAnim : MonoBehaviour
{

public Animation_Handler Anim;

    // Start is called before the first frame update

public GameObject player;
public Camera cam1;
public Camera cam2;
public Camera cam3;
private GameObject cv2;
private GameObject cv3;
public VerifyDialogueScanForCanvas VerifyForCanvas;


    void Start()
    {
        player = GameObject.Find("XR Leap Rig");
        cam1 = GameObject.Find("Main Camera").GetComponent<Camera>();
        cam2 = GameObject.Find("Main Camera_2").GetComponent<Camera>();
        //cam3 = GameObject.Find("Main Camera_3").GetComponent<Camera>();
        cv2  = GameObject.Find("Canvas2");
        cv3  = GameObject.Find("Canvas3");
        VerifyForCanvas = GameObject.Find("BalaoDeFalaCanvas").GetComponent<VerifyDialogueScanForCanvas>();
    }

    void OnTriggerEnter(Collider collider)
    {
        Anim = GetComponent<Animation_Handler>();

        if(collider.gameObject.tag == "endlocomotion")
        {   
          Anim._isMoving = false;    
          //Debug.Log("Encerra Cutscene !!!!!!!!!!!!!!!!!!!!!!!!!!!"); 
          cam1.enabled = true;
          cam2.enabled = false;
          //cam3.enabled = false;
          //GameObject.Find("Main Camera_3").SetActive(false);
          cv2.SetActive(false);
          //cv3.SetActive(false);
          VerifyForCanvas.isInScanningRoomForCanvas = true;
          
          player.transform.position = new Vector3(-79.84f,7.48f,28.06f);  
          Destroy(gameObject);
          Destroy(collider.gameObject);
        }
    }
            
}
