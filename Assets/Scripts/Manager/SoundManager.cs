using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public SO_IntroMessages message;
    public ParticleSystem ps_effect; 

    [Header("AudioClips")]
    public AudioClip[] beginningAudioClips;
    public AudioClip[] landingAudioClips; 
    public AudioClip[] simpleVoices;
    public AudioClip shipIgnitionClip;
    public AudioClip shipEngineMaxClip;
    public AudioClip alarmClip;
    public AudioClip uiHoverSound; 

    [Header("Components")]
    public AudioSource audioSource;
    public GameObject textWindow;
    public GameObject buttonForHatch;
    public GameObject buttonForLanding; 

    public bool isPlayingNaviMsg = false; 

    private void Awake()
    {
        instance = this;
        buttonForHatch.SetActive(false);
        buttonForLanding.SetActive(false);
    }


    public void PlayMessageWithVoice(AudioClip[] clips, string[] msg)
    {
        StartCoroutine(PlayNaviMessage(clips, msg));
    }

    IEnumerator PlayNaviMessage(AudioClip[] clips, string[] msg)
    {
        isPlayingNaviMsg = true;
        textWindow.SetActive(true);
        yield return null;
        for (int i = 0; i < clips.Length; i++)
        {
            audioSource.clip = clips[i];
            audioSource.Play();
            MessageController.instance.textMesh.text = msg[i];
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
        isPlayingNaviMsg = false;
        audioSource.clip = null;
        MessageController.instance.textMesh.text = "";
        textWindow.SetActive(false);
        buttonForHatch.SetActive(true);
        buttonForLanding.SetActive(true);
    }

    public void PlayLandingSoundCoroutine()
    {
        StartCoroutine(PlayLandingSound()); 
    }

    IEnumerator PlayLandingSound()
    {
        isPlayingNaviMsg = true;
        textWindow.SetActive(true);
        yield return null;
        for (int i = 0; i < message.landingBeginningMessages.Length; i++)
        {
            audioSource.clip =  landingAudioClips[i];
            audioSource.Play();
            MessageController.instance.textMesh.text = message.landingBeginningMessages[i];
            yield return new WaitWhile(() => audioSource.isPlaying);

            if(i == 0)
            {
                audioSource.PlayOneShot(shipIgnitionClip);
            }
            else if(i == 1)
            {
                audioSource.PlayOneShot(shipEngineMaxClip);
                textWindow.SetActive(false);
                ps_effect.Play();
                Invoke("SceneChange", 10f);
            }
        }
        isPlayingNaviMsg = false;
        MessageController.instance.textMesh.text = "";
        textWindow.SetActive(false);
    }

    void SceneChange()
    {
        SceneManager.LoadScene(1);
    }

    public void PlaySimpleVoice(int index)
    {
        //0 해치를 엽니다. 
        //1 해치를 닫습니다. 
        //2 해치를 먼저 닫아주십시오. 
        //3 귀환을 위해 선내로 이동해 주십시오. 
        audioSource.PlayOneShot(simpleVoices[index]); 
    }

    public void PlayShipIgnitionSound()
    {
        audioSource.PlayOneShot(shipIgnitionClip); 
    }

    public void PlayShipMaxEngineSound()
    {
        audioSource.PlayOneShot(shipEngineMaxClip); 
    }

    public void PlayUiHoverSound()
    {
        audioSource.PlayOneShot(uiHoverSound);
    }
}
