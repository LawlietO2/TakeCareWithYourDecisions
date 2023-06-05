using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mouse : MonoBehaviour
{
    public Transform _transform;
    public Transform cameraTransform;
    Vector2 rotacaoMouse;
    public int sensibilidade;
    private Camera _camera;
    public Image panel; 
    public Text diseaseName;
    // Start is called before the first frame update
    void Start()
    {
      _camera  = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 controelMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        rotacaoMouse = new Vector2(rotacaoMouse.x + controelMouse.x * sensibilidade * Time.deltaTime, rotacaoMouse.y + controelMouse.y * sensibilidade * Time.deltaTime);
        rotacaoMouse.x = Mathf.Clamp(rotacaoMouse.x, -80, 80);
        _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, rotacaoMouse.x, _transform.eulerAngles.z);
       
        rotacaoMouse.y = Mathf.Clamp(rotacaoMouse.y, -80, 80);
        cameraTransform.localEulerAngles = new Vector3(-rotacaoMouse.y, cameraTransform.localEulerAngles.y, cameraTransform.localEulerAngles.z);
        
        if(Input.GetMouseButtonDown(0))
        {
          Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
          RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
              //ter os iso instaciados para ativar por aqui a animação do texto
              //Debug.Log("HITEI : " + hit.collider.gameObject.name);
              switch(hit.collider.gameObject.name)	{
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
        }

    }
}
