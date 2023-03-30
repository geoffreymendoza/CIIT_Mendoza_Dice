using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePlacement : MonoBehaviour {
    [SerializeField] private Tile[] tilesList;

    // Start is called before the first frame update
    void Start() {
        tilesList = this.transform.GetComponentsInChildren<Tile>();
    }
}
