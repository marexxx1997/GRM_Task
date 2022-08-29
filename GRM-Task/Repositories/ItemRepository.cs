using GRM_Task.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
namespace GRM_Task.Repositories
{
    public class ItemRepository
    {
        public List<Item> items = new List<Item>() {
        new Item() {Position=1,Name="Item1",Score=0},
        new Item() {Position=2,Name="Item2",Score=0},
        new Item() {Position=3,Name="Item3",Score=0},
        new Item() {Position=4,Name="Item4",Score=0},
        new Item() {Position=5,Name="Item5",Score=0},
        new Item() {Position=6,Name="Item6",Score=0}
        };

        public List<Item> GetAllItems()
        {
            items = items.OrderByDescending(x => x.Score).ToList();
            int position = 0;
            foreach (var item in items)
            {
                item.Position = ++position;
            }
            return items;
        }

        public List<ItemsPair> pairs = new List<ItemsPair>()
         {
             new ItemsPair() {Position1=1,Name1="Item1",Score1=0,Position2=2,Name2="Item2",Score2=0},
             new ItemsPair() {Position1=1,Name1="Item1",Score1=0,Position2=3,Name2="Item3",Score2=0},
             new ItemsPair() {Position1=1,Name1="Item1",Score1=0,Position2=4,Name2="Item4",Score2=0},
             new ItemsPair() {Position1=1,Name1="Item1",Score1=0,Position2=5,Name2="Item5",Score2=0},
             new ItemsPair() {Position1=1,Name1="Item1",Score1=0,Position2=6,Name2="Item6",Score2=0},
             new ItemsPair() {Position1=2,Name1="Item2",Score1=0,Position2=3,Name2="Item3",Score2=0},
             new ItemsPair() {Position1=2,Name1="Item2",Score1=0,Position2=4,Name2="Item4",Score2=0},
             new ItemsPair() {Position1=2,Name1="Item2",Score1=0,Position2=5,Name2="Item5",Score2=0},
             new ItemsPair() {Position1=2,Name1="Item2",Score1=0,Position2=6,Name2="Item6",Score2=0},
             new ItemsPair() {Position1=3,Name1="Item3",Score1=0,Position2=4,Name2="Item4",Score2=0},
             new ItemsPair() {Position1=3,Name1="Item3",Score1=0,Position2=5,Name2="Item5",Score2=0},
             new ItemsPair() {Position1=3,Name1="Item3",Score1=0,Position2=6,Name2="Item6",Score2=0},
             new ItemsPair() {Position1=4,Name1="Item4",Score1=0,Position2=5,Name2="Item5",Score2=0},
             new ItemsPair() {Position1=4,Name1="Item4",Score1=0,Position2=6,Name2="Item6",Score2=0},
             new ItemsPair() {Position1=5,Name1="Item5",Score1=0,Position2=6,Name2="Item6",Score2=0}
         };

        public List<ItemsPair> GetAllPairs()
        {
            return pairs;
        }
        public void CompareItems(CompareItemsModel comparedItems)
        {
            if(comparedItems != null)
            {
                if(comparedItems.Value1.GetType()==typeof(int) && comparedItems.Value2.GetType()==typeof(int))
                {
                    int indexOfPair = 0;
                    bool isFound = false;
                    for (int i = 0; i < pairs.Count(); i++)
                    {
                        if (string.Equals(pairs[i].Name1, comparedItems.Name1)
                            && string.Equals(pairs[i].Name2, comparedItems.Name2))
                        {
                            indexOfPair = i;
                            isFound = true;
                            Item item = new Item();
                            if (comparedItems.Value1 > comparedItems.Value2)
                            {
                                item.Name = comparedItems.Name1;
                                var indexOfItem = items.FindIndex(x => x.Name == item.Name);
                                items[indexOfItem].Score++;
                            }
                            else if(comparedItems.Value1 < comparedItems.Value2)
                            {
                                item.Name = comparedItems.Name2;
                                var indexOfItem = items.FindIndex(x => x.Name == item.Name);
                                items[indexOfItem].Score++;
                            }

                            items.OrderByDescending(x => x.Score).ToList();
                            int position = 0;
                            foreach (var it in items)
                            {
                                it.Position = ++position;
                            }
                            break;
                        }
                    }
                    if(isFound==true)
                    {
                        pairs.RemoveAt(indexOfPair);
                    }
                }
            }
        }

        public CompareItemsModel GetNextPair()
        {
            if(pairs.Count()>0)
            {
                CompareItemsModel compareItemsModel = new CompareItemsModel();
                Random random = new Random();
                int indexOfNextPair=random.Next(0,pairs.Count()-1);

                compareItemsModel.Name1 = pairs[indexOfNextPair].Name1;
                compareItemsModel.Name2 = pairs[indexOfNextPair].Name2;
                compareItemsModel.Position1 = pairs[indexOfNextPair].Position1;
                compareItemsModel.Position2 = pairs[indexOfNextPair].Position2;
                compareItemsModel.Score1 = pairs[indexOfNextPair].Score1;
                compareItemsModel.Score2 = pairs[indexOfNextPair].Score2;

                return compareItemsModel;
            }
            return new CompareItemsModel
            {
                Name1 = "",
                Name2 = "",
                Position1 = 0,
                Position2 = 0,
                Score1 = 0,
                Score2 = 0
            };
        }
    }
}
