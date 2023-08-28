using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject doorLeft, doorRight, doorUp, doorDown;
    public bool roomLeft, roomRight, roomUp, roomDown;
    public GameObject wallLeft, wallRight, wallUp, wallDown;

    public Transform[] doors;

    public Animation dooranim;
    public int stepToStart;
    public Text text;
    public int doorNumber;
    // Start is called before the first frame update
    void Start()
    {
        //布尔门是否生成
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);

        wallLeft.SetActive(!roomLeft);
        wallRight.SetActive(!roomRight);
        wallUp.SetActive(!roomUp);
        wallDown.SetActive(!roomDown);
    }

    public void UpdateRoom() 
    {
        stepToStart = (int)(Mathf.Abs(transform.position.x / 20.8f)+Mathf.Abs(transform.position.z/20.4f)/2);

        text.text = stepToStart.ToString();

        if (roomUp)
            doorNumber++;
        if (roomDown)
            doorNumber++;
        if (doorLeft)
            doorNumber++;
        if (doorRight)
            doorNumber++;

    }

    public void DoorController(int mode)
    {
        Debug.Log("开门");
        if (roomUp)
            PlayAnim(2,mode);
        if (roomDown)
            PlayAnim(3, mode);
        if (doorLeft)
            PlayAnim(1, mode);
        if (doorRight)
            PlayAnim(0, mode);
    }


    
    void PlayAnim(int index,int speed)
    {
        dooranim = doors[index].GetComponent<Animation>();
        dooranim["DoorAnim"].speed = speed;
        dooranim["DoorAnim"].time = dooranim["DoorAnim"].length;
        if (speed == -1)
        {

            dooranim.Play("DoorAnim");
            return;
        }
        dooranim["DoorAnim"].time = 0;
        dooranim.Play("DoorAnim");


    }

}
