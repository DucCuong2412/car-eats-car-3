using System;
using UnityEngine;

namespace SA.IOSNative.StoreKit
{
	[Serializable]
	public class Product
	{
		public bool IsOpen = true;

		[SerializeField]
		private bool _IsAvailable;

		[SerializeField]
		private string _Id = string.Empty;

		[SerializeField]
		private string _DisplayName = "New Product";

		[SerializeField]
		private string _Description;

		[SerializeField]
		private float _Price = 0.99f;

		[SerializeField]
		private string _LocalizedPrice = string.Empty;

		[SerializeField]
		private string _CurrencySymbol = "$";

		[SerializeField]
		private string _CurrencyCode = "USD";

		[SerializeField]
		private Texture2D _Texture;

		[SerializeField]
		private ProductType _ProductType;

		[SerializeField]
		private PriceTier _PriceTier;

		public string Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id = value;
			}
		}

		public string DisplayName
		{
			get
			{
				return _DisplayName;
			}
			set
			{
				_DisplayName = value;
			}
		}

		public string Description
		{
			get
			{
				return _Description;
			}
			set
			{
				_Description = value;
			}
		}

		public ProductType Type
		{
			get
			{
				return _ProductType;
			}
			set
			{
				_ProductType = value;
			}
		}

		public float Price
		{
			get
			{
				return _Price;
			}
			set
			{
				_Price = value;
			}
		}

		public int PriceInMicros => Convert.ToInt32(_Price * 1000000f);

		public string LocalizedPrice
		{
			get
			{
				if (_LocalizedPrice.Equals(string.Empty))
				{
					return Price + " " + _CurrencySymbol;
				}
				return _LocalizedPrice;
			}
			set
			{
				_LocalizedPrice = value;
			}
		}

		public string CurrencySymbol
		{
			get
			{
				return _CurrencySymbol;
			}
			set
			{
				_CurrencySymbol = value;
			}
		}

		public string CurrencyCode
		{
			get
			{
				return _CurrencyCode;
			}
			set
			{
				_CurrencyCode = value;
			}
		}

		public Texture2D Texture
		{
			get
			{
				return _Texture;
			}
			set
			{
				_Texture = value;
			}
		}

		public PriceTier PriceTier
		{
			get
			{
				return _PriceTier;
			}
			set
			{
				_PriceTier = value;
			}
		}

		public bool IsAvailable
		{
			get
			{
				return _IsAvailable;
			}
			set
			{
				_IsAvailable = value;
			}
		}

		public void UpdatePriceByTier()
		{
			int priceTier = (int)_PriceTier;
			float num = _Price = (float)priceTier + 1f - 0.01f;
		}
	}
}
