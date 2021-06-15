using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject pool_manager;
    public GameObject object_to_pool;
    public int amount;

    private List<GameObject> objects_list;

    //public static ObjectPooler SharedInstance;

    void Awake()
    {
        //SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        objects_list = new List<GameObject>();

        for (int i = 0; i < amount; i++)
        {
            GameObject object_instance = Instantiate(object_to_pool, pool_manager.transform);
            object_instance.SetActive(false);
            objects_list.Add(object_instance);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < objects_list.Count; i++)
        {
            if (!objects_list[i].activeInHierarchy)
            {
                return objects_list[i];
            }
        }
        return null;
    }
}
