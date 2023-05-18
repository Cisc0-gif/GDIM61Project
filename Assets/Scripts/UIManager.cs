using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private GameObject pauseMenuParent;

    [SerializeField]
    private GameObject pauseMenuBG;

    [SerializeField]
    private List<TextMeshProUGUI> menuText = new List<TextMeshProUGUI>();

    [SerializeField]
    private float pauseMenuSpeed;

    [SerializeField]
    private float pauseMenuMoveRate;
    
    private int menuTextIndex = 0;
    private GameStateManager GMS;
    private RectTransform pauseMenuPos;
    private bool posHit;

    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.onGamePause += EnablePauseMenu;
        GameStateManager.onGameUnpause += DisablePauseMenu;

        GMS = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();

        pauseMenuPos = pauseMenuParent.GetComponent<RectTransform>();
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
            //Get inputs

            if (Input.GetKeyDown(KeyCode.W)) //UP
            {
                menuTextIndex--;
            }

            if (Input.GetKeyDown(KeyCode.S)) //DOWN
            {
                menuTextIndex++;
            }

            if (Input.GetButtonDown("Jump")) //SELECT
			{
                switch(menuTextIndex)
				{
                    case 0:
                        GMS.TogglePause();
                        break;

                    case 1:
                        break;

                    case 2:
                        Application.Quit();
                        break;
				}
			}


            if (pauseMenuPos.anchoredPosition.x < 1 && !posHit)
            {
                pauseMenuPos.anchoredPosition = new Vector3(pauseMenuPos.anchoredPosition.x + pauseMenuMoveRate, pauseMenuPos.anchoredPosition.y, 0);
            }
            if (pauseMenuPos.anchoredPosition.y < -1 && !posHit)
			{
                pauseMenuPos.anchoredPosition = new Vector3(pauseMenuPos.anchoredPosition.x, pauseMenuPos.anchoredPosition.y + pauseMenuMoveRate, 0);
			}
            else
			{
                posHit = true;
                //Move Y pos to mimic arm movement
                pauseMenuPos.anchoredPosition = new Vector2(pauseMenuPos.anchoredPosition.x, Mathf.Sin(Time.realtimeSinceStartup) * pauseMenuSpeed);
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
        //pauseMenuParent.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.844207764f, -1.31155396f, 0);
    }

    public void DisablePauseMenu()
	{
        pauseMenuParent.SetActive(false);
        pauseMenuBG.SetActive(false);
        pauseMenuPos.anchoredPosition = new Vector3(-328.199982f, -286.898926f, 0);
        posHit = false;
    }

}
