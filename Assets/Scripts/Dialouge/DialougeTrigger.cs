using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeTrigger : MonoBehaviour
{
    [SerializeField]
    private Dialouge[] Conversation; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<DialougeManager>().TriggerDialouge(Conversation);
    }
}
