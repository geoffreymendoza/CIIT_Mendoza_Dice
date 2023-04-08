using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] private GameObject redPortal;
    [SerializeField] private GameObject greenPortal;
    
    // Start is called before the first frame update
    void Start() {
        
    }

    public void ChangeToGreen() {
        redPortal.SetActive(false);
        greenPortal.SetActive(true);
    }
    
    public void ChangeToRed() {
        redPortal.SetActive(true);
        greenPortal.SetActive(false);
    }
}
