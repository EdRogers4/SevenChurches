using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private SelectionManager scriptSelectionManager;
    [SerializeField] private Map scriptMap;
    [SerializeField] private GameObject[] screens;
    [SerializeField] private GameObject buttonContinue;
    [SerializeField] private GameObject buttonBack;
    [SerializeField] private GameObject buttonHome;
    [SerializeField] private RectTransform[] transformQuestion;

    [Header("Causes & Disciplines")]
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
    [SerializeField] private RectTransform questionObjectTransform;
    [SerializeField] private Sprite[] spriteDisciplineColors;
    [SerializeField] private Sprite[] spriteCauseDescription;
    [SerializeField] private int currentScreen;
    [SerializeField] private int currentCause;
    [SerializeField] private int currentDiscipline;
    private Color colorDefaultSymbol;
    private Color colorDefaultStart = new Color(1f, 1f, 1f, 0.05f);

    [Header("Aura")]
    public int progressState;
    public int[] dataCore;
    public int[] dataShell;
    public int[] dataTree;
    [SerializeField] private Image auraScreen;
    [SerializeField] private Sprite[] auraSprite;
    [SerializeField] private TextMeshProUGUI textCore;
    [SerializeField] private TextMeshProUGUI textShell;
    [SerializeField] private TextMeshProUGUI textTree;
    [SerializeField] private Image imageCore;
    [SerializeField] private Image imageShell;
    [SerializeField] private Image imageTree;
    [SerializeField] private Image imageCauseDescription;
    [SerializeField] private GameObject shell;
    [SerializeField] private GameObject core;
    [SerializeField] private GameObject tree;
    public Sprite[] spriteCore;
    public Sprite[] spriteShell;
    public Sprite[] spriteTree;
    [SerializeField] private string[] stringCause;
    [SerializeField] private string[] stringDiscipline;
    public bool isTree;

    [Header("Screen Prompt")]
    [SerializeField] private GameObject screenPrompt;
    [SerializeField] private Animator animatorScreenPrompt;
    [SerializeField] private Animator animatorQuestion;
    [SerializeField] private TextMeshProUGUI textScreenPrompt;
    [SerializeField] private bool isScreenPrompt;
    [SerializeField] private GameObject buttonPromptBackground;
    [SerializeField] private TextMeshProUGUI textButtonPrompt;
    [SerializeField] private Image imageButtonNo;
    [SerializeField] private Sprite[] spriteButtonNo;

    [Header("Church")]
    [SerializeField] private Button[] buttonChurch;
    [SerializeField] private GameObject churchButtons;
    public bool isChurch;

    [Header("Missions")]
    [SerializeField] private Button[] buttonMission;
    [SerializeField] private Sprite[] spriteMissionButtons;
    [SerializeField] private string[] stringMission;
    [SerializeField] private TextMeshProUGUI[] missionText;
    private int currentMission;

    [Header("Home")]
    private bool isHome;
    public TextMeshProUGUI textTeamCode;
    [SerializeField] private TextMeshProUGUI textHomeChurch;
    [SerializeField] private TextMeshProUGUI textHomeLeader;
    [SerializeField] private TextMeshProUGUI textHomeObjective;
    [SerializeField] private TextMeshProUGUI textHomeMission;
    [SerializeField] private TextMeshProUGUI textHomeShell;
    [SerializeField] private TextMeshProUGUI textHomeCore;
    [SerializeField] private TextMeshProUGUI textHomeTree;
    [SerializeField] private Image imageHomeAnimal;
    [SerializeField] private Image imageHomeShell;
    [SerializeField] private Image imageHomeCore;
    [SerializeField] private Image imageHomeTree;
    [SerializeField] private Image imageHomeTextChurch;
    [SerializeField] private Image imageCrosshair;
    [SerializeField] private Button[] buttonNavigation;
    [SerializeField] private Sprite[] spriteNavigationLocked;
    private Color colorLocked = new Color(1f, 1f, 1f, 0.35f);

    private void Start()
    {
        colorDefaultSymbol = imageSymbolCause[0].color;
        screens[0].SetActive(true);
        QuestionText();

        for (int i = 0; i < missionText.Length; i++)
        {
            missionText[i].text = stringMission[i];
        }
    }

    public void NextScreen()
    {
        if (isScreenPrompt)
        {
            animatorScreenPrompt.speed = 1;
            isScreenPrompt = false;
            ToggleButtonContinue(false);
            NextScreenSet();
        }
        else if (!isScreenPrompt)
        {
            if (currentScreen == 3 && progressState < 1)
            {
                ShowScreenPrompt();
            }
            else if (currentScreen == 3 && progressState >= 1)
            {
                ToggleButtonPrompt(true);

                if (progressState == 2)
                {
                    UpdateButtonPrompt();
                }
            }
            else
            {
                screens[currentScreen].SetActive(false);
                currentScreen += 1;
                screens[currentScreen].SetActive(true);

                if (!buttonBack.activeSelf)
                {
                    buttonBack.SetActive(true);
                }

                if (currentScreen == 6)
                {
                    scriptSelectionManager.UpdateHomeText(dataShell[0] - 1);
                }

                if (!isHome)
                {
                    QuestionText();
                    SetButtonsOnScreen();
                }
            }
        }
    }

    private void ShowScreenPrompt()
    {
        buttonBack.SetActive(false);
        ToggleButtonContinue(false);
        screenPrompt.SetActive(true);
        animatorScreenPrompt.SetBool("isShow", true);
        isScreenPrompt = true;
        progressState += 1;

        switch (progressState)
        {
            case 1:
                textScreenPrompt.text = "Choose your mode of expression";
                break;
            case 2:
                textScreenPrompt.text = "Choose your sidekick's cause";
                break;
        }
    }

    public void ToggleButtonPrompt(bool value)
    {
        buttonPromptBackground.SetActive(value);
    }
    
    public void ButtonYes()
    {
        switch (progressState)
        {
            case 1:
                ShowScreenPrompt();
                isTree = true;
                break;
            case 2:
                ToggleButtonPrompt(false);
                isChurch = true;
                progressState += 1;
                screens[currentScreen].SetActive(false);
                currentScreen = 4;
                screens[currentScreen].SetActive(true);
                buttonChurch[dataShell[0] - 1].onClick.Invoke();
                churchButtons.SetActive(false);
                QuestionText();
                animatorQuestion.enabled = false;
                textQuestion.color = Color.white;
                break;
            case 3:
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
                break;
        }
    }

    public void ButtonNo()
    {
        switch (progressState)
        {
            case 1:
                progressState += 1;
                UpdateButtonPrompt();
                break;
            case 2:
                progressState += 1;
                ToggleButtonPrompt(false);
                ToggleButtonContinue(false);
                screens[currentScreen].SetActive(false);
                currentScreen = 6;
                screens[currentScreen].SetActive(true);
                scriptSelectionManager.UpdateHomeText(dataShell[0] - 1);
                break;
            case 3:
                ToggleButtonPrompt(false);
                break;
        }
    }

    public void ButtonReset()
    {
        UpdateButtonPrompt();
        ToggleButtonPrompt(true);
    }

    private void UpdateButtonPrompt()
    {
        switch (progressState)
        {
            case 1:
                textButtonPrompt.text = "Do you have a sidekick?";
                imageButtonNo.sprite = spriteButtonNo[0];
                break;
            case 2:
                textButtonPrompt.text = "Do you want to be assigned to an angel of a church?";
                imageButtonNo.sprite = spriteButtonNo[1];
                break;
            case 3:
                textButtonPrompt.text = "Are you sure you want to start from the beginning?";
                imageButtonNo.sprite = spriteButtonNo[0];
                break;
        }
    }


    public void NextScreenSet()
    {
        screens[currentScreen].SetActive(false);
        buttonBack.SetActive(false);
        currentScreen = 0;
        screens[currentScreen].SetActive(true);

        if (progressState <= 2)
        {
            auraScreen.sprite = auraSprite[progressState];
        }

        if (buttonPromptBackground.activeSelf)
        {
            ToggleButtonPrompt(false);
        }

        if (!isHome)
        {
            QuestionText();
            SetButtonsOnScreen();
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

        if (currentScreen <= 0 || currentScreen == 4)
        {
            buttonBack.SetActive(false);
        }

        if (!isHome)
        {
            QuestionText();
            SetButtonsOnScreen();
        }
    }

    public void ButtonMission(int index)
    {
        currentMission = index + 1;
        ToggleButtonContinue(true);

        for (int i = 0; i < buttonMission.Length; i++)
        {
            if (i == index)
            {
                buttonMission[i].image.sprite = spriteMissionButtons[1];
            }
            else
            {
                buttonMission[i].image.sprite = spriteMissionButtons[0];
            }
        }
    }

    public void ButtonNavigation(int index)
    {
        screens[currentScreen].SetActive(false);
        currentScreen = index;
        screens[currentScreen].SetActive(true);

        if (currentScreen == 0)
        {
            ResetCauseSelection();
        }
        else if (currentScreen == 1)
        {
            buttonBack.SetActive(true);
        }
        else if (currentScreen == 2)
        {
            ResetDisciplineSelection();
        }
        else if (currentScreen == 4)
        {
            if (!churchButtons.activeSelf)
            {
                churchButtons.SetActive(true);
            }
        }

        if (currentScreen == 6)
        {
            buttonHome.SetActive(false);

            if (buttonContinue.activeSelf)
            {
                buttonContinue.SetActive(false);
            }
        }
        else
        {
            buttonHome.SetActive(true);
        }    
    }

    public void ResetCauseSelection()
    {
        imageSelect.SetActive(false);

        for (int i = 0; i < imageSymbolCause.Length; i++)
        {
            imageSymbolCause[i].color = colorDefaultSymbol;
            imageButtonCause[i].color = colorDefaultStart;
        }
    }
    
    public void ResetDisciplineSelection()
    {
        imageSelect2.SetActive(false);

        for (int i = 0; i < imageSymbolDiscipline.Length; i++)
        {
            imageSymbolDiscipline[i].color = colorDefaultSymbol;
            imageButtonDiscipline[i].color = colorDefaultStart;
        }
    }

    public void ToggleScreenPromptOff()
    {
        screenPrompt.SetActive(false);
    }

    private void SetButtonsOnScreen()
    {
        if (currentScreen == 0 || currentScreen == 2)
        {
            buttonContinue.SetActive(false);

            if (currentScreen == 0)
            {
                imageSelect.SetActive(false);
                imageSymbolCause[currentCause].color = colorDefaultSymbol;
                imageButtonCause[currentCause].color = colorDefaultStart;
            }
            else if (currentScreen == 2)
            {
                imageSelect2.SetActive(false);
                imageSymbolDiscipline[currentDiscipline].color = colorDefaultSymbol;
                imageButtonDiscipline[currentDiscipline].color = colorDefaultStart;
            }
        }
        else if (currentScreen == 6 || (currentScreen == 5 && currentMission == 0))
        {
            buttonContinue.SetActive(false);
        }
        else
        {
            buttonContinue.SetActive(true);
        }
    }
    
    private void QuestionText()
    {
        if (currentScreen == 0 || currentScreen == 2 || currentScreen == 4 || currentScreen == 5)
        {
            questionObject.SetActive(true);
            
            switch (progressState)
            {
                case 0:
                    switch (currentScreen)
                    {
                        case 0:
                            textQuestion.text = "What is your cause?";
                            questionObjectTransform.anchoredPosition = transformQuestion[0].localPosition;
                            break;
                        case 2:
                            textQuestion.text = "What negative emotion do you embrace most?";
                            questionObjectTransform.anchoredPosition = transformQuestion[1].localPosition;
                            break;
                    }
                    break;
                case 1:
                    switch (currentScreen)
                    {
                        case 0:
                            textQuestion.text = "What is your mode of expression?";
                            questionObjectTransform.anchoredPosition = transformQuestion[0].localPosition;
                            break;
                        case 2:
                            textQuestion.text = "What negative emotion do you express most?";
                            questionObjectTransform.anchoredPosition = transformQuestion[1].localPosition;
                            break;
                    }
                    break;
                case 2:
                    switch (currentScreen)
                    {
                        case 0:
                            textQuestion.text = "What is your sidekick's cause?";
                            questionObjectTransform.anchoredPosition = transformQuestion[0].localPosition;
                            break;
                        case 2:
                            textQuestion.text = "What negative emotion does your sidekick embrace most?";
                            questionObjectTransform.anchoredPosition = transformQuestion[1].localPosition;
                            break;
                    }
                    break;
                case 3:
                    switch (currentScreen)
                    {
                        case 4:
                            textQuestion.text = "Seek in prayer to find out more about this church";
                            questionObjectTransform.anchoredPosition = transformQuestion[2].localPosition;
                            break;
                        case 5:
                            textQuestion.text = "Choose one to overcome and they will speak to you";
                            questionObjectTransform.anchoredPosition = transformQuestion[3].localPosition;
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

    public void SelectCause(int index)
    {
        currentCause = index;

        if (!isHome)
        {
            UpdateData(0, currentCause);

            if (!imageSelect.activeSelf)
            {
                imageSelect.SetActive(true);
            }

            if (!buttonContinue.activeSelf)
            {
                buttonContinue.SetActive(true);
            }

            imageSelect.transform.position = imageButtonCause[index].transform.position;
            imageSymbolCause[currentCause].color = Color.white;
            imageButtonCause[currentCause].color = new Color(1f, 1f, 1f, 0.24f);
        }
        else
        {
            ButtonNavigation(1);
        }

        for (int i = 0; i < imageSymbolCause.Length; i++)
        {
            if (i != currentCause)
            {
                imageSymbolCause[i].color = colorDefaultSymbol;
                imageButtonCause[i].color = colorDefaultStart;
            }
        }

        imageDisciplineColors.sprite = spriteDisciplineColors[currentCause];
        imageCauseDescription.sprite = spriteCauseDescription[currentCause];
    }

    public void SelectDiscipline(int index)
    {
        if (!isHome)
        {
            currentDiscipline = index;
            UpdateData(1, currentDiscipline);
            if (!imageSelect2.activeSelf)
            {
                imageSelect2.SetActive(true);
            }

            if (!buttonContinue.activeSelf)
            {
                buttonContinue.SetActive(true);
            }

            imageSelect2.transform.position = imageButtonDiscipline[currentDiscipline].transform.position;
            imageSymbolDiscipline[currentDiscipline].color = Color.white;
            imageButtonDiscipline[currentDiscipline].color = new Color(1f, 1f, 1f, 0.24f);
        }

        for (int i = 0; i < imageSymbolDiscipline.Length; i++)
        {
            if (i != currentDiscipline)
            {
                imageSymbolDiscipline[i].color = colorDefaultSymbol;
                imageButtonDiscipline[i].color = colorDefaultStart;
            }
        }
    }

    private void UpdateData(int data, int index)
    {
        switch (progressState)
        {
            case 0:
                dataShell[data] = index + 1;
                imageShell.sprite = spriteShell[((dataShell[0] - 1) * 6) + dataShell[1]];
                textShell.text = stringCause[dataShell[0]] + " | " + stringDiscipline[dataShell[1]];
                break;
            case 1:
                dataCore[data] = index + 1;
                imageCore.sprite = spriteCore[((dataCore[0] - 1) * 6) + dataCore[1]];
                textCore.text = stringCause[dataCore[0]] + " | " + stringDiscipline[dataCore[1]];

                if (!core.activeSelf)
                {
                    core.SetActive(true);
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

    public void SetHomeText(string church, string leader, string objective, Sprite animal)
    {
        isHome = true;
        questionObject.SetActive(false);
        buttonBack.SetActive(false);
        buttonContinue.SetActive(false);
        scriptMap.SetYourChart();

        if (isChurch)
        {
            textHomeChurch.text = church;
            textHomeLeader.text = leader;
            textHomeObjective.text = objective;
            imageHomeAnimal.sprite = animal;
            textHomeMission.text = stringMission[currentMission - 1];
        }
        else
        {
            imageHomeTextChurch.color = colorLocked;
            imageCrosshair.color = colorLocked;

            for (int i = 0; i < 2; i++)
            {
                buttonNavigation[i].enabled = false;
                buttonNavigation[i].image.color = colorLocked;
            }
        }

        textHomeShell.text = textShell.text = stringCause[dataShell[0]] + " | " + stringDiscipline[dataShell[1]];
        textHomeCore.text = textCore.text = stringCause[dataCore[0]] + " | " + stringDiscipline[dataCore[1]];
        imageHomeShell.sprite = spriteShell[((dataShell[0] - 1) * 6) + dataShell[1]];
        imageHomeCore.sprite = spriteCore[((dataCore[0] - 1) * 6) + dataCore[1]];

        if (isTree)
        {
            textHomeTree.text = textTree.text = stringCause[dataTree[0]] + " | " + stringDiscipline[dataTree[1]];
            imageHomeTree.sprite = spriteTree[((dataTree[0] - 1) * 6) + dataTree[1]];
        }
    }
}
