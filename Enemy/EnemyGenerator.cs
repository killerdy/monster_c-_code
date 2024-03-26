using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public static EnemyGenerator Instance;
    
    void Start()
    {
        Instance = this;
        for (var i = 0; i <= 8; i++)
            Invoke("Func", 5f);
    }
    void Func()
    {
        InvokeRepeating("generator", 1f, Random.Range(0, 3f));
    }
    void generator()
    {
        if(Score.instance.haveBoss&&Score.instance.EnemyScore>=30)
        {
            Score.instance.EnemyScore -= 30;
            GameObject boss;
            boss = BossChickenPool.instance.GameFromPool();
            boss.GetComponent<Parameter>().Init();
            boss.GetComponent<ChickenFirst>().Init();
            int pos = Random.Range(0, 6);
            boss.transform.position = this.transform.GetChild(pos).position;

        }
        if (Score.instance.EnemyScore >= 1)
        {
            Score.instance.EnemyScore--;
            GameObject prefab;
            if (Random.Range(0, 1f) > 0.5f)
                prefab = ZombiePool.instance.GameFromPool();
            else
                prefab = FlyEyePool.instance.GameFromPool();
            prefab.GetComponent<Parameter>().Init();
            int pos = Random.Range(0, 6);
            //Debug.Log(pos);
            prefab.transform.position = this.transform.GetChild(pos).position;

        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
