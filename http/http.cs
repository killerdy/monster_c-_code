using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class http : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    IEnumerator IGetDate()
    {
        WWW www = new WWW("http://localhost");
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
