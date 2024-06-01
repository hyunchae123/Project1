using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject EscPanel;

    public static GameManager Instance {  get; private set; }

    public bool isPause { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            isPause = !isPause;
        }

        if(isPause)
        {
            EscPanel.SetActive(true);
        }
    }

    public void ReStart()
    {
        isPause = !isPause;
        EscPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
