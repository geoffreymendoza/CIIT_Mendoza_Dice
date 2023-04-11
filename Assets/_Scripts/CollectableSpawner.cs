using UnityEditor;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour{
    [SerializeField, Range(5,50)] private float spawnRadius = 5f;
    [SerializeField, Range(10, 50)] private int amtToSpawn = 20;

    private void Start() {
        InitSpawn(amtToSpawn);
        Collect.OnRespawnCubes += OnRespawnCubes;
    }

    private void OnDestroy() {
        Collect.OnRespawnCubes -= OnRespawnCubes;
    }

    private void OnRespawnCubes(int amt) {
        InitSpawn(amt);
    }

    private void InitSpawn(int amount) {
        for (int i = 0 ; i < amount ; i++) {
            var go = ObjectPooler.SharedInstance.GetPooledObject();
            Vector2 randPos = GetRandPos();
            Vector3 newPos = new Vector3(randPos.x, this.transform.position.y, randPos.y);
            go.transform.position = this.transform.position + newPos;
            var poolObj = go.GetComponent<PoolObject>();
            go.SetActive(true);
            poolObj.Init();
        }
    }

    Vector2 GetRandPos() {
        float magnitude = Random.Range(1, spawnRadius);
        Vector2 result = Random.insideUnitCircle * magnitude;
        return result;
    }

    private void OnDrawGizmos() {
        Handles.matrix = this.transform.localToWorldMatrix;
        Handles.color = Color.green;
        Handles.DrawWireDisc(default, Vector3.up, spawnRadius);
    }
}