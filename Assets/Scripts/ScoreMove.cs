using UnityEngine;
using System.Collections;

public class ScoreMove : MonoBehaviour {
    private double delta = 0;
    void Start() {

    }

    void Update() {
        var position = transform.position;
        float factor = 1f - (0.4f * Time.deltaTime) ; 
        position.y = position.y + 5*Time.deltaTime;
        position.x += Random.Range(-4, 4) * Time.deltaTime; 
        delta += Time.deltaTime;
        transform.position = position;
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(factor, factor, factor)); 
        if (delta > 1.2) {
            Destroy(gameObject); 
        }
    }
}
