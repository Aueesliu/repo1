using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemData {

	private int count=1;
	private int id;
	private  int coins;
	private ItemType type = ItemType.No;
	private  string goodsname;
    private  string effect;
	private Sprite sprite;

    public int Count
    {
        get
        {
            return count;
        }

        set
        {
            count = value;
        }
    }

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public int Coins
    {
        get
        {
            return coins;
        }

        set
        {
            coins = value;
        }
    }

    public ItemType Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public string Goodsname
    {
        get
        {
            return goodsname;
        }

        set
        {
            goodsname = value;
        }
    }

    public string Effect
    {
        get
        {
            return effect;
        }

        set
        {
            effect = value;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return sprite;
        }

        set
        {
            sprite = value;
        }
    }
}
