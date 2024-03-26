using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Diagnostics.Tracing;
using System.Globalization;


class person
{
    public string name;
    public int age;
    public int health;
}
public class main : MonoBehaviour
{
    public Text text;
    person p;
    string datapath;
    string js;
    private void Init()
    {
        //datapath = Application.dataPath + "/SaveFiles" + "/dy.json";
        //datapath = Application.dataPath + "/Resources" + "/dy.json";
        datapath = Application.persistentDataPath + "/SaveFiles";
        if(!Directory.Exists(datapath))
        {
            Directory.CreateDirectory(datapath);
        }
        datapath += "/dy.json";
        p = new person();
        ReadData();
        if (js == null)
        {
            p.age = 1;
            p.name = "dy";
            p.health = 1;
            js = JsonUtility.ToJson(p);
        }
    }
    public void ReadData()
    {
        StreamReader sr = new StreamReader(datapath);
        js = sr.ReadToEnd();
        sr.Close();
        p = JsonUtility.FromJson<person>(js);
        //Debug.Log(js);
    }
    // Start is called before the first frame update
    public void WriteData()
    {
        //Debug.Log(js);
        StreamWriter sw = new StreamWriter(datapath);
        sw.WriteLine(js);
        sw.Close();
        //Debug.Log(datapath);
        //sw.Dispose();
    }
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "HP" + p.health;
    }
    public void addHealth()
    {
        p.health++;
    }
    public void disHealth()
    {
        p.health--;
    }
    public void saveHealth()
    {
        js = JsonUtility.ToJson(p);
        WriteData();
    }
    public void loadHealth()
    {
        ReadData();
    }
    public void end()
    {
        Application.Quit();
    }
}
