using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

[Serializable]
public class ColorInventorySystemData : ISerializable
{
	public List<ColorInventoryItem> items = new List<ColorInventoryItem>();

	public void Serialize()
	{
		string json = JsonUtility.ToJson(this);

		Debug.Log(json);

		string path = Path.Combine(Application.persistentDataPath, "ColorInventoryData.json");

		Debug.Log(path);

		try
		{
			File.WriteAllText(path, json);
		}
		catch
		{

			Debug.LogError("Serialize fail");
		}
	}

	public object Deserialize()
	{
		string path = Path.Combine(Application.persistentDataPath, "ColorInventoryData.json");

		try
		{
			if (File.Exists(path))
			{
				string dataAsJson = File.ReadAllText(path);

				return JsonUtility.FromJson<ColorInventorySystemData>(dataAsJson); ;
			}
			else
			{
				return null;
			}
		}
		catch
		{
			Debug.LogError("Deserialize fail");
		}

		return null;
	}

	public void ClearData()
	{
		items.Clear();
	}
}
