using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private GameObject pauseMenuParent;

    [SerializeField]
    private GameObject pauseMenuBG;

    [SerializeField]
    private TextMeshProUGUI textResume;

    [SerializeField]
    private TextMeshProUGUI textSettings;

    [SerializeField]
    private TextMeshProUGUI textQuit;

    private List<TextMeshProUGUI> menuText = new List<TextMeshProUGUI>();
    private int menuTextIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.onGamePause += EnablePauseMenu;
        GameStateManager.onGameUnpause += DisablePauseMenu;
        menuText.Add(textResume);
        menuText.Add(textSettings);
        menuText.Add(textQuit);
    }

    // Update is called once per frame
    void Update()
    {
        //Update selected text color
        for (int i = 0; i < menuText.Count; i++)
		{
            menuText[i].color = new Color32(27, 76, 54, 255);
            if (i == menuTextIndex)
			{
                menuText[i].color = new Color32(255, 255, 255, 255);
			}
		}

        if (pauseMenuParent.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                menuTextIndex--;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                menuTextIndex++;
            }
        }
        

        //if greater than max, set to 0
        if (menuTextIndex > menuText.Count-1)
		{
            menuTextIndex = 0;
		} 
        else if (menuTextIndex < 0) //if less than 0, set to max (loop to top)
		{
            menuTextIndex = menuText.Count-1;
		}
        Debug.Log(menuTextIndex);
        
    }

    public void EnablePauseMenu()
	{
        pauseMenuParent.SetActive(true);
        pauseMenuBG.SetActive(true);
	}

    public void DisablePauseMenu()
	{
        pauseMenuParent.SetActive(false);
        pauseMenuBG.SetActive(false);
    }

}
