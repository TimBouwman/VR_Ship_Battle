//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag = null;
        public GameObject prefab = null;
        public int size = 0;
    }

    #region Singleton
    public static ObjectPooler Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    #region Variables
    [SerializeField] private List<Pool> pools = null;
    private Dictionary<string, Queue<GameObject>> dictionary = null;
    #endregion

    private void Start()
    {
        SetupObjectPooler();
    }

    #region Methods
    private void SetupObjectPooler()
    {
        dictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            GameObject parent = Instantiate(new GameObject(pool.tag), Vector3.zero, Quaternion.identity);
            parent.transform.parent = this.transform;
            parent.name = pool.tag;

            for (int i = 0; i < pool.size; i++)
            {
                GameObject ins = Instantiate(pool.prefab);
                ins.transform.parent = parent.transform;

                ins.SetActive(false);
                objectPool.Enqueue(ins);
            }

            dictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        if (!dictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " Does not exist");
            return null;
        }

        GameObject obj = dictionary[tag].Dequeue();

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        IPooledObject pooledObject = obj.GetComponent<IPooledObject>();

        if (pooledObject != null)
            pooledObject.OnObjectSpawn();

        dictionary[tag].Enqueue(obj);

        return obj;
    }
    #endregion
}