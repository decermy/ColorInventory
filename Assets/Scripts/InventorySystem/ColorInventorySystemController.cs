using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ColorInventorySystemController : MonoBehaviour
{
	[Header("Prefab")]
	[SerializeField] private GameObject colorSystemItemPrefab;

	[Header("Items Panel")]
	[SerializeField] private Transform inventoryPanel;

	[Header("Buttons")]
	[SerializeField] private GameObject AddButton;
	[SerializeField] private GameObject ModifyButton;
	[SerializeField] private GameObject RemoveButton;

	public bool Initialized { get; private set; }

	public IColorInventorySystem colorInventorySystem { get; private set; }

	private ColorInventoryItem selectedColorInventoryItem;
	public ColorInventoryItem SelectedColorInventoryItem
	{
		get
		{
			return selectedColorInventoryItem;
		}
		set
		{
			//remove last selection
			if (selectedColorInventoryItem != null)
			{
				selectedColorInventoryItem.colorInventoryItemComponent.Select(false);
			}

			ColorInventoryItem lastSelectedItem = selectedColorInventoryItem;
			selectedColorInventoryItem = value;

			if (lastSelectedItem == selectedColorInventoryItem)
			{
				selectedColorInventoryItem = null;
			}

			if (selectedColorInventoryItem == null)
			{
				ModifyButton.SetActive(false);
				RemoveButton.SetActive(false);
			}
			else
			{
				ModifyButton.SetActive(true);
				RemoveButton.SetActive(true);

				//remove set new selection
				selectedColorInventoryItem.colorInventoryItemComponent.Select(true);
			}
		}
	}

	private void OnEnable()
	{
		ColorInventoryItemComponent.SelectItem += OnColorInventoryItemComponentSelect;
	}

	private void OnDisable()
	{
		ColorInventoryItemComponent.SelectItem -= OnColorInventoryItemComponentSelect;
	}

	public void Init(IColorInventorySystem colorInventorySystem)
	{
		ModifyButton.SetActive(false);
		RemoveButton.SetActive(false);

		this.colorInventorySystem = colorInventorySystem;

		Initialized = true;
	}

	public void Clear()
	{
		colorInventorySystem.ClearData();
	}

	public void Load()
	{
		colorInventorySystem.LoadData();
		InitColorInventorySystemItems();
	}

	private void InitColorInventorySystemItems()
	{
		colorInventorySystem.InitItems(colorSystemItemPrefab, inventoryPanel);
	}

	private void OnColorInventoryItemComponentSelect(ColorInventoryItemComponent colorInventoryItemComponent)
	{
		SelectedColorInventoryItem = colorInventoryItemComponent.colorInventoryItem;
	}

	/// <summary>
	/// use through button
	/// </summary>
	public void OnAdd(PickColorController pickColorController)
	{
		if (!Initialized)
		{
			Debug.LogError("Need Init");
			return;
		}

		pickColorController.ShowPanel(true, OnSetColorAndCreate);
	}

	/// <summary>
	/// use through button
	/// </summary>
	public void OnModify(PickColorController pickColorController)
	{
		if (!Initialized)
		{
			Debug.LogError("Need Init");
			return;
		}

		if (SelectedColorInventoryItem == null)
		{
			return;
		}

		pickColorController.ShowPanel(true, OnSetColor);
	}

	/// <summary>
	/// use through button
	/// </summary>
	public void OnRemove()
	{
		if (!Initialized)
		{
			Debug.LogError("Need Init");
			return;
		}

		if (SelectedColorInventoryItem == null)
		{
			return;
		}

		colorInventorySystem.Remove(SelectedColorInventoryItem);
		SelectedColorInventoryItem = null;
	}

	/// <summary>
	/// use through action, modify selected color inventory item color
	/// </summary>
	private void OnSetColor(Color color)
	{
		if (!Initialized)
		{
			Debug.LogError("Need Init");
			return;
		}

		colorInventorySystem.Modify(SelectedColorInventoryItem, color);
	}

	/// <summary>
	/// use through action, when user opens pickColorPanel and choose color, create new item and add into data collection
	/// </summary>
	private void OnSetColorAndCreate(Color color)
	{
		if (!Initialized)
		{
			Debug.LogError("Need Init");
			return;
		}

		var colorSystemItem = new ColorInventoryItem(color);
		colorSystemItem.Init(colorSystemItemPrefab, inventoryPanel);

		colorInventorySystem.Add(colorSystemItem);

		SelectedColorInventoryItem = colorSystemItem;
		SelectedColorInventoryItem.colorInventoryItemComponent.Select(true);
	}

	private void OnApplicationQuit()
	{
		if (!Initialized)
		{
			Debug.LogError("Need Init");
			return;
		}

		colorInventorySystem.SaveData();
	}
}
