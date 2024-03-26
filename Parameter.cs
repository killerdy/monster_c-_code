using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Parameter : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public bool isDeath = false;
    //public Rigidbody2D rigidbody;
    public Dictionary<string, float> map = new Dictionary<string, float>();
    public void Init()
    {
        isDeath = false;
        if (this.CompareTag("Enemy"))
        {
            Debug.Log("enemy");
            map["maxHealth"] = Map.instance.map["lv"] * 2;
            map["health"] = Map.instance.map["lv"] * 2;
            map["attack"] = Map.instance.map["lv"];
            map["defense"] = Map.instance.map["lv"];
            map["cirt"] = Map.instance.map["lv"] * (0.02f);
            map["cirtEffect"] = Map.instance.map["lv"] * (0.05f);
            GameObject prefab = HealthPool.instance.GameFromPool();
            prefab.transform.GetChild(0).GetComponent<HealthScript>().Init(gameObject);
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        if (this.CompareTag("Player"))
        {
            map["maxHealth"] = Map.instance.map["health"];
            map["health"] = Map.instance.map["health"];
            map["maxMagic"] = Map.instance.map["magic"];
            map["magic"] = Map.instance.map["magic"];
            map["attack"] = Map.instance.map["attack"];
            map["defense"] = Map.instance.map["defense"];
            map["cirt"] = Map.instance.map["cirt"];
            map["cirtEffect"] = Map.instance.map["cirtEffect"];
        }
        //GameObject prefab = HealthPool.instance.GameFromPool();
        //prefab.transform.GetChild(0).GetComponent<HealthScript>().Init(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (map.ContainsKey("health") && map["health"] <= 0)
        {
            Death();
        }
    }
    public void Hit()
    {
        animator.Play("hit");
    }
    public void Death()
    {
        animator.Play("death");
    }
    public void Attack(GameObject defender)
    {
        GameObject prefab = HurtPool.instance.GameFromPool();
        float cirt=Random.value;
        bool isCirt = false;
        float damageNum = map["attack"] * 100f / (100f + defender.GetComponent<Parameter>().map["defense"]);
        if (cirt <= map["cirt"])
        {
            isCirt= true;
            damageNum *= 1f + map["cirtEffect"];
        }
        damageNum=Mathf.Max(1f,damageNum);
        prefab.transform.GetChild(0).GetComponent<HurtScript>().Init(defender,(int)damageNum,isCirt);
        defender.GetComponent<Parameter>().map["health"] -= map["attack"]; 
        //Debug.Log(defender.GetComponent<Parameter>().map["health"]);
    }
}
