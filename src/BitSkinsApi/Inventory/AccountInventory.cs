﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;

namespace BitSkinsApi.Inventory
{
    /// <summary>
    /// Work with inventories.
    /// </summary>
    public static class Inventories
    {
        /// <summary>
        /// Allows you to retrieve your account's available inventory on Steam (items listable for sale), 
        /// your BitSkins inventory (items currently on sale), and your pending withdrawal inventory (items you delisted or purchased). 
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="page">Page number for BitSkins inventory.</param>
        /// <returns>Account's inventories.</returns>
        public static AccountInventory GetAccountInventory(Market.AppId.AppName app, int page)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_my_inventory/");
            urlCreator.AppendUrl($"&page={page}");
            urlCreator.AppendUrl($"&app_id={(int)app}");

            string result = Server.ServerRequest.RequestServer(urlCreator.ReadUrl());
            AccountInventory accountInventory = ReadAccountInventory(result);
            return accountInventory;
        }

        static AccountInventory ReadAccountInventory(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic steamInventoryD = responseServerD.data.steam_inventory;
            dynamic bitskinsInventoryD = responseServerD.data.bitskins_inventory;
            dynamic pendingWithdrawalInventoryD = responseServerD.data.pending_withdrawal_from_bitskins;

            SteamInventory steamInventory = ReadSteamInventory(steamInventoryD);
            BitSkinsInventory bitSkinsInventory = ReadBitSkinsInventory(bitskinsInventoryD);
            PendingWithdrawalFromBitskinsInventory pendingWithdrawalFromBitskinsInventory = ReadPendingWithdrawalFromBitskinsInventory(pendingWithdrawalInventoryD);

            AccountInventory accountInventorys = new AccountInventory(steamInventory, bitSkinsInventory, pendingWithdrawalFromBitskinsInventory);

            return accountInventorys;
        }

        static SteamInventory ReadSteamInventory(dynamic steamInventoryD)
        {
            SteamInventory steamInventory = null;
            if (steamInventoryD != null)
            {
                dynamic items = steamInventoryD.items;

                int totalItems = steamInventoryD.total_items;

                List<SteamInventoryItem> steamInventoryItems = new List<SteamInventoryItem>();
                if (items != null)
                {
                    foreach (dynamic item in items)
                    {
                        ReadSteamInventoryItem(item, out string marketHashName, out double suggestedPrice, out string itemType,
                            out string image, out int numberOfItems, out double recentAveragePrice, out List<string> itemIds);

                        SteamInventoryItem steamInventoryItem = new SteamInventoryItem(marketHashName, suggestedPrice,
                            itemType, image, numberOfItems, itemIds, recentAveragePrice);
                        steamInventoryItems.Add(steamInventoryItem);
                    }
                }

                steamInventory = new SteamInventory(totalItems, steamInventoryItems);
            }

            return steamInventory;
        }

        static BitSkinsInventory ReadBitSkinsInventory(dynamic bitskinsInventoryD)
        {
            BitSkinsInventory bitSkinsInventory = null;
            if (bitskinsInventoryD != null)
            {
                dynamic items = bitskinsInventoryD.items;

                int totalItems = bitskinsInventoryD.total_items;
                double TotalPrice = bitskinsInventoryD.total_price;

                List<BitSkinsInventoryItem> bitSkinsInventoryItems = new List<BitSkinsInventoryItem>();
                if (items != null)
                {
                    foreach (dynamic item in items)
                    {
                        ReadBitskinsInventoryItem(item, out string marketHashName, out double suggestedPrice, out string itemType, out string image,
                            out int numberOfItems, out double recentAveragePrice, out List<string> itemIds, out List<double> prices,
                            out List<DateTime> createdAt, out List<DateTime> updatedAt, out List<DateTime> withdrawableAt);

                        BitSkinsInventoryItem bitSkinsInventoryItem = new BitSkinsInventoryItem(marketHashName, suggestedPrice, itemType, image,
                            numberOfItems, itemIds, prices, createdAt, updatedAt, withdrawableAt, recentAveragePrice);
                        bitSkinsInventoryItems.Add(bitSkinsInventoryItem);
                    }
                }

                bitSkinsInventory = new BitSkinsInventory(totalItems, TotalPrice, bitSkinsInventoryItems);
            }

            return bitSkinsInventory;
        }

        static PendingWithdrawalFromBitskinsInventory ReadPendingWithdrawalFromBitskinsInventory(dynamic pendingWithdrawalInventoryD)
        {
            PendingWithdrawalFromBitskinsInventory pendingWithdrawalFromBitskinsInventory = null;
            if (pendingWithdrawalInventoryD != null)
            {
                dynamic items = pendingWithdrawalInventoryD.items;

                int totalItems = pendingWithdrawalInventoryD.total_items;

                List<PendingWithdrawalFromBitskinsInventoryItem> pendingWithdrawalFromBitskinsInventoryItems = new List<PendingWithdrawalFromBitskinsInventoryItem>();
                if (items != null)
                {
                    foreach (dynamic item in items)
                    {
                        ReadPendingWithdrawalFromBitskinsInventoryItem(item, out string marketHashName, out double suggestedPrice, out string itemType,
                            out string image, out string itemId, out DateTime withdrawableAt, out double lastPrice);

                        PendingWithdrawalFromBitskinsInventoryItem pendingWithdrawalFromBitskinsInventoryItem = new PendingWithdrawalFromBitskinsInventoryItem(
                            marketHashName, suggestedPrice, itemType, image, itemId, withdrawableAt, lastPrice);
                        pendingWithdrawalFromBitskinsInventoryItems.Add(pendingWithdrawalFromBitskinsInventoryItem);
                    }
                }

                pendingWithdrawalFromBitskinsInventory = new PendingWithdrawalFromBitskinsInventory(totalItems, pendingWithdrawalFromBitskinsInventoryItems);
            }

            return pendingWithdrawalFromBitskinsInventory;
        }

        static void ReadInventoryItem(dynamic item, out string marketHashName, out double suggestedPrice, out string itemType, out string image)
        {
            marketHashName = item.market_hash_name;
            suggestedPrice = item.suggested_price;
            itemType = item.item_type;
            image = item.image;
        }

        static void ReadSteamInventoryItem(dynamic item, out string marketHashName, out double suggestedPrice, out string itemType, out string image,
            out int numberOfItems, out double recentAveragePrice, out List<string> itemIds)
        {
            ReadInventoryItem(item, out marketHashName, out suggestedPrice, out itemType, out image);
            numberOfItems = item.number_of_items;
            recentAveragePrice = (item.recent_sales_info != null) ? (double)item.recent_sales_info.average_price : 0;

            itemIds = new List<string>();
            foreach (dynamic itemId in item.item_ids)
            {
                itemIds.Add((string)itemId);
            }
        }

        static void ReadBitskinsInventoryItem(dynamic item, out string marketHashName, out double suggestedPrice, out string itemType, out string image,
            out int numberOfItems, out double recentAveragePrice, out List<string> itemIds, out List<double> prices, out List<DateTime> createdAt,
            out List<DateTime> updatedAt, out List<DateTime> withdrawableAt)
        {
            ReadInventoryItem(item, out marketHashName, out suggestedPrice, out itemType, out image);
            numberOfItems = item.number_of_items;
            recentAveragePrice = (item.recent_sales_info != null) ? (double)item.recent_sales_info.average_price : 0;

            itemIds = new List<string>();
            foreach (dynamic itemId in item.item_ids)
            {
                itemIds.Add((string)itemId);
            }

            prices = new List<double>();
            foreach (dynamic price in item.prices)
            {
                prices.Add((double)price);
            }

            createdAt = new List<DateTime>();
            foreach (dynamic date in item.created_at)
            {
                createdAt.Add(DateTimeExtension.FromUnixTime((long)date));
            }

            updatedAt = new List<DateTime>();
            foreach (dynamic date in item.updated_at)
            {
                updatedAt.Add(DateTimeExtension.FromUnixTime((long)date));
            }

            withdrawableAt = new List<DateTime>();
            foreach (dynamic date in item.withdrawable_at)
            {
                withdrawableAt.Add(DateTimeExtension.FromUnixTime((long)date));
            }
        }

        static void ReadPendingWithdrawalFromBitskinsInventoryItem(dynamic item, out string marketHashName, out double suggestedPrice, 
            out string itemType, out string image, out string itemId, out DateTime withdrawableAt, out double lastPrice)
        {
            ReadInventoryItem(item, out marketHashName, out suggestedPrice, out itemType, out image);
            itemId = item.item_id;
            withdrawableAt = DateTimeExtension.FromUnixTime((long)item.withdrawable_at);
            lastPrice = item.last_price;
        }
    }

    /// <summary>
    /// All user's inventories.
    /// </summary>
    public class AccountInventory
    {
        public SteamInventory SteamInventory { get; private set; }
        public BitSkinsInventory BitSkinsInventory { get; private set; }
        public PendingWithdrawalFromBitskinsInventory PendingWithdrawalFromBitskinsInventory { get; private set; }

        internal AccountInventory(SteamInventory steamInventory, BitSkinsInventory bitSkinsInventory,
            PendingWithdrawalFromBitskinsInventory pendingWithdrawalFromBitskinsInventory)
        {
            SteamInventory = steamInventory;
            BitSkinsInventory = bitSkinsInventory;
            PendingWithdrawalFromBitskinsInventory = pendingWithdrawalFromBitskinsInventory;
        }
    }

    /// <summary>
    /// User's inventory.
    /// </summary>
    public abstract class Inventory
    {
        public int TotalItems { get; private set; }

        protected Inventory(int totalItems)
        {
            TotalItems = totalItems;
        }
    }

    /// <summary>
    /// User's Steam inventory.
    /// </summary>
    public class SteamInventory : Inventory
    {
        public List<SteamInventoryItem> SteamInventoryItems { get; private set; }

        internal SteamInventory(int totalItems, List<SteamInventoryItem> steamInventoryItems)
            : base(totalItems)
        {
            SteamInventoryItems = steamInventoryItems;
        }
    }

    /// <summary>
    /// User's BitSkins inventory.
    /// </summary>
    public class BitSkinsInventory : Inventory
    {
        public double TotalPrice { get; private set; }
        public List<BitSkinsInventoryItem> BitSkinsInventoryItems { get; private set; }

        internal BitSkinsInventory(int totalItems, double totalPrice, List<BitSkinsInventoryItem> bitSkinsInventoryItems)
             : base(totalItems)
        {
            TotalPrice = totalPrice;
            BitSkinsInventoryItems = bitSkinsInventoryItems;
        }
    }

    /// <summary>
    /// User's pending withdrawal from BitSkins inventory.
    /// </summary>
    public class PendingWithdrawalFromBitskinsInventory : Inventory
    {
        public List<PendingWithdrawalFromBitskinsInventoryItem> PendingWithdrawalFromBitskinsInventoryItems { get; private set; }

        internal PendingWithdrawalFromBitskinsInventory(int totalItems, 
            List<PendingWithdrawalFromBitskinsInventoryItem> pendingWithdrawalFromBitskinsInventoryItems)
            : base(totalItems)
        {
            PendingWithdrawalFromBitskinsInventoryItems = pendingWithdrawalFromBitskinsInventoryItems;
        }
    }

    /// <summary>
    /// Inventory's item.
    /// </summary>
    public abstract class InventoryItem
    {
        public string MarketHashName { get; private set; }
        public double SuggestedPrice { get; private set; }
        public string ItemType { get; private set; }
        public string Image { get; private set; }

        protected InventoryItem(string marketHashName, double suggestedPrice, string itemType, string image)
        {
            MarketHashName = marketHashName;
            SuggestedPrice = suggestedPrice;
            ItemType = itemType;
            Image = image;
        }
    }

    /// <summary>
    /// Steam inventory's item.
    /// </summary>
    public class SteamInventoryItem : InventoryItem
    {
        public int NumberOfItems { get; private set; }
        public List<string> ItemIds { get; private set; }
        public double RecentAveragePrice { get; private set; }

        internal SteamInventoryItem(string marketHashName, double suggestedPrice, string itemType, string image, 
            int numberOfItems, List<string> itemIds, double recentAveragePrice)
            : base(marketHashName, suggestedPrice, itemType, image)
        {
            NumberOfItems = numberOfItems;
            ItemIds = itemIds;
            RecentAveragePrice = recentAveragePrice;
        }
    }

    /// <summary>
    /// BitSkins inventory's item.
    /// </summary>
    public class BitSkinsInventoryItem : InventoryItem
    {
        public int NumberOfItems { get; private set; }
        public List<string> ItemIds { get; private set; }
        public List<double> Prices { get; private set; }
        public List<DateTime> CreatedAt { get; private set; }
        public List<DateTime> UpdatedAt { get; private set; }
        public List<DateTime> WithdrawableAt { get; private set; }
        public double RecentAveragePrice { get; private set; }

        internal BitSkinsInventoryItem(string marketHashName, double suggestedPrice, string itemType, string image,
            int numberOfItems, List<string> itemIds, List<double> prices, List<DateTime> createdAt, List<DateTime> updatedAt,
            List<DateTime> withdrawableAt, double recentAveragePrice)
            : base(marketHashName, suggestedPrice, itemType, image)
        {
            NumberOfItems = numberOfItems;
            ItemIds = itemIds;
            Prices = prices;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            WithdrawableAt = withdrawableAt;
            RecentAveragePrice = recentAveragePrice;
        }
    }

    /// <summary>
    /// Pending withdrawal from BitSkins inventory's item.
    /// </summary>
    public class PendingWithdrawalFromBitskinsInventoryItem : InventoryItem
    {
        public string ItemId { get; private set; }
        public DateTime WithdrawableAt { get; private set; }
        public double LastPrice { get; private set; }

        internal PendingWithdrawalFromBitskinsInventoryItem(string marketHashName, double suggestedPrice, string itemType, string image,
            string itemId, DateTime withdrawableAt, double lastPrice)
            : base(marketHashName, suggestedPrice, itemType, image)
        {
            ItemId = itemId;
            WithdrawableAt = withdrawableAt;
            LastPrice = lastPrice;
        }
    }
}