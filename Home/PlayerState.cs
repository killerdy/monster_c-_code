using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        
    }
    private void Awake()
    {
        //DontDestroyOnLoad(this);
    }
    // Update is called once per frame
    void Update()
    {
        
        if (player != null)
        {
            transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Map.instance.mapString["name"];
            transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "µÈ¼¶" + Map.instance.map["lv"];
            transform.GetChild(1).GetChild(4).GetComponent<Text>().text = ((int)(player.GetComponent<Parameter>().map["health"])).ToString() + "/" + ((int)(player.GetComponent<Parameter>().map["maxHealth"])).ToString();
            transform.GetChild(1).GetChild(5).GetComponent<Text>().text = ((int)(player.GetComponent<Parameter>().map["magic"])).ToString() + "/" + ((int)(player.GetComponent<Parameter>().map["maxMagic"])).ToString();
            transform.GetChild(1).GetChild(2).GetComponent<Image>().fillAmount = player.GetComponent<Parameter>().map["health"] / player.GetComponent<Parameter>().map["maxHealth"];
            transform.GetChild(1).GetChild(3).GetComponent<Image>().fillAmount = player.GetComponent<Parameter>().map["magic"] / player.GetComponent<Parameter>().map["maxMagic"];
        }
    }
}
