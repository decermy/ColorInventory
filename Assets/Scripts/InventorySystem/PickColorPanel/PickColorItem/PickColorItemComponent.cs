using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class PickColorItemComponent : MonoBehaviour, IPickable
{
	public static event Action<PickColorItemComponent> PickColorItemComponentPick = delegate { };

	[SerializeField]
	private Image image;

	public IPickColorItem pickColorItem { get; private set; }

	public void Init(IPickColorItem pickColorItem)
	{
		this.pickColorItem = pickColorItem;
		this.image.color = pickColorItem.Color;
	}

	/// <summary>
	/// choose color in pick color panel, invoke set color action, close panel
	/// </summary>
	public void OnPointerClick(PointerEventData eventData)
	{
		PickColorItemComponentPick.Invoke(this);
	}
}
