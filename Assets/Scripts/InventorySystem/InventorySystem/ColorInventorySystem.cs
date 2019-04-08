using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorInventorySystem : IColorInventorySystem
{
	public ColorInventorySystemData colorInventorySystemData { get; private set; }

	public ColorInventorySystem(ColorInventorySystemData colorInventorySystemData)
	{
		this.colorInventorySystemData = colorInventorySystemData;
	}

	/// <summary>
	/// Add new element into data collection
	/// </summary>
	public void Add(IColorInventoryItem inventoryItem)
	{
		if (inventoryItem is ColorInventoryItem == false)
		{
			Debug.LogError("wrong type, Need 'ColorInventoryItem'");
			return;
		}

		ColorInventoryItem colorInventoryItem = (inventoryItem as ColorInventoryItem);

		colorInventorySystemData.items.Add(colorInventoryItem);
	}

	/// <summary>
	/// remove element from data collection
	/// </summary>
	public void Remove(IColorInventoryItem inventoryItem)
	{
		if (inventoryItem is ColorInventoryItem == false)
		{
			Debug.LogError("wrong type, Need 'ColorInventoryItem'");
			return;
		}

		ColorInventoryItem colorInventoryItem = (inventoryItem as ColorInventoryItem);

		if (colorInventorySystemData.items.Contains(colorInventoryItem) == false)
		{
			Debug.LogWarning("system doen't contains this item");
			return;
		}

		colorInventorySystemData.items.Remove(colorInventoryItem);
		colorInventoryItem.SelfDestroy();
	}

	/// <summary>
	/// set item color
	/// </summary>
	public void Modify(ColorInventoryItem colorInventoryItem, Color color)
	{
		colorInventoryItem.SetColor(color);
	}

	/// <summary>
	/// deserialize data, create items
	/// </summary>
	public void LoadData()
	{
		if (colorInventorySystemData == null)
		{
			Debug.LogError("colorInventorySystemData not init");
		}

		var save = colorInventorySystemData.Deserialize() as ColorInventorySystemData;

		if (save == null)
		{
			return;
		}

		if (save.items != null && save.items.Count > 0)
		{
			for (int i = 0; i < save.items.Count; i++)
			{
				Add(save.items[i]);
			}
		}
	}

	/// <summary>
	/// clear data
	/// </summary>
	public void ClearData()
	{
		if (colorInventorySystemData != null && colorInventorySystemData.items != null && colorInventorySystemData.items.Count != 0)
		{
			for (int i = 0; i < colorInventorySystemData.items.Count; i++)
			{
				colorInventorySystemData.items[i].SelfDestroy();
			}
		}

		colorInventorySystemData.ClearData();
	}

	/// <summary>
	/// serialize data and save it
	/// </summary>
	public void SaveData()
	{
		colorInventorySystemData.Serialize();
	}

	/// <summary>
	/// Initialize ColorInventoryItem and his Component
	/// </summary>
	public void InitItems(GameObject prefab, Transform parent)
	{
		var data = colorInventorySystemData;
		for (int i = 0; i < data.items.Count; i++)
		{
			data.items[i].Init(prefab, parent);
		}
	}

	/// <summary>
	/// get items from colorInventorySystemData
	/// </summary>
	public List<ColorInventoryItem> GetColorInventoryItems()
	{
		return colorInventorySystemData.items;
	}
}
