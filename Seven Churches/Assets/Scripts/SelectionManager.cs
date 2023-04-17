using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
	[SerializeField] private AudioSource audioSourceMusic;
	[SerializeField] private AudioSource audioSourceSFX;
	[SerializeField] private Animator animatorPrompt;
	[SerializeField] private AudioClip[] song;
	[SerializeField] private AudioClip click;
	[SerializeField] private Sprite[] animal;
	[SerializeField] private Sprite noAnimal;
	[SerializeField] private TextMeshProUGUI[] textObjects;
	[SerializeField] private string titleChurch;
	[SerializeField] private string titleLeader;
	[SerializeField] private string titleObjective;
	[SerializeField] private string[] textChurch;
	[SerializeField] private string[] textLeader;
	[SerializeField] private string[] textObjective;
	[SerializeField] private string[] textQuote;
	[SerializeField] private string textName;
	[SerializeField] private string[] textScripture;
	[SerializeField] private float speedTextSlow;
	[SerializeField] private float speedTextFast;
	[SerializeField] private float speedTextTransition;
	[SerializeField] private float speedTextName;
	[SerializeField] private Image imageAnimal;
	[SerializeField] private Button[] buttons;
	[SerializeField] private Sprite[] spriteUnselected;
	[SerializeField] private Sprite[] spriteSelected;
	private int currentButton;
	private int currentSong;
	private string stringCheck;
	private bool isFirstSelection;

	private void Start()
	{
		StartCoroutine(ShowPrompt());
	}

	private IEnumerator ShowPrompt()
	{
		yield return new WaitForSeconds(12.0f);

		/*
		if (!isFirstSelection)
		{
			animatorPrompt.SetBool("isShow", true);
		}
		*/
	}

	private void CheckForLineBreak(int quoteIndex, int charIndex)
	{
		stringCheck = "";
		stringCheck += textQuote[quoteIndex][charIndex];

		if (stringCheck == "–")
		{
			textObjects[2].text += "<br>";
		}
	}

	private IEnumerator AnimateTitleChurch()
	{
		for (int i = 0; i < titleChurch.Length; i++)
		{
			yield return new WaitForSeconds(speedTextSlow);
			textObjects[0].text += titleChurch[i];
		}

		textObjects[0].text += "<b>";
		StartCoroutine(AnimateTextChurch());
	}

	private IEnumerator AnimateTextChurch()
	{
		for (int i = 0; i < textChurch[currentButton - 1].Length; i++)
		{
			yield return new WaitForSeconds(speedTextSlow);
			textObjects[0].text += textChurch[currentButton - 1][i];
		}

		yield return new WaitForSeconds(speedTextTransition);
		StartCoroutine(AnimateTitleLeader());
	}

	private IEnumerator AnimateTitleLeader()
	{
		for (int i = 0; i < titleLeader.Length; i++)
		{
			yield return new WaitForSeconds(speedTextSlow);
			textObjects[1].text += titleLeader[i];
		}

		textObjects[1].text += "<b>";
		StartCoroutine(AnimateTextLeader());
	}

	private IEnumerator AnimateTextLeader()
	{
		for (int i = 0; i < textLeader[currentButton - 1].Length; i++)
		{
			textObjects[1].text += textLeader[currentButton - 1][i];
			yield return new WaitForSeconds(speedTextSlow);
		}

		textObjects[1].text += "</b>";
		yield return new WaitForSeconds(speedTextTransition);
		StartCoroutine(AnimateTitleObjective());
	}

	private IEnumerator AnimateTitleObjective()
	{
		for (int i = 0; i < titleObjective.Length; i++)
		{
			yield return new WaitForSeconds(speedTextSlow);
			textObjects[1].text += titleObjective[i];
		}

		textObjects[1].text += "<b>";
		StartCoroutine(AnimateTextObjective());
	}

	private IEnumerator AnimateTextObjective()
	{
		for (int i = 0; i < textObjective[currentButton].Length; i++)
		{
			textObjects[1].text += textObjective[currentButton][i];
			yield return new WaitForSeconds(speedTextSlow);
		}

		yield return new WaitForSeconds(speedTextTransition);
		StartCoroutine(AnimateTextQuote());
	}

	private IEnumerator AnimateTextQuote()
	{
		if (currentButton != 4 && currentButton != 1)
		{
			for (int i = 0; i < textQuote[currentButton - 1].Length; i++)
			{
				textObjects[2].text += textQuote[currentButton - 1][i];
				yield return new WaitForSeconds(speedTextFast);
			}
		}
		else if (currentButton == 4)
		{
			for (int i = 0; i < textQuote[7].Length; i++)
			{
				CheckForLineBreak(7, i);
				textObjects[2].text += textQuote[7][i];
				yield return new WaitForSeconds(speedTextFast);
			}

			yield return new WaitForSeconds(speedTextTransition);

			for (int i = 0; i < textQuote[currentButton - 1].Length; i++)
			{
				textObjects[3].text += textQuote[currentButton - 1][i];
				yield return new WaitForSeconds(speedTextFast);
			}
		}
		else if (currentButton == 1)
		{
			for (int i = 0; i < textQuote[currentButton - 1].Length; i++)
			{
				CheckForLineBreak(1, i);
				textObjects[2].text += textQuote[currentButton - 1][i];
				yield return new WaitForSeconds(speedTextFast);
			}

			for (int i = 0; i < textQuote[8].Length; i++)
			{
				textObjects[8].text += textQuote[8][i];
				yield return new WaitForSeconds(speedTextFast);
			}
		}
	}

	private IEnumerator AnimateTextScripture()
	{
		for (int i = 0; i < textName.Length; i++)
		{
			textObjects[4].text += textName[i];
			yield return new WaitForSeconds(speedTextSlow);
		}
		
		for (int i = 0; i < titleObjective.Length; i++)
		{
			if (i != 0 && i != 1)
			{
				yield return new WaitForSeconds(speedTextSlow);
				textObjects[1].text += titleObjective[i];
			}
		}

		textObjects[1].text += "<b>";
		
		for (int i = 0; i < textObjective[currentButton].Length; i++)
		{
			textObjects[1].text += textObjective[currentButton][i];
			yield return new WaitForSeconds(speedTextSlow);
		}

		yield return new WaitForSeconds(speedTextTransition);

		for (int i = 0; i < textScripture[0].Length; i++)
		{
			textObjects[5].text += textScripture[0][i];
			yield return new WaitForSeconds(speedTextFast);
		}
		
		for (int i = 0; i < textScripture[1].Length; i++)
		{
			textObjects[6].text += textScripture[1][i];
			yield return new WaitForSeconds(speedTextFast);
		}
		
		for (int i = 0; i < textScripture[2].Length; i++)
		{
			textObjects[7].text += textScripture[2][i];
			yield return new WaitForSeconds(speedTextFast);
		}
	}

	public void ButtonSelect(int button)
	{
		currentButton = button;
		
		if (currentSong != button)
		{
			NewSelection();
		}

		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].image.sprite = spriteUnselected[i];
		}
		
		buttons[currentButton].image.sprite = spriteSelected[currentButton];

		if (!isFirstSelection && currentButton == 0)
		{
			NewSelection();
		}
	}

	public void NewSelection()
	{
		StopAllCoroutines();
		currentSong = currentButton;

		if (!isFirstSelection && currentButton == 0)
		{
			
		}
		else
		{
			audioSourceMusic.clip = song[currentButton];
			audioSourceMusic.Play();
		}

		for (int i = 0; i < textObjects.Length; i++)
		{
			textObjects[i].text = "";
		}

		if (!isFirstSelection)
		{
			isFirstSelection = true;
			animatorPrompt.SetBool("isShow", false);
		}

		if (currentButton > 0)
		{
			imageAnimal.sprite = animal[currentButton - 1];
			StartCoroutine(AnimateTitleChurch());
		}
		else
		{
			imageAnimal.sprite = noAnimal;
			StartCoroutine(AnimateTextScripture());
		}
	}

	public void CloseApplication()
	{
		Application.Quit();
	}
}
