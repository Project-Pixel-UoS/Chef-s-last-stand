using System.Collections;
using System.Collections.Generic;
using Music;
using UnityEngine;

public class InspectObject : MonoBehaviour
{
    [SerializeField] private string information;
    [SerializeField] private TMPro.TextMeshProUGUI textObject;

    public void press(){
        SoundPlayer.instance.PlayButtonClickFX();
        textObject.text = information;
    }
}
