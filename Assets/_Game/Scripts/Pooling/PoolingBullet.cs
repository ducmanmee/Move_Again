using System.Collections.Generic;
using UnityEngine;

public class PoolingBullet : MonoBehaviour
{
    #region Singleton
    public static PoolingBullet ins;
    public void Awake()
    {
        ins = this;
        SetUp();
    }
    #endregion

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public BulletBase prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<BulletBase>> poolDictionary;

    void SetUp()
    {
        poolDictionary = new Dictionary<string, Queue<BulletBase>>();

        foreach (Pool p in pools)
        {
            Queue<BulletBase> objectPool = new Queue<BulletBase>();

            for (int i = 0; i < p.size; i++)
            {
                BulletBase obj = Instantiate(p.prefab, transform);
                obj.gameObject.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(p.tag, objectPool);
        }
    }

    BulletBase preb;
    public BulletBase SpawnFromPool(string tag)
    {
        BulletBase objToSpawn;
        try
        {
            objToSpawn = poolDictionary[tag].Dequeue();
        }
        catch
        {

            foreach (Pool p in pools)
            {
                if (p.tag.Equals(tag))
                {
                    preb = p.prefab;
                }
            }

            BulletBase obj = Instantiate(preb, transform);
            obj.gameObject.SetActive(false);
            poolDictionary[tag].Enqueue(obj);

            objToSpawn = poolDictionary[tag].Dequeue();
        }

        objToSpawn.gameObject.SetActive(true);
        return objToSpawn;
    }

    public void EnQueueObj(string tag, BulletBase objToEnqueue)
    {
        poolDictionary[tag].Enqueue(objToEnqueue);
        objToEnqueue.gameObject.SetActive(false);
    }
}
