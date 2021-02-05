using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MonologueManager : MonoBehaviour
{
    // public Text nameText;
    private Text textBoxG;
    public GameObject dialogueGraphic;
    private Queue <string> monologueSentences;
    public Dictionary<string, int> observableDialogue;
    // Start is called before the first frame update
    public delegate void endDialogueAction(string monoName);
    public static event endDialogueAction onEndDialogue;
    Monologue currentMonologue;
    void Start()
    {
        monologueSentences = new Queue<string>();
    }
    void handleDialogueBoxMovement(Monologue monologue){
        int strength=2;
        int randomness = 40;
        if (monologue.monologueName=="Knight"||monologue.monologueName=="cook2"||monologue.monologueName=="Cook"||monologue.monologueName=="releasedRatCook"||monologue.monologueName=="chaplain1"||monologue.monologueName=="releasedRatPost"){
            strength =8;
            randomness =90;
        }
        dialogueGraphic.GetComponent<RectTransform>().DOShakeAnchorPos(15f, new Vector2(3, 3), strength,randomness ,false , false);
        
    }

    public void StartDialogue(Monologue monologue, Text textBox){
        if (textBox.name=="DialogueText"){
            if (MainCharMovement.monologueing) return;
            dialogueGraphic.SetActive(true);
            handleDialogueBoxMovement(monologue);
            MainCharMovement.monologueing =true;

            //depending on who

            //shaky
            
        } else{
            dialogueGraphic.SetActive(false);
            MainCharMovement.monologueing =true;
            
        }
        currentMonologue = monologue;
        monologueSentences.Clear();
        textBoxG=textBox;
        foreach (string sentence in monologue.sentences){
            monologueSentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence(){
        if (monologueSentences.Count ==0){
            EndDialogue();
            return;
        }
        string sentence = monologueSentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        
    }
    void EndDialogue(){
        textBoxG.text="";
        
        if (textBoxG.name=="DialogueText"){
            dialogueGraphic.SetActive(false);
            MainCharMovement.monologueing =false;

            
        } else{
            MainCharMovement.monologueing =false;
        }
        
        if(onEndDialogue != null){
            onEndDialogue(currentMonologue.monologueName);
        } 
        
    }
    IEnumerator TypeSentence (string sentence){
        //ToCharArray()
        textBoxG.text="";
        foreach (char letter in sentence.ToCharArray()){
            textBoxG.text +=letter;
            dialogueGraphic.GetComponent<AudioSource>().Play();

            yield return new WaitForSeconds(0.01f);
            yield return null;
        }
        dialogueGraphic.GetComponent<AudioSource>().Stop();
    }
    // IEnumerator TypeSentence(string sentence)
    // {
    //     textBoxG.text = "";
    //     m_showWholeSentence = false;

    //     foreach (char letter in sentence.ToCharArray())
    //     {
    //         textBoxG.text += letter;

    //         if (!m_showWholeSentence)
    //             yield return null;
    //     }
    //     // canClick = true;
    // }

    bool m_showWholeSentence;
}
