using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PickColorPanel : IPickColorPanel
{
	private List<GameObject> items = new List<GameObject>();

	/// <summary>
	/// clear items, choose non dublicated colors, create items 
	/// </summary>
	public void GenerateItems(Transform panel, GameObject prefab, Color[] validColors)
	{
		ClearItems();

		for (int i = 0; i < validColors.Length; i++)
		{
			GameObject pickColorItemGmaobject = GameObject.Instantiate(prefab) as GameObject;

			items.Add(pickColorItemGmaobject);

			pickColorItemGmaobject.transform.SetParent(panel);

			PickColorItemComponent pickColorItemComponent = pickColorItemGmaobject.GetComponent<PickColorItemComponent>();

			IPickColorItem pickColorItem = new PickColorItem(validColors[i]);
			pickColorItemComponent.Init(pickColorItem);
		}
	}

	/// <summary>
	/// clear items if has it
	/// </summary>
	private void ClearItems()
	{
		if (items == null && items.Count == 0)
		{
			return;
		}

		for (int i = 0; i < items.Count; i++)
		{
			GameObject.Destroy(items[i]);
		}
	}

	/// <summary>
	/// return non used/non dublicated colors
	/// </summary>
	public Color[] GetNonDublicateColors(Color[] colorPool, IEnumerable<ColorInventoryItem> colorInventoryItemCollection)
	{
		var dataColors = colorInventoryItemCollection.Select(x => x.color).Distinct();

		return colorPool.Except(dataColors).ToArray();
	}
}
