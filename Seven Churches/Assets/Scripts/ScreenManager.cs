using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject[] screens;
    [SerializeField] private GameObject buttonContinue;
    [SerializeField] private GameObject buttonBack;
    [SerializeField] private GameObject imageSelect;
    [SerializeField] private GameObject imageSelect2;
    [SerializeField] private Image[] imageSymbolCause;
    [SerializeField] private Image[] imageButtonCause;
    [SerializeField] private Image[] imageSymbolDiscipline;
    [SerializeField] private Image[] imageButtonDiscipline;
    [SerializeField] private Image imageDisciplineColors;
    [SerializeField] private TextMeshProUGUI textContinue;
    [SerializeField] private TextMeshProUGUI textQuestion;
    [SerializeField] private Sprite[] spriteDisciplineColors;
    private int progressState;
    private int currentScreen;
    private int currentCause;
    private int currentDiscipline;
    private Color colorDefaultSymbol;
    private Color colorDefaultStart = new Color(1f, 1f, 1f, 0.05f);

    [Header("Aura")]
    [SerializeField] private int[] dataCore;
    [SerializeField] private int[] dataShell;
    [SerializeField] private int[] dataAdjunct;
    [SerializeField] private TextMeshProUGUI textCore;
    [SerializeField] private TextMeshProUGUI textShell;
    [SerializeField] private TextMeshProUGUI textAdjunct;
    [SerializeField] private Image imageCore;
    [SerializeField] private Image imageShell;
    [SerializeField] private Image imageAdjunct;
    [SerializeField] private Sprite[] spriteAura;
    [SerializeField] private Sprite[] spriteAdjunct;

    private void Start()
    {
        colorDefaultSymbol = imageSymbolCause[0].color;
        screens[0].SetActive(true);
    }

    public void NextScreen()
    {
        screens[currentScreen].SetActive(false);
        currentScreen += 1;
        screens[currentScreen].SetActive(true);

        if (!buttonBack.activeSelf)
        {
            buttonBack.SetActive(true);
        }
    }

    public void PreviousScreen()
    {
        if (currentScreen > 0)
        {
            screens[currentScreen].SetActive(false);
            currentScreen -= 1;
            screens[currentScreen].SetActive(true);
        }

        if (currentScreen <= 0)
        {
            buttonBack.SetActive(false);
        }
    }

    public void AssignContinueButtonText()
    {
        switch (currentScreen)
        {
            case 0:
                textContinue.text = "continue";
                break;
            case 1:
                textContinue.text = "choose this cause";
                break;
            case 2:
                textContinue.text = "choose this discipline";
                break;
            case 4:
                textContinue.text = "continue";
                break;
        }
    }    

    public void ToggleButtonContinue(bool value)
    {
        buttonContinue.SetActive(value);
    }

    private void DisplayDisciplineScreen()
    {

    }

    public void SelectCause(int index)
    {
        currentCause = index;
        imageDisciplineColors.sprite = spriteDisciplineColors[currentCause];

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

    public void SelectDiscipline(int index)
    {
        currentDiscipline = index;

        if (!imageSelect2.activeSelf)
        {
            imageSelect2.SetActive(true);
        }

        imageSelect2.transform.position = imageButtonDiscipline[index].transform.position;
        imageSymbolDiscipline[index].color = Color.white;
        imageButtonDiscipline[index].color = new Color(1f, 1f, 1f, 0.24f);

        for (int i = 0; i < imageSymbolDiscipline.Length; i++)
        {
            if (i != index)
            {
                imageSymbolDiscipline[i].color = colorDefaultSymbol;
                imageButtonDiscipline[i].color = Color.clear;
            }
        }
    }
}
