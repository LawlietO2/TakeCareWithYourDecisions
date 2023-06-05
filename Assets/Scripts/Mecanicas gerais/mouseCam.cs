using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mouseCam : MonoBehaviour
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
        rotacaoMouse.x = Mathf.Clamp(rotacaoMouse.x, 90, 125);
        _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, rotacaoMouse.x, _transform.eulerAngles.z);
       
        rotacaoMouse.y = Mathf.Clamp(rotacaoMouse.y, 11, 19);
        cameraTransform.localEulerAngles = new Vector3(cameraTransform.localEulerAngles.x, cameraTransform.localEulerAngles.y, -rotacaoMouse.y);

    }
    
}
