using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ElevatorController : MonoBehaviour
{
    public RectTransform dialogueBox, elevator;
    public GameObject elevatorBounds, elevatorUI,fifthFloorBtn, towerFloorBtn,fourthFloorBtn,thirdFloorBtn, secFloorBtn, mainFloorBtn, defaultButton;

    Dictionary<string,float> elevatorFloors= new Dictionary<string, float>{
        {"floor1Button", 116.59f}, 
        {"floor2Button", 133.52f}, 
        {"floor3Button", 146.77f}, 
        {"floor4Button", 162.96f}, 
        {"floor5Button", 187.04f}, 
        {"floor6Button", 247.58f}, 
        {"floor7Button", 246.85f}
    };
    public static bool elevatorTaken=false;
    public AudioSource elevatorSound;
    
    // Start is called before the first frame update
    void Start()
    {
        // dialogueBox.DOPunchScale(new Vector2(40,40), 1f);
        defaultButton.GetComponent<Button>().Select();
        towerFloorBtn.SetActive(false); //until get poison
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  IEnumerator EnableDisableElevatorBounds(float seconds)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        elevatorBounds.SetActive(true);
        yield return new WaitForSeconds(seconds);
        elevatorBounds.SetActive(false);


    }
    // void playDingWhenAtFloor(){
    //     gameObject.GetComponent<AudioSource>().Play();
    // }
    IEnumerator playDingWhenAtFloor(float seconds)
    {
        elevatorSound.Play();
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(seconds);
        gameObject.GetComponent<AudioSource>().Play();
        elevatorSound.Stop();

    }
    public void goTo2ndFloor(){
        float timeTakes = 2.75f;
        elevator.DOAnchorPos(new Vector2(-9.12f, elevatorFloors["floor2Button"]), timeTakes).SetEase(Ease.InOutQuad);
        StartCoroutine(EnableDisableElevatorBounds(timeTakes));
        StartCoroutine(playDingWhenAtFloor(timeTakes));
    }

     public void goTo3rdFloor(){
        float timeTakes = 2.75f;
        elevator.DOAnchorPos(new Vector2(-9.12f, elevatorFloors["floor3Button"]), timeTakes).SetEase(Ease.InOutQuad);
        StartCoroutine(EnableDisableElevatorBounds(timeTakes));
        StartCoroutine(playDingWhenAtFloor(timeTakes));
    }
    public void goTo4thFloor(){
        float timeTakes = 2.75f;
        elevator.DOAnchorPos(new Vector2(-9.12f, elevatorFloors["floor4Button"]), timeTakes).SetEase(Ease.InOutQuad);
        StartCoroutine(EnableDisableElevatorBounds(timeTakes));
        StartCoroutine(playDingWhenAtFloor(timeTakes));
    }
    public void goTo5thFloor(){
        float timeTakes = 2.75f;
        elevator.DOAnchorPos(new Vector2(-9.12f, elevatorFloors["floor5Button"]), timeTakes).SetEase(Ease.InOutQuad);
        StartCoroutine(EnableDisableElevatorBounds(timeTakes));
        StartCoroutine(playDingWhenAtFloor(timeTakes));
    }
      public void goToTowerFloor(){
        float timeTakes = 7f;
        elevator.DOAnchorPos(new Vector2(-9.12f, elevatorFloors["floor6Button"]), timeTakes).SetEase(Ease.InOutQuad);
        StartCoroutine(EnableDisableElevatorBounds(timeTakes));
        StartCoroutine(playDingWhenAtFloor(timeTakes));
    }

      public void goToMainFloor(){
        float timeTakes = 2.75f;
        elevator.DOAnchorPos(new Vector2(-9.12f, elevatorFloors["floor1Button"]), timeTakes).SetEase(Ease.InOutQuad);
        StartCoroutine(EnableDisableElevatorBounds(timeTakes));
        StartCoroutine(playDingWhenAtFloor(timeTakes));

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            elevatorTaken=true;
        //    elevatorUI.SetActive(true);
            towerFloorBtn.GetComponent<RectTransform>().DOLocalMoveX(324, 0.8f,false);
            fifthFloorBtn.GetComponent<RectTransform>().DOLocalMoveX(324, 0.7f,false);
            fourthFloorBtn.GetComponent<RectTransform>().DOLocalMoveX(324, 0.6f,false);
            thirdFloorBtn.GetComponent<RectTransform>().DOLocalMoveX(324, 0.5f,false);
            secFloorBtn.GetComponent<RectTransform>().DOLocalMoveX(324, 0.4f,false);
            mainFloorBtn.GetComponent<RectTransform>().DOLocalMoveX(324, 0.3f,false);

            mainFloorBtn.GetComponent<Button>().Select();


        }
    }

    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            elevatorTaken=false;

        //    elevatorUI.SetActive(false);
            towerFloorBtn.GetComponent<RectTransform>().DOLocalMoveX(525, 0.3f,false);
            fifthFloorBtn.GetComponent<RectTransform>().DOLocalMoveX(525, 0.4f,false);
            fourthFloorBtn.GetComponent<RectTransform>().DOLocalMoveX(525, 0.5f,false);
            thirdFloorBtn.GetComponent<RectTransform>().DOLocalMoveX(525, 0.6f,false);
            secFloorBtn.GetComponent<RectTransform>().DOLocalMoveX(525, 0.7f,false);
            mainFloorBtn.GetComponent<RectTransform>().DOLocalMoveX(525, 0.8f,false);
            defaultButton.GetComponent<Button>().Select();
        }
    }

}
