using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour
{
    public Dialouge[] Convo;

    private void Start()
    {
        FindObjectOfType<DialougeManager>().TriggerDialouge(Convo);
    }
}
