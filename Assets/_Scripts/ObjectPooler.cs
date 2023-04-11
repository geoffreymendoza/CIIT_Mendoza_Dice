using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    public static ObjectPooler SharedInstance;
    public List<GameObject> PoolObjs;
    public GameObject ObjToPool;
    public int amtToPool = 50;

    private void Awake() {
        SharedInstance = this;
        
        PoolObjs = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < amtToPool; i++) {
            tmp = Instantiate(ObjToPool);
            tmp.SetActive(false);
            PoolObjs.Add(tmp);
        }
    }

    public GameObject GetPooledObject() {
        for (int i = 0; i < amtToPool; i++) {
            if (!PoolObjs[i].activeInHierarchy) {
                return PoolObjs[i];
            }
        }
        return null;
    }

    public int GetAvailableObjCount() {
        int result = 0;
        for (int i = 0; i < amtToPool; i++) {
            if (!PoolObjs[i].activeInHierarchy)
                result++;
        }
        return result;
    }

    public GameObject[ ] GetAvailableObjects() {
        List<GameObject> goList = new List<GameObject>();
        for (int i = 0; i < amtToPool; i++) {
            if (!PoolObjs[i].activeInHierarchy)
                goList.Add( PoolObjs[i]);
        }
        return goList.ToArray();
    }
}
