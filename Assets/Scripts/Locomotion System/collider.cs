using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider : MonoBehaviour
{

public Animation_Handler Anim;

    // Start is called before the first frame update
public Global gb;
public int cont;
public GameObject male;
public Camera cam1;
public Camera cam2;
public Camera cam3;
public Camera cam4;
public Vector3 maleV3;
public Vector3 locomotionV3;
public GameObject locomotion;
public int contagemObject = 0;  
public SkinnedMeshRenderer[] smr; 
private bool IsReadyToDoCoroutine;
public CutSceneHandler csh;
public HandAnimation HandAni;
public WorkStationStatusManager WorkStationStatusManager;


    void Start()
    {
        smr = new SkinnedMeshRenderer[5];
        csh = GameObject.Find("CutSceneHandler").GetComponent<CutSceneHandler>();
        gb =  GameObject.Find("GlobalSystem").GetComponent<Global>();
        cont = 0;
        cam1 = GameObject.Find("Main Camera").GetComponent<Camera>();
        cam2 = GameObject.Find("Main Camera_2").GetComponent<Camera>();
        cam3 = GameObject.Find("Main Camera_3").GetComponent<Camera>();
        cam4 = GameObject.Find("Main Camera_4").GetComponent<Camera>();
        HandAni = GameObject.Find("LeftHand").GetComponent<HandAnimation>();
        WorkStationStatusManager = GameObject.Find("UI_Objects").GetComponent<WorkStationStatusManager>();
    }

    void OnTriggerEnter(Collider collider)
    {
        //Debug.Log("ATIVANDO O TRIGGER"); 
        Anim = GetComponent<Animation_Handler>();

        if(collider.gameObject.tag == "endlocomotion")
        {   
          Anim._isMoving = false;    
         
          switch(cont) { 
            case 0: 
            //frente da recepção status troca para entrevista
                   smr = this.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
            break;
            case 1: 
                         
                  
                    
            
                    //Debug.Log("ESTADO DE EXAME" +  gb.state);
                    gb.state = State.exam;       
                    contagemObject = gb.contagemObject;
                    cam1.enabled = false;
                    cam2.enabled = true;
                    //cam3.enabled = false;
                    male = Resources.Load<GameObject>("Player_0");
                    locomotion = Resources.Load<GameObject>("Locomotion");
                    maleV3 = new Vector3(-81.06f,5.17f,12.07f);
                    locomotionV3 = new Vector3(-81.12f,5.48f,27.56f);
                    male.name = "male_" + contagemObject.ToString() + "_";
                    locomotion.name = "locomotion_" + contagemObject.ToString() + "_";
                    Instantiate(locomotion, locomotionV3, transform.rotation);
                    Instantiate(male, maleV3, transform.rotation);
                    male.name = "male_" + contagemObject.ToString() + "_";
                    locomotion.name = "locomotion_" + contagemObject.ToString() + "_";          
                    

                    HandAni.isInScanningRoom = true;
                    WorkStationStatusManager.isInScanningRoom = true;

                    for(int i = 0; i < 5; i++){
                        if(!smr[i].Equals(null)){
                            //Debug.Log("ESTADO DE EXAME");
                            smr[i].enabled = false;
                        }
                    }
                    cont = 2;
            break;
            case 2: 
                 //   Debug.Log("ESTADO FINAL COLLIDER" +  gb.state);  

                 
                    //leva a camera de volta para o protagonista na recepção
                    cam1.enabled = true;
                    cam2.enabled = false;
                    cam3.enabled = false;
                    cam4.enabled = false;
                    
                    WorkStationStatusManager.isInScanningRoom = false;
                    //seta as cameras para a posição inicial
                    csh.cam3.transform.localPosition = new Vector3(-0.42f,0.15f,-0.02f);
                    csh.cam3.transform.localEulerAngles = new Vector3(40.02f,278.76f,354.16f);
                    csh.cam4.transform.localPosition = new Vector3(-0.42f,0.15f,-0.026f);

                    gb.button = true;
                    gb.state = State.intro;
                    gb.cont = 3;
                    cont = 0;
            break;
            case 3:
                    gb.state = State.exit;   
                    cont = 0;                 
            break;
            default: //Debug.Log("Classe collider erro default!"); 
                     //Debug.Log("Cont: " + cont); 
            break;
          }
              
        }
    }


  IEnumerator WaitForSomething()
    { 
       
         IsReadyToDoCoroutine = false;
         yield return new WaitForSeconds(7f);
         IsReadyToDoCoroutine = true;
         
    }


        void OnTriggerExit(Collider collider)
    {
        
        Anim = GetComponent<Animation_Handler>();
 
         if(gb.state == State.laboratory || gb.state == State.quarantine){
         for(int i = 0; i < 5; i++){
                        
                        smr[i].enabled = true;
                    }
            }

          //Debug.Log("ESTADO ATUAL: " +  gb.state);
          if(gb.state.Equals(State.interview)){
          // Debug.Log("cont: " +  cont);    
          cont++;
          }

          if(cont == 3)
          cont =0;

          //Debug.Log("Reinicia locomoção");   
          Anim._isMoving = true; 
          Anim._isFirstTime = true;      
        
    }
}
