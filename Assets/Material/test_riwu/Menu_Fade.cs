using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LeftMenu_Fade : MonoBehaviour
{
    public GameObject StatusMenuP1;
    public GameObject StatusMenuP2;
    private GameObject StatusMenu;
    public bool MenuShow = false;
    public Animation P1Fade;
    public Animation P2Fade;
    // Start is called before the first frame update
    void Start()
    {
        StatusMenuFade(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Change Key Here
        if (Input.GetKeyDown(KeyCode.Escape)){
            MenuShow = !MenuShow;
            StartCoroutine(StatusMenuFade(MenuShow));
        }
    }

    IEnumerator StatusMenuFade(bool fade){
        
        yield return null;
    }
}
