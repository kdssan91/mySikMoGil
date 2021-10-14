using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro;

public class LandingToEarth : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public Image LandingUiBtn;
    public bool isLandingStarted = false;

    public GameObject landingButton;
    public GameObject hatchButton;

    public Light pointLight;
    public float maxLightIntensity = 0f; 
    public float minLightIntensity = 3.5f;
    public float flickerTime = 2f;
    float currTimeFlick =0;


    bool isMaxLight = false;

    public void MoveToMainScene()
    {
        if (!Boundry.instance.isPlayerInShip)
        {
            SoundManager.instance.PlaySimpleVoice(3); 
            return; 
        }
        else if (OpenAndCloseHetch.instance.isHatchOpened)
        {
            SoundManager.instance.PlaySimpleVoice(2);
            return; 
        }
        StartLanding(); 
    }

    void StartLanding()
    {
        isLandingStarted = true;
        landingButton.SetActive(false);
        hatchButton.SetActive(false);
        SoundManager.instance.PlayLandingSoundCoroutine();
    }



    public void Hover()
    {
        LandingUiBtn.color = new Color32(0, 140, 255, 255);
        SoundManager.instance.PlayUiHoverSound();
    }

    public void HoverExit()
    {
        LandingUiBtn.color = new Color32(127, 138, 147, 255);
    }

    private void Update()
    {
        if (isLandingStarted)
        {
            currTimeFlick += Time.deltaTime;
            if(!isMaxLight)
            {
                pointLight.intensity = Mathf.Lerp(minLightIntensity, maxLightIntensity, currTimeFlick / flickerTime);
                if(pointLight.intensity >= maxLightIntensity)
                {
                    isMaxLight = true;
                    currTimeFlick = 0; 
                }
            }
            else
            {
                pointLight.intensity = Mathf.Lerp(maxLightIntensity, minLightIntensity, currTimeFlick / flickerTime);
                if(pointLight.intensity <= 0.1f)
                {
                    isMaxLight = false;
                    currTimeFlick = 0;
                }
            }
        }
    }
}
