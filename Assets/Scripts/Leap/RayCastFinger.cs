using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCastFinger : MonoBehaviour
{
    private LineRenderer line;
    public bool button = false;
    public HandAnimation HandAni;
    public Text diseaseName;
    public Image panel; 
    private bool CloseAnimationHand = false;
   
    void Start()
    {
        line = this.gameObject.AddComponent<LineRenderer>();
        //Settar largura da linha
        line.startWidth = 0.007f;
        line.endWidth = 0.007f;
        line.material = new Material(Shader.Find("Sprites/Default"));//Setta material padrao Obs: Sem isso nao conseguimos manipular as cores da linha
        line.SetColors(Color.white, Color.white); //Setta a cor da linha
        HandAni = GameObject.Find("LeftHand").GetComponent("HandAnimation") as HandAnimation;
    }

    void Update()
    {    
       // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right),Color.white); Cria Raycast em Debug somente na aba "Scene"
        
        if(button == true)
        {
            line.SetPosition(0, this.transform.position); //Setta posição inicial da linha
            line.SetPosition(1, DetectHit(this.transform.position, 3.5f, this.transform.right)); //Setta posição final da linha
        }
        else
        {
            line.enabled = false;
            HandAni.bOnNPC = false;
        }
    }

     //Essa função cria um raycast e retorna o ponto de "hit" para posição final da linha (LineRenderer)
     Vector3 DetectHit(Vector3 startPos, float distance, Vector3 direction)
     {
        Ray ray = new Ray(startPos, direction); 
        RaycastHit hit;
        Vector3 endPos = startPos + direction * distance;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit) )
        {
           // Debug.Log("HIT NAME [" + hit.collider.name + "]");
           // Debug.Log("HIT TAG ["  + hit.collider.tag +  "]");

            if (hit.collider.tag.Equals("NPC"))
            {
                HandAni.bOnNPC = true;
            }
            else if(hit.collider.tag.Equals("NPC_Scan"))
            {
                HandAni.bOnNPCScan = true;
            }
            else if(hit.collider.tag.Equals("Ping"))
            {
                HandAni.bOnPing = true;
            }
            else
            {
              HandAni.bOnPing = false;
              HandAni.bOnNPC = false;
              HandAni.bOnNPCScan = false;
            }

            if(CloseAnimationHand == true && HandAni.bOnPing == true)
            {
                CloseAnimationHand = false;
                switch(hit.collider.gameObject.name){
                    case "Head":
                        panel = GameObject.Find("Panel cabeca").GetComponent<Image>();
                        diseaseName = GameObject.Find("Text cabeca").GetComponent<Text>();
                        if(!panel.enabled){
                        panel.enabled = true;
                        }else{
                            panel.enabled = false;
                        }
                        if(!diseaseName.enabled){
                            diseaseName.enabled = true;
                        }else{
                            diseaseName.enabled = false;
                        }

                        break;
                    case "Torso":
                        panel = GameObject.Find("Panel torso").GetComponent<Image>();
                        diseaseName = GameObject.Find("Text torso").GetComponent<Text>();
                        if(!panel.enabled){
                            panel.enabled = true;
                        }else{
                            panel.enabled = false;
                        }
                        if(!diseaseName.enabled){
                            diseaseName.enabled = true;
                        }else{
                            diseaseName.enabled = false;
                        }
                    break;
                    case "Pants":
                        panel = GameObject.Find("Panel perna").GetComponent<Image>();
                        diseaseName = GameObject.Find("Text perna").GetComponent<Text>();
                        if(!panel.enabled){
                        panel.enabled = true;
                        }else{
                        panel.enabled = false;
                        }
                        if(!diseaseName.enabled){
                            diseaseName.enabled = true;
                        }else{
                        diseaseName.enabled = false;
                        }
                    break;
                    case "Shorts":
                        panel = GameObject.Find("Panel pe").GetComponent<Image>();
                        diseaseName = GameObject.Find("Text pe").GetComponent<Text>();
                        if(!panel.enabled){
                        panel.enabled = true;
                        }else{
                        panel.enabled = false;
                        }
                        if(!diseaseName.enabled){
                        diseaseName.enabled = true;
                        }else{
                            diseaseName.enabled = false;
                        }
                        break;
                    //default:Debug.Log("erro");
                    break;
                }
            } 

            endPos = hit.point;
            line.enabled = true;
            return endPos;
        }
        line.enabled = true;
        return endPos;   
     }

    public void RayCastOnOff()
    {
        //Debug.Log(" RayCastOnOff - Begin");
       // Debug.Log(" RayCastOnOff - [" + button + "]");
        button = !button;
        //Debug.Log(" RayCastOnOff - End");
        
    }    

    
    public void HandAnimationMovement()
    {
        CloseAnimationHand = true;
    }
    
}