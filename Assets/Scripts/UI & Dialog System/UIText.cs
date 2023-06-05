using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UI file", menuName = "UI Files Archive")]
public class UIText : ScriptableObject
{
    [TextArea(3, 15)]
    public string[] diseaseName;
    
}
