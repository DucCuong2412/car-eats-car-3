namespace UnionAssets.FLE
{
	public class CEvent
	{
		private int _id;

		private string _name;

		private object _data;

		private IDispatcher _dispatcher;

		private bool _isStoped;

		private bool _isLocked;

		public object _currentTarget;

		public int id => _id;

		public string name => _name;

		public object data => _data;

		public IDispatcher target => _dispatcher;

		public IDispatcher dispatcher => _dispatcher;

		public object currentTarget => _currentTarget;

		public bool isStoped => _isStoped;

		public bool isLocked => _isLocked;

		public CEvent(int id, string name, object data, IDispatcher dispatcher)
		{
			_id = id;
			_name = name;
			_data = data;
			_dispatcher = dispatcher;
		}

		public void stopPropagation()
		{
			_isStoped = true;
		}

		public void stopImmediatePropagation()
		{
			_isStoped = true;
			_isLocked = true;
		}

		public bool canBeDisptached(object val)
		{
			if (_isLocked)
			{
				return false;
			}
			if (_isStoped)
			{
				if (_currentTarget == val)
				{
					return true;
				}
				return false;
			}
			_currentTarget = val;
			return true;
		}
	}
}
