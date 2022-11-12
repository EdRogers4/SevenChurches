using UnityEngine;
using System;

namespace LBG.UI.Radial
{
	[CreateAssetMenu(fileName = "New Radial Button", menuName = "LBG/RadialMenu/Element/Button", order = 2)]
	public class RadialMenuButton : RadialMenuObject
	{
		#region Public Variables

		[Header("Displayed Name")]
		[Tooltip("The name that is displayed on screen")]
		/// <summary>
		/// The displayed name of the button
		/// </summary>
		public string m_ElementName;

		[Header("Event Name (Must be unique for each button in a layer")]
		[Tooltip("The name of the event used in the menu input manager")]
		/// <summary>
		/// The event that is called in the input manager
		/// </summary>
		public string m_ElementEvent;
		
		[Header("Event Index (Must be unique for each button in a layer")]
		[Tooltip("The index used when calling church events")]
		/// <summary>
		/// The index associated with the element
		/// </summary>
		public int m_ElementIndex;


		#endregion
		
		/// <summary>
		/// Deals with all of the interaction - called from the layer
		/// </summary>
		public override Type Interact(RadialBase menu,string layerEvent)
		{
			if (m_ElementIndex > 0)
			{
				menu.m_InputManager.GetComponent<ChurchMenuManager>().currentChurch = m_ElementIndex;
			}

			menu.m_InputManager.ProcessButton(layerEvent, m_ElementEvent);

			return GetType();
		}

		/// <summary>
		/// Returns the name of the button
		/// </summary>
		/// <returns>Returns the name of the button</returns>
		public override string GetName()
		{
			return m_ElementName;
		}
	}
}