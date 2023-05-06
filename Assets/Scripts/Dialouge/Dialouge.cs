using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialouge
{
    public Sprite CharacterImage;
    public string CharacterName;
    [TextArea(3,20)]
    public string [] Sentences;
}
