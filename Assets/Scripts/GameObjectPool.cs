using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject[] objToSpawn;
        public int startingSize = 0;
        public Pool(string newtag, GameObject ots, int startingsize)
        {
            tag = newtag;
            objToSpawn = new GameObject[1];
            objToSpawn[0] = ots;
            startingSize = startingsize;
        }
    }

    public List<Pool> pools = new List<Pool>();
    public bool canAddToPools = false;
    static public Dictionary<string, Queue<GameObject>> objectsPool = new Dictionary<string, Queue<GameObject>>();
    public static GameObjectPool instance;

    void Awake()
    {
        //Debug.Log("Scriptable Awake " + name);
        InitializePool();
        instance = this;
    }

    Queue<GameObject> AddQueue(Pool p)
    {
        Queue<GameObject> Q = new Queue<GameObject>();
        for (int i = 0; i < p.startingSize; i++)
        {
            GameObject g = Instantiate<GameObject>(p.objToSpawn[Random.Range(0, p.objToSpawn.Length)]);
            g.name = p.tag + "_" + i.ToString();
            g.SetActive(false);
            Q.Enqueue(g);
        }
        return Q;
    }

    void InitializePool()
    {
        foreach(var p in pools)
        {
            objectsPool.Add(p.tag, AddQueue(p));
        }
    }

    public void AddPoolableObject(string poolerTag, GameObject go)
    {
        bool canAdd = true;
        foreach (var p in pools)
        {
            if (p.tag == poolerTag) { }
                canAdd = false;
            break;
        }
        if (canAdd)
        {
            Pool p = new Pool(poolerTag, go, 10);
            pools.Add(p);
            objectsPool.Add(p.tag, AddQueue(p));
        }
    }

    GameObject AddNewInstance(string poolerTag)
    {
        GameObject newObject = null;
        foreach (var p in pools)
        {
            if (p.tag == poolerTag)
            {
                newObject = Instantiate<GameObject>(p.objToSpawn[Random.Range(0, p.objToSpawn.Length)]);
                break;
            }
        }
        return newObject;
    }

    public void ResetPool()
    {
        foreach(var p in pools)
        {
            foreach(var o in objectsPool[p.tag])
            {
                o.SetActive(false);
            }
        }
    }

    public GameObject Spawn(string poolerTag, Vector3 position, Quaternion rotation, Vector3 localScale )
    {
        if (objectsPool.ContainsKey(poolerTag))
        {
            GameObject g = objectsPool[poolerTag].Dequeue();
                
            if (g.activeSelf && canAddToPools)
            {
                objectsPool[poolerTag].Enqueue(g);
                g = AddNewInstance(poolerTag);
            }else if (g.activeSelf)
            {
                if (g.GetComponent<IPooledObject>() != null)
                    g.GetComponent<IPooledObject>().OnDespawn();
            }
            g.transform.position = position;
            g.transform.rotation = rotation;
            g.transform.localScale = localScale;
            g.SetActive(true);
            
            if (g.GetComponent<IPooledObject>()!=null)
                g.GetComponent<IPooledObject>().OnSpawn();
            /*if (g.GetComponent<IonSpawn>() != null)
            {
                g.GetComponent<IonSpawn>().spawn();
            }*/
            objectsPool[poolerTag].Enqueue(g);
            return g;
        }
        else
        {
            Debug.LogWarning("Pooling Tag " + poolerTag + " inesistente.");
            return null;
        }
            
    }

	
}
