using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    public static Map instance;
    [Header("parameter")]
    public Dictionary<string, float> map = new Dictionary<string, float>();
    public Dictionary<string, string> mapString = new Dictionary<string, string>();
    public Dictionary<string, int> scene = new Dictionary<string, int>()
    {
        {"login",0 },
        {"home",1 },
        {"game1",2 }
    };

    public string datapath;
    Player player;
    string js;
    void Start()
    {
        datapath = Application.persistentDataPath + "/SaveFiles/PlayerData.json";
        instance = this;
        player = new Player();
        if (File.Exists(datapath))
        {
            ReadData();
            UpdateMap();
        }
    }
    public void ReadData()
    {
        StreamReader sr = new StreamReader(datapath);
        js = sr.ReadToEnd();
        sr.Close();
        if (js != "")
            player = JsonUtility.FromJson<Player>(js);
    }
    public void UpdateMap()
    {
        Map.instance.map["skillsNum"] = player.skillsNum;
        Map.instance.map["health"] = player.health;
        Map.instance.map["magic"] = player.magic;
        Map.instance.map["attack"] = player.attack;
        Map.instance.map["defense"] = player.defense;
        Map.instance.map["cirt"] = player.cirt;
        Map.instance.map["cirtEffect"] = player.cirtEffect;
        Map.instance.map["lv"] = player.lv;
        Map.instance.map["exp"] = player.exp;
        Map.instance.map["hp"] = player.health;
        Map.instance.map["mp"] = player.magic;
        Map.instance.mapString["name"] = player.name;
        if(SceneManager.GetActiveScene().buildIndex!=1)
        Invoke("UpdataScore", 1f);
    }
    void UpdataScore()
    {
        Score.instance.maxScore += player.lv;
        Score.instance.EnemyScore += player.lv;
        if(player.lv%3==0)
        Score.instance.haveBoss = true;
    }
    public void UpdatePlayer()
    {
        player.skillsNum = (int)Map.instance.map["skillsNum"];
        player.health = (int)Map.instance.map["health"];
        player.magic = (int)Map.instance.map["magic"];
        player.attack = (int)Map.instance.map["attack"];
        player.defense = (int)Map.instance.map["defense"];
        player.cirt = Map.instance.map["cirt"];
        player.cirtEffect = Map.instance.map["cirtEffect"];
        player.lv = (int)Map.instance.map["lv"];
        player.exp = (int)Map.instance.map["exp"];
        player.health = (int)Map.instance.map["hp"];
        player.magic = (int)Map.instance.map["mp"];
        player.name = Map.instance.mapString["name"];
    }
    public void WriteData()
    {
        StreamWriter sw = new StreamWriter(datapath);
        js = JsonUtility.ToJson(player);

        //Hashtable param=new Hashtable();
        //param["rank"] = player.lv;
        sw.WriteLine(js);
        sw.Close();
        //index.instance.SaveDate(param);
    }
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    void Update()
    {

    }

}
