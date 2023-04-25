using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPannel : MonoBehaviour
{
    [SerializeField]
    private List<Bar> Pannels;

    private int SelectedBar = 1;
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Bar>() != null)
            {
                Pannels.Add(transform.GetChild(i).GetComponent<Bar>());
            }
        }
        ChangePannels();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {

            SelectedBar++;
            if (SelectedBar > Pannels.Count - 1)
            {
                SelectedBar = 0;
            }
            ChangePannels();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SelectedBar--;
            if (SelectedBar < 0)
            {
                SelectedBar = Pannels.Count - 1;
            }
            
            ChangePannels();
        }
    }

    private void ChangePannels()
    {
        for(int i = 0; i < Pannels.Count; i++)
        {
            
            if(i == SelectedBar)
            {
                Pannels[i].EnableBar();
            }
            else
            {
                Pannels[i].DisableBar();
            }
        }
    }
}
