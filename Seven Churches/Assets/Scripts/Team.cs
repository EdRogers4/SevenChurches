using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Team : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private ScreenManager scriptScreenManager;
    [SerializeField] private SelectionManager scriptSelectionManager;
    [SerializeField] private Map scriptMap;

    [Header("Slots")]
    [SerializeField] private GameObject[] memberSlot;
    [SerializeField] private Image[] imageShell;
    [SerializeField] private Image[] imageCore;
    [SerializeField] private Image[] imageTree;
    [SerializeField] private Image[] imageAnimal;
    [SerializeField] private TextMeshProUGUI[] textMemberName;
    [SerializeField] private TextMeshProUGUI[] textMemberRole;
    [SerializeField] private Button[] buttonSlot;

    [Header("Submit")]
    [SerializeField] private Sprite[] spriteButtonSlotUnselected;
    [SerializeField] private Sprite[] spriteButtonSlotSelected;
    [SerializeField] private TMP_InputField inputMemberCode;
    [SerializeField] private TMP_InputField inputMemberName;
    private int currentSlot;
    [SerializeField] private string[] stringCodeSection;
    private string stringChurchSection;
    [SerializeField] private int[] intCodeSection;
    private int intChurchSection;
    private int isSlot2;
    private int isSlot3;
    private int isSlot4;
    private int slot2Data0;
    private int slot2Data1;
    private int slot2Data2;
    private int slot3Data0;
    private int slot3Data1;
    private int slot3Data2;
    private int slot4Data0;
    private int slot4Data1;
    private int slot4Data2;
    private int slot2Church;
    private int slot3Church;
    private int slot4Church;
    private string slot2Name;
    private string slot3Name;
    private string slot4Name;

    public void UpdateUserSlot()
    {
        textMemberRole[0].text = scriptMap.stringRole[scriptMap.indexAura[0]];
        imageShell[0].sprite = scriptScreenManager.spriteShell[scriptMap.indexAura[0]];
        imageCore[0].sprite = scriptScreenManager.spriteCore[scriptMap.indexAura[1]];

        if (scriptScreenManager.isTree)
        {
            imageTree[0].sprite = scriptScreenManager.spriteTree[scriptMap.indexAura[2]];
        }
        else
        {
            imageTree[0].enabled = false;
        }

        if (scriptScreenManager.isChurch)
        {
            imageAnimal[0].sprite = scriptSelectionManager.animal[scriptScreenManager.dataShell[0] - 1];
        }
        else
        {
            imageAnimal[0].sprite = scriptSelectionManager.none;
        }
    }

    public void UpdateCurrentSlot(int index)
    {
        if (index == currentSlot)
        {
            return;
        }

        currentSlot = index;

        for (int i = 0; i < buttonSlot.Length; i++)
        {
            if (i == currentSlot)
            {
                buttonSlot[i].image.sprite = spriteButtonSlotSelected[currentSlot];
            }
            else
            {
                buttonSlot[i].image.sprite = spriteButtonSlotUnselected[i];
            }
        }
    }

    public void ButtonSubmit()
    {
        for (int i = 0; i < stringCodeSection.Length; i++)
        {
            stringCodeSection[i] = "";
        }

        stringChurchSection = "";

        if (inputMemberCode.text.Length == 7)
        {
            stringCodeSection[0] += inputMemberCode.text[0];
            stringCodeSection[0] += inputMemberCode.text[1];
            stringCodeSection[1] += inputMemberCode.text[2];
            stringCodeSection[1] += inputMemberCode.text[3];
            stringCodeSection[2] += inputMemberCode.text[4];
            stringCodeSection[2] += inputMemberCode.text[5];
            stringChurchSection += inputMemberCode.text[6];

            for (int i = 0; i < intCodeSection.Length; i++)
            {
                intCodeSection[i] = int.Parse(stringCodeSection[i]);
            }

            intChurchSection = int.Parse(stringChurchSection);
            Debug.Log("inChurchSection: " + intChurchSection);
        }

        if ((intCodeSection[0] > 0 && intCodeSection[0] <= 42) && (intCodeSection[1] > 0 && intCodeSection[1] <= 42) 
            && (intCodeSection[2] >= 0 && intCodeSection[2] <= 42) && (intChurchSection < 8 && intChurchSection >= 0))
        {
            if (!memberSlot[currentSlot + 1].activeSelf)
            {
                memberSlot[currentSlot + 1].SetActive(true);
            }

            imageShell[currentSlot + 1].sprite = scriptScreenManager.spriteShell[intCodeSection[0]];
            imageCore[currentSlot + 1].sprite = scriptScreenManager.spriteCore[intCodeSection[1]];

            if (intCodeSection[2] > 0)
            {
                imageTree[currentSlot + 1].enabled = true;
                imageTree[currentSlot + 1].sprite = scriptScreenManager.spriteTree[intCodeSection[2]];
            }
            else
            {
                imageTree[currentSlot + 1].enabled = false;
            }

            if (intChurchSection > 0)
            {
                imageAnimal[currentSlot + 1].sprite = scriptSelectionManager.animal[intChurchSection - 1];
            }
            else
            {
                imageAnimal[currentSlot + 1].sprite = scriptSelectionManager.none;
            }

            textMemberRole[currentSlot + 1].text = scriptMap.stringRole[intCodeSection[0]];
            textMemberName[currentSlot + 1].text = inputMemberName.text;

            switch (currentSlot + 1)
            {
                case 1:
                    isSlot2 = 1;
                    slot2Data0 = intCodeSection[0];
                    slot2Data1 = intCodeSection[1];
                    slot2Data2 = intCodeSection[2];
                    slot2Name = inputMemberName.text;
                    slot2Church = intChurchSection;
                    break;
                case 2:
                    isSlot3 = 1;
                    slot3Data0 = intCodeSection[0];
                    slot3Data1 = intCodeSection[1];
                    slot3Data2 = intCodeSection[2];
                    slot3Name = inputMemberName.text;
                    slot3Church = intChurchSection;
                    break;
                case 3:
                    isSlot4 = 1;
                    slot4Data0 = intCodeSection[0];
                    slot4Data1 = intCodeSection[1];
                    slot4Data2 = intCodeSection[2];
                    slot4Name = inputMemberName.text;
                    slot4Church = intChurchSection;
                    break;
            }
        }
    }

    public void ButtonClear()
    {
        if (memberSlot[currentSlot + 1].activeSelf)
        {
            memberSlot[currentSlot + 1].SetActive(false);
        }

        switch (currentSlot + 1)
        {
            case 1:
                isSlot2 = 0;
                slot2Data0 = 0;
                slot2Data1 = 0;
                slot2Data2 = 0;
                slot2Name = "Member Name";
                slot2Church = 0;
                break;
            case 2:
                isSlot3 = 0;
                slot3Data0 = 0;
                slot3Data1 = 0;
                slot3Data2 = 0;
                slot3Name = "Member Name";
                slot3Church = 0;
                break;
            case 3:
                isSlot4 = 0;
                slot4Data0 = 0;
                slot4Data1 = 0;
                slot4Data2 = 0;
                slot4Name = "Member Name";
                slot4Church = 0;
                break;
        }
    }

    public void ButtonSave()
    {
        PlayerPrefs.SetInt("isSlot2", isSlot2);
        PlayerPrefs.SetInt("isSlot3", isSlot3);
        PlayerPrefs.SetInt("isSlot4", isSlot4);
        PlayerPrefs.SetInt("slot2Data0", slot2Data0);
        PlayerPrefs.SetInt("slot2Data1", slot2Data1);
        PlayerPrefs.SetInt("slot2Data2", slot2Data2);
        PlayerPrefs.SetInt("slot3Data0", slot3Data0);
        PlayerPrefs.SetInt("slot3Data1", slot3Data1);
        PlayerPrefs.SetInt("slot3Data2", slot3Data2);
        PlayerPrefs.SetInt("slot4Data0", slot4Data0);
        PlayerPrefs.SetInt("slot4Data1", slot4Data1);
        PlayerPrefs.SetInt("slot4Data2", slot4Data2);
        PlayerPrefs.SetString("slot2Name", slot2Name);
        PlayerPrefs.SetString("slot3Name", slot3Name);
        PlayerPrefs.SetString("slot4Name", slot4Name);
        PlayerPrefs.SetInt("slot2Church", slot2Church);
        PlayerPrefs.SetInt("slot3Church", slot3Church);
        PlayerPrefs.SetInt("slot4Church", slot4Church);
    }

    public void ButtonLoad()
    {
        for (int i = 0; i < 3; i++)
        {
            if ((PlayerPrefs.GetInt("isSlot" + (i + 2))) >= 1)
            {
                memberSlot[i + 1].SetActive(true);
                imageShell[i + 1].sprite = scriptScreenManager.spriteShell[PlayerPrefs.GetInt("slot" + (i + 2) + "Data0")];
                imageCore[i + 1].sprite = scriptScreenManager.spriteCore[PlayerPrefs.GetInt("slot" + (i + 2) + "Data1")];

                if (PlayerPrefs.GetInt("slot" + (i + 2) + "Data2") > 0)
                {
                    imageTree[i + 1].enabled = true;
                    imageTree[i + 1].sprite = scriptScreenManager.spriteTree[PlayerPrefs.GetInt("slot" + (i + 2) + "Data2")];
                }
                else
                {
                    imageTree[i + 1].enabled = false;
                }

                if ((PlayerPrefs.GetInt("slot" + (i + 2) + "Church")) == 0)
                {
                    imageAnimal[i + 1].sprite = scriptSelectionManager.none;
                }
                else
                {
                    imageAnimal[i + 1].sprite = scriptSelectionManager.animal[(PlayerPrefs.GetInt("slot" + (i + 2) + "Church"))];
                    Debug.Log("Church index: " + PlayerPrefs.GetInt("slot" + (i + 2) + "Church"));
                }

                textMemberRole[i + 1].text = scriptMap.stringRole[PlayerPrefs.GetInt("slot" + (i + 2) + "Data0")];
                textMemberName[i + 1].text = PlayerPrefs.GetString("slot" + (i + 2) + "Name");
            }
            else
            {
                memberSlot[i + 1].SetActive(false);
            }
        }
    }
}
