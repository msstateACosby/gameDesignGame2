using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelectBack : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mainMenu;

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
        GameObject spawnedMenu = Instantiate(mainMenu);
        spawnedMenu.transform.SetParent(mainCanvas.transform, false);
        Destroy(transform.parent.gameObject);
    }
}
