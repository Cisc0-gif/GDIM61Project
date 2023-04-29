using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismType : MonoBehaviour
{
    [SerializeField]
    private Prism type;

    public Prism GetPrism() { return type; }
}

public enum Prism
{
    EMITTER, DEFLECTOR, MERGER, SPLITTER, TARGET
}
