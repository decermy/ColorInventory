Color Config in Assets/Configs/ColorConfig

Example Code Access:
	
		ColorInventorySystemData colorInventorySystemData = new ColorInventorySystemData();
		IColorInventorySystem colorInventorySystem = new ColorInventorySystem(colorInventorySystemData);
		colorInventorySystem.ClearData();
		colorInventorySystem.LoadData();

		var itemList = colorInventorySystem.GetColorInventoryItems();

		if (itemList.Count > 0)
		{
			var item = itemList[0];
			Debug.Log(itemList[0].color);
			colorInventorySystem.Remove(item);
		}

		var newItem = new ColorInventoryItem(Color.blue);

		colorInventorySystem.Add(newItem);
		colorInventorySystem.Modify(newItem, Color.red);
		colorInventorySystem.Remove(newItem);

		colorInventorySystem.SaveData();