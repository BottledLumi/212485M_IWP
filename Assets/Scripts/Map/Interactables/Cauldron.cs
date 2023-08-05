using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cauldron : MonoBehaviour
{
    enum ITEM
    {
        CARROT = 401,
        CHEESE,
        EGG,
        FLOUR,
        MANGO,
        MILK,
        OLIVE,
        POTATO,
        STEAK,
        SUGAR,
        WATER,
        BREAD,
        CAKE,
        CREAM,
        PASTA,
        CARROTCAKE,
        FRENCHTOAST,
        HAMBURGER,
        MACANDCHEESE,
        MANGOCAKE,
        MASHEDPOTATO,
        CHEESEBURGER,
        ALFREDO,
    }

    [SerializeField] List<Item> listOfResults;
    List<Item> mixture = new();
    bool success = false;
    public void AddItem(Item item)
    {
        mixture.Add(item);
        success = false;
        // remove item from inventory
        PlayerData.Instance.RemoveItem(item);
        if (mixture.Count >= 1)
        {
            Cook();
        }
    }

    private void Cook()
    {
        // some sort of item variable here
        foreach(Item mix1 in mixture)
        {
            switch (mix1.Index)
            {
                case (int)ITEM.CAKE:
                    {
                        foreach (Item mix2 in mixture)
                        {
                            if (mix2.Index == (int)ITEM.CAKE)
                            {
                                //item = carrot cake
                                SpawnResult(ITEM.CARROTCAKE);
                            }
                            else if (mix2.Index == (int)ITEM.MANGO)
                            {
                                // item = mango cake
                                SpawnResult(ITEM.MANGOCAKE);
                            }
                        }
                        break;
                    }
                case (int)ITEM.CHEESE:
                    {
                        foreach (Item mix2 in mixture)
                        {
                            if (mix2.Index == (int)ITEM.PASTA)
                            {
                                // item = mac and cheese
                                SpawnResult(ITEM.MACANDCHEESE);
                            }
                            else if (mix2.Index == (int)ITEM.HAMBURGER)
                            {
                                // item = cheeseburger
                                SpawnResult(ITEM.CHEESEBURGER);
                            }
                        }
                        break;
                    }
                case (int)ITEM.EGG:
                    {
                        foreach (Item mix2 in mixture)
                        {
                            if (mix2.Index == (int)ITEM.FLOUR)
                            {
                                // item = pasta
                                SpawnResult(ITEM.PASTA);
                            }
                            else if (mix2.Index == (int)ITEM.BREAD)
                            {
                                // item = frenchtoast
                                SpawnResult(ITEM.FRENCHTOAST);
                            }
                        }
                        break;
                    }
                case (int)ITEM.FLOUR:
                    {
                        foreach (Item mix2 in mixture)
                        {
                            if (mix2.Index == (int)ITEM.WATER)
                            {
                                // item = BREAD
                                SpawnResult(ITEM.BREAD);
                            }
                            else if (mix2.Index == (int)ITEM.SUGAR)
                            {
                                // item = cake
                                SpawnResult(ITEM.CAKE);
                            }
                        }
                        break;
                    }
                case (int)ITEM.MILK:
                    {
                        foreach (Item mix2 in mixture)
                        {
                            if (mix2.Index == (int)ITEM.SUGAR)
                            {
                                // item = cream
                                SpawnResult(ITEM.CREAM);
                            }
                        }
                        break;
                    }
                case (int)ITEM.CREAM:
                    {
                        foreach (Item mix2 in mixture)
                        {
                            if (mix2.Index == (int)ITEM.POTATO)
                            {
                                // item = mashed potato
                                SpawnResult(ITEM.MASHEDPOTATO);
                            }
                            else if (mix2.Index == (int)ITEM.PASTA)
                            {
                                // item = alfredo
                                SpawnResult(ITEM.ALFREDO);
                            }
                        }
                        break;
                    }
                case (int)ITEM.BREAD:
                    {
                        foreach (Item mix2 in mixture)
                        {
                            if (mix2.Index == (int)ITEM.STEAK)
                            {
                                // item = hamburger
                                SpawnResult(ITEM.HAMBURGER);
                            }
                        }
                        break;
                    }
            }
        }
        //if item == null then no cook
        if (!success)
        {
            //spit out
            foreach (Item mix in mixture)
            {
                PlayerData.Instance.AddItem(mix);
            }
        }    
    }

    private void SpawnResult(ITEM item)
    {
        foreach (Item result in listOfResults)
        {
            if (result.Index == (int)item)
            {
                success = true;
                PlayerData.Instance.AddItem(result);
            }
        }
        
    }
}
