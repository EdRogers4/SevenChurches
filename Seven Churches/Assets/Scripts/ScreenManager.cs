using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject[] screens;
    [SerializeField] private GameObject buttonContinue;
    [SerializeField] private GameObject imageSelect;
    [SerializeField] private Image[] imageSymbolCause;
    [SerializeField] private Image[] imageButtonCause;
    private int currentScreen;
    private Color colorDefaultSymbol;

    private void Start()
    {
        colorDefaultSymbol = imageSymbolCause[0].color;
    }

    public void NextScreen()
    {
        screens[currentScreen].SetActive(false);
        currentScreen += 1;
        screens[currentScreen].SetActive(true);
    }

    public void PreviousScreen()
    {
        screens[currentScreen].SetActive(false);
        currentScreen -= 1;
        screens[currentScreen].SetActive(true);
    }

    public void ToggleButtonContinue(bool value)
    {
        buttonContinue.SetActive(value);
    }

    public void SelectCause(int index)
    {
        if (!imageSelect.activeSelf)
        {
            imageSelect.SetActive(true);
        }

        if (!buttonContinue.activeSelf)
        {
            buttonContinue.SetActive(true);
        }

        imageSelect.transform.position = imageButtonCause[index].transform.position;
        imageSymbolCause[index].color = Color.white;
        imageButtonCause[index].color = new Color(1f, 1f, 1f, 0.24f);

        for (int i = 0; i < imageSymbolCause.Length; i++)
        {
            if (i != index)
            {
                imageSymbolCause[i].color = colorDefaultSymbol;
                imageButtonCause[i].color = Color.clear;
            }
        }
    }
}
