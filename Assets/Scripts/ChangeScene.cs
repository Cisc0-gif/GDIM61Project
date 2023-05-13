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
        StartCoroutine(FindObjectOfType<SceneTransition>().LeaveScene(SceneIndex));
    }
}
