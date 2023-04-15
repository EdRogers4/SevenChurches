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
    [SerializeField] private GameObject questionObject;
    [SerializeField] private Sprite[] spriteDisciplineColors;
    private int currentScreen;
    private Color colorDefaultSymbol;
    private Color colorDefaultStart = new Color(1f, 1f, 1f, 0.05f);

    [Header("Aura")]
    private int progressState;
    [SerializeField] private int[] dataCore;
    [SerializeField] private int[] dataShell;
    [SerializeField] private int[] dataTree;
    [SerializeField] private Image auraScreen;
    [SerializeField] private Sprite[] auraSprite;
    [SerializeField] private TextMeshProUGUI textCore;
    [SerializeField] private TextMeshProUGUI textShell;
    [SerializeField] private TextMeshProUGUI textTree;
    [SerializeField] private Image imageCore;
    [SerializeField] private Image imageShell;
    [SerializeField] private Image imageTree;
    [SerializeField] private GameObject shell;
    [SerializeField] private GameObject tree;
    [SerializeField] private Sprite[] spriteCore;
    [SerializeField] private Sprite[] spriteShell;
    [SerializeField] private Sprite[] spriteTree;
    [SerializeField] private string[] stringCause;
    [SerializeField] private string[] stringDiscipline;

    private void Start()
    {
        colorDefaultSymbol = imageSymbolCause[0].color;
        screens[0].SetActive(true);
    }

    public void NextScreen()
    {
        screens[currentScreen].SetActive(false);

        if (currentScreen == 3)
        {
            buttonBack.SetActive(false);
            currentScreen = 0;
            screens[currentScreen].SetActive(true);
            progressState += 1;

            if (progressState <= 2)
            {
                auraScreen.sprite = auraSprite[progressState];
            }
            
        }
        else
        {
            currentScreen += 1;
            screens[currentScreen].SetActive(true);

            if (!buttonBack.activeSelf)
            {
                buttonBack.SetActive(true);
            }
        }

        QuestionText();
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

        QuestionText();
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
    
    private void QuestionText()
    {
        if (currentScreen == 0 || currentScreen == 2)
        {
            questionObject.SetActive(true);
            
            switch (progressState)
            {
                case 0:
                    switch (currentScreen)
                    {
                        case 0:
                            textQuestion.text = "What is your primary cause?";
                            break;
                        case 2:
                            textQuestion.text = "What is your primary gift?";
                            break;
                    }
                    break;
                case 1:
                    switch (currentScreen)
                    {
                        case 0:
                            textQuestion.text = "What is your secondary cause?";
                            break;
                        case 2:
                            textQuestion.text = "What is your secondary gift?";
                            break;
                    }
                    break;
                case 2:
                    switch (currentScreen)
                    {
                        case 0:
                            textQuestion.text = "What is your partner's cause?";
                            break;
                        case 2:
                            textQuestion.text = "What is your partner's gift?";
                            break;
                    }
                    break;
            }
        }
        else
        {
            questionObject.SetActive(false);
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
        UpdateData(0, index);
        imageDisciplineColors.sprite = spriteDisciplineColors[index];

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
        UpdateData(1, index);
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

    private void UpdateData(int data, int index)
    {
        switch (progressState)
        {
            case 0:
                dataCore[data] = index + 1;
                imageCore.sprite = spriteCore[((dataCore[0] - 1) * 6) + dataCore[1]];
                textCore.text = stringCause[dataCore[0]] + " | " + stringDiscipline[dataCore[1]];
                break;
            case 1:
                dataShell[data] = index + 1;
                imageShell.sprite = spriteShell[((dataShell[0] - 1) * 6) + dataShell[1]];
                textShell.text = stringCause[dataShell[0]] + " | " + stringDiscipline[dataShell[1]];

                if (!shell.activeSelf)
                {
                    shell.SetActive(true);
                }
                break;
            case 2:
                dataTree[data] = index + 1;
                imageTree.sprite = spriteTree[((dataTree[0] - 1) * 6) + dataTree[1]];
                textTree.text = stringCause[dataTree[0]] + " | " + stringDiscipline[dataTree[1]];

                if (!tree.activeSelf)
                {
                    tree.SetActive(true);
                }
                break;
        }
    }
}
