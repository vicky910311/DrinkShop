using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour,IStoreListener
{
   public static IAPManager self;

    private void Awake()
    {
        self = this;
    }
    private string Money10000 = "money_10000";
    private string Money30000 = "money_30000";
    private string Money50000 = "money_50000";
    private string Money100000 = "money_100000";
    private string RemoveADs = "remove_ads";
    private string AddStock = "add_stock";

    private static IStoreController m_StoreController;      
    private static IExtensionProvider m_StoreExtensionProvider;

    private bool bedone = false;

    public void InitializePurchasing()
     {
         if (IsInitialized())
         {
             return;
         }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(Money10000, ProductType.Consumable);
        builder.AddProduct(Money30000, ProductType.Consumable);
        builder.AddProduct(Money50000, ProductType.Consumable);
        builder.AddProduct(Money100000, ProductType.Consumable);
        builder.AddProduct(RemoveADs, ProductType.NonConsumable);
        builder.AddProduct(AddStock, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }

    /*public void Buy(string BuyThing)
    {
        bedone = false;
        for (int j = 0; j <= 5; j++)
        {
          Invoke(BuyThing, j);
        }
    }*/

    public void BuyMoney10000()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        
            BuyProductID(Money10000);
      
        
    }
    public void BuyMoney30000()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        //if (bedone == false)
            BuyProductID(Money30000);
    }
    public void BuyMoney50000()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        //if (bedone == false)
            BuyProductID(Money50000);
    }
    public void BuyMoney100000()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        //if (bedone == false)
            BuyProductID(Money100000);
    }
    public void BuyRemoveADs()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        if (PlayerDataManager.self.Player.DeleteAD)
        {
            UIManager.self.BuyInfoWindow.GetComponentInChildren<Text>().text = "已購買加速免廣告";
            UIManager.self.OpenBuyInfo();
        }
        else
        {
           // if (bedone == false)
                BuyProductID(RemoveADs);
        }
       
    }
    public void BuyAddStock()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        if (PlayerDataManager.self.Player.AddStockLimit >= 30)
        {
            UIManager.self.BuyInfoWindow.GetComponentInChildren<Text>().text = "庫存上限已達最大值";
            UIManager.self.OpenBuyInfo();
        }
        else
        {
           // if (bedone == false)
                BuyProductID(AddStock);
        }
    
    }
    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            bedone = true;
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }
    private bool IsInitialized()
     {
         return m_StoreController != null && m_StoreExtensionProvider != null;
     }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
        //throw new System.NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
       // throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        int Moto;
        string BuyNarrate;
        if (String.Equals(args.purchasedProduct.definition.id, Money10000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            Moto = PlayerDataManager.self.Player.Money;
            PlayerDataManager.self.Player.Money += 10000;
            BuyNarrate = "資金增加\n" + Moto + " > " + PlayerDataManager.self.Player.Money;
            UIManager.self.BuyInfoWindow.GetComponentInChildren<Text>().text = BuyNarrate;
            UIManager.self.OpenBuyInfo();
            return PurchaseProcessingResult.Complete;
        }
        if (String.Equals(args.purchasedProduct.definition.id, Money30000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            Moto = PlayerDataManager.self.Player.Money;
            PlayerDataManager.self.Player.Money += 30000;
            BuyNarrate = "資金增加\n" + Moto + " > " + PlayerDataManager.self.Player.Money;
            UIManager.self.BuyInfoWindow.GetComponentInChildren<Text>().text = BuyNarrate;
            UIManager.self.OpenBuyInfo();
            return PurchaseProcessingResult.Complete;
        }
        if (String.Equals(args.purchasedProduct.definition.id, Money50000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            Moto = PlayerDataManager.self.Player.Money;
            PlayerDataManager.self.Player.Money += 50000;
            BuyNarrate = "資金增加\n" + Moto + " > " + PlayerDataManager.self.Player.Money;
            UIManager.self.BuyInfoWindow.GetComponentInChildren<Text>().text = BuyNarrate;
            UIManager.self.OpenBuyInfo();
            return PurchaseProcessingResult.Complete;
        }
        if (String.Equals(args.purchasedProduct.definition.id, Money100000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            Moto = PlayerDataManager.self.Player.Money;
            PlayerDataManager.self.Player.Money += 100000;
            BuyNarrate = "資金增加\n" + Moto + " > " + PlayerDataManager.self.Player.Money;
            UIManager.self.BuyInfoWindow.GetComponentInChildren<Text>().text = BuyNarrate;
            UIManager.self.OpenBuyInfo();
            return PurchaseProcessingResult.Complete;
        }
        if (String.Equals(args.purchasedProduct.definition.id, RemoveADs, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            PlayerDataManager.self.Player.DeleteAD = true;
            BuyNarrate = "購買成功\n目前使用加速免看廣告";
            UIManager.self.BuyInfoWindow.GetComponentInChildren<Text>().text = BuyNarrate;
            UIManager.self.OpenBuyInfo();
            return PurchaseProcessingResult.Complete;
        }
        if (String.Equals(args.purchasedProduct.definition.id, AddStock, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            Moto = PlayerDataManager.self.Player.AddStockLimit +10;
            PlayerDataManager.self.Player.AddStockLimit += 15;
            int Now = Moto +15;
            BuyNarrate = "庫存上限增加\n" + Moto + " > " + Now;
            UIManager.self.BuyInfoWindow.GetComponentInChildren<Text>().text = BuyNarrate;
            UIManager.self.OpenBuyInfo();
            return PurchaseProcessingResult.Complete;
        }
       
        throw new System.NotImplementedException();
    }
    


    // Start is called before the first frame update
    void Start()
    {
        
        if (m_StoreController == null)
        {
            Debug.Log("m_StoreController == null");
            InitializePurchasing();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
