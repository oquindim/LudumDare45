using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class StaticClass {
    public static string CrossSceneInformation { get; set; }
}
public class LoadNextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start () {
        Debug.Log(StaticClass.CrossSceneInformation);
    }
    void OnTriggerEnter2D (Collider2D other ) {
        StaticClass.CrossSceneInformation = "Hello Scene2!";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
