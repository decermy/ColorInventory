using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{

	[SerializeField] private ColorInventorySystemController colorInventorySystemController;
	[SerializeField] private PickColorController pickColorController;

	void Start()
	{
		ColorInventorySystemData colorInventorySystemData = new ColorInventorySystemData();
		IColorInventorySystem colorInventorySystem = new ColorInventorySystem(colorInventorySystemData);

		colorInventorySystemController.Init(colorInventorySystem);
		colorInventorySystemController.Clear();

		colorInventorySystemController.Load();

		IPickColorPanel pickColorPanel = new PickColorPanel();

		pickColorController.Init(colorInventorySystem, pickColorPanel);
	}
}
