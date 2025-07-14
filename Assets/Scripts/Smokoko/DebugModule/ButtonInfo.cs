using UnityEngine.Events;

namespace Smokoko.DebugModule
{
	public class ButtonInfo
	{
		public string Name;

		public UnityAction OnClickEvent;

		public int ID;

		public ButtonInfo(string _name, UnityAction _onClickEvent, int _id = -1)
		{
			Name = _name;
			OnClickEvent = _onClickEvent;
			ID = _id;
		}
	}
}
