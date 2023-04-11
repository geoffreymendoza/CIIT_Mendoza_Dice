using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    public static ObjectPooler SharedInstance;
    public List<GameObject> PoolObjs;
    public GameObject ObjToPool;
    public int amtToPool = 100;

    private void Awake() {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start() {
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

    // Update is called once per frame
    void Update() {

    }
}
