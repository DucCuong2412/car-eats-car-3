using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Smokoko.DebugModule
{
	public class InputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		private bool pointerDown;

		private UnityAction pointerUpAction;

		private UnityAction pointerDownAction;

		public void SetPointerUpAction(UnityAction _pointerUpAction)
		{
			pointerUpAction = _pointerUpAction;
		}

		public void SetPointerDownAction(UnityAction _pointerDownAction)
		{
			pointerDownAction = _pointerDownAction;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			pointerDown = true;
			if (pointerDownAction != null)
			{
				pointerDownAction();
			}
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (pointerDown)
			{
				pointerDown = false;
				if (pointerUpAction != null)
				{
					pointerUpAction();
				}
			}
		}
	}
}
