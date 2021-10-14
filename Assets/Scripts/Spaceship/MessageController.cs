using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class MessageController : MonoBehaviour
{
    public static MessageController instance; 

    public SO_IntroMessages so_messageScripts;

    public TextMeshProUGUI textMesh;



    private void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        Invoke("PlayBeginningMsg", 4f); 
    }

    void PlayBeginningMsg()
    {
        SoundManager.instance.PlayMessageWithVoice(SoundManager.instance.beginningAudioClips, so_messageScripts.beginningMessages);
    }

    public void PlayLandingMsg()
    {
        SoundManager.instance.PlayMessageWithVoice(SoundManager.instance.landingAudioClips, so_messageScripts.landingBeginningMessages);
    }

}
