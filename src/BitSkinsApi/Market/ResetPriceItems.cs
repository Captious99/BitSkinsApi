﻿using System.Collections.Generic;
using Newtonsoft.Json;
using BitSkinsApi.CheckParameters;

namespace BitSkinsApi.Market
{
    /// <summary>
    /// Work with reset price items.
    /// </summary>
    public static class ResetPriceItems
    {
        /// <summary>
        /// Returns a paginated list of items that need their prices reset. 
        /// Items need prices reset when Steam changes tracker so BitSkins are unable to match specified prices to the received items
        /// when you list them for sale. 
        /// Upto 30 items per page. 
        /// Items that need price resets always have the reserved price of 4985.11.
        /// </summary>
        /// <param name="app">Inventory's game id.</param>
        /// <param name="page">Page number.</param>
        /// <returns>List of reset price items.</returns>
        public static List<ResetPriceItem> GetResetPriceItems(AppId.AppName app, int page)
        {
            CheckParameters(page);
            string urlRequest = GetUrlRequest(app, page);
            string result = Server.ServerRequest.RequestServer(urlRequest);
            List<ResetPriceItem> resetPriceItems = ReadResetPriceItems(result);
            return resetPriceItems;
        }

        private static void CheckParameters(int page)
        {
            Checking.PositiveInt(page, "page");
        }

        private static string GetUrlRequest(AppId.AppName app, int page)
        {
            Server.UrlCreator urlCreator = new Server.UrlCreator($"https://bitskins.com/api/v1/get_reset_price_items/");
            urlCreator.AppendUrl($"&app_id={(int)app}");
            urlCreator.AppendUrl($"&page={page}");

            return urlCreator.ReadUrl();
        }

        private static List<ResetPriceItem> ReadResetPriceItems(string result)
        {
            dynamic responseServerD = JsonConvert.DeserializeObject(result);
            dynamic itemsD = responseServerD.data.items;

            List<ResetPriceItem> resetPriceItems = new List<ResetPriceItem>();
            if (itemsD != null)
            {
                foreach (dynamic item in itemsD)
                {
                    ResetPriceItem resetPriceItem = ReadResetPriceItem(item);
                    resetPriceItems.Add(resetPriceItem);
                }
            }

            return resetPriceItems;
        }

        private static ResetPriceItem ReadResetPriceItem(dynamic item)
        {
            string marketHashName = item.market_hash_name ?? null;
            double? price = item.price ?? null;

            ResetPriceItem resetPriceItem = new ResetPriceItem(marketHashName, price);
            return resetPriceItem;
        }
    }

    /// <summary>
    /// BitSkins reset price item.
    /// </summary>
    public class ResetPriceItem
    {
        public string MarketHashName { get; private set; }
        public double? Price { get; private set; }

        internal ResetPriceItem(string marketHashName, double? price)
        {
            MarketHashName = marketHashName;
            Price = price;
        }
    }
}
