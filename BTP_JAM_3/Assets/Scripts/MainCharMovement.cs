using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MainCharMovement : MonoBehaviour
{
    private BoxCollider2D boxCol;
    public GameObject elevatorUI;
    public static int facingRight = 1;
    private GameObject pickupable, carrying, speakable, observableItem, NPCchar, area;
    public static bool onElevator = false;
    public static int xPressed = 0;
    public static bool monologueing = false;
    public static bool hasKey;
    public SpriteRenderer head, body, legR, legL;
    public delegate void endDialogueAction(string monoName);
    public static event endDialogueAction onEndDialogue;
    AudioSource[] allSongs;
    Animator scullyAnim;
    public GameObject releaseRatArea, giveListArea, key, steward, KingFloorBtn;
    // Start is called before the first frame update
    void Start()
    {
        boxCol = transform.GetComponent<BoxCollider2D>();
        scullyAnim = gameObject.GetComponent<Animator>();
        //enable when ready to build? idk
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        hasKey = false;
        allSongs = gameObject.GetComponents<AudioSource>();
        allSongs[0].Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            //interact
            if (monologueing)
            {
                NPCchar = null;
                observableItem = null;

                GameObject monologueManager = GameObject.Find("MonologueManager");
                monologueManager.GetComponent<MonologueManager>().DisplayNextSentence();
            }
            else if (pickupable != null)
            {
                pickUpItem();
            }
            else if (observableItem != null)
            {
                if (xPressed == 0)
                {
                    observableItem.GetComponent<MonologueTrigger>().TriggerMonologue();
                    xPressed = xPressed + 1;
                    observableItem.gameObject.transform.GetChild(0).transform.gameObject.SetActive(false);
                }
                else
                {
                    GameObject monologueManager = GameObject.Find("MonologueManager");
                    monologueManager.GetComponent<MonologueManager>().DisplayNextSentence();
                }
            }
            else if (NPCchar != null)
            {
                if (xPressed == 0)
                {
                    NPCchar.GetComponent<MonologueTrigger>().TriggerMonologue();
                    xPressed = xPressed + 1;
                    NPCchar.gameObject.transform.GetChild(0).transform.gameObject.SetActive(false);

                }
                else
                {
                    GameObject monologueManager = GameObject.Find("MonologueManager");
                    monologueManager.GetComponent<MonologueManager>().DisplayNextSentence();
                }
            }
            else if (carrying && speakable == null && !ElevatorController.elevatorTaken)
            {
                dropItem();
            }



        }

    }


    void FixedUpdate()
    {
        scullyAnim.SetBool("walkingState", false);
        if (xPressed == 0 && !MainCharMovement.monologueing)
        {
            HandleMovement();
        }

    }

    private void HandleMovement()
    {
        float moveSpeed = 5.5f;
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;
        if (movement.x < 0f)
        {
            facingRight = -1;
            // this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            head.flipX = true;
            body.flipX = true;
            legL.flipX = true;
            legR.flipX = true;
            scullyAnim.SetBool("walkingState", true);

        }
        else if (movement.x > 0f)
        {
            facingRight = 1;
            // this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            head.flipX = false;
            body.flipX = false;
            legL.flipX = false;
            legR.flipX = false;
            scullyAnim.SetBool("walkingState", true);

        }



    }


    public void pickUpItem()
    {
        if (carrying == null)
        {
            if (facingRight == 1)
            {
                scullyAnim.SetTrigger("pickupState");
            }
            else
            {
                scullyAnim.SetTrigger("pickupStateL");
            }

            pickupable.transform.parent = gameObject.transform;
            Vector2 pos2 = new Vector2(0.06f, 0.3f);
            pickupable.GetComponent<RectTransform>().DOJumpAnchorPos(pos2, 1, 1, 0.75f).SetDelay(0.25f);
            carrying = pickupable;
            if (carrying.name == "key")
            {
                hasKey = true;
                //destroy secret door?
            }
            else if (carrying.name == "poison")
            {
                Destroy(area);
                if (onEndDialogue != null)
                {
                    onEndDialogue("gotPoison");
                }
                //enable button for kings
                KingFloorBtn.SetActive(true);
            }

            pickupable = null;
        }

    }

    public void dropItem()
    {
        Vector2 endPos = new Vector2(gameObject.transform.position.x + (2.5f * MainCharMovement.facingRight), gameObject.transform.position.y - 0.75f);
        carrying.transform.parent = null;
        carrying.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.75f);
        carrying.GetComponent<RectTransform>().DOJumpAnchorPos(endPos, 1, 1, 0.75f);



        //if in area
        if (area != null && area.name == "Window" && carrying && carrying.name == "chamberpot")
        {
            Destroy(carrying);
            Destroy(area);
            if (onEndDialogue != null)
            {
                onEndDialogue("ThrewChamberpot");
            }
        }
        else if (area != null && area.name == "MopArea" && carrying && carrying.name == "mop")
        {
            Destroy(carrying);
            Destroy(area);
            if (onEndDialogue != null)
            {
                onEndDialogue("MoppedTheFloor");
                giveListArea.SetActive(true);
            }
        }
        else if (area != null && area.name == "GiveCandles" && carrying && carrying.name == "candle")
        {
            Destroy(carrying);
            Destroy(area);
            if (onEndDialogue != null)
            {
                onEndDialogue("GaveCandles");
            }
        }
        else if (area != null && area.name == "GiveList" && carrying && carrying.name == "list")
        {
            Destroy(carrying);
            Destroy(area);
            if (onEndDialogue != null)
            {
                onEndDialogue("GaveList");
            }
            releaseRatArea.SetActive(true);
        }
        else if (area != null && area.name == "GiveLeftovers" && carrying && carrying.name == "leftovers")
        {
            Destroy(carrying);
            Destroy(area);
            if (onEndDialogue != null)
            {
                onEndDialogue("GaveLeftovers");
            }
        }
        else if (area != null && area.name == "ThrowAshes" && carrying && carrying.name == "ashes")
        {
            Destroy(carrying);
            Destroy(area);
            if (onEndDialogue != null)
            {
                onEndDialogue("ThrewAshes");
            }
        }
        else if (area != null && area.name == "ReleaseRat" && carrying && carrying.name == "rat")
        {
            Destroy(area);
            if (onEndDialogue != null)
            {
                onEndDialogue("ReleasedRat");
            }
            key.SetActive(true);
            steward.SetActive(false);
        }
        else if (area != null && area.name == "SecretDoor" && carrying && carrying.name == "key")
        {
            Destroy(area);
            Destroy(carrying);
            if (onEndDialogue != null)
            {
                onEndDialogue("OpenedDoor");
            }
            //start eerie music
            allSongs[0].Stop();
            //stop other music
            allSongs[1].Play();
        }
        carrying = null;
    }


    // when the GameObjects collider arrange for this GameObject to travel to the left of the screen
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Interactable")
        {
            col.gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
            //allow Pickup
            pickupable = col.gameObject;

        }
        else if (col.gameObject.tag == "NPC")
        {
            col.gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
            NPCchar = col.gameObject;

        }
        else if (col.gameObject.tag == "Observable")
        {
            //not sure about this
            col.gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
            observableItem = col.gameObject;


        }
        else if (col.gameObject.tag == "Area")
        {
            area = col.gameObject;
            Debug.Log(area.name);
            //check for cases to complete task
            if (area && area.name == "Window" && carrying && carrying.name == "chamberpot")
            {
                col.gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
            }
            else if (area && area.name == "MopArea" && carrying && carrying.name == "mop")
            {
                col.gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
            }
            else if (area && area.name == "GiveCandles" && carrying && carrying.name == "candle")
            {
                col.gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
            }
            else if (area && area.name == "GiveList" && carrying && carrying.name == "list")
            {
                col.gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
            }
            else if (area && area.name == "GiveLeftovers" && carrying && carrying.name == "leftovers")
            {
                col.gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
            }
            else if (area != null && area.name == "ThrowAshes" && carrying && carrying.name == "ashes")
            {
                col.gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
            }
            else if (area != null && area.name == "ReleaseRat" && carrying && carrying.name == "rat")
            {
                col.gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
            }
            else if ((col.gameObject.name == "SecretDoor"))
            {
                col.gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
                // col.gameObject.transform.GetChild(1).transform.gameObject.GetChild(0).text =

            }


        }
    }

    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D col)
    {
        //this is def gonna bite me in the butt
        if (col.gameObject.tag == "Interactable")
        {
            pickupable = null;
            col.gameObject.transform.GetChild(0).transform.gameObject.SetActive(false);

        }
        else if (col.gameObject.tag == "NPC")
        {
            NPCchar = null;
            col.gameObject.transform.GetChild(0).transform.gameObject.SetActive(false);

        }
        else if (col.gameObject.tag == "Observable")
        {
            observableItem = null;
            col.gameObject.transform.GetChild(0).transform.gameObject.SetActive(false);

        }
        else if (col.gameObject.tag == "Area")
        {
            area = null;
            col.gameObject.transform.GetChild(0).transform.gameObject.SetActive(false);


        }


    }
}
