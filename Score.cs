using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isDeath = false;
    public int maxScore;
    public int score;
    public int EnemyScore;
    public bool haveBoss = false;
    public bool isEnd = false;
    public int lv;
    public static Score instance;
    public GameObject gb;
    public GameObject player;
    void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (score >= maxScore)
            EndShow();
        if (player.GetComponent<Parameter>().map["health"] <= 0)
            DeathShow();
    }
    public void DeathShow()
    {
        isDeath = true;
        gb.SetActive(true);
        gb.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "��Ϸ����\n" + "��ʧ����";
        Invoke("End", 1f);
    }
    public void EndShow()
    {
        gb.SetActive(true);
        gb.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "��Ϸ����\n" + "��ʤ����\n" + "�ȼ� +" + 1 + "\n�츳��" + 5;
        Invoke("End", 0.5f);
    }
    public void End()
    {
        if (!isEnd)
        {
            isEnd = true;
            if (!isDeath)
            {
                Map.instance.map["lv"]++;
                Map.instance.map["skillsNum"] += 5;
            }
            Map.instance.UpdatePlayer();
            Map.instance.WriteData();
            //SceneManager.LoadScene(0);
            Invoke("ChangeScene", 0.5f);
        }
    }
    void ChangeScene()
    {
        SceneManager.LoadScene(Map.instance.scene["home"]);
    }
}
