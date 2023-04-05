using System;
using System.Collections.Generic;
using UnityEngine;

public class TilePlacement : MonoBehaviour {
    public static event Action<Tile[ ]> OnStartTravel;
    private Tile[] tilesList;
    private int currTileIdx = 0;
    private int maxTileIdx;
    
    private void Awake() {
        tilesList = this.transform.GetComponentsInChildren<Tile>();
        maxTileIdx = tilesList.Length;
        DiceScript.OnDiceRoll += OnDiceRoll;
    }

    private void OnDestroy() {
        DiceScript.OnDiceRoll -= OnDiceRoll;
    }
    
    private void OnDiceRoll(int diceValue) {
        int prevTileIdx = currTileIdx;
        currTileIdx += diceValue;
        int destIdx = currTileIdx;

        int tileIdx = prevTileIdx + 1;
        List<Tile> path = new List<Tile>();
        while (tileIdx <= destIdx) {
            if (tileIdx >= maxTileIdx) {
                tileIdx -= maxTileIdx;
                destIdx -= maxTileIdx;
            }
            path.Add(tilesList[tileIdx]);
            tileIdx++;
        }
        
        // Reset Index
        if (currTileIdx >= maxTileIdx)
            currTileIdx -= maxTileIdx;
        
        OnStartTravel?.Invoke(path.ToArray());
    }
}
