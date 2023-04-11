using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        
    }

    private void OnEnable() {
        Invoke("SelfDestroy", 3f);
    }

    public void SelfDestroy() {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }
}
