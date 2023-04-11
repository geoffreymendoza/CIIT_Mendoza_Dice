using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class CoroutineTest : MonoBehaviour {
    public GameObject cubeObj;
    public Gradient gradient;

    public Color colorA, colorB;
    public float tValue = 0;
    public float time = 0;
    public float changeColourTime = 3f;

    // Start is called before the first frame update
    void Start() {
        //StartCoroutine(DelaySpawn());
        DelaySpawnAsync();
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator DelaySpawn() {
        //cubeObj.SetActive(true);
        time = 0;
        MeshRenderer rend = cubeObj.GetComponent<MeshRenderer>();
        rend.material.color = colorA;
        tValue = 0;
        yield return new WaitForSeconds(1f);
        while (tValue < 1) {
            time += Time.deltaTime;
            //Color color = Color.Lerp(colorA, colorB, time);

            tValue = Mathf.Clamp01(time / changeColourTime);
            // Gradiant
            Color color = gradient.Evaluate(tValue);

            rend.material.color = color;
            yield return new WaitForEndOfFrame();
        }
        time = 0;
    }

    async void DelaySpawnAsync() {
        time = 0;
        MeshRenderer rend = cubeObj.GetComponent<MeshRenderer>();
        //rend.material.color = colorA;
        await Task.Delay(1000);
        tValue = 0;
        while (tValue < 1) {
            time += Time.deltaTime;
            //Color color = Color.Lerp(colorA, colorB, time);
            tValue = Mathf.Clamp01( time / changeColourTime);
            //Debug.Log(tValue);
            // Gradiant
            Color color = gradient.Evaluate(tValue);

            rend.material.color = color;
            await Task.Yield();
        }
        tValue = 0;
        time = 0;
    }
}
