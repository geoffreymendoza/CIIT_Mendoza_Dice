using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class Collect : MonoBehaviour {
    public static Action<int> OnRespawnCubes;
    
    [SerializeField] List<GameObject> collectibles;
    [SerializeField] private float detectRadius = 2f;
    [SerializeField] private Color detectColor;
    [SerializeField] private LayerMask interactableMask;
    
    // Start is called before the first frame update
    void Start() {
        collectibles = new List<GameObject>();
    }

    // Update is called once per frame
    void Update() {
        Core.DebugKeyPress(KeyCode.Space, RespawnCubes);
        
        Vector3 currentPos = this.transform.position;
        var cols = Physics.OverlapSphere(currentPos, detectRadius, interactableMask);
        if (cols.Length == 0) return;
        foreach (var c in cols) {
            if(collectibles.Contains(c.gameObject)) continue;
            collectibles.Add(c.gameObject);
            c.transform.DOScale(Vector3.zero, 0.5f);
            c.transform.DOJump(transform.position, 1, 1, 0.15f).SetEase(Ease.Linear);
        }
        DisableCubes(1000);
    }

    async void DisableCubes(int duration) {
        await Task.Delay(duration);
        foreach (var c in collectibles)
            c.SetActive(false);
        collectibles.Clear();
    }

    private void RespawnCubes() {
        Debug.Log("Respawn");
        int amtToRespawn = ObjectPooler.SharedInstance.GetAvailableObjCount();
        OnRespawnCubes?.Invoke(amtToRespawn);
    }

    private void OnDrawGizmos() {
        Vector3 origin = transform.position;
        Gizmos.color = detectColor;
        Gizmos.DrawWireSphere(origin, detectRadius);
    }
}
