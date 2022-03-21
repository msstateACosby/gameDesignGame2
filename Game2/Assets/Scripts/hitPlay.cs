using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HitPlay : MonoBehaviour
{
    [SerializeField]
    Text currentlySelectedText;
    bool highlightedRed = false;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(buttonClick);
    }

    // Update is called once per frame
    void Update()
    {
        //highlights the text to the let the player know they need to select a character
        if (highlightedRed)
        {
            //lowers the timer value
            timer -= Time.deltaTime;

            //a kinda silly way to see if a character was selected before the timer goes to zero to make it stil not be red when someone is selected.

            if (ScenePassInfo.charSelected != -1) timer = 0;

            //interpolates the color based on timer value between red and white
            currentlySelectedText.color = Color.Lerp(Color.white, Color.red, timer/2.0f);

            //turns the timer off if lower than 0;
            if (timer <= 0)
            {
                
                highlightedRed = false;

            }
        }
    }
    void buttonClick()
    {

        //makes sure the player actually chose someone
        if (ScenePassInfo.charSelected != -1)
        {
            ScenePassInfo.won= -1;
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
        else
        {
            //sets the timer if no one is selected
            timer = 2.0f;
            highlightedRed = true;
        }
    }
}
