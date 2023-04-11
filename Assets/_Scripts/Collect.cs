using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collect : MonoBehaviour {
    public List<GameObject> collectibles;

    // Start is called before the first frame update
    void Start() {
        collectibles = new List<GameObject>();
    }

    float time = 0;
    float tValue = 0;

    // Update is called once per frame
    void Update() {

    }

    // TODO change to physics collider
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Collectibles")) return;
        collectibles.Add(other.gameObject);
        
        for (int i = 0; i < collectibles.Count; i++) {
            float duration = 0.5f;
            collectibles[i].transform.DOScale(Vector3.zero, duration);
            collectibles[i].transform.DOJump(transform.position, 1, 1, 0.15f).SetEase(Ease.Linear);
        }
        // TODO async + object pooling
        StartCoroutine(DisableCube(1f));
    }

    IEnumerator DisableCube(float duration) {

        yield return new WaitForSeconds(duration);
        collectibles.Clear(); 
    }
}
