using UnityEngine;
using UnityEngine.UI;

namespace LBG.UI.Radial
{
	public class ChurchMenuManager : RadialMenuInputManager
	{
		[SerializeField] private AudioSource audioSourceMusic;
		[SerializeField] private AudioClip[] song;
		[SerializeField] private Sprite[] animal;
		[SerializeField] private string[] textChurch;
		[SerializeField] private string[] textLeader;
		[SerializeField] private string[] textTrait;
		[SerializeField] private string[] textQuote;
		[SerializeField] private Image imageAnimal;
		[SerializeField] private RadialLayerButtons layerMenu;
		public int currentChurch;
		private int currentSong;
		
		/// <summary>
		/// Process a button based on that layers event name and the buttons event name
		/// </summary>
		/// <param name="layerEvent">Event name of the layer</param>
		/// <param name="buttonEvent">Event name of the button</param>
		public override void ProcessButton(string layerEvent, string buttonEvent)
		{
			if (currentSong != currentChurch)
			{
				currentSong = currentChurch;
				audioSourceMusic.clip = song[currentChurch - 1];
				audioSourceMusic.Play();
				imageAnimal.sprite = animal[currentChurch];
			}

			switch(layerEvent)
            {
                case "Main":

                    switch (buttonEvent)
                    {
                        case "Settings":
                            ChangeLayer("Settings");
                            break;

                        case "Useless Buttons":
                            ChangeLayer("Useless Buttons");
                            break;
                        
                        case "Church1":
	                        Debug.Log("Church1");
	                        break;
                        
                        case "Church2":
	                        Debug.Log("Church2");
	                        break;
                        
                        case "Church3":
	                        Debug.Log("Church3");
	                        break;
                        
                        case "Church4":
	                        Debug.Log("Church4");
	                        break;
                        
                        case "Church5":
	                        Debug.Log("Church5");
	                        break;
                        
                        case "Church6":
	                        Debug.Log("Church6");
	                        break;
                        
                        case "Church7":
	                        Debug.Log("Church7");
	                        break;

                        default:
                            Debug.LogWarning("No button event handler for " + buttonEvent + " in layer " + layerEvent);
                            break;
                    }
                    break;

                case "Settings":
                    switch (buttonEvent)
                    { 
                        case "Volume":
                            ChangeLayer("Volume", ExampleSettings.Volume);
                            break;

                        case "Mute":
                            ExampleSettings.ToggleMute();
                            break;

                        case "Background Colour":
                            ChangeLayer("Background Colour", ExampleSettings.CurrentBackgroundIndex, ExampleSettings.BackgroundColours);
                            break;
                        
                        case "Cancel":
	                        ChangeLayer("Main");
	                        break;

                        default:
                            Debug.LogWarning("No button event handler for " + buttonEvent + " in layer " + layerEvent);
                            break;
                    }
                    break;
             
                default:
                    Debug.LogWarning("No layer event handler for " + layerEvent);
                    break;
            }
		}

		/// <summary>
		/// Process a selection layer based on it's layer event name
		/// </summary>
		/// <param name="layerEvent">Event name of the layer</param>
		/// <param name="buttonEvent">Event name of the button</param>
		/// <param name="newIndex">The index that is returned from the selction layer</param>
		/// <param name="originalIndex">The original index that was set on the layers construct</param>
		public override void ProcessSelection(string layerEvent, string buttonEvent, int newIndex, int originalIndex)
		{
            switch (layerEvent)
            {
                case "Background Colour":

                    switch (buttonEvent)
                    {
                        case "Update":
                            ExampleSettings.ChangeBackgroundColour(newIndex);
                            break;

                        case "Confirm":
                            ExampleSettings.ChangeBackgroundColour(newIndex);
                            GoToPreviousLayer();
                            break;

                        case "Cancel":
                            ExampleSettings.ChangeBackgroundColour(originalIndex);
                            GoToPreviousLayer();
                            break;

                        default:
                            Debug.LogWarning("No button event handler for " + buttonEvent + " in layer " + layerEvent);
                            break;
                    }
                    break;
                    
                default:
                    Debug.LogWarning("No layer event handler for " + layerEvent);
                    break;
            }
		}

		/// <summary>
		/// Process a slider based on it's layer event name
		/// </summary>
		/// <param name="layerEvent">Event name of the layer</param>
		/// <param name="value">Value that is returned from the slider</param>
		/// <param name="saveValue">Should the value be saved</param>
		public override void ProcessSlider(string layerEvent, float value, bool saveValue)
		{
            switch (layerEvent)
            {
                case "Volume":
                    ExampleSettings.ChangeVolume(value);
                    break;

                default:
                    Debug.LogWarning("No layer event handler for " + layerEvent);
                    break;
            }
		}
		
		/// <summary>
		/// Process to return values for a slider, selection or toggle button
		/// </summary>
		/// <param name="layerEvent">Event name of the layer</param>
		/// <param name="buttonEvent">Event name of the button</param>
		/// <returns>Process to return values for a slider, selection or toggle button</returns>
		public override T ProcessReturnValues<T>(string layerEvent, string buttonEvent)
		{
            switch (layerEvent)
            {
                case "Settings":

                    switch (buttonEvent)
                    {
                        case "Mute":
                            return GetToggleValue<T>(ExampleSettings.Mute);
                            
                        case "Background Colour":
                            return GetSelectionValue<T>(ExampleSettings.BackgroundColours[ExampleSettings.CurrentBackgroundIndex]);

                        case "Volume":
                            return GetSliderValue<T>(ExampleSettings.Volume);
                            
                        default:
                            Debug.LogWarning("No button event handler for " + buttonEvent + " in layer " + layerEvent);
                            return GetNullValue<T>();
                    }
                    
                default:
                    Debug.LogWarning("No layer event handler for " + layerEvent);
                    return GetNullValue<T>();
            }
		}

		/// <summary>
		/// What happens when you are on the top layer and cancel is pressed
		/// </summary>
		public override void LastCancel()
		{
			//Leave blank unless you want to do something unique on your top layer when using the cancel button for example, quit the application.
		}
	}
}