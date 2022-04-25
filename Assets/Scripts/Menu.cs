using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    Button Play;
    Button Controls;
    Button Quit;
    Button CloseControls;
    
    void Start()
    {
        Play = GameObject.Find("Play").GetComponent<Button>();
        Controls = GameObject.Find("Controls").GetComponent<Button>();
        Quit = GameObject.Find("Quit").GetComponent<Button>();
        CloseControls = GameObject.Find("CloseControls").GetComponent<Button>();
        transform.Find("ControlScreen").gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Play.onClick.AddListener(StartGame);
        Controls.onClick.AddListener(ShowControls);
        Quit.onClick.AddListener(QuitApplication);
        CloseControls.onClick.AddListener(CloseControlScreen);
    }

    void StartGame(){
        SceneManager.LoadScene(1);
    }

    void ShowControls(){
        transform.Find("ControlScreen").gameObject.SetActive(true);
        Play.interactable = false;
        Quit.interactable = false;
        Controls.interactable = false;
    }
    
    void QuitApplication(){
        Application.Quit();
    }
    
    void CloseControlScreen(){
        transform.Find("ControlScreen").gameObject.SetActive(false);
        Play.interactable = true;
        Quit.interactable = true;
        Controls.interactable = true;
    }
}
