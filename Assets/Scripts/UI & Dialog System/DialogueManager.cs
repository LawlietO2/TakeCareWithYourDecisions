using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public NPC npc;

    bool isTalking = false;

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

    public GameObject globalSystem; 
    public Global gb;
    public int cont;
    

    private bool IsReadyToDoCoroutine = true;
    private bool CloseAnimationHand = false;
    private bool SelectMessage = false;
    public  int SwitchDialogNPC = 0;


    public HandAnimation HandAni;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("XR Leap Rig");
        dialogueUI = GameObject.Find("Dialogue Menu");    
        npcName = GameObject.Find("Texto").GetComponent<Text>();
        npcDialogueBox = GameObject.Find("Dialogue Text").GetComponent<Text>();
        playerResponse = GameObject.Find("Player Dialogue Text").GetComponent<Text>();
        StartCoroutine("WaitForDisable");
        globalSystem = GameObject.Find("GlobalSystem");
        gb = globalSystem.GetComponent<Global>();
        HandAni = GameObject.Find("LeftHand").GetComponent<HandAnimation>();
        cont = 0;

        panel = GameObject.Find("Panel").GetComponent<Image>();
    }

    void Update()
    {
        if(HandAni.bOnNPC == true || SelectMessage == true)
        { 
            distance = Vector3.Distance(player.transform.position, this.transform.position);
        
            if(distance <= 7f)
            {
                HandAni.InteractionNPC = true;

                // Debug.Log("VALOR SwitchDialogNPC [" + SwitchDialogNPC + "]");

                if(Input.GetAxis("Mouse ScrollWheel") < 0f || SwitchDialogNPC == -1)
                {
                    SwitchDialogNPC = 0;
                    if(trackerController > 0){
                        curResponseTracker++;
                        curResponseTracker = Mathf.Clamp(curResponseTracker, 2, 3);
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
                        CloseAnimationHand = false;
                        trackerController = 1;
                        npcDialogueBox.text = npc.dialogue[2];
                    }
                    SelectMessage = true;
                }
            
                //permite a entrada 
                else if (curResponseTracker == 3 && npc.playerDialogue.Length >= 3)
                {
                    playerResponse.text = npc.playerDialogue[3];                
                    if (Input.GetKeyDown(KeyCode.Return) || CloseAnimationHand == true)
                    {   
                        //Debug.Log("Autorizado");
                        CloseAnimationHand = false;
                        npcDialogueBox.text = npc.dialogue[4];
                        StartCoroutine("WaitForClose");   
                        gb.button = true;
                        gb.state = State.interview;
                        gb.cont = 1;
                        HandAni.DisableDialogueScript = false;
                        HandAni.NPC = this;
                        this.tag = "Untagged";
                    }
                }
                else if (curResponseTracker == 2 && npc.playerDialogue.Length >= 2)
                {
                    playerResponse.text = npc.playerDialogue[2];
                    if (Input.GetKeyDown(KeyCode.Return) || CloseAnimationHand == true)
                    {
                        //Debug.Log("NEGADO");
                        CloseAnimationHand = false;
                        npcDialogueBox.text = npc.dialogue[3];
                        StartCoroutine("WaitForClose");
                        gb.button = true;
                        gb.state = State.exit;
                        gb.cont = 4;
                    }
                }

            }
        }
    }

    void OnMouseOver()
    {
        
      
    }

    void OnMouseExit()
    {
        CloseAnimationHand = false;
//        Debug.Log("Mouse is no longer on GameObject.");
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
        yield return new WaitForSeconds(0.2f);
        IsReadyToDoCoroutine = true;
        //dialogueUI.SetActive(false); 
        panel.enabled = false;
        npcName.enabled = false;
        npcDialogueBox.enabled = false;
        playerResponse.enabled = false;
        //desabilitar os componentes dos filhos de DIALOGUEUI ao inves do obj
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
