using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonologueEventListener : MonoBehaviour
{
    public Text dialogueBox;
    public Text monologueBox;
    public Text taskBox;

    public GameObject cookNPC;
    bool doneMop, doneChamberpot, doneAshes, doneCandle =false;
    void OnEnable()
    {
        // EventManager.OnClicked += Teleport;
        MonologueManager.onEndDialogue += FinishedDialogueCheck;
        MainCharMovement.onEndDialogue += FinishedDialogueCheck;
        Debug.Log("enable");
    }


    void OnDisable()
    {



    }


    void FinishedDialogueCheck(string name)
    {
        Debug.Log("Given name of finished dialogue " + name);
        Monologue monologue = new Monologue();
        if (name == "Knight")
        {
            //load in dialogue for scully
            if (!doneChamberpot) taskBox.text="Empty the chamber pots in the Kitchen";
            monologue.monologueName = "knight1PostMono";
            monologue.sentences = new string[] {
                "I hath spent years rotting in yond jail cell, anon it is time to taketh revenge on the King!",
                "I shall make yond FOPDOODLE regret locking ME in the dungeon!"
            };
            FindObjectOfType<MonologueManager>().StartDialogue(monologue, monologueBox);
        }else if (name=="Chambermaid"||name=="chamberMaid"){
            monologue.monologueName = "chambermaid1PostMono";
            doneMop=true;
            if (!doneCandle) taskBox.text="Bringeth the candles to the Steward";
            monologue.sentences = new string[] {
                "I-... ",
                "...has this wench asked me on a date?",
                "I am alarmed, yet strangely attracted by thy confidence...",
                "Just taketh the candles and carry on Scully, we doth not have time for simpletons."
            };
            FindObjectOfType<MonologueManager>().StartDialogue(monologue, monologueBox);
        }
        else if (name == "ThrewChamberpot")
        {
            taskBox.text="";
            monologue.monologueName = "Yuck";
            monologue.sentences = new string[] {
                "Yuck.",
                "This ain’teth it.",
                "Just wait till I killeth thy king and take over thy castle!"
            };

            FindObjectOfType<MonologueManager>().StartDialogue(monologue, monologueBox);
            cookNPC.SetActive(true);
            
        }else if (name=="Cook") {
            doneChamberpot =true;
            if (!doneMop) taskBox.text="Taketh the mop to the Servants Quarters";
            monologue.monologueName = "CookPost";
            monologue.sentences = new string[] {
                "I best hurry along before someone findeth out my scheme!"
                
            };
            FindObjectOfType<MonologueManager>().StartDialogue(monologue, monologueBox);
        } else if (name=="MoppedTheFloor"){
            taskBox.text="";
            monologue.monologueName = "chamberMaid";
            monologue.sentences = new string[] {
                "Well aren’t thou a sight to behold! I have not seen thee around here before!",
                "Art thou single?",
                "Would thou perhaps fancy a meal beneath the stars this evening with mine own company? ",
                "I swear the only thing I shall bite is the moldy bread I tucked away in my bosom, I promise thee! ",
                "They don’t calleth me ‘ye olde chambermaid’ for nothing!",
                "...",
                "Oh! Whilst thou art here, would thou please be a doe and bringeth these candles to the Steward?",
                "That LUBBERWORT scatters his belongings all over the castle!",
                "It is but a miracle he can leaveth his chamber without forgetting his own brain!",
                "A fine, young, sir such as thou must help a delicate lady in dire need! ;)"

            };
            FindObjectOfType<MonologueManager>().StartDialogue(monologue, dialogueBox);
        }
        else if (name=="GaveCandles"){
            taskBox.text="";
            doneCandle=true;
            monologue.monologueName = "steward1";
            monologue.sentences = new string[] {
                "Where art my candles?!",
                "Ah! Many thanks to you, fine sir!",
                "I have turned over every book and bottle in my chamber yet I could not findeth a single wick!",
                "I wonder what other items I may have lost!","Thou must work in the kitchen, correct?",
                "Please do me a favor young lad, I needeth thee to deliver this list to the Chef to fill the castle pantry.",
                "I’ll see to it that the cook will hear only praise from me if thou can complete the task!"

            };
            FindObjectOfType<MonologueManager>().StartDialogue(monologue, dialogueBox);
        }
        else if (name=="steward1"){
            taskBox.text="Delivereth the pantry list to the Chef";
            monologue.monologueName = "steward1Mono";
            monologue.sentences = new string[] {
                "What hath taken that WHIFFLE-WHAFFLE so long to not notice my presence?",
                "I dost not brag, yet I dost taketh pride of my stature.",
                "6’2” to be exacteth, not that it matters of course.",
                "What didst the Steward mean when he said he had lost other items in the castle? ",
                "I must look around to findeth whatever the item may be,","it could possibly assist my grand scheme to kill the King…"

            };
            FindObjectOfType<MonologueManager>().StartDialogue(monologue, monologueBox);
        }else if (name=="GaveList"){
            taskBox.text="";
            monologue.monologueName = "cook2";
            monologue.sentences = new string[] {
                "Oi! Where have thou been squandering about?",
                "Yond kitchen doth not clean itself!",
                "What rubbish is in thy hand, boy?",
                "The pantry list?!",
                "Thou must be trying to spoil mine own reputation!",
                "I must travel to the market, boy if thou knows what is good for thee,",
                "thou will delivereth this list with speed next time, if there is a next time!",
                "Before I make haste, delivereth the leftover scraps to the Chapel.",
                "I dost not know his intention, but the Chaplain begs for scraps every evening!",
                "Get to it before the rats do, their legs scurry faster than thy own, thou should feeleth ashamed!"

            };
            FindObjectOfType<MonologueManager>().StartDialogue(monologue, dialogueBox);
        }else if (name=="cook2"){
            taskBox.text="Bringeth the leftover scraps to the Chapel";
            monologue.monologueName = "cook2Post";
            monologue.sentences = new string[] {
                "Not this bloke again.",
                "He hath the face of a baby, yet the shrill squawk of a raven.",
                "If only the Falconer could tame his anger…",
                "The Chef has departed, good riddance. I still haveth no clue how to kill the King, perhaps the Chaplain withholds the information I seek.",
                "These scraps doth smell of death, God forbid I get the black plague."

            };
            FindObjectOfType<MonologueManager>().StartDialogue(monologue, monologueBox);
        }else if (name=="GaveLeftovers"){
            taskBox.text="";
            monologue.monologueName = "chaplain1";
            monologue.sentences = new string[] {
                "You shalt not jest with me, I possess the power of God AND Anime on MY side! AAAAAAAAAAA!",
                "The power of Christ compels thee!",
                "Be gone, THOUGHT!",
                "...",
                "That performance may hath been the most remarkable yet! I applaud thee Chaplain!", 
                "Aha! Our Holy Father has blessed us with yet another lamb for the flock! What is the purpose of this visit my Child? Ah! I see the Lord has graced us with more leftovers! Hallelujah! The orphans will be delighted!",
                "Tis by His grace that thou got to the scraps before the rats! The Steward detests rats in the kitchen, though they are still one of God’s wonderful creations!",
                "...",
                "My Child, I pray, would you be so kind and delivereth these spare ashes to the dungeon? I fear, if the Steward were to see them outside, my position of Chaplain would be at stake.","...unless a certain soul were to reveal the truth…",
                "I hath had it with thee! I am disgusted, I am revolted, I dedicate my entire life to our Lord and Saviour Jesus Christ and THIS is the mercy I receiveth?!"

               
            };
            //come back to finish this
            FindObjectOfType<MonologueManager>().StartDialogue(monologue, dialogueBox);
        }else if (name=="chaplain1"){
            if (!doneAshes) taskBox.text = "Delivereth the spare ashes to the Dungeon";
            monologue.monologueName = "chaplain1Post";
            monologue.sentences = new string[] {
                "The Chaplain has gone mad! I did not think of him to be such a SNOUTBAND!",
                "Perhaps from all the communion wine?",
                "I shall take my leave from this nonsense, I sure hope ‘tis God he is speaking with…",
                "The Chaplain did mention that the Steward abhors rats in the kitchen, I should take advantage of this knowledge..."
               
            };
            //come back to finish this
            FindObjectOfType<MonologueManager>().StartDialogue(monologue, monologueBox);
        }else if (name=="ThrewAshes"){
            doneAshes =true;
            taskBox.text="Bringeth the rat to the Kitchen";
            taskBox.color=Color.red;
            monologue.monologueName = "SawRat";
            monologue.sentences = new string[] {
                "What luck!",
                "I shall nameth thee Remi and he shall be mine and he shall be my Remi.",
                "Come brother, we hath a mission to fulfill in the kitchen!",
                "Thou can even teacheth me how to cook!"
            };
            FindObjectOfType<MonologueManager>().StartDialogue(monologue, monologueBox);
        }else if (name=="ReleasedRat"){
            taskBox.text="Unlocketh the Stewards Mysterious Door";
            monologue.monologueName = "releasedRatCook";
            monologue.sentences = new string[] {
                "Oi! What are thou just standing around for?",
                "A SADDLE-GOOSE like thyself should be scrubbing the pots!",
                "Tis a r-r-r-rat I s-s-see with m-m-ine own eyes?!",
                "P-p-prithee S-S-S-Scully! ",
                "Drive yond vermin out before I soil my trousers!",
                "EEEEEEEEKKK!!!",
                "Anon, the Steward shall be here any minute! Why, I ought to form a union after all the ghastly events I hath been through!",
                "I told that DRIGGLE-DRAGGLE that if it ever happens again, I ought to be prancing out with the entire kitchen staff!",
                "Surely a brave, honest soul like thyself would walk out with me, would you not Scully?"

            };
            FindObjectOfType<MonologueManager>().StartDialogue(monologue, dialogueBox);
        }else if (name=="releasedRatCook"){
            monologue.monologueName = "releasedRatMono";
            monologue.sentences = new string[] {
                "I… had not expected Chef to go off like so and coil at the sight of Remi…and to think I would walk out with that zany fry cook?",
                "Never!",
                "How dare he insult Remi, I think Remi is quite lovely and quaint.",
                "A little master chef!",
                "Very well, I shall tell the Steward. I wonder if I can findeth anything in his chamber that will lead me closer to my plan."
            };
            //make steward disappear
            //make key appear
            FindObjectOfType<MonologueManager>().StartDialogue(monologue, monologueBox);
        }else if (name=="OpenedDoor"){
            taskBox.text="";
            monologue.monologueName = "openedDoorMono";
            monologue.sentences = new string[] {
                "It fits! What mystery through yonder door revealeth?",
                
            };
            //make steward disappear
            //make key appear
            FindObjectOfType<MonologueManager>().StartDialogue(monologue, monologueBox);
        }else if (name=="gotPoison"){
            taskBox.text="Taketh the poison to the King’s Chamber";
            monologue.monologueName = "gotPoisonMono";
            monologue.sentences = new string[] {
                "Alas the poison I was looking for... My revenge is yet to prevail...",
            };
            //make steward disappear
            //make key appear
            FindObjectOfType<MonologueManager>().StartDialogue(monologue, monologueBox);
        }
        else{
            MainCharMovement.xPressed=0;
        }

    }
}
