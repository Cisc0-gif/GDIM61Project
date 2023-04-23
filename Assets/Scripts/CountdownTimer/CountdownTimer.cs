using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField]
    private float startTime = 10f;

    [SerializeField]
    private TextMeshProUGUI countdownText;

    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0) {
            currentTime -= 1 * Time.deltaTime; //subtract 1 every time the second changes
            countdownText.color = Color.Lerp(Color.red, Color.green, Mathf.Clamp(currentTime / startTime, 0, 1)); //slowly adjust color based on currentTime/startTime
        } 
        else
		{
            currentTime = 0;
            countdownText.color = Color.red;
		}
        countdownText.text = currentTime.ToString();
        
    }
}
