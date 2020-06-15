using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onStart() {
        SceneManager.LoadScene("SampleScene");
    }
    public void OnBack() { 
        Bootstrapper.gameSaver.Save(); 
        SceneManager.LoadScene("Menu"); 
    }
    public void OnExit() {
        Application.Quit(); 
    }
}
