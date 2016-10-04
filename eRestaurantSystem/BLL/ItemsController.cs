using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additonal Namespaces
using System.ComponentModel; //ODS
using eRestaurantSystem.Data.Entities;
using eRestaurantSystem.Data.POCOs;
using eRestaurantSystem.Data.DTOs;
using eRestaurantSystem.DAL;
#endregion

namespace eRestaurantSystem.BLL
{
    [DataObject]
    public class ItemsController
    {
       
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<MenuCategoryFoodItemsPOCO> MenuCategoryFoodItemsPOCO_Get()
        {
            //set up transaction area
            using (var context = new eRestaurantContext())
            {
                var results = from food in context.Items
                              orderby food.MenuCategory.Description
                              select new MenuCategoryFoodItemsPOCO
                              {
                                  MenuCategoryDescription = food.MenuCategory.Description,
                                  ItemID = food.ItemID,
                                  FoodDescription = food.Description,
                                  CurrentPrice = food.CurrentPrice
                                 // TimeSaved = 10
                                  
                              };
                return results.ToList();
            }

        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<MenuCategoryFoodItems> MenuCategoryFoodItemsDTO_Get()
        {
            //set up transaction area
            using (var context = new eRestaurantContext())
            {
                var results = from food in context.Items
                              group food by new { food.MenuCategoryID, food.CurrentPrice } into tempdataset
                              orderby food.MenuCategory.Description
                              select new MenuCategoryFoodItems
                              {
                                  MenuCategoryDescription = tempdataset.Key.Description,
                                  FoodItems = (from x in tempdataset


                                               ) };
                                  //ItemID = food.ItemID,
                                  //FoodDescription = food.Description,
                                  //CurrentPrice = food.CurrentPrice
                                  // TimeSaved = 10

                //              };
                //from x in tempdataset
                //select new
                //{
                //    ItemID = x.ItemID,
                //    FoodDescription = x.Description,
                //    TimesServed = x.BillItems.Count()
                //}
                return results.ToList();

            }

        }
    }
}
