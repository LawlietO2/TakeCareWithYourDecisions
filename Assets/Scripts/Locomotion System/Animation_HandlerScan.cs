using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_HandlerScan : MonoBehaviour
{
    public Animator anim;
    public float Vertical;
    public float Horizontal;


    
    void Start()
    {
        anim = GetComponentInChildren<Animator>();     
    }

    // Update is called once per frame
    void Update()
    {
         Anim();       
    }

    void Anim()
        {
         anim.SetFloat("Vertical", 0);
         anim.SetFloat("Horizontal", Horizontal);
               
        }
        
}
