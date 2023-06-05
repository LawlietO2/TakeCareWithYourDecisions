using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ScannerEffectDemo : MonoBehaviour
{
    public Transform ScannerOrigin;
    public Material EffectMaterial;
    public float ScanDistance;
    public GameObject[] isoTransf;
    private Camera _camera;
    private Camera cam;
    Vector3 pos;
    public GameObject panel;
    public PersonList person;
    public bool isNewChar;
    // Demo Code
    public bool _scanning;
    public bool _isInScanningRoom;
    public Scannable[] _scannables;  
    public DialogueManagerScan dms;
    public HandAnimation HandAni;
    public int only1 = 0;  
    private bool IsReadyToDoCoroutine = true;
    public Image[] panels; 
    public Text[] diseaseNames;
    public bool _disableScan;
    public string[] nomes;  
    public string[] dmsNames;  
    public Transform scan;
    public Global gb;
    public int count;

    void Start()
    {
        gb =  GameObject.Find("GlobalSystem").GetComponent<Global>();
        HandAni = GameObject.Find("LeftHand").GetComponent<HandAnimation>();
        count = 0;
        diseaseNames = new Text[4];
        panels = new Image[4];
        dmsNames = new string [5];
        dmsNames[0] = "NPC_0_Scan";
        dmsNames[1] = "NPC_1_Scan(Clone)";
        dmsNames[2] = "NPC_2_Scan(Clone)";
        dmsNames[3] = "NPC_3_Scan(Clone)";
        dmsNames[4] = "NPC_4_Scan(Clone)";

       
       dms = GameObject.FindWithTag("NPC_Scan").GetComponent("DialogueManagerScan") as DialogueManagerScan;

        //Metodo gerar dados xml
        GerarDadosXML();
        isNewChar = false;     
        _isInScanningRoom = false;
        cam = Camera.main;

        nomes = new string[5];
        nomes[0] = person.Personagem1.Name;
        nomes[1] = person.Personagem2.Name;
        nomes[2] = person.Personagem3.Name;
        nomes[3] = person.Personagem4.Name;
        nomes[4] = person.Personagem5.Name;

        panels[0] = GameObject.Find("Panel cabeca").GetComponent<Image>();
        diseaseNames[0] = GameObject.Find("Text cabeca").GetComponent<Text>();
        panels[1] = GameObject.Find("Panel torso").GetComponent<Image>();
        diseaseNames[1] = GameObject.Find("Text torso").GetComponent<Text>();
        panels[2] = GameObject.Find("Panel perna").GetComponent<Image>(); 
        diseaseNames[2] =  GameObject.Find("Text perna").GetComponent<Text>();
        panels[3] = GameObject.Find("Panel pe").GetComponent<Image>();
        diseaseNames[3] = GameObject.Find("Text pe").GetComponent<Text>();
        
    }

    //Metodo para serializar xml em dados c#
    void GerarDadosXML()
    {
        var asset = Resources.Load<TextAsset>("ObjetosGameObject");
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "personList";
        xRoot.IsNullable = true;
        var serializer = new XmlSerializer(typeof(PersonList), xRoot);
        using (var stream = new StringReader(asset.text))
        using (var reader = XmlReader.Create(stream))
        {
            person = (PersonList)serializer.Deserialize(reader);
        }
    }

    void EnableDisableAlertScript()
    {

      /*  GameObject.Find("Torso").GetComponent<Scannable>().enabled = person.Body.Torso.ScriptEnable;
        //Debug.Log("Torso:   " +person.Body.Torso.ScriptEnable);
        GameObject.Find("Head").GetComponent<Scannable>().enabled = person.Body.Head.ScriptEnable;
        //Debug.Log("Head:   " +person.Body.Head.ScriptEnable);

        GameObject.Find("Pants").GetComponent<Scannable>().enabled = person.Body.Pants.ScriptEnable;
        //Debug.Log("Pants:   " +person.Body.Pants.ScriptEnable);

        GameObject.Find("Shorts").GetComponent<Scannable>().enabled = person.Body.Shorts.ScriptEnable;
        //Debug.Log("Shorts:   " +person.Body.Shorts.ScriptEnable);*/


        
        
        GameObject.Find("Torso").GetComponent<Scannable>().enabled = 
        scan.parent.name == nomes[0] ? person.Personagem1.Body.Torso.ScriptEnable : 
        scan.parent.name == nomes[1] ? person.Personagem2.Body.Torso.ScriptEnable : 
        scan.parent.name == nomes[2] ? person.Personagem3.Body.Torso.ScriptEnable : 
        scan.parent.name == nomes[3] ? person.Personagem4.Body.Torso.ScriptEnable : 
        scan.parent.name == nomes[4] ? person.Personagem5.Body.Torso.ScriptEnable : false;

        GameObject.Find("Head").GetComponent<Scannable>().enabled = 
        scan.parent.name == nomes[0] ? person.Personagem1.Body.Head.ScriptEnable : 
        scan.parent.name == nomes[1] ? person.Personagem2.Body.Head.ScriptEnable : 
        scan.parent.name == nomes[2] ? person.Personagem3.Body.Head.ScriptEnable : 
        scan.parent.name == nomes[3] ? person.Personagem4.Body.Head.ScriptEnable : 
        scan.parent.name == nomes[4] ? person.Personagem5.Body.Head.ScriptEnable : false;

        GameObject.Find("Pants").GetComponent<Scannable>().enabled = 
        scan.parent.name == nomes[0] ? person.Personagem1.Body.Pants.ScriptEnable : 
        scan.parent.name == nomes[1] ? person.Personagem2.Body.Pants.ScriptEnable : 
        scan.parent.name == nomes[2] ? person.Personagem3.Body.Pants.ScriptEnable : 
        scan.parent.name == nomes[3] ? person.Personagem4.Body.Pants.ScriptEnable : 
        scan.parent.name == nomes[4] ? person.Personagem5.Body.Pants.ScriptEnable : false;

        GameObject.Find("Shorts").GetComponent<Scannable>().enabled = 
        scan.parent.name == nomes[0] ? person.Personagem1.Body.Shorts.ScriptEnable : 
        scan.parent.name == nomes[1] ? person.Personagem2.Body.Shorts.ScriptEnable : 
        scan.parent.name == nomes[2] ? person.Personagem3.Body.Shorts.ScriptEnable : 
        scan.parent.name == nomes[3] ? person.Personagem4.Body.Shorts.ScriptEnable : 
        scan.parent.name == nomes[4] ? person.Personagem5.Body.Shorts.ScriptEnable : false;
       


    }



    void Update()
    {

       if(_disableScan){
        StartCoroutine("WaitForDisableScan");   
        
       }

            //descobrir onde esta sendo chamada    
            if(isNewChar){    
            count = gb.contagemObject;
            dms = GameObject.FindWithTag("NPC_Scan").GetComponent("DialogueManagerScan") as DialogueManagerScan;
            isoTransf = new GameObject[4];
            scan = GameObject.Find("Torso").GetComponent<Transform>();
            isoTransf[0] = GameObject.Find("iso ping cabeca");
            isoTransf[1] = GameObject.Find("iso ping torso");
            isoTransf[2] = GameObject.Find("iso ping perna");
            isoTransf[3] = GameObject.Find("iso ping pe");
            _scannables = new Scannable[4];
            _scannables = FindObjectsOfType<Scannable>();
            EnableDisableAlertScript();
           
            isNewChar = false;
            }
            //scanner
			if (_scanning)
			{
               // Debug.Log("ESCANEANDO!" );
				ScanDistance += Time.deltaTime * 1;
				foreach (Scannable s in _scannables)
				{
					//Debug.Log("ESTA ATIVO: " + s.enabled + " Scannable: " + s);
					if(s.enabled){
                            if (Vector3.Distance(ScannerOrigin.position, s.transform.position) <= ScanDistance){
                                
                                
                                s.Ping();                   

                            }
						
                            switch(s.id)	{
                                case 0:
                                    //Debug.Log("Scannable: 0 " + s);
                                    pos = cam.WorldToScreenPoint(s.pos.position);
                                    if(isoTransf[0].transform.position != pos)
                                    isoTransf[0].transform.position = pos;
                                    
                                break;
                                case 1:
                                    //Debug.Log("Scannable: 1 " + s);
                                    pos = cam.WorldToScreenPoint(s.pos.position);
                                    if(isoTransf[1].transform.position != pos)
                                    isoTransf[1].transform.position = pos;

                                break;
                                case 2:
                                    //Debug.Log("Scannable: 2 " + s);
                                    pos = cam.WorldToScreenPoint(s.pos.position);
                                    if(isoTransf[2].transform.position != pos)
                                    isoTransf[2].transform.position = pos;
                                break;
                                case 3:
                                    //Debug.Log("Scannable: 3 " + s);
                                    pos = cam.WorldToScreenPoint(s.pos.position);
                                    if(isoTransf[3].transform.position != pos)
                                    isoTransf[3].transform.position = pos;
                                break;
                                default:Debug.Log("erro");
                                break;

                            }
				    	}
                        if(Input.GetKeyDown(KeyCode.C) && _scanning == true)
                            {
                                //Debug.Log("C pressionado na 2 vez");
                                StartCoroutine("WaitForDisableScan");   
                                
                                dms.only1 = 1;   
                                _scanning = false;
                                //s.invPing();
                            }
				}      
                 
			}
            if((_isInScanningRoom && _scanning == false) ){
                if (Input.GetKeyDown(KeyCode.C) || HandAni.StartScannerAnimation == true)
                {
                  //Debug.Log("C pressionado na 1 vez");
                   StartCoroutine("WaitForEnabletalk");   
                    _scanning = true;
                    HandAni.StartScannerAnimation = false;
                    ScanDistance = 0;
                }
            }
    }

    public void DisableScan()
    {
        // Debug.Log("Teste corrotina: " + count);
         isoTransf[0].transform.position = new Vector3(-13f,1008f,0f);
         isoTransf[1].transform.position = new Vector3(-13f,1008f,0f);
         isoTransf[2].transform.position = new Vector3(-13f,1008f,0f);
         isoTransf[3].transform.position = new Vector3(-13f,1008f,0f);
         count++;
         StopCoroutine("WaitForDisableScan");
         _disableScan = false;
    }

    IEnumerator WaitForDisableScan()
    { 
        
         IsReadyToDoCoroutine = false;
         yield return new WaitForSeconds(0.5f);
         IsReadyToDoCoroutine = true;
         _scanning = false;
         for(int i = 0; i < 4; i++){
         panels[i].enabled = false;
         diseaseNames[i].enabled = false;
         }
         
         DisableScan();
    }

IEnumerator WaitForEnabletalk()
    { 
        
         IsReadyToDoCoroutine = false;
         yield return new WaitForSeconds(5f);
         IsReadyToDoCoroutine = true;
         if(dms.only1 != 0)    
         dms.only1 = 0;  
     
    }



    void OnEnable()
    {
        _camera = GetComponent<Camera>();
        _camera.depthTextureMode = DepthTextureMode.Depth;
    }

    [ImageEffectOpaque]
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        EffectMaterial.SetVector("_WorldSpaceScannerPos", ScannerOrigin.position);
        EffectMaterial.SetFloat("_ScanDistance", ScanDistance);
        RaycastCornerBlit(src, dst, EffectMaterial);
    }

    void RaycastCornerBlit(RenderTexture source, RenderTexture dest, Material mat)
    {
        // Compute Frustum Corners
        float camFar = _camera.farClipPlane;
        float camFov = _camera.fieldOfView;
        float camAspect = _camera.aspect;

        float fovWHalf = camFov * 0.5f;

        Vector3 toRight = _camera.transform.right * Mathf.Tan(fovWHalf * Mathf.Deg2Rad) * camAspect;
        Vector3 toTop = _camera.transform.up * Mathf.Tan(fovWHalf * Mathf.Deg2Rad);

        Vector3 topLeft = (_camera.transform.forward - toRight + toTop);
        float camScale = topLeft.magnitude * camFar;

        topLeft.Normalize();
        topLeft *= camScale;

        Vector3 topRight = (_camera.transform.forward + toRight + toTop);
        topRight.Normalize();
        topRight *= camScale;

        Vector3 bottomRight = (_camera.transform.forward + toRight - toTop);
        bottomRight.Normalize();
        bottomRight *= camScale;

        Vector3 bottomLeft = (_camera.transform.forward - toRight - toTop);
        bottomLeft.Normalize();
        bottomLeft *= camScale;

        // Custom Blit, encoding Frustum Corners as additional Texture Coordinates
        RenderTexture.active = dest;

        mat.SetTexture("_MainTex", source);

        GL.PushMatrix();
        GL.LoadOrtho();

        mat.SetPass(0);

        GL.Begin(GL.QUADS);

        GL.MultiTexCoord2(0, 0.0f, 0.0f);
        GL.MultiTexCoord(1, bottomLeft);
        GL.Vertex3(0.0f, 0.0f, 0.0f);

        GL.MultiTexCoord2(0, 1.0f, 0.0f);
        GL.MultiTexCoord(1, bottomRight);
        GL.Vertex3(1.0f, 0.0f, 0.0f);

        GL.MultiTexCoord2(0, 1.0f, 1.0f);
        GL.MultiTexCoord(1, topRight);
        GL.Vertex3(1.0f, 1.0f, 0.0f);

        GL.MultiTexCoord2(0, 0.0f, 1.0f);
        GL.MultiTexCoord(1, topLeft);
        GL.Vertex3(0.0f, 1.0f, 0.0f);

        GL.End();
        GL.PopMatrix();
    }
}
