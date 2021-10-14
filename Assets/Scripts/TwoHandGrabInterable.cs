using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Digger;

public class TwoHandGrabInterable : XRGrabInteractable
{
    public List<XRSimpleInteractable> secondHandGripPoints = new List<XRSimpleInteractable>();
    public XRSimpleInteractable secondInteractable; 
    XRBaseInteractor secondInteractor;
    Quaternion attachInitialRotation;

    public bool snapToSecondHand = true ;
    bool isGrabbed = false; 
    public Transform targetTransform; 
    Quaternion initialRotationOffset;

    DiggerRuntimeUsageExample diggerRuntimeUsageExample;

    Rigidbody rigid; 
        
    [System.Obsolete]
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        diggerRuntimeUsageExample = GetComponent<DiggerRuntimeUsageExample>(); 

        foreach (var item in secondHandGripPoints)
        {
            item.onSelectEntered.AddListener(OnSecondHandGrab);
            item.onSelectExited.AddListener(OnSecondHandRelease);
        }
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {

        if(secondInteractor && selectingInteractor)
        {
            if (snapToSecondHand)
            {
                selectingInteractor.attachTransform.rotation = GetTwoHandRotation();
            }
            else
            {
                selectingInteractor.attachTransform.rotation = GetTwoHandRotation() * initialRotationOffset;
            }
        }

        base.ProcessInteractable(updatePhase);
    }

    private Quaternion GetTwoHandRotation()
    {
        Quaternion targetRotation;
        targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position,
            secondInteractor.attachTransform.up);

        return targetRotation;
    }


    public void OnSecondHandGrab(XRBaseInteractor interactor)
    {
        secondInteractor = interactor;
        initialRotationOffset = Quaternion.Inverse(GetTwoHandRotation()) * selectingInteractor.attachTransform.rotation;
        diggerRuntimeUsageExample.isUsingSecondGrip = true; 
    }

    public void OnSecondHandRelease(XRBaseInteractor interactor)
    {
        secondInteractor = null;
        diggerRuntimeUsageExample.isUsingSecondGrip = false;

    }
    [System.Obsolete]
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);
        attachInitialRotation = interactor.attachTransform.localRotation;
        secondInteractable.enabled = true;
        CancelInvoke("ReturnToBag");
        isGrabbed = true; 

    }
    [System.Obsolete]
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        secondInteractor = null;
        interactor.attachTransform.localRotation = attachInitialRotation;
        diggerRuntimeUsageExample.isUsingSecondGrip = false;
        secondInteractable.enabled = false;
        Invoke("ReturnToBag", 3f);

    }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        bool isAlreadyGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }

    public void ReturnToBag()
    {
        isGrabbed = false;
        rigid.useGravity = false;

    }

    private void Update()
    {
        if (!isGrabbed)
        {
            transform.position = targetTransform.position;
        }
        
    }
}
