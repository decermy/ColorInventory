using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorInventoryItemComponent : MonoBehaviour, ISelectable
{
	public static event Action<ColorInventoryItemComponent> SelectItem = delegate { };

	public Image image;
	[SerializeField]
	private Image selection;

	public ColorInventoryItem colorInventoryItem { get; private set; }

	public void Init(ColorInventoryItem colorInventoryItem)
	{
		this.colorInventoryItem = colorInventoryItem;
	}

	/// <summary>
	/// invoke acion -> select color inventory item
	/// </summary>
	public void OnPointerClick(PointerEventData eventData)
	{
		SelectItem.Invoke(this);
	}

	/// <summary>
	/// show selection flag image
	/// </summary>
	public void Select(bool selected)
	{
		selection.enabled = selected;
	}
}
