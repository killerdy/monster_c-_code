using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoblieUI : MonoBehaviour
{
    public GameObject player;
    RectTransform rockIn;
    EventTrigger left, right, fireball, fireballs, jump;
    Button bg;
    RuntimePlatform platform;
    //[DllImport("user32.dll", EntryPoint = "keybd_event")]
    //static extern void keybd_event(
    //    byte key,
    //    byte bScan,
    //    int flags,//按下0，按住1，释放2
    //    int dwExtraInfo
    //    );
    //[DllImport("user32.dll", EntryPoint = "mouse_event")]
    //static extern void mouse_event(
    //     int dwFlag,
    //     int dx,
    //     int dy,
    //     int cButtons,
    //     int dwExtraInfo
    //    );

    // Start is called before the first frame update
    void Start()
    {
        platform = Application.platform;
        rockIn = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();

        if (platform != RuntimePlatform.Android)
            gameObject.SetActive(false);
        //left = transform.GetChild(0).GetChild(0).GetComponent<EventTrigger>();
        //right = transform.GetChild(0).GetChild(1).GetComponent<EventTrigger>();
        //fireball = transform.GetChild(1).GetChild(0).GetComponent<EventTrigger>();
        //fireballs = transform.GetChild(1).GetChild(1).GetComponent<EventTrigger>();
        //jump = transform.GetChild(1).GetChild(2).GetComponent<EventTrigger>();
        //left.triggers[0].callback.AddListener();

    }
    private void Update()
    {
        if (rockIn.anchoredPosition.x < -1)
            player.GetComponent<PlayerMovement>().inputX = -1;
        else if (rockIn.anchoredPosition.x > 1)
            player.GetComponent<PlayerMovement>().inputX = 1;
        else
            player.GetComponent<PlayerMovement>().inputX = 0;
    }
    public void Joy(Vector2 v)
    {
        Debug.Log(v);
        //player.GetComponent<PlayerMovement>().inputX = -1;
    }

    public void MoveLeftDown()
    {
        player.GetComponent<PlayerMovement>().inputX = -1;
    }
    public void MoveLeftUp()
    {
        player.GetComponent<PlayerMovement>().inputX = 0;
    }
    public void MoveRightDown()
    {
        player.GetComponent<PlayerMovement>().inputX = 1;
    }
    public void MoveRightUp()
    {
        player.GetComponent<PlayerMovement>().inputX = 0;
    }
    // Update is called once per frame
    //public void Left0()
    //{
    //    keybd_event(65, 0, 0, 0);
    //}
    //public void Left1()
    //{
    //    keybd_event(65, 0, 1, 0);
    //}
    //public void Left2()
    //{
    //    keybd_event(65, 0, 2, 0);
    //}
    //public void Right0()
    //{
    //    keybd_event(68, 0, 0, 0);
    //}
    //public void Right1()
    //{
    //    keybd_event(68, 0, 1, 0);
    //}
    //public void Right2()
    //{
    //    keybd_event(68, 0, 2, 0);
    //}
    //    public void Fire()
    //    {
    //        mouse_event(2, 100, 100, 0, 0);
    //        mouse_event(4, 100, 100, 0, 0);

    //    }
    //    public void Fires()
    //    {
    //        mouse_event(8, 100, 100, 0, 0);
    //        mouse_event(16, 100, 100, 0, 0);
    //    }
    //    public void Jump0()
    //    {
    //        keybd_event(32, 0, 0, 0);
    //    }
    //    public void Jump1()
    //    {
    //        keybd_event(32, 1, 0, 0);
    //    }
    //    public void Jump2()
    //    {
    //        keybd_event(32, 2, 0, 0);
    //    }

}
