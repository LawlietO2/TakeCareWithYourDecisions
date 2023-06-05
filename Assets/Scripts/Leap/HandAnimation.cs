using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimation : MonoBehaviour
{
    public  DialogueManager dm;
    public  DialogueManagerScan dmScan;
    private bool CloseAnimationHand = false;
    public  RayCastFinger RayCastFinger;
    public  bool InConversation = false;
    public  bool bOnNPC = false;
    public  bool bOnNPCScan = false;
    public  bool InteractionNPC = false;
    public  bool isInScanningRoom = false;
    public  bool StartScannerAnimation = false;
    public  bool DisableDialogueScript = false;
    public  bool bOnPing = false;
    
    //public GameObject NPC;
    public  DialogueManager NPC;
 
    // Start is called before the first frame update


    public void OnAnimation(){

        if(isInScanningRoom == false)
        {
             //Debug.Log("bOnNPC: " + bOnNPC);
             //.Log("InConversation: " + InConversation);
            if((InConversation == true && bOnNPC == true) || (InteractionNPC == true && bOnNPC == true)){
//                dm = GameObject.FindWithTag("NPC").GetComponent("DialogueManager") as DialogueManager;
                dm = GameObject.FindWithTag("NPC").GetComponent<DialogueManager>();
                dm.HandAnimationMovement();           
            }
        }
        else
        {

            //Debug.Log("bOnPing [" + bOnPing + "]");
            if(bOnPing){
                RayCastFinger = GameObject.Find("RayCastFinger").GetComponent<RayCastFinger>();
                RayCastFinger.HandAnimationMovement();
            }

            //Debug.Log("bOnNPCScan [" + bOnNPCScan +"]");
           // Debug.Log("InteractionNPC [" + InteractionNPC +"]");
            if((InConversation == true && bOnNPCScan == true) || (InteractionNPC == true && bOnNPCScan == true)){
                DisableDialogueScript = false;
                //dmScan = GameObject.FindWithTag("NPC_Scan").GetComponent("DialogueManagerScan") as DialogueManagerScan;
                dmScan = GameObject.FindWithTag("NPC_Scan").GetComponent<DialogueManagerScan>();
                dmScan.HandAnimationMovement();           
            }
        }
    }


    public void Update(){
        if(isInScanningRoom == true && DisableDialogueScript == false)
        {
            //NPC.GetComponent<DialogueManager>().enabled = false;
            NPC.enabled = false;
            DisableDialogueScript = true;
            //dm.enabled = false;
        }
    }


    public void OffAnimation(){
        
    }

    public void OnNPC() {
        bOnNPC = true;
    }

    
    public void ButtonSwitchDialogUP()
    {
        if(isInScanningRoom == false){
            dm = GameObject.FindWithTag("NPC").GetComponent("DialogueManager") as DialogueManager;
            dm.SwitchDialogNPC = 1;
        }
        else{
            dmScan = GameObject.FindWithTag("NPC_Scan").GetComponent("DialogueManagerScan") as DialogueManagerScan;
            dmScan.SwitchDialogNPC = 1;
        }
    }

    public void ButtonSwitchDialogDown()
    {
        if(isInScanningRoom == false){
            dm = GameObject.FindWithTag("NPC").GetComponent("DialogueManager") as DialogueManager;
            dm.SwitchDialogNPC = -1;
        }
        else{
            dmScan = GameObject.FindWithTag("NPC_Scan").GetComponent("DialogueManagerScan") as DialogueManagerScan;
            dmScan.SwitchDialogNPC = -1;
        }
    }

    public void StartScanner()
    {
        StartScannerAnimation = true;
    }

    public void SlideScan(){
        Debug.Log("TESTE AQUI");
    }
}
