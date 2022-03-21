using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPlay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject charSelectMenu;

    GameObject mainCanvas;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(buttonClick);
        mainCanvas = GameObject.Find("MainCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void buttonClick()
    {
        GameObject spawnedMenu = Instantiate(charSelectMenu);
        spawnedMenu.transform.SetParent(mainCanvas.transform, false);
        //set charSelected to -1 to clear the selection.
        ScenePassInfo.charSelected = -1;
        Destroy(transform.parent.gameObject);
    }
}
