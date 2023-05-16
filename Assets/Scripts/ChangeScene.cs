using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private int SceneIndex = 0;
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneIndex == -1)
            SceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        else if (SceneIndex == -2)
            SceneIndex =(SceneManager.GetActiveScene().buildIndex -1);
        StartCoroutine(FindObjectOfType<SceneTransition>().LeaveScene(SceneIndex));
    }
}
