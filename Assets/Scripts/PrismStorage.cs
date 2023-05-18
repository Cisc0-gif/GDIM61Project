using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private bool InWorldReference = false;

    [SerializeField]
    private Sprite[] sprites;

    private SpriteRenderer spriteRenderer;
    private Vector3 textOriginalPos;

    void Start()
	{
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateVisuals();
        textOriginalPos = new Vector3(0, -0.674999952f, 0);

    }

    void OnEnable()
    {
        Canvas canvas = GetComponentInChildren<Canvas>();
        canvas.worldCamera = Camera.main;
        CurrentPrisms = MaxPrisms;
        StoredType = Prism.GetComponent<PrismType>().GetPrism();
        //UpdateVisuals();
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
        if(InWorldReference)
        {
            prism.SetActive(true);
        }
        prism.name = $"{Prism.name} {MaxPrisms - CurrentPrisms}";
        UpdateVisuals();
        return prism;
    }

    public void UpdateVisuals()
    {
      
        PrismDisplay.enabled = PrismCrateName.enabled = (CurrentPrisms > 0);
        PrismDisplay.sprite = Prism.GetComponent<SpriteRenderer>().sprite;
        PrismCrateName.text = Prism.name;
        if (CurrentPrisms <= 0)
		{
            spriteRenderer.sprite = sprites[0]; //off face
            Text.text = "Crate Empty";
            Text.GetComponent<RectTransform>().localPosition = Vector3.zero;
        } else
		{
            spriteRenderer.sprite = sprites[1]; //on face
            Text.text = $"{CurrentPrisms}";
            Text.GetComponent<RectTransform>().localPosition = textOriginalPos;
        }
        
        if(InWorldReference && CurrentPrisms <= 0)
        {
            Destroy(Prism);
        }
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
            Prism = Instantiate(g, transform);
            Prism.transform.localScale = new Vector3(1, 1, 1); 
            Prism.name = string.Concat(g.name.Where(char.IsLetter));
            Prism.SetActive(false);
            InWorldReference = true;
            StoredType = Prism.GetComponent<PrismType>().GetPrism();
        }
        CurrentPrisms++;
        UpdateVisuals();
       
    }
}
