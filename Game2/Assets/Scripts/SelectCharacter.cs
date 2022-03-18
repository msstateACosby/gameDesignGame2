using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    [SerializeField]
    int charId;
    [SerializeField]
    string charName;
    [SerializeField]
    Text currentlySelectedText;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(buttonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonClick()
    {
        ScenePassInfo.charSelected = charId;
        currentlySelectedText.text = "Currently Selected: " + charName;
        
    }
}
