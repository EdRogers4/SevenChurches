using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
	[SerializeField] private AudioSource audioSourceMusic;
	[SerializeField] private AudioClip[] song;
	[SerializeField] private Sprite[] animal;
	[SerializeField] private TextMeshProUGUI[] textObjects;
	[SerializeField] private string titleChurch;
	[SerializeField] private string titleLeader;
	[SerializeField] private string titleObjective;
	[SerializeField] private string[] textChurch;
	[SerializeField] private string[] textLeader;
	[SerializeField] private string[] textObjective;
	[SerializeField] private string[] textQuote;
	[SerializeField] private float speedTextSlow;
	[SerializeField] private float speedTextFast;
	[SerializeField] private float speedTextTransition;
	[SerializeField] private Image imageAnimal;
	private int currentButton;
	private int currentSong;
	private string stringCheck;

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
		for (int i = 0; i < textObjective[currentButton - 1].Length; i++)
		{
			textObjects[1].text += textObjective[currentButton - 1][i];
			yield return new WaitForSeconds(speedTextSlow);
		}

		yield return new WaitForSeconds(speedTextTransition);
		StartCoroutine(AnimateTextQuote());
	}

	private IEnumerator AnimateTextQuote()
	{
		if (currentButton != 4)
		{
			for (int i = 0; i < textQuote[currentButton - 1].Length; i++)
			{
				if (currentButton == 1)
				{
					var quoteIndex = currentButton - 1;
					CheckForLineBreak(quoteIndex, i);
				}

				textObjects[2].text += textQuote[currentButton - 1][i];
				yield return new WaitForSeconds(speedTextFast);
			}
		}
		else
		{
			//textObjects
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
	}

	public void ButtonSelect(int button)
	{
		currentButton = button;
		
		if (currentSong != button)
		{
			currentSong = currentButton;
			audioSourceMusic.clip = song[currentButton - 1];
			audioSourceMusic.Play();
			imageAnimal.sprite = animal[currentButton];
			textObjects[0].text = "";
			textObjects[1].text = "";
			textObjects[2].text = "";
			textObjects[3].text = "";
			StartCoroutine(AnimateTitleChurch());
		}
	}
		
}
