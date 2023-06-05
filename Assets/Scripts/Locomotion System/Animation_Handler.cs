using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Handler : MonoBehaviour
{
    public Animator anim;
    public float Vertical;
    public float Horizontal;
    public GameObject chair ;
    private bool IsReadyToDoCoroutine = true;
    public bool button;
    public bool _isMoving;
    public bool _isFirstTime;
    public Vector3 chairmaleV3;
    public int logCleaner;
    public GameObject male;
    public Rigidbody m_Rigidbody;

    
    void Start()
    {
        male = gameObject;
        chair = GameObject.Find("OfficeChair");
        anim = GetComponentInChildren<Animator>();         
        _isMoving = true;
        _isFirstTime = true;
        logCleaner = 0;
        m_Rigidbody = male.GetComponent<Rigidbody>();
         
    }

    // Update is called once per frame
    void Update()
    {
         Anim();
         animTrigger();
    }

    void Anim()
        {
         anim.SetFloat("Vertical", Vertical);
         anim.SetFloat("Horizontal", Horizontal);
               
        }
    void animTrigger(){

          if(_isMoving == true){
             Vertical = 1;    
             if(logCleaner == 0){
                //Debug.Log("Animação de locomoção iniciada");
                logCleaner = 1; 
                }
             }
             else{
                 //aqui impedimos que o personagem rotacione indevidamente
               m_Rigidbody.constraints = RigidbodyConstraints.FreezePosition;
               chairmaleV3 = chair.transform.position;
               //m_Rigidbody.transform.LookAt(chairmaleV3);
               transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(chairmaleV3), 2f);            
                if(logCleaner  == 1){
                    //Debug.Log("rotação corrigida");
                    logCleaner = 0;
                }
                
                //aqui paramos a movimentação de forma suavizada
                if(_isFirstTime == true){
                        for(int i = 10; i > 0; i--){
                         
                            float j;
                            j = i;             
                            Vertical = j/10; 

                            if(i == 1){
                                
                                //Debug.Log("Animação de locomoção interrompida");
                                logCleaner = 0;
                                Vertical = 0;
                            }

                            _isFirstTime = false;
                        }

                    }
                }
    }    
}
