using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MonologueTrigger : MonoBehaviour
{
    
    public Monologue monologue;
    public Text textboxToUse;
    public void TriggerMonologue(){
        FindObjectOfType<MonologueManager>().StartDialogue(monologue, textboxToUse);
    }

}
