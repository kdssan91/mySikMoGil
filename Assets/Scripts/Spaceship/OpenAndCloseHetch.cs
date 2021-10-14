using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OpenAndCloseHetch : MonoBehaviour
{
    public static OpenAndCloseHetch instance;

    public TextMeshProUGUI textMesh;
    public Animator anim;
    bool isHatchWorking= false;
    public bool isHatchOpened;
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;

    public Image buttonUiImg;


    private void Awake()
    {
        instance = this; 
    }

    public void OpenAndCloseHatch()
    {
        if (!isHatchOpened && !isHatchWorking)
        {
            SoundManager.instance.PlaySimpleVoice(0);
            isHatchWorking = true;
            Invoke("OpenHatch", 1.5f);
        }
        else if (isHatchOpened && !isHatchWorking)
        {
            SoundManager.instance.PlaySimpleVoice(1);
            isHatchWorking = true;
            Invoke("CloseHatch", 1.5f);
        }
    }

    void OpenHatch()
    {
        anim.SetTrigger("Open");
        audioSource.PlayOneShot(openSound);
        isHatchOpened = true;
    }

    void CloseHatch()
    {
        anim.SetTrigger("Close");
        audioSource.PlayOneShot(closeSound);
        isHatchOpened = false;
    }

    //gray 127 138 147   blue 0 140 255
    public void Hover()
    {
        buttonUiImg.color = new Color32(0, 140, 255, 255);
        SoundManager.instance.PlayUiHoverSound();
    }

    public void HoverExit()
    {
        buttonUiImg.color = new Color32(127, 138, 147, 255);
    }

    public void TrueHatchOpen()
    {
        textMesh.text = "해치 닫기";
        isHatchWorking = false;

    }

    public void FalseHatchOpen()
    {
        textMesh.text = "해치 열기";
        isHatchWorking = false;
    }
}
