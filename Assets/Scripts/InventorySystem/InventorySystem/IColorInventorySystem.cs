using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColorInventorySystem : IInventorySystem, ISaveable
{
	void Modify(ColorInventoryItem colorInventoryItem, Color color);

	void InitItems(GameObject prefab, Transform parent);

	List<ColorInventoryItem> GetColorInventoryItems();
}
