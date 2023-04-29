using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrismStorage : MonoBehaviour
{
    #region References
    [Header("Reference")]
    [SerializeField]
    private GameObject Prism;
    [SerializeField]
    private Image PrismDisplay;
    [SerializeField]
    private TextMeshProUGUI Text;
    [SerializeField]
    private TextMeshProUGUI PrismCrateName;
    #endregion

    [Header("Max Prisms")]
    [SerializeField]
    private int MaxPrisms;
    private int CurrentPrisms;
    private Prism StoredType;

    void Start()
    {
        Canvas canvas = GetComponentInChildren<Canvas>();
        canvas.worldCamera = Camera.main;
        CurrentPrisms = MaxPrisms;
        StoredType = Prism.GetComponent<PrismType>().GetPrism();

        UpdateVisuals();
    }

   

    public GameObject SpawnPrism()
    {
        Debug.Log("Spawnning Prisms");
        if(CurrentPrisms <= 0)
        {
            return null;
        }
        CurrentPrisms--;
        GameObject prism = Instantiate(Prism, transform.position, Quaternion.identity);
        prism.name = $"{Prism.name} {MaxPrisms - CurrentPrisms}";
        UpdateVisuals();
        return prism;
    }

    private void UpdateVisuals()
    {
      
        PrismDisplay.enabled = PrismCrateName.enabled = (CurrentPrisms > 0);
        PrismDisplay.sprite = Prism.GetComponent<SpriteRenderer>().sprite;
        PrismCrateName.text = Prism.name;
        Text.text = $"{CurrentPrisms}";
    }

    public bool CheckType(Prism t)
    {
        if(CurrentPrisms <= 0)
        {
            return true;
        }
        return (t == StoredType);
    }

    public void PlaceObject(GameObject g)
    {
        if(CurrentPrisms <= 0)
        {
            Prism = g;
            StoredType = Prism.GetComponent<PrismType>().GetPrism();
        }
        CurrentPrisms++;
        UpdateVisuals();
       
    }
}
