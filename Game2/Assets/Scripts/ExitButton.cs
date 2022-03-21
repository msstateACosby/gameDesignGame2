using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(buttonClick);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void buttonClick()
    {
        Application.Quit();
        
    }
}
