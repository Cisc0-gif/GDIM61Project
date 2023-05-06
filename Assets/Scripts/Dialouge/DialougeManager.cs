using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class DialougeManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI NameText;
    [SerializeField]
    private TextMeshProUGUI BodyText;
    [SerializeField]
    private Image CharacterImage;
    [SerializeField]
    private Button Continue;
    [SerializeField]
    private Animator animator;

    private Queue<string> scetences = new Queue<string>();
    private int index;
    private Dialouge[] Conversation;

    private string CurrentScentence;

    bool Animating = false;

    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnChange;
        index = 0;
        Continue.onClick.AddListener(delegate { DoneWithThing(); });
        scetences = new Queue<string>();
    }
    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnChange;
    }

    private void OnChange(GameState newState)
    {
        enabled = newState == GameState.DIALOUGE;
    }


    public void TriggerDialouge(Dialouge[] Dialouge)
    {
        
        Conversation = Dialouge;
        index = 0;
        

        GameStateManager.Instance.SetState(GameState.DIALOUGE);
        StartDialouge(Conversation[0]);
    }

    private void StartDialouge(Dialouge dialogue)
    {
        //Open box
        animator.SetBool("IsOpen", true);
        scetences.Clear();

        Debug.Log("Started");
        NameText.text = dialogue.CharacterName;
        CharacterImage.sprite = dialogue.CharacterImage;

        

        foreach (string sentence in dialogue.Sentences)
        {
            scetences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        
        if(scetences.Count == 0)
        {
            EndDialouge();
            return;
        }
        //CurrentScentence = scetences.Dequeue();
        //StopAllCoroutines();
        CurrentScentence = scetences.Dequeue();
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        BodyText.text = "";
        foreach (char letter in CurrentScentence.ToCharArray())
        {
            BodyText.text += letter;
            yield return null;
        }
    }


    private void EndDialouge ()
    {
        
        if(index + 1 < Conversation.Length)
        {
            index++;
            scetences.Clear();
            StartDialouge(Conversation[index]);
        }
        else
        {
            animator.SetBool("IsOpen", false);
            index = 0;
            Debug.Log("Ended Dialouge");
            GameStateManager.Instance.SetState(GameState.GAMEPLAY);
        }
    }

    private void DoneWithThing()
    {        
        if (CurrentScentence.Length > BodyText.text.Length)
        {
            StopCoroutine(TypeSentence());
            BodyText.text = CurrentScentence;
        }
        else
        {
            DisplayNextSentence();
        }
    }
}
