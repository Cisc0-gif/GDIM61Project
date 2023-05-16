using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    #region Singleton
    public static SceneTransition Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        SceneManager.sceneLoaded += EnterSceneTransition;
    }
    #endregion

    [SerializeField]
    private Animator animator;


    public bool Transitioning = false;
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= EnterSceneTransition;

       
    }
    void Start()
    {
        GetComponent<Canvas>().enabled = true;   
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

   
    public IEnumerator LeaveScene(int Index)
    {
        animator.SetTrigger("Leave");
        Transitioning = true;
        yield return new WaitUntil(() => Transitioning == false);
        SceneManager.LoadScene(Index);
    }
    private void EnterSceneTransition(Scene s, LoadSceneMode mode)
    {
        Debug.Log("Loading");
        animator.SetTrigger("Start");
        GameStateManager.Instance.SetState(GameState.TRANSITION);
    }

    public void ResetState()
    {
        Transitioning = false;
    }
}
