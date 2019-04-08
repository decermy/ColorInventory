
using UnityEngine.EventSystems;

public interface ISelectable : IPointerClickHandler
{
	void Select(bool selected);
}
