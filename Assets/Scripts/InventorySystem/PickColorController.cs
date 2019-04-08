using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickColorController : MonoBehaviour
{
	private Action<Color> SetPickedColorAction = delegate { };

	[Header("Panel Model")]
	[SerializeField] private GameObject pickColorPanelGameObject;

	[Header("Config")]
	[SerializeField] private ColorConfig colorConfig;
	[Header("Item Prefab")]
	[SerializeField] private GameObject pickColorItemPrefab;
	[Header("Items Panel")]
	[SerializeField] private Transform itemPanel;

	private IPickColorPanel pickColorPanel;
	private IColorInventorySystem colorInventorySystem;

	public bool Initialized { get; private set; }

	private void OnEnable()
	{
		PickColorItemComponent.PickColorItemComponentPick += OnPickColorItemComponentPick;
	}

	private void OnDisable()
	{
		PickColorItemComponent.PickColorItemComponentPick -= OnPickColorItemComponentPick;
	}

	public void Init(IColorInventorySystem colorInventorySystem, IPickColorPanel pickColorPanel)
	{
		this.colorInventorySystem = colorInventorySystem;
		this.pickColorPanel = pickColorPanel;

		Initialized = true;
	}

	/// <summary>
	/// calls through pickColorItemComponent event
	/// </summary>
	private void OnPickColorItemComponentPick(PickColorItemComponent pickColorItemComponent)
	{
		if (SetPickedColorAction == null)
		{
			return;
		}

		SetPickedColorAction.Invoke(pickColorItemComponent.pickColorItem.Color);
		SetPickedColorAction = null;
		ShowPanel(false, null);
	}

	/// <summary>
	/// open pick color panel, generate items for choose
	/// </summary>
	public void ShowPanel(bool show, Action<Color> PickedColorActionCallback)
	{
		if (!Initialized)
		{
			Debug.LogError("Need Init");
			return;
		}

		Color[] validColors = pickColorPanel.GetNonDublicateColors(colorConfig.colorsForColorInventorySystem, colorInventorySystem.GetColorInventoryItems());
		pickColorPanel.GenerateItems(itemPanel, pickColorItemPrefab, validColors);
		pickColorPanelGameObject.SetActive(show);

		this.SetPickedColorAction = PickedColorActionCallback;
	}

}
