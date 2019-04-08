using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickColorItem : IPickColorItem
{
	public PickColorItem(Color color)
	{
		Color = color;
	}

	public Color Color { get; private set; }
}
