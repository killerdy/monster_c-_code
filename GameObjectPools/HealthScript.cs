using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class HealthScript: MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject owner;
    void Start()
    {
        //this.transform.LookAt(camera.transform.position);
        //owner = this.gameObject;
    }
    public void Init(GameObject gb)
    {
        //if(owner== null)
        owner = gb;
        Update();
    }
    void Return()
    {
        owner = null;
        HealthPool.instance.ReturnPool(gameObject.transform.parent.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if(owner!=null)
        {

            transform.position= owner.transform.position+ new Vector3(0,0.5f,0);
            transform.GetChild(1).GetComponent<Image>().fillAmount = owner.GetComponent<Parameter>().map["health"] / owner.GetComponent<Parameter>().map["maxHealth"];
            if (owner.GetComponent<Parameter>().map["health"] <= 0)
            {
                //Invoke("Return",0.1f);
                Return();
            }
        }
        
    }
}
