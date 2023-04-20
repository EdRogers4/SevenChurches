using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [SerializeField] private ScreenManager screenManager;
    [SerializeField] private GameObject[] arrow;
    [SerializeField] private GameObject[] color;
    [SerializeField] private Image[] imageArrow;
    [SerializeField] private Image[] imageColor;
    [SerializeField] private Sprite[] spriteColor;
    [SerializeField] private RectTransform[] transformArrow;
    [SerializeField] private int[] indexAura;
    private float rotationFirstColor = -265f;
    private float rotationInterval = 8.5714f;

    public void SetAura()
    {
        indexAura[0] = ((screenManager.dataShell[0] - 1) * 6) + screenManager.dataShell[1];
        indexAura[1] = ((screenManager.dataCore[0] - 1) * 6) + screenManager.dataCore[1];
        indexAura[2] = ((screenManager.dataTree[0] - 1) * 6) + screenManager.dataTree[1];
        screenManager.textTeamCode.text = "team code: " + indexAura[0] + "-" + indexAura[1] + "-" + indexAura[2];

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

                arrow[i].SetActive(true);
                color[i].SetActive(true);
                imageColor[i].sprite = spriteColor[indexAura[i] - 1];
                var newRotation = rotationFirstColor + (((float)indexAura[i] - 1) * rotationInterval);
                transformArrow[i].rotation = Quaternion.Euler(0f, 0f, newRotation);
            }
            else
            {
                arrow[i].SetActive(false);
                color[i].SetActive(false);
            }
        }
    }
}
