using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Text scoreLabel;

    [SerializeField]
    private GameObject inventoryWindow;

    static private HUD _instance;

    public static HUD Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void SetScore(string scoreValue)
    {
        scoreLabel.text = scoreValue;
    }

    public void CloseWindow(GameObject window)
    {
        window.GetComponent<Animator>().SetBool("inv.Button", false);
    }

    public void ShowWindow(GameObject window)
    {
        window.GetComponent<Animator>().SetBool("inv.Button", true);
    }
}
