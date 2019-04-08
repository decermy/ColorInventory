using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[Serializable]
public class ColorInventoryItem : IColorInventoryItem
{
	public Color color;

	[NonSerialized]
	public ColorInventoryItemComponent colorInventoryItemComponent;

	public ColorInventoryItem(Color color)
	{
		this.color = color;
	}
	public void Init(GameObject prefab, Transform parent)
	{
		GameObject colorSystemItemGameobject = GameObject.Instantiate(prefab);
		colorInventoryItemComponent = colorSystemItemGameobject.GetComponent<ColorInventoryItemComponent>();
		colorInventoryItemComponent.Init(this);

		colorInventoryItemComponent.transform.SetParent(parent);

		colorInventoryItemComponent.image.color = color;
	}

	/// <summary>
	/// destroy gameobject
	/// </summary>
	public void SelfDestroy()
	{
		if (colorInventoryItemComponent == null)
		{
			return;
		}

		colorInventoryItemComponent.transform.SetParent(null);

		GameObject.Destroy(colorInventoryItemComponent.gameObject);
	}

	/// <summary>
	/// set color and image color
	/// </summary>
	public void SetColor(Color color)
	{
		if (colorInventoryItemComponent == null)
		{
			Debug.LogError("colorInventoryItemComponent is null");
			return;
		}

		this.color = color;
		this.colorInventoryItemComponent.image.color = color;
	}
}
