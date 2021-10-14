using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="IntroMessage", menuName ="Intro/Messages") ]
public class SO_IntroMessages : ScriptableObject
{   
    [TextArea] public string[] beginningMessages;
    [TextArea] public string[] landingBeginningMessages; 
}
