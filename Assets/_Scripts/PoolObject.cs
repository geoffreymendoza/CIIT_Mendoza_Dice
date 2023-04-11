using UnityEngine;

public class PoolObject : MonoBehaviour {
    private Vector3 startPos;
    private Vector3 startScale;
    private bool init = false;

    private void OnEnable() {
        // ResetObject();
    }
    
    public void Init() {
        startPos = this.transform.position;
        startScale = Vector3.one;
        ResetObject();
    }
    
    public void ReturnObj() {
        this.gameObject.SetActive(false);
    }

    private void ResetObject() {
        this.transform.position = startPos;
        this.transform.localScale = startScale;
    }
}