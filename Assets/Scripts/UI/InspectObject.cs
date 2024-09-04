using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectObject : MonoBehaviour
{
    [SerializeField] private string information;
    [SerializeField] private TMPro.TextMeshProUGUI textObject;

    public void press(){
        textObject.text = information;
    }
}
