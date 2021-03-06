﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.Extensions;
using BitSkinsApi.CheckParameters;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with BitSkins recent sales.
    /// </summary>
    public static class RecentSaleInfo
    {
        /// <summary>
        /// Allows you to retrieve upto 5 pages worth of recent sale data for a given item name. 
        /// These are the recent sales for the given item at BitSkins, in descending order.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="marketHashName">The item's name.</param>
        /// <param name="page">The page number. From 1 to 5.</param>
        /// <returns>List of item's recent sales.</returns>
        public static List<ItemRecentSale> GetRecentSaleInfo(AppId.AppName app, string marketHashName, int page)
        {
            CheckParameters(marketHashName, page);
            string urlRequest = GetUrlRequest(app, marketHashName, page);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            List<ItemRecentSale> itemRecentSales = ReadItemRecentSales(result);
            return itemRecentSales;
        }

        private static void CheckParameters(string marketHashName, int page)
        {
            Checking.NotEmptyString(marketHashName, "marketHashName");
            Checking.PositiveInt(page, "page");
            Checking.UptoInt(page, "page", 5);
        }

        private static string GetUrlRequest(AppId.AppName app, string marketHashName, int page)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_sales_info/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&market_hash_name={marketHashName}");
            urlCreator.AppendUrl($"&page={page}");

            return urlCreator.ReadUrl();
        }

        private static List<ItemRecentSale> ReadItemRecentSales(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic salesD = responseServerD.data.sales;

            List<ItemRecentSale> itemRecentSales = new List<ItemRecentSale>();
            if (salesD != null)
            {
                foreach (dynamic item in salesD)
                {
                    ItemRecentSale recentSaleItem = ReadItemRecentSale(item);
                    itemRecentSales.Add(recentSaleItem);
                }
            }

            return itemRecentSales;
        }

        private static ItemRecentSale ReadItemRecentSale(dynamic item)
        {
            double? price = item.price ?? null;
            double? wearValue = item.wear_value ?? null;
            DateTime? soldAt = null;
            if (item.sold_at != null)
            {
                soldAt = DateTimeExtension.FromUnixTime((long)item.sold_at);
            }

            ItemRecentSale recentSaleItem = new ItemRecentSale(price, wearValue, soldAt);
            return recentSaleItem;
        }
    }

    /// <summary>
    /// Information about item's recent sale. 
    /// </summary>
    public class ItemRecentSale
    {
        public double? Price { get; private set; }
        public double? WearValue { get; private set; }
        public DateTime? SoldAt { get; private set; }

        internal ItemRecentSale(double? price, double? wearValue, DateTime? soldAt)
        {
            Price = price;
            WearValue = wearValue;
            SoldAt = soldAt;
        }
    }
}
