using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPool : MonoBehaviour
{
    public static HealthPool instance;
    public GameObject prefab;
    public int count;
    private Queue<GameObject> queue = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        FillPool();
    }
    void FillPool()
    {
        for (var i = 0; i < count; i++)
        {
            var newPrefab = Instantiate(prefab);
            ReturnPool(newPrefab);
        }
    }
    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        queue.Enqueue(gameObject);
    }
    public GameObject GameFromPool()
    {
        if (queue.Count == 0)
        {
            FillPool();
        }
        GameObject outPrefab = queue.Dequeue();
        outPrefab.SetActive(true);
        return outPrefab;
    }

}
