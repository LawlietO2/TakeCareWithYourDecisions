using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManagerScan : MonoBehaviour
{
    public NPC npc;

    public bool isTalking = false;

    float distance;
    float curResponseTracker = 0;
    int trackerController = 0;

    //gameObject do leap
    public GameObject player;
    public GameObject dialogueUI;

    public Text npcName;
    public Text npcDialogueBox;
    public Text playerResponse;
    public Image panel; 

    public ScannerEffectDemo sc;
    public GameObject globalSystem; 
    public Global gb;
    public int cont;
    public int only1 = 1; 
    public CutSceneHandler csh;
    public HandAnimation HandAni;
    public VerifyDialogueScanForCanvas VerifyForCanvas;
    private bool IsReadyToDoCoroutine = true;
    private bool CloseAnimationHand = false;
    private bool SelectMessage = false;
    public  int SwitchDialogNPC = 0;

    // Start is called before the first frame update
    void Start()
    {
        csh = GameObject.Find("CutSceneHandler").GetComponent<CutSceneHandler>();
        sc = GameObject.Find("Main Camera").GetComponent<ScannerEffectDemo>();
        player = GameObject.Find("XR Leap Rig");
        dialogueUI = GameObject.Find("Dialogue Menu");    
        npcName = GameObject.Find("Texto").GetComponent<Text>();
        npcDialogueBox = GameObject.Find("Dialogue Text").GetComponent<Text>();
        playerResponse = GameObject.Find("Player Dialogue Text").GetComponent<Text>();
        HandAni = GameObject.Find("LeftHand").GetComponent<HandAnimation>();
        VerifyForCanvas = GameObject.Find("BalaoDeFalaCanvas").GetComponent<VerifyDialogueScanForCanvas>();
        StartCoroutine("WaitForDisable");
        globalSystem = GameObject.Find("GlobalSystem");
        gb = globalSystem.GetComponent<Global>();
        cont = 0;
        
        panel = GameObject.Find("Panel").GetComponent<Image>();
     }
     void Update(){
        
        if(HandAni.bOnNPCScan == true)
        { 
            if(!sc._scanning){
                distance = Vector3.Distance(player.transform.position, this.transform.position);
                // so permite duas falas
                if(only1 == 1){
                    if(distance <= 10f)
                    {
                        HandAni.InteractionNPC = true;
                    
                        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
                        {
                            if(trackerController > 0){
                                curResponseTracker++;
                                curResponseTracker = Mathf.Clamp(curResponseTracker, 2, 3);
                            }
                            if (curResponseTracker >= npc.playerDialogue.Length - 1)
                            {
                                curResponseTracker = npc.playerDialogue.Length - 1;
                            }
                        }
                        else if(Input.GetAxis("Mouse ScrollWheel") > 0f)
                        {  
                            if(trackerController > 0){                 
                                curResponseTracker--;
                                curResponseTracker = Mathf.Clamp(curResponseTracker, 2, 3);
                                //Debug.Log("tracjer: " +curResponseTracker);
                            }
                            if(curResponseTracker < 0)
                            {
                                curResponseTracker = 0;
                            }
                        }

                        //trigger dialogue

                        if((Input.GetKeyDown(KeyCode.E) && isTalking==false) || (CloseAnimationHand == true && isTalking==false))
                        {
                            StartConversation();
                            HandAni.InteractionNPC = false;
                            CloseAnimationHand = false; 
                        }
                        else if(Input.GetKeyDown(KeyCode.E) && isTalking == true)
                        {
                            EndDialogue();
                        }

                        //Local onde as duas falas iniciais ocorrem 
                        if(curResponseTracker == 0 && npc.playerDialogue.Length >= 0)
                        {
                            playerResponse.text = npc.playerDialogue[0];
                            if(Input.GetKeyDown(KeyCode.Return) || CloseAnimationHand == true)
                            {        
                                CloseAnimationHand = false;
                                npcDialogueBox.text = npc.dialogue[1];
                                curResponseTracker = 1;
                            }
                        }
                        else if(curResponseTracker == 1 && npc.playerDialogue.Length >= 1)
                        {
                            playerResponse.text = npc.playerDialogue[1];
                            if(Input.GetKeyDown(KeyCode.Return) || CloseAnimationHand == true)
                            {
                                trackerController = 1;
                                npcDialogueBox.text = npc.dialogue[2];
                                CloseAnimationHand = false;
                                //passa a possibilitar o escaneamento
                                sc._isInScanningRoom = true;
                                EndDialogue();
                            }
                        }
                    }
                }
            }
        }

           //Debug.Log("CLASSE DialogueManagerScan: only1: " + only1) ; 
           if(only1 == 0 && isTalking==false){
            //Debug.Log("2 ETAPA ");    
            StartConversation();
            HandAni.InteractionNPC = false;
            CloseAnimationHand = false; 
           
           }
           if(only1 == 0){
           if(Input.GetAxis("Mouse ScrollWheel") < 0f || SwitchDialogNPC == -1)
            {
               SwitchDialogNPC = 0;
               if(trackerController > 0){                 
                    curResponseTracker++;
                    curResponseTracker = Mathf.Clamp(curResponseTracker, 2, 3);
                   // Debug.Log("curResponseTracker: " + curResponseTracker);
                }
            
                if (curResponseTracker >= npc.playerDialogue.Length - 1)
                {
                    curResponseTracker = npc.playerDialogue.Length - 1;
                }
            }
            else if(Input.GetAxis("Mouse ScrollWheel") > 0f || SwitchDialogNPC == 1)
            {  
                SwitchDialogNPC = 0;
                if(trackerController > 0){                 
                    curResponseTracker--;
                    curResponseTracker = Mathf.Clamp(curResponseTracker, 2, 3);
                   // Debug.Log("curResponseTracker: " + curResponseTracker);
                }
                if(curResponseTracker < 0)
                {
                    curResponseTracker = 0;
                }
            }
            //trigger dialogue
            if((Input.GetKeyDown(KeyCode.E) && isTalking==false) || (CloseAnimationHand == true && isTalking==false && HandAni.bOnNPCScan == true))
            {
                StartConversation();
                HandAni.InteractionNPC = false;
                CloseAnimationHand = false; 
            }
            else if(Input.GetKeyDown(KeyCode.E) && isTalking == true)
            {
                EndDialogue();
            }

            if(curResponseTracker == 0 && npc.playerDialogue.Length >= 2)
            {
                playerResponse.text = npc.playerDialogue[2];
                if(Input.GetKeyDown(KeyCode.Return) || (CloseAnimationHand == true && HandAni.bOnNPCScan == true))
                {        
                    npcDialogueBox.text = npc.dialogue[3];
                    curResponseTracker = 1;
                    CloseAnimationHand = false;
                }
            }
            else if(curResponseTracker == 1 && npc.playerDialogue.Length >= 3)
            {
                playerResponse.text = npc.playerDialogue[3];
                if(Input.GetKeyDown(KeyCode.Return) || (CloseAnimationHand == true && HandAni.bOnNPCScan == true))
                {
                    CloseAnimationHand = false;
                    trackerController = 1;
                    npcDialogueBox.text = npc.dialogue[4];
                }
                SelectMessage = true;
            }
           
            //permite a entrada 
            else if (curResponseTracker == 3 && npc.playerDialogue.Length >= 5)
            {
                playerResponse.text = npc.playerDialogue[5];                
                if (Input.GetKeyDown(KeyCode.Return) || (CloseAnimationHand == true && HandAni.bOnNPCScan == true))
                {   
                    //manda pro laboratorio
                    CloseAnimationHand = false;
                    npcDialogueBox.text = npc.dialogue[6];
                    StartCoroutine("WaitForClose");   
                    gb.button = true;
                    gb.state = State.laboratory;
                    gb.cont = 2;
                    only1 = 1;
                    sc._disableScan = true; 
                    csh._activateCam4 = true;
                    sc._isInScanningRoom  = false;    
                    HandAni.isInScanningRoom = false;
                    VerifyForCanvas.isInScanningRoomForCanvas = false;
                    sc._scanning = false;
                     gb.labPosCount++;  
                }
            }
             else if (curResponseTracker == 2 && npc.playerDialogue.Length >= 4)
            {
                playerResponse.text = npc.playerDialogue[4];
                if (Input.GetKeyDown(KeyCode.Return) || (CloseAnimationHand == true && HandAni.bOnNPCScan == true))
                {
                    //manda pra quarentena
                       
                    CloseAnimationHand = false;
                    npcDialogueBox.text = npc.dialogue[5];
                    StartCoroutine("WaitForClose");
                    gb.button = true;
                    gb.state = State.quarantine;
                    gb.cont = 5;
                    only1 = 1;
                    sc._disableScan = true; 
                    csh._activateCam3 = true;
                    sc._isInScanningRoom  = false;    
                    HandAni.isInScanningRoom = false;
                    VerifyForCanvas.isInScanningRoomForCanvas = false;
                    sc._scanning = false;
                    gb.quarPosCount++;                     
                }
            }
        }
    }

    void OnMouseOver()
    {
       // Debug.Log("DIALOGUEMANAGERSCAN: onmouseover");
    }


 IEnumerator WaitForClose()
    { 
        
         IsReadyToDoCoroutine = false;
         yield return new WaitForSeconds(4f);
         IsReadyToDoCoroutine = true;
         EndDialogue();
        
    }

     IEnumerator WaitForDisable()
    { 
        
         IsReadyToDoCoroutine = false;
         yield return new WaitForSeconds(0.5f);
         IsReadyToDoCoroutine = true;
         //dialogueUI.SetActive(false); 
         panel.enabled = false;
         npcName.enabled = false;
         npcDialogueBox.enabled = false;
         playerResponse.enabled = false;
        
    }



    void StartConversation()
    {
        isTalking = true;
        curResponseTracker = 0;
        //dialogueUI.SetActive(true);
        panel.enabled = true;
        npcName.enabled = true;
        npcDialogueBox.enabled = true;
        HandAni.InConversation = true;
        playerResponse.enabled = true;
        npcName.text = npc.name;
        npcDialogueBox.text = npc.dialogue[0];
    }

    void EndDialogue()
    {
        isTalking = false;
        //dialogueUI.SetActive(false); 
        panel.enabled = false;
        npcName.enabled = false;
        npcDialogueBox.enabled = false;
        playerResponse.enabled = false;
        HandAni.InConversation = false;
        curResponseTracker = 0;
        trackerController = 0;
    }

    public void HandAnimationMovement()
    {
        CloseAnimationHand = true;
    }
}
