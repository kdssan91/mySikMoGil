using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Fly : MonoBehaviour
{
    XRIDefaultInputActions controller;

    public float flySpeed;
    public float boosterForce; 

    public GameObject leftHandFlyObj;
    public GameObject hand;
    public GameObject naviArrow;
    public GameObject twinkleTrigger;

    public GameObject flyCube;
    public FlyingCubeController flyingCubeController;
    public Rigidbody flyCubeRigidbody;

    public bool isFlying; 


    public AudioSource jetAudioSource;
    public AudioClip jetSound;
    public AudioClip ignitionSound;
    public AudioClip airDiffusingSound;

    public Animator leftHandAnim;


    public bool isIntroScene = false;
    bool isEverFlied = false; 
    public Animator rightHandAnim; 

    private void Start()
    {
        controller = new XRIDefaultInputActions();
        controller.Enable();
        jetAudioSource.clip = jetSound;

        if (isIntroScene)
        {
            StartCoroutine(FlickTrigger());
        }
    }


    private void Update()
    {
        if (controller.XRILeftHand.Activate.ReadValue<float>() > 0.1f)
        {
            Vector3 moveDir = leftHandFlyObj.transform.position - hand.transform.position;
            flyCubeRigidbody.AddForce(moveDir * flySpeed);
            isFlying = true;
            naviArrow.SetActive(true); 
        }
        
        if(controller.XRILeftHand.Activate.ReadValue<float>() <= 0 && isFlying)
        {
            jetAudioSource.Stop();
            jetAudioSource.PlayOneShot(airDiffusingSound);
            isFlying = false;
            leftHandAnim.SetTrigger("Release");
            naviArrow.SetActive(false);
        }

        if (isFlying)
        {
            if (controller.XRILeftHand.Select.triggered)
            {
                Debug.Log("Booster shot");
                Vector3 moveDir = leftHandFlyObj.transform.position - hand.transform.position;
                flyCubeRigidbody.AddForce(moveDir * boosterForce, ForceMode.Impulse);
            }
        }

        if (controller.XRILeftHand.Activate.triggered)
        {
            jetAudioSource.PlayOneShot(ignitionSound);
            jetAudioSource.Play();
            leftHandAnim.SetTrigger("Trigger");
        }

        if (isIntroScene)
        {
            if (controller.XRIRightHand.Select.triggered) 
            {
                rightHandAnim.SetTrigger("Trigger");
            }

            if (controller.XRILeftHand.Activate.triggered && !isEverFlied)
            {
                isEverFlied = true; 
            }
        }

        //if (controller.XRIRightHand.Activate.triggered)
        //{
        //    BagController.instance.FlyToPlayer();
        //}

    }

    IEnumerator FlickTrigger()
    {
        while (!isEverFlied)
        {
            twinkleTrigger.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            twinkleTrigger.SetActive(true);

            yield return new WaitForSeconds(0.5f);
        }
        twinkleTrigger.SetActive(false);
    }
}
