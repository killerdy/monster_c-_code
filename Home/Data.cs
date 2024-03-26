//using System.Collections;
//using System.Collections.Generic;
using System.Collections;
using System.IO;
using Unity.VisualScripting;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.Windows;
class Player
{
    public string name;
    public int skillsNum;
    public int health;
    public int magic;
    public int attack;
    public int defense;
    public float cirt;
    public float cirtEffect;
    public int lv;
    public int exp;
}
public class Data : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("参数")]
    public string name;
    public int skillsNum;
    public int health;
    public int magic;
    public int attack;
    public int defense;
    public float cirt;
    public float cirtEffect;
    public int lv;

    public GameObject talentsText;
    public GameObject talent;
    public GameObject playerState;
    Player player;
    string datapath;
    string js;
    void Start()
    {
        datapath = Application.persistentDataPath + "/SaveFiles";
        if (!Directory.Exists(datapath))
            Directory.CreateDirectory(datapath);
        datapath += "/PlayerData.json";
        player = new Player();
        if (!File.Exists(datapath))
        {
            PlayerInit();
            WriteData();
        }
        else
            ReadData();
        Invoke("update", 0.5f);
        //update();   
    }
    void PlayerInit()
    {
        player.name = "Player";
        player.skillsNum = skillsNum;
        player.health = health;
        player.magic = magic;
        player.attack = attack;
        player.defense = defense;
        player.cirt = cirt;
        player.cirtEffect = cirtEffect;
        player.lv = 1;
        player.exp = 0;
    }
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        //update();
    }
    private void OnDestroy()
    {
        WriteData();
    }
    private void OnApplicationQuit()
    {
        WriteData();
    }
    public void update()
    {
        if (player != null)
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
        }
        talentsText.transform.GetChild(0).GameObject().GetComponent<Text>().text = "天赋点数" + player.skillsNum;
        talentsText.transform.GetChild(1).GetComponent<Text>().text = "生命值：" + player.health;
        talentsText.transform.GetChild(2).GetComponent<Text>().text = "法力值：" + player.magic;
        talentsText.transform.GetChild(3).GetComponent<Text>().text = "攻击：" + player.attack;
        talentsText.transform.GetChild(4).GetComponent<Text>().text = "防御：" + player.defense;
        talentsText.transform.GetChild(5).GetComponent<Text>().text = "暴击率：" + player.cirt;
        talentsText.transform.GetChild(6).GetComponent<Text>().text = "暴击伤害：" + player.cirtEffect;
        playerState.transform.GetChild(1).GetChild(4).GetComponent<Text>().text = (Map.instance.map["health"]).ToString() + "/" + ((int)(Map.instance.map["health"])).ToString();
        playerState.transform.GetChild(1).GetChild(5).GetComponent<Text>().text = ((int)(Map.instance.map["magic"])).ToString() + "/" + ((int)(Map.instance.map["magic"])).ToString();
        playerState.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Map.instance.mapString["name"];
        playerState.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "等级" + Map.instance.map["lv"];
    }
    public void ReadData()
    {
        StreamReader sr = new StreamReader(datapath);
        js = sr.ReadToEnd();
        sr.Close();
        if (js != "")
            player = JsonUtility.FromJson<Player>(js);
    }
    public void WriteData()
    {
        StreamWriter sw = new StreamWriter(datapath);
        js = JsonUtility.ToJson(player);

        sw.WriteLine(js);
        sw.Close();
        //SaveDate();
        
    }
   
    // Update is called once per frame
    public void GameEnd()
    {
        WriteData();
        Application.Quit();
    }
    public void TalentTrue()
    {
        talent.SetActive(true);
    }
    public void TalentFalse()
    {
        talent.SetActive(false);
    }
    public void SkillReset()
    {
        player.skillsNum += (player.health - health) / 10;
        player.skillsNum += (player.magic - magic) / 10;
        player.skillsNum += player.attack - attack;
        player.skillsNum += player.defense - defense;
        player.skillsNum += (int)((player.cirt - cirt) / 0.02f);
        player.skillsNum += (int)((player.cirtEffect - cirtEffect) / 0.05f);
        player.health = health;
        player.magic = magic;
        player.attack = attack;
        player.defense = defense;
        player.cirt = cirt;
        player.cirtEffect = cirtEffect;
        update();
    }
    public void AddHealth()
    {
        if (player.skillsNum > 0)
        {
            player.skillsNum--;
            player.health += 10;
            update();
        }
    }
    public void AddMagic()
    {
        if (player.skillsNum > 0)
        {
            player.skillsNum--;
            player.magic += 10;
            update();
        }
    }
    public void AddAttack()
    {
        Debug.Log(11111);
        if (player.skillsNum > 0)
        {
            player.skillsNum--;
            player.attack += 1;
            update();
        }
    }
    public void AddDefense()
    {
        if (player.skillsNum > 0)
        {
            player.skillsNum--;
            player.defense += 1;
            update();
        }
    }
    public void AddCrit()
    {
        if (player.skillsNum > 0)
        {
            player.skillsNum--;
            player.cirt += 0.02f;
            update();
        }
    }
    public void AddCritEffect()
    {
        if (player.skillsNum > 0)
        {
            player.skillsNum--;
            player.cirtEffect += 0.05f;
            update();
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(Map.instance.scene["game1"]);
    }
}
