using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public GameObject [] theDoors;
    public bool open_Door;
    public bool hit_Door;
    public int id;
    //posições iniciais e finais das portas{aberto,fechado}
    public Vector3[] doorsDestV3;
    public Vector3[] doorsOrigV3;
    // Start is called before the first frame update
    private bool TEST = false;
    private bool IsReadyToDoCoroutine = true;
    public CutSceneHandler csh;

    void Start()
    {   
        csh = GameObject.Find("CutSceneHandler").GetComponent<CutSceneHandler>();
        doorsOrigV3 = new Vector3[7];
        doorsDestV3 = new Vector3[7];
        theDoors = new GameObject[7];
        // ordem das portas 
        //front door, lab door, lab interior door, quarentine door
        //  quarentine glass door 
        theDoors[0] = GameObject.Find("FrontDoor_0");
		theDoors[1] = GameObject.Find("LabDoor_1");
		theDoors[2] = GameObject.Find("LabInsideDoor_2");
		theDoors[3] = GameObject.Find("QuarantineDoor_3");
        theDoors[4] = GameObject.Find("GlassDoor_4");
        theDoors[5] = GameObject.Find("ExameDoor_5");
        theDoors[6] = GameObject.Find("SecurityDoor_6");
       
        //posições de abertura e fechamento das portas
        doorsDestV3[0] = new Vector3(-67.19f,5.08f,1.94f);
        doorsOrigV3[0] = new Vector3(-69.79f,5.08f,1.94f);
        theDoors[0].transform.position  = doorsOrigV3[0];

        doorsDestV3[1] = new Vector3(-66.11f, 5.08f, 15f);
        doorsOrigV3[1] = new Vector3(-66.11f, 5.08f, 12.55f);
        theDoors[1].transform.position  = doorsOrigV3[1];

        doorsDestV3[2] = new Vector3(-47.56f,5.08f,-0.73f);
        doorsOrigV3[2] = new Vector3(-47.56f,5.08f,-3.19f);
        theDoors[2].transform.position  = doorsOrigV3[2];

        doorsDestV3[3] = new Vector3(-66.20f,5.08f,23.37f);
        doorsOrigV3[3] = new Vector3(-66.20f,5.08f,20.98f);
        theDoors[3].transform.position  = doorsOrigV3[3];

        doorsDestV3[4] = new Vector3(-59.95f,5.08f,22.94f);
        doorsOrigV3[4] = new Vector3(-59.95f,5.08f,25.04f);
        theDoors[4].transform.position  = doorsOrigV3[4];

        doorsDestV3[5] = new Vector3(-72.48f,5.08f,17.81f);
        doorsOrigV3[5] = new Vector3(-69.83f,5.07f,17.80f);
        theDoors[5].transform.position  = doorsOrigV3[5];

        //porta chumbada sempre fechada!
        doorsDestV3[6] = new Vector3(-79.19f,5.08f,2.01f);
        doorsOrigV3[6] = new Vector3(-79.19f,5.08f,2.01f);
        theDoors[6].transform.position  = doorsOrigV3[6];

    }

    // Update is called once per frame
    void Update()
    {
        if(hit_Door){
        	switch(id)	{
                case 0:
                openDoor(id,open_Door);
                break;
                case 1:
                openDoor(id,open_Door);
                break;
                case 2:
                openDoor(id,open_Door);
                break;
                case 3:
                openDoor(id,open_Door);
                break;
                case 4:
                openDoor(id,open_Door);
                break;
                case 5:
                openDoor(id,open_Door);
                break;
                case 6:
                openDoor(id,open_Door);
                break;
                default:Debug.Log("erro");
                break;

		    }
            //
        }
    }

    void openDoor(int id, bool open_Door){
        //abre porta
        if(open_Door){
        if(id == 3 || id == 1){
           if(id == 3){
          csh.cam3.transform.position = new Vector3(-65.19f,10.43f,16.67f);
          csh.cam3.transform.localEulerAngles = new Vector3(4.96f,303.68f,341f);
          //LEMBRAR DE RETORNAR A CAMERA PARA A POSIÇÃO E ROTACÃO INICIAIS
           } else if(id == 1){
          csh.cam4.transform.position = new Vector3(-65.26f,9.03f,-4.84f);
          //csh.cam4.transform.eulerAngles = new Vector3(354.02f,288.84f,1.63f);    
           }
        }
        theDoors[id].transform.position = Vector3.Lerp(theDoors[id].transform.position, doorsDestV3[id], 0.1f);
        open_Door = false;
        }
        
     StartCoroutine("WaitForClose");
         
    }
  
    
     IEnumerator WaitForClose()
    { 
       
         IsReadyToDoCoroutine = false;
         yield return new WaitForSeconds(2f);
         IsReadyToDoCoroutine = true;
         hit_Door = false;
         //fecha porta
         theDoors[id].transform.position = Vector3.Lerp(doorsOrigV3[id], theDoors[id].transform.position, 0.9f);
        }
        
}
