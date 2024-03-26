using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float initTime = Random.Range(0, 3.5f);
        InvokeRepeating("generator", initTime, 3f);
    }
    void generator()
    {
        if (Score.instance.EnemyScore >= 1)
        {
            Score.instance.EnemyScore--;
            //GameObject prefab = ZombiePool.instance.GameFromPool();
            GameObject prefab = FlyEyePool.instance.GameFromPool();
            prefab.transform.position=this.transform.position;
            prefab.GetComponent<Parameter>().Init();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
