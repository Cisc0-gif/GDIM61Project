using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class LaserSplitter : MonoBehaviour
{
    public GameObject LaserPrefab;
    public float Output1_DeflectionAngle=45;
    public float Output2_DeflectionAngle = -45;
    public float Output3_DeflectionAngle=0;
    public Gradient Red;
    public Gradient Blue;
    public Gradient Green;
    public void Collide(Laser other)
    {
        if (!other.HasBeenSplit)
        {
            //Split the beam into its three sub parts, Red Blue and Green.
            other.HasBeenSplit = true;
            GameObject output1 = Instantiate(LaserPrefab, this.transform.position, other.transform.rotation, this.transform);
            output1.transform.eulerAngles += new Vector3(0, 0, Output1_DeflectionAngle);
            GameObject output2 = Instantiate(LaserPrefab, this.transform.position, other.transform.rotation, this.transform);
            output2.transform.eulerAngles += new Vector3(0, 0, Output2_DeflectionAngle);
            GameObject output3 = Instantiate(LaserPrefab, this.transform.position, other.transform.rotation, this.transform);
            output3.transform.eulerAngles += new Vector3(0, 0, Output3_DeflectionAngle);

            output1.GetComponent<Laser>().HasBeenSplit = true;
            output2.GetComponent<Laser>().HasBeenSplit = true;
            output3.GetComponent<Laser>().HasBeenSplit = true;
            output1.GetComponent<Laser>().SetColor(Red,"Red");
            output2.GetComponent<Laser>().SetColor(Blue,"Blue");
            output3.GetComponent<Laser>().SetColor(Green,"Green");
            other.DestroyMe();
        }
    }
}
