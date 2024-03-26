using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HurtScript : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 v;
    Vector3 a;
    Vector3 pos;
    GameObject owner;
    public float vv = 0.02f;
    public float aa = -0.0004f;
    bool flag = true;
    bool isCirt = false;
    void Start()
    {

    }
    public void Init(GameObject gb, int hurtNum, bool iscirt)
    {
        flag = true;
        owner = gb;
        v = new Vector3(0, vv, 0);
        a = new Vector3(0, aa, 0);
        pos = new Vector3(0, 0, 0);
        pos = gb.transform.position;
        this.transform.position = owner.transform.position;
        transform.GetComponent<Text>().text = hurtNum.ToString();
        if (isCirt != iscirt)
        {
            isCirt = iscirt;
            if (isCirt)
                transform.localScale *= 2;
            else
                transform.localScale *= 0.5f;
        }

    }
    // Update is called once per frame
    void Return()
    {
        HurtPool.instance.ReturnPool(transform.parent.gameObject);
    }
    void Update()
    {
        if (v.y >= -vv)
        {
            pos += v;
            v += a;
        }
        else
        {
            if (flag)
            {
                Invoke("Return", 0.5f);
                flag = false;
            }

        }
        //transform.position = owner.transform.position + pos;
        transform.position = pos;
    }
}
