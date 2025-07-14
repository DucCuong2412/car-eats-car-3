using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;

namespace CompleteProject
{
	public class Purchaser : MonoBehaviour, IStoreListener
	{
		[HideInInspector]
		public static IStoreController m_StoreController;

		private static IExtensionProvider m_StoreExtensionProvider;

		public static string Rubies1 = "com.smokoko.careatscar3.rubies1";

		public static string Rubies2 = "com.smokoko.careatscar3.rubies2";

		public static string Rubies3 = "com.smokoko.careatscar3.rubies3";

		public static string Rubies4 = "com.smokoko.careatscar3.rubies4";

		public static string UnlockNext = "com.smokoko.careatscar3.alllevels";

		public static string UnlockWorld1 = "com.smokoko.careatscar3.world1";

		public static string UnlockWorld2 = "com.smokoko.careatscar3.world2";

		public static string UnlockWorld3 = "com.smokoko.careatscar3.world3";

		public static string UnlockWorldUnder = "com.smokoko.careatscar3.world4";

		public static string UnlockWorldUnder2 = "com.smokoko.careatscar3.world5";

		public static string UnlimitedFuel = "com.smokoko.careatscar3.unlimited_fuel";

		public static string FuelUpTank = "com.smokoko.careatscar3.tankupgrade";

		public static string FuelAddMore = "com.smokoko.careatscar3.fueltruck";

		public static string DroneBee = "com.smokoko.careatscar3.premiumdrone";

		public static string DroneBomber = "com.smokoko.careatscar3.drone";

		public static string PremiumCar = "com.smokoko.careatscar3.premiumcar";

		public static string PremiumCar2 = "com.smokoko.careatscar3.jason";

		public static string PremiumCar3 = "com.smokoko.careatscar3.beetlee";

		public static string Megapack = "com.smokoko.careatscar3.megapack";

		public static string Carpark = "com.smokoko.careatscar3.carpark";

		public static string Carpark2 = "com.smokoko.careatscar3.carpark2";

		public static string PremiumCar4 = "com.smokoko.careatscar3.francopstein";

		public static string PremiumCar5 = "com.smokoko.careatscar3.carocop";

		public static string PremiumCar6 = "com.smokoko.careatscar3.rabbitster";

		public static string Tankominator = "com.smokoko.careatscar3.tankominator";

		public static string Croco = "com.smokoko.careatscar3.alligator";

		public static string Cherepaha = "com.smokoko.careatscar3.turtle";

		public static string PremiumCar9 = "com.smokoko.careatscar3.bug1";

		public static string PremiumCar10 = "com.smokoko.careatscar3.bug2";

		public static Action<int> buyRubies1;

		public static Action<int> buyRubies2;

		public static Action<int> buyRubies3;

		public static Action<int> buyRubies4;

		public static Action buyUnlockNext;

		public static Action buyUnlockWorld1;

		public static Action buyUnlockWorld2;

		public static Action buyUnlockWorld3;

		public static Action buyUnlockWorldUnder;

		public static Action buyUnlockWorldUnder2;

		public static Action buyUnlimitedFuel;

		public static Action buyFuelUpTank;

		public static Action buyFuelAddMore;

		public static Action buyMegapack;

		public static Action buyDroneBee;

		public static Action buyDroneBomber;

		public static Action buyPremiumCar;

		public static Action buyPremiumCar2;

		public static Action buyPremiumCar3;

		public static Action buyPremiumCar4;

		public static Action buyPremiumCar5;

		public static Action buyPremiumCar6;

		public static Action buyPremiumCar9;

		public static Action buyPremiumCar10;

		public static Action buyTankominator;

		public static Action buyCarpack;

		public static Action buyCarpack2;

		public static Action buyCherepaha;

		public static Action buyCroco;

		public static Action<bool> ForRestoreIos;

		private CrossPlatformValidator validator;

		private void Start()
		{
			AllActionNull();
			if (m_StoreController == null)
			{
				InitializePurchasing();
				UnityEngine.Object.DontDestroyOnLoad(base.transform.gameObject);
			}
		}

		public void InitializePurchasing()
		{
			if (!IsInitialized())
			{
				ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
				configurationBuilder.AddProduct(Rubies1, ProductType.Consumable);
				configurationBuilder.AddProduct(Rubies2, ProductType.Consumable);
				configurationBuilder.AddProduct(Rubies3, ProductType.Consumable);
				configurationBuilder.AddProduct(Rubies4, ProductType.Consumable);
				configurationBuilder.AddProduct(FuelUpTank, ProductType.Consumable);
				configurationBuilder.AddProduct(FuelAddMore, ProductType.Consumable);
				configurationBuilder.AddProduct(UnlockNext, ProductType.NonConsumable);
				configurationBuilder.AddProduct(UnlockWorld1, ProductType.NonConsumable);
				configurationBuilder.AddProduct(UnlockWorld2, ProductType.NonConsumable);
				configurationBuilder.AddProduct(UnlockWorld3, ProductType.NonConsumable);
				configurationBuilder.AddProduct(UnlockWorldUnder, ProductType.NonConsumable);
				configurationBuilder.AddProduct(UnlockWorldUnder2, ProductType.NonConsumable);
				configurationBuilder.AddProduct(UnlimitedFuel, ProductType.NonConsumable);
				configurationBuilder.AddProduct(DroneBee, ProductType.NonConsumable);
				configurationBuilder.AddProduct(DroneBomber, ProductType.NonConsumable);
				configurationBuilder.AddProduct(PremiumCar, ProductType.NonConsumable);
				configurationBuilder.AddProduct(PremiumCar2, ProductType.NonConsumable);
				configurationBuilder.AddProduct(PremiumCar3, ProductType.NonConsumable);
				configurationBuilder.AddProduct(Megapack, ProductType.NonConsumable);
				configurationBuilder.AddProduct(Carpark, ProductType.NonConsumable);
				configurationBuilder.AddProduct(Carpark2, ProductType.NonConsumable);
				configurationBuilder.AddProduct(PremiumCar4, ProductType.NonConsumable);
				configurationBuilder.AddProduct(PremiumCar5, ProductType.NonConsumable);
				configurationBuilder.AddProduct(PremiumCar6, ProductType.NonConsumable);
				configurationBuilder.AddProduct(PremiumCar9, ProductType.NonConsumable);
				configurationBuilder.AddProduct(PremiumCar10, ProductType.NonConsumable);
				configurationBuilder.AddProduct(Tankominator, ProductType.NonConsumable);
				configurationBuilder.AddProduct(Cherepaha, ProductType.NonConsumable);
				configurationBuilder.AddProduct(Croco, ProductType.NonConsumable);
				UnityPurchasing.Initialize(this, configurationBuilder);
				string identifier = Application.identifier;
				validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), UnityChannelTangle.Data(), identifier);
			}
		}

		private static bool IsInitialized()
		{
			return m_StoreController != null && m_StoreExtensionProvider != null;
		}

		public static void BuyProductID(string productId, Action callBack = null, Action<int> CallBackRuby = null)
		{
			if (IsInitialized())
			{
				if (string.Equals(productId, Rubies1, StringComparison.Ordinal))
				{
					buyRubies1 = CallBackRuby;
				}
				else if (string.Equals(productId, Rubies2, StringComparison.Ordinal))
				{
					buyRubies2 = CallBackRuby;
				}
				else if (string.Equals(productId, Rubies3, StringComparison.Ordinal))
				{
					buyRubies3 = CallBackRuby;
				}
				else if (string.Equals(productId, Rubies4, StringComparison.Ordinal))
				{
					buyRubies4 = CallBackRuby;
				}
				else if (string.Equals(productId, FuelUpTank, StringComparison.Ordinal))
				{
					buyFuelUpTank = callBack;
				}
				else if (string.Equals(productId, FuelAddMore, StringComparison.Ordinal))
				{
					buyFuelAddMore = callBack;
				}
				else if (string.Equals(productId, UnlockNext, StringComparison.Ordinal))
				{
					buyUnlockNext = callBack;
				}
				else if (string.Equals(productId, UnlockWorld1, StringComparison.Ordinal))
				{
					buyUnlockWorld1 = callBack;
				}
				else if (string.Equals(productId, UnlockWorld2, StringComparison.Ordinal))
				{
					buyUnlockWorld2 = callBack;
				}
				else if (string.Equals(productId, UnlockWorld3, StringComparison.Ordinal))
				{
					buyUnlockWorld3 = callBack;
				}
				else if (string.Equals(productId, UnlockWorldUnder, StringComparison.Ordinal))
				{
					buyUnlockWorldUnder = callBack;
				}
				else if (string.Equals(productId, UnlockWorldUnder2, StringComparison.Ordinal))
				{
					buyUnlockWorldUnder2 = callBack;
				}
				else if (string.Equals(productId, UnlimitedFuel, StringComparison.Ordinal))
				{
					buyUnlimitedFuel = callBack;
				}
				else if (string.Equals(productId, DroneBomber, StringComparison.Ordinal))
				{
					buyDroneBomber = callBack;
				}
				else if (string.Equals(productId, DroneBee, StringComparison.Ordinal))
				{
					buyDroneBee = callBack;
				}
				else if (string.Equals(productId, PremiumCar, StringComparison.Ordinal))
				{
					buyPremiumCar = callBack;
				}
				else if (string.Equals(productId, PremiumCar2, StringComparison.Ordinal))
				{
					buyPremiumCar2 = callBack;
				}
				else if (string.Equals(productId, PremiumCar3, StringComparison.Ordinal))
				{
					buyPremiumCar3 = callBack;
				}
				else if (string.Equals(productId, Megapack, StringComparison.Ordinal))
				{
					buyMegapack = callBack;
				}
				else if (string.Equals(productId, Carpark, StringComparison.Ordinal))
				{
					buyCarpack = callBack;
				}
				else if (string.Equals(productId, Carpark2, StringComparison.Ordinal))
				{
					buyCarpack2 = callBack;
				}
				else if (string.Equals(productId, PremiumCar4, StringComparison.Ordinal))
				{
					buyPremiumCar4 = callBack;
				}
				else if (string.Equals(productId, PremiumCar5, StringComparison.Ordinal))
				{
					buyPremiumCar5 = callBack;
				}
				else if (string.Equals(productId, PremiumCar6, StringComparison.Ordinal))
				{
					buyPremiumCar6 = callBack;
				}
				else if (string.Equals(productId, PremiumCar9, StringComparison.Ordinal))
				{
					buyPremiumCar9 = callBack;
				}
				else if (string.Equals(productId, PremiumCar10, StringComparison.Ordinal))
				{
					buyPremiumCar10 = callBack;
				}
				else if (string.Equals(productId, Tankominator, StringComparison.Ordinal))
				{
					buyTankominator = callBack;
				}
				else if (string.Equals(productId, Cherepaha, StringComparison.Ordinal))
				{
					buyCherepaha = callBack;
				}
				else if (string.Equals(productId, Croco, StringComparison.Ordinal))
				{
					buyCroco = callBack;
				}
				Product product = m_StoreController.products.WithID(productId);
				if (product != null && product.availableToPurchase)
				{
					UnityEngine.Debug.Log($"Purchasing product asychronously: '{product.definition.id}'");
					m_StoreController.InitiatePurchase(product);
				}
				else
				{
					UnityEngine.Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				}
			}
			else
			{
				UnityEngine.Debug.Log("BuyProductID FAIL. Not initialized.");
			}
		}

		public static void RestorePurchases()
		{
			if (!IsInitialized())
			{
				UnityEngine.Debug.Log("RestorePurchases FAIL. Not initialized.");
			}
			else if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
			{
				UnityEngine.Debug.Log("RestorePurchases started ...");
				IAppleExtensions extension = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
				extension.RestoreTransactions(delegate(bool result)
				{
					ForRestoreIos(result);
					UnityEngine.Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
				});
			}
			else
			{
				UnityEngine.Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
			}
		}

		public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
		{
			UnityEngine.Debug.Log("OnInitialized: PASS");
			m_StoreController = controller;
			m_StoreExtensionProvider = extensions;
		}

		public void OnInitializeFailed(InitializationFailureReason error)
		{
			UnityEngine.Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
		}

		private PurchaseProcessingResult ProcessFinalPurchase(PurchaseEventArgs args)
		{
			if (string.Equals(args.purchasedProduct.definition.id, Rubies1, StringComparison.Ordinal))
			{
				if (buyRubies1 != null)
				{
					buyRubies1(2);
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Rubies2, StringComparison.Ordinal))
			{
				if (buyRubies2 != null)
				{
					buyRubies2(3);
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Rubies3, StringComparison.Ordinal))
			{
				if (buyRubies3 != null)
				{
					buyRubies3(4);
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Rubies4, StringComparison.Ordinal))
			{
				if (buyRubies4 != null)
				{
					buyRubies4(5);
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, FuelUpTank, StringComparison.Ordinal))
			{
				if (buyFuelUpTank != null)
				{
					buyFuelUpTank();
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, FuelAddMore, StringComparison.Ordinal))
			{
				if (buyFuelAddMore != null)
				{
					buyFuelAddMore();
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, UnlockNext, StringComparison.Ordinal))
			{
				if (buyUnlockNext != null)
				{
					buyUnlockNext();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					for (int i = 1; i <= 3; i++)
					{
						for (int j = 1; j <= 12; j++)
						{
							if (!Progress.levels.Pack(i).Level(j).isOpen)
							{
								Progress.levels.Pack(i).Level(j).isOpen = true;
							}
						}
					}
					for (int k = 1; k <= 3; k++)
					{
						for (int l = 1; l <= 12; l++)
						{
							if (!Progress.levels._packUnderground[k].Level(l).isOpen)
							{
								Progress.levels._packUnderground[k].Level(l).isOpen = true;
							}
						}
					}
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, UnlockWorld1, StringComparison.Ordinal))
			{
				if (buyUnlockWorld1 != null)
				{
					buyUnlockWorld1();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					for (int m = 1; m <= 12; m++)
					{
						if (!Progress.levels.Pack(1).Level(m).isOpen)
						{
							Progress.levels.Pack(1).Level(m).isOpen = true;
						}
					}
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, UnlockWorld2, StringComparison.Ordinal))
			{
				if (buyUnlockWorld2 != null)
				{
					buyUnlockWorld2();
				}
				else
				{
					for (int n = 1; n <= 12; n++)
					{
						if (!Progress.levels.Pack(2).Level(n).isOpen)
						{
							Progress.levels.Pack(2).Level(n).isOpen = true;
						}
					}
					Progress.shop.BuyForRealMoney = true;
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, UnlockWorld3, StringComparison.Ordinal))
			{
				if (buyUnlockWorld3 != null)
				{
					buyUnlockWorld3();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					for (int num = 1; num <= 12; num++)
					{
						if (!Progress.levels.Pack(3).Level(num).isOpen)
						{
							Progress.levels.Pack(3).Level(num).isOpen = true;
						}
					}
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, UnlockWorldUnder, StringComparison.Ordinal))
			{
				if (buyUnlockWorldUnder != null)
				{
					buyUnlockWorldUnder();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					for (int num2 = 1; num2 <= 12; num2++)
					{
						if (!Progress.levels._packUnderground[1].Level(num2).isOpen)
						{
							Progress.levels._packUnderground[1].Level(num2).isOpen = true;
						}
					}
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, UnlockWorldUnder2, StringComparison.Ordinal))
			{
				if (buyUnlockWorldUnder2 != null)
				{
					buyUnlockWorldUnder2();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					for (int num3 = 1; num3 <= 12; num3++)
					{
						if (!Progress.levels._packUnderground[2].Level(num3).isOpen)
						{
							Progress.levels._packUnderground[2].Level(num3).isOpen = true;
						}
					}
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, UnlimitedFuel, StringComparison.Ordinal))
			{
				if (buyUnlimitedFuel != null)
				{
					buyUnlimitedFuel();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					Progress.gameEnergy.isInfinite = true;
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, DroneBomber, StringComparison.Ordinal))
			{
				if (buyDroneBomber != null)
				{
					buyDroneBomber();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					Progress.shop.dronBombsBuy = true;
					Progress.shop.dronBombsActive = true;
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, DroneBee, StringComparison.Ordinal))
			{
				if (buyDroneBee != null)
				{
					buyDroneBee();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					Progress.shop.dronBeeBuy = true;
					Progress.shop.dronBeeActive = true;
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, PremiumCar, StringComparison.Ordinal))
			{
				if (buyPremiumCar != null)
				{
					buyPremiumCar();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					Progress.shop.Cars[3].equipped = true;
					Progress.shop.Cars[3].bought = true;
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, PremiumCar2, StringComparison.Ordinal))
			{
				if (buyPremiumCar2 != null)
				{
					buyPremiumCar2();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					Progress.shop.Cars[4].equipped = true;
					Progress.shop.Cars[4].bought = true;
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Megapack, StringComparison.Ordinal))
			{
				if (buyMegapack != null)
				{
					buyMegapack();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					Progress.shop.dronBeeBuy = true;
					Progress.shop.dronBeeActive = true;
					Progress.shop.Cars[3].equipped = true;
					Progress.shop.Cars[3].bought = true;
					Progress.gameEnergy.isInfinite = true;
					Progress.review.atLeastOnePurchase = true;
					Progress.shop.BuyLimittedOffer = true;
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Carpark, StringComparison.Ordinal))
			{
				if (buyCarpack != null)
				{
					buyCarpack();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					Progress.shop.Cars[3].equipped = true;
					Progress.shop.Cars[3].bought = true;
					Progress.shop.Cars[4].equipped = true;
					Progress.shop.Cars[4].bought = true;
					Progress.shop.Cars[5].equipped = true;
					Progress.shop.Cars[5].bought = true;
					Progress.shop.Cars[6].equipped = true;
					Progress.shop.Cars[6].bought = true;
					Progress.shop.Get1partForPoliceCar = true;
					Progress.shop.Get2partForPoliceCar = true;
					Progress.shop.Get3partForPoliceCar = true;
					Progress.shop.Get4partForPoliceCar = true;
					Progress.shop.CollKill1Car = 120;
					Progress.shop.CollKill2Car = 120;
					Progress.shop.CollKill3Car = 120;
					Progress.shop.CollKill4Car = 120;
					Progress.shop.BuyLimittedOffer = true;
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Carpark2, StringComparison.Ordinal))
			{
				if (buyCarpack2 != null)
				{
					buyCarpack2();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					Progress.shop.Cars[7].equipped = true;
					Progress.shop.Cars[7].bought = true;
					Progress.shop.Cars[8].equipped = true;
					Progress.shop.Cars[8].bought = true;
					Progress.shop.Cars[9].equipped = true;
					Progress.shop.Cars[9].bought = true;
					Progress.shop.Cars[10].equipped = true;
					Progress.shop.Cars[10].bought = true;
					Progress.shop.Get1partForPoliceCar2 = true;
					Progress.shop.Get2partForPoliceCar2 = true;
					Progress.shop.Get3partForPoliceCar2 = true;
					Progress.shop.Get4partForPoliceCar2 = true;
					Progress.shop.CollKill1Car2 = 1200;
					Progress.shop.CollKill2Car2 = 1200;
					Progress.shop.CollKill3Car2 = 1200;
					Progress.shop.CollKill4Car2 = 1200;
					Progress.shop.BuyLimittedOffer = true;
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, PremiumCar3, StringComparison.Ordinal))
			{
				if (buyPremiumCar3 != null)
				{
					buyPremiumCar3();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					Progress.shop.Cars[5].equipped = true;
					Progress.shop.Cars[5].bought = true;
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, PremiumCar4, StringComparison.Ordinal))
			{
				if (buyPremiumCar4 != null)
				{
					buyPremiumCar4();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					Progress.shop.Cars[6].equipped = true;
					Progress.shop.Cars[6].bought = true;
					Progress.shop.Get1partForPoliceCar = true;
					Progress.shop.Get2partForPoliceCar = true;
					Progress.shop.Get3partForPoliceCar = true;
					Progress.shop.Get4partForPoliceCar = true;
					Progress.shop.CollKill1Car = 120;
					Progress.shop.CollKill2Car = 120;
					Progress.shop.CollKill3Car = 120;
					Progress.shop.CollKill4Car = 120;
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, PremiumCar5, StringComparison.Ordinal))
			{
				if (buyPremiumCar5 != null)
				{
					buyPremiumCar5();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					Progress.shop.Cars[7].equipped = true;
					Progress.shop.Cars[7].bought = true;
					Progress.shop.Get1partForPoliceCar2 = true;
					Progress.shop.Get2partForPoliceCar2 = true;
					Progress.shop.Get3partForPoliceCar2 = true;
					Progress.shop.Get4partForPoliceCar2 = true;
					Progress.shop.CollKill1Car2 = 500;
					Progress.shop.CollKill2Car2 = 500;
					Progress.shop.CollKill3Car2 = 500;
					Progress.shop.CollKill4Car2 = 500;
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, PremiumCar6, StringComparison.Ordinal))
			{
				if (buyPremiumCar6 != null)
				{
					buyPremiumCar6();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					Progress.shop.Cars[8].equipped = true;
					Progress.shop.Cars[8].bought = true;
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, PremiumCar9, StringComparison.Ordinal))
			{
				if (buyPremiumCar9 != null)
				{
					buyPremiumCar9();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					Progress.shop.Cars[9].equipped = true;
					Progress.shop.Cars[9].bought = true;
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, PremiumCar10, StringComparison.Ordinal))
			{
				if (buyPremiumCar10 != null)
				{
					buyPremiumCar10();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					Progress.shop.Cars[10].equipped = true;
					Progress.shop.Cars[10].bought = true;
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Cherepaha, StringComparison.Ordinal))
			{
				if (buyCherepaha != null)
				{
					buyCherepaha();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					Progress.shop.Cars[13].equipped = true;
					Progress.shop.Cars[13].bought = true;
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Croco, StringComparison.Ordinal))
			{
				if (buyCroco != null)
				{
					buyCroco();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					Progress.shop.Cars[12].equipped = true;
					Progress.shop.Cars[12].bought = true;
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Tankominator, StringComparison.Ordinal))
			{
				if (buyTankominator != null)
				{
					buyTankominator();
				}
				else
				{
					Progress.shop.BuyForRealMoney = true;
					Progress.shop.Cars[11].equipped = true;
					Progress.shop.Cars[11].bought = true;
					Progress.shop.Get1partForPoliceCar3 = true;
					Progress.shop.Get2partForPoliceCar3 = true;
					Progress.shop.Get3partForPoliceCar3 = true;
					Progress.shop.Get4partForPoliceCar3 = true;
					Progress.shop.CollKill1Car3 = 1500;
					Progress.shop.CollKill2Car3 = 1500;
					Progress.shop.CollKill3Car3 = 1500;
					Progress.shop.CollKill4Car3 = 1500;
				}
				UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
			}
			else
			{
				UnityEngine.Debug.Log($"ProcessPurchase: FAIL. Unrecognized product: '{args.purchasedProduct.definition.id}'");
			}
			AllActionNull();
			return PurchaseProcessingResult.Complete;
		}

		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
		{
			try
			{
				IPurchaseReceipt[] array = validator.Validate(args.purchasedProduct.receipt);
				UnityEngine.Debug.Log("Receipt is valid. Contents:");
				IPurchaseReceipt[] array2 = array;
				foreach (IPurchaseReceipt purchaseReceipt in array2)
				{
					UnityEngine.Debug.Log(purchaseReceipt.productID);
					UnityEngine.Debug.Log(purchaseReceipt.purchaseDate);
					UnityEngine.Debug.Log(purchaseReceipt.transactionID);
					GooglePlayReceipt googlePlayReceipt = purchaseReceipt as GooglePlayReceipt;
					if (googlePlayReceipt != null)
					{
						UnityEngine.Debug.Log(googlePlayReceipt.purchaseState);
						UnityEngine.Debug.Log(googlePlayReceipt.purchaseToken);
					}
					UnityChannelReceipt unityChannelReceipt = purchaseReceipt as UnityChannelReceipt;
					if (unityChannelReceipt != null)
					{
						UnityEngine.Debug.Log(unityChannelReceipt.productID);
						UnityEngine.Debug.Log(unityChannelReceipt.purchaseDate);
						UnityEngine.Debug.Log(unityChannelReceipt.transactionID);
					}
					AppleInAppPurchaseReceipt appleInAppPurchaseReceipt = purchaseReceipt as AppleInAppPurchaseReceipt;
					if (appleInAppPurchaseReceipt != null)
					{
						UnityEngine.Debug.Log(appleInAppPurchaseReceipt.originalTransactionIdentifier);
						UnityEngine.Debug.Log(appleInAppPurchaseReceipt.subscriptionExpirationDate);
						UnityEngine.Debug.Log(appleInAppPurchaseReceipt.cancellationDate);
						UnityEngine.Debug.Log(appleInAppPurchaseReceipt.quantity);
					}
				}
				UnityEngine.Debug.Log("Purchase complete");
			}
			catch (IAPSecurityException arg)
			{
				UnityEngine.Debug.Log("Invalid receipt, not unlocking content. " + arg);
				return PurchaseProcessingResult.Complete;
			}
			return ProcessFinalPurchase(args);
		}

		public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
		{
			UnityEngine.Debug.Log($"OnPurchaseFailed: FAIL. Product: '{product.definition.storeSpecificId}', PurchaseFailureReason: {failureReason}");
			if (failureReason == PurchaseFailureReason.DuplicateTransaction)
			{
				if (string.Equals(product.definition.id, UnlockNext, StringComparison.Ordinal))
				{
					if (buyUnlockNext != null)
					{
						buyUnlockNext();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, UnlockWorld1, StringComparison.Ordinal))
				{
					if (buyUnlockWorld1 != null)
					{
						buyUnlockWorld1();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, UnlockWorld2, StringComparison.Ordinal))
				{
					if (buyUnlockWorld2 != null)
					{
						buyUnlockWorld2();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, UnlockWorld3, StringComparison.Ordinal))
				{
					if (buyUnlockWorld3 != null)
					{
						buyUnlockWorld3();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, UnlockWorldUnder, StringComparison.Ordinal))
				{
					if (buyUnlockWorldUnder != null)
					{
						buyUnlockWorldUnder();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, UnlockWorldUnder2, StringComparison.Ordinal))
				{
					if (buyUnlockWorldUnder2 != null)
					{
						buyUnlockWorldUnder2();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, UnlimitedFuel, StringComparison.Ordinal))
				{
					if (buyUnlimitedFuel != null)
					{
						buyUnlimitedFuel();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, DroneBomber, StringComparison.Ordinal))
				{
					if (buyDroneBomber != null)
					{
						buyDroneBomber();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, DroneBee, StringComparison.Ordinal))
				{
					if (buyDroneBee != null)
					{
						buyDroneBee();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, PremiumCar, StringComparison.Ordinal))
				{
					if (buyPremiumCar != null)
					{
						buyPremiumCar();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, PremiumCar2, StringComparison.Ordinal))
				{
					if (buyPremiumCar2 != null)
					{
						buyPremiumCar2();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, PremiumCar3, StringComparison.Ordinal))
				{
					if (buyPremiumCar3 != null)
					{
						buyPremiumCar3();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, PremiumCar9, StringComparison.Ordinal))
				{
					if (buyPremiumCar9 != null)
					{
						buyPremiumCar9();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, PremiumCar10, StringComparison.Ordinal))
				{
					if (buyPremiumCar10 != null)
					{
						buyPremiumCar10();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, PremiumCar4, StringComparison.Ordinal))
				{
					if (buyPremiumCar4 != null)
					{
						buyPremiumCar4();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, PremiumCar5, StringComparison.Ordinal))
				{
					if (buyPremiumCar5 != null)
					{
						buyPremiumCar5();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, PremiumCar6, StringComparison.Ordinal))
				{
					if (buyPremiumCar6 != null)
					{
						buyPremiumCar6();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, Megapack, StringComparison.Ordinal))
				{
					if (buyMegapack != null)
					{
						buyMegapack();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, Carpark, StringComparison.Ordinal))
				{
					if (buyCarpack != null)
					{
						buyCarpack();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, Carpark2, StringComparison.Ordinal))
				{
					if (buyCarpack2 != null)
					{
						buyCarpack2();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, Tankominator, StringComparison.Ordinal))
				{
					if (buyTankominator != null)
					{
						buyTankominator();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, Croco, StringComparison.Ordinal))
				{
					if (buyCroco != null)
					{
						buyCroco();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else if (string.Equals(product.definition.id, Cherepaha, StringComparison.Ordinal))
				{
					if (buyCherepaha != null)
					{
						buyCherepaha();
					}
					UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
				}
				else
				{
					UnityEngine.Debug.Log($"ProcessPurchase: FAIL. Unrecognized product: '{product.definition.id}'");
				}
			}
			AllActionNull();
		}

		public static void AllActionNull()
		{
			buyUnlockNext = null;
			buyUnlockWorld1 = null;
			buyUnlockWorld2 = null;
			buyUnlockWorld3 = null;
			buyUnlockWorldUnder = null;
			buyUnlimitedFuel = null;
			buyFuelUpTank = null;
			buyFuelAddMore = null;
			buyMegapack = null;
			buyDroneBee = null;
			buyDroneBomber = null;
			buyPremiumCar = null;
			buyPremiumCar2 = null;
			buyPremiumCar3 = null;
			buyPremiumCar4 = null;
			buyPremiumCar5 = null;
			buyPremiumCar6 = null;
			buyPremiumCar9 = null;
			buyPremiumCar10 = null;
			buyCarpack = null;
			buyCherepaha = null;
			buyCroco = null;
		}
	}
}
