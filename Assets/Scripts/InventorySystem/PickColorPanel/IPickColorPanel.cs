using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickColorPanel
{
	void GenerateItems(Transform panel, GameObject prefab, Color[] validColors);

	Color[] GetNonDublicateColors(Color[] colorPool, IEnumerable<ColorInventoryItem> colorInventoryItemCollection);
}
