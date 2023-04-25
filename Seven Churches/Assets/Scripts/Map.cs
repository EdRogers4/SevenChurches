using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Map : MonoBehaviour
{
    public int[] indexAura;
    [SerializeField] private ScreenManager screenManager;
    [SerializeField] private Team scriptTeam;
    [SerializeField] private GameObject[] arrow;
    [SerializeField] private GameObject[] color;
    [SerializeField] private Image[] imageArrow;
    [SerializeField] private Image[] imageColor;
    [SerializeField] private Sprite[] spriteColor;
    [SerializeField] private RectTransform[] transformArrow;
    [SerializeField] private Button[] buttonChart;
    [SerializeField] private Sprite[] spriteButtonUnselected;
    [SerializeField] private Sprite[] spriteButtonSelected;
    [SerializeField] private TextMeshProUGUI[] textTitle;
    [SerializeField] private TextMeshProUGUI[] textRole;
    public string[] stringRole;
    private float rotationFirstColor = -291.5f;
    private float rotationInterval = 8.5714f;
    private int currentChart;
    private bool isSetCode;
    private bool isSkipThisStep;

    private void SetTeamCode()
    {
        isSetCode = true;
        screenManager.textTeamCode.text = "team code: ";

        for (int i = 0; i < 4; i++)
        {
            if (!screenManager.isTree && i == 2)
            {
                screenManager.textTeamCode.text += "00";
                isSkipThisStep = true;
            }
            else if (i < indexAura.Length && indexAura[i] < 10)
            {
                screenManager.textTeamCode.text += "0";
            }

            if (i < indexAura.Length)
            {
                if (!isSkipThisStep)
                {
                    screenManager.textTeamCode.text += indexAura[i];
                }
                else
                {
                    isSkipThisStep = false;
                }
            }
            else
            {
                if (!screenManager.isChurch)
                {
                    screenManager.textTeamCode.text += "0";
                }
                else
                {
                    var index = (int)Mathf.Floor(indexAura[0] / 6f);
                    screenManager.textTeamCode.text += index;
                }
            }
        }

        scriptTeam.UpdateUserSlot();
    }
    public void SetYourChart()
    {
        currentChart = 0;
        SetButtonSprites();
        indexAura[0] = ((screenManager.dataShell[0] - 1) * 6) + screenManager.dataShell[1];
        indexAura[1] = ((screenManager.dataCore[0] - 1) * 6) + screenManager.dataCore[1];
        indexAura[2] = ((screenManager.dataTree[0] - 1) * 6) + screenManager.dataTree[1];
        textTitle[0].text = "SHELL";
        textTitle[1].text = "CORE";

        if (screenManager.isTree)
        {
            textTitle[2].text = "TREE";
        }
        else
        {
            textTitle[2].text = "";
            textRole[2].text = "";
        }

        textTitle[3].text = "";
        textRole[3].text = "";

        if (!isSetCode)
        {
            SetTeamCode();
        }

        for (int i = 0; i < arrow.Length; i++)
        {
            if (i < 3)
            {
                if (i == 2 && !screenManager.isTree)
                {
                    arrow[i].SetActive(false);
                    color[i].SetActive(false);
                    break;
                }
                else
                {
                    textRole[i].text = "";
                }

                arrow[i].SetActive(true);
                color[i].SetActive(true);
                textRole[i].text = stringRole[indexAura[i]];
                imageColor[i].sprite = spriteColor[indexAura[i] - 1];
                var newRotation = rotationFirstColor + (((float)indexAura[i] - 1) * rotationInterval);
                transformArrow[i].rotation = Quaternion.Euler(0f, 0f, newRotation);
            }
            else
            {
                arrow[i].SetActive(false);
                color[i].SetActive(false);
                textRole[i].text = stringRole[0];
            }
        }
    }

    public void SetTwinFlame()
    {
        currentChart = 1;
        SetButtonSprites();
        textTitle[0].text = "SHELL";
        textTitle[1].text = "TWIN FLAME";
        textTitle[2].text = "";
        textTitle[3].text = "";

        for (int i = 0; i < arrow.Length; i++)
        {
            if (i < 2)
            {
                arrow[i].SetActive(true);
                color[i].SetActive(true);

                if (i == 0)
                {
                    textRole[i].text = stringRole[indexAura[0]];
                    imageColor[i].sprite = spriteColor[indexAura[0] - 1];
                    var newRotation = rotationFirstColor + (((float)indexAura[0] - 1) * rotationInterval);
                    transformArrow[i].rotation = Quaternion.Euler(0f, 0f, newRotation);
                }
                else if (i == 1)
                {
                    var index = indexAura[0];

                    for (int j = 0; j < 21; j++)
                    {
                        index += 1;

                        if (index > 42)
                        {
                            index = 1;
                        }
                    }

                    textRole[i].text = stringRole[index];
                    imageColor[i].sprite = spriteColor[index - 1];
                    var newRotation = rotationFirstColor + (((float)index - 1) * rotationInterval);
                    transformArrow[i].rotation = Quaternion.Euler(0f, 0f, newRotation);
                }
            }
            else
            {
                arrow[i].SetActive(false);
                color[i].SetActive(false);
                textRole[i].text = "";
            }
        }
    }

    public void SetHarmony()
    {
        currentChart = 2;
        SetButtonSprites();
        textTitle[0].text = "SHELL";
        textTitle[1].text = "Tone 1";
        textTitle[2].text = "Tone 2";
        textTitle[3].text = "";

        for (int i = 0; i < arrow.Length; i++)
        {
            if (i < 3)
            {
                arrow[i].SetActive(true);
                color[i].SetActive(true);

                if (i == 0)
                {
                    textRole[i].text = stringRole[indexAura[0]];
                    imageColor[i].sprite = spriteColor[indexAura[0] - 1];
                    var newRotation = rotationFirstColor + (((float)indexAura[0] - 1) * rotationInterval);
                    transformArrow[i].rotation = Quaternion.Euler(0f, 0f, newRotation);
                }
                else if (i > 0)
                {
                    var index = indexAura[0];

                    for (int j = 0; j < 14; j++)
                    {
                        if (i == 1)
                        {
                            index += 1;

                            if (index > 42)
                            {
                                index = 1;
                            }
                        }
                        else if (i == 2)
                        {
                            index -= 1;

                            if (index < 1)
                            {
                                index = 42;
                            }
                        }
                    }

                    textRole[i].text = stringRole[index];
                    imageColor[i].sprite = spriteColor[index - 1];
                    var newRotation = rotationFirstColor + (((float)index - 1) * rotationInterval);
                    transformArrow[i].rotation = Quaternion.Euler(0f, 0f, newRotation);
                }
            }
            else
            {
                arrow[i].SetActive(false);
                color[i].SetActive(false);
                textRole[i].text = "";
            }
        }
    }

    public void SetTeamChart()
    {
        currentChart = 3;
        SetButtonSprites();
        textTitle[0].text = "You";
        textTitle[1].text = scriptTeam.slot2Name;
        textTitle[2].text = scriptTeam.slot3Name;
        textTitle[3].text = scriptTeam.slot4Name;

        if (scriptTeam.isSlot2 >= 1)
        {
            arrow[1].SetActive(true);
            color[1].SetActive(true);
            textRole[1].text = stringRole[scriptTeam.slot2Data0];
            imageColor[1].sprite = spriteColor[scriptTeam.slot2Data0 - 1];
            var newRotation = rotationFirstColor + (((float)scriptTeam.slot2Data0 - 1) * rotationInterval);
            transformArrow[1].rotation = Quaternion.Euler(0f, 0f, newRotation);
        }
        else
        {
            arrow[1].SetActive(false);
            color[1].SetActive(false);
            textRole[1].text = "";
        }

        if (scriptTeam.isSlot3 >= 1)
        {
            arrow[2].SetActive(true);
            color[2].SetActive(true);
            textRole[2].text = stringRole[scriptTeam.slot3Data0];
            imageColor[2].sprite = spriteColor[scriptTeam.slot3Data0 - 1];
            var newRotation = rotationFirstColor + (((float)scriptTeam.slot3Data0 - 1) * rotationInterval);
            transformArrow[2].rotation = Quaternion.Euler(0f, 0f, newRotation);
        }
        else
        {
            arrow[2].SetActive(false);
            color[2].SetActive(false);
            textRole[2].text = "";
        }

        if (scriptTeam.isSlot4 >= 1)
        {
            arrow[3].SetActive(true);
            color[3].SetActive(true);
            textRole[3].text = stringRole[scriptTeam.slot3Data0];
            imageColor[3].sprite = spriteColor[scriptTeam.slot3Data0 - 1];
            var newRotation = rotationFirstColor + (((float)scriptTeam.slot3Data0 - 1) * rotationInterval);
            transformArrow[3].rotation = Quaternion.Euler(0f, 0f, newRotation);
        }
        else
        {
            arrow[3].SetActive(false);
            color[3].SetActive(false);
            textRole[3].text = "";
        }
    }

    private void SetButtonSprites()
    {
        for (int i = 0; i < buttonChart.Length; i++)
        {
            if (i == currentChart)
            {
                buttonChart[i].image.sprite = spriteButtonSelected[i];
            }
            else
            {
                buttonChart[i].image.sprite = spriteButtonUnselected[i];
            }
        }
    }
}
