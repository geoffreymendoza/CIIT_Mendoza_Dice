using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour {
    public Transform firePt;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject bul = ObjectPooler.SharedInstance.GetPooledObject();
            if (bul == null) return;
            bul.SetActive(true);
            bul.transform.position = firePt.position;
            bul.transform.rotation = firePt.rotation;
            var rb = bul.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.AddForce(bul.transform.forward * 20f, ForceMode.Impulse);
        }
    }
}
