using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum ItemType
{
    RedDrug = 1,
    BuleDrug,
    EnergyDrug,
    MagicDrug,
    No
}
public class DataManager : MonoBehaviour 
{
	public Dictionary<int , ItemData> dicData =new Dictionary<int, ItemData>();			//所有Item的管理
	Dictionary<ItemType,Sprite> dicSprite =new Dictionary<ItemType, Sprite>();			//所有类型的对应的图片
	Dictionary<ItemType,int>  dicCount =new Dictionary<ItemType, int>();				//所有类型对应的数量
	
	private  int nowItemdata = -1;							//当前选中的数据
	 void  Awake()
	 {
		 
		 CreateDicAddSprite();								//存储图片
	 }		
	
	private static DataManager instance;

	public static DataManager Instance                                  //单例模式
    {
        get
        {
            if (instance==null)
            {
                GameObject go = GameObject.FindWithTag("MainManager");
                instance=go.GetComponent<DataManager>();
            }
            return instance;
        }
    }

    public int NowItemdata
    {
        get
        {
            return nowItemdata;
        }

        set
        {
            nowItemdata = value;
        }
    }
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.J))
		{
            System.Random a =new System.Random();
			int i = a.Next(0, 4);		
			PrintOne( i);	
		}	
		if (Input.GetKeyDown(KeyCode.K))
		{
			PrintAll();
		}
		if (Input.GetKeyDown(KeyCode.L))	
		{
			AddRandOne();
		}
		if (Input.GetKeyDown(KeyCode.U))
		{
			DelRandOne();
		}
		if(Input.GetKeyDown(KeyCode.G))
		{
			CleanCount();
		}
	}


	public ItemData DicDataItemData(int i){ return dicData[i];}
	public Sprite DicDataGetImg(int i){return dicData[i].Sprite;}
	
	public void DicDataResert(int i)
	{
		dicData[i].Type=ItemType.No;
		dicData[i].Count =0;
		dicData[i].Id   =0;
		dicData[i].Coins =0;
		dicData[i].Goodsname="";
		dicData[i].Effect ="";
		dicData[i].Sprite =null;
	}
	public void DicDataResert(ItemData itemData2)
	{
		itemData2.Type=ItemType.No;
		itemData2.Count =0;
		itemData2.Id   =0;
		itemData2.Coins =0;
		itemData2.Goodsname="";
		itemData2.Effect ="";
		itemData2.Sprite =null;
	}
	
	public void PrintOne(int i)			
	{
		Debug.Log("类型："+dicData[i].Type+" 数量："+ dicData[i].Count+" 图片:"+dicData[i].Sprite);
	}
	
	private void PrintAll()
	{
		for (int i = 0; i < dicData.Count; i++)
		{
			Debug.Log("类型："+dicData[i].Type+" 数量："+ dicData[i].Count+" 图片:"+dicData[i].Sprite);
		}
	}
	public void CreatItemData(int i)			
	{
		ItemData itemCopy = CfgManager.Instance.JsonAddItemData(i);
		ItemData data2= new ItemData();
		if (itemCopy!=data2)
		{
			data2.Id=itemCopy.Id;
			data2.Goodsname=itemCopy.Goodsname;
			data2.Effect=itemCopy.Effect;
			data2.Coins=itemCopy.Coins;
			data2.Type=itemCopy.Type;
			data2.Count=0;
		}
		dicData.Add(i,data2);
		if (data2.Type != ItemType.No)
		{
			dicData[i].Sprite=DicSpriteGet(data2.Type);
			dicData[i].Count=1;
		}
	}
	public void CopyItem(int i)
	{
		dicData[i].Id = CfgManager.Instance.JsonAddItemData(i).Id;
		dicData[i].Goodsname=CfgManager.Instance.JsonAddItemData(i).Goodsname;
		dicData[i].Effect=CfgManager.Instance.JsonAddItemData(i).Effect;
		dicData[i].Coins=CfgManager.Instance.JsonAddItemData(i).Coins;
		dicData[i].Type=CfgManager.Instance.JsonAddItemData(i).Type;
		dicData[i].Sprite=dicSprite[(ItemType)(i+1)];
		dicData[i].Count=dicCount[(ItemType)(i+1)];
	}

	private void  CreateDicAddSprite()					//初始化资源库的文件（图片，总数）
	{
		ItemType e = new ItemType();
        string[] EItemType = System.Enum.GetNames(e.GetType());
		Sprite[] sprites =Resources.LoadAll<Sprite>("Img");
		for (int i = 0; i < EItemType.Length-1; i++)
		{
			dicSprite.Add((ItemType)(i+1),sprites[i]);
			dicCount.Add((ItemType)(i+1),0);
		} 
	}
	public Sprite DicSpriteGet(ItemType itemType)
	{
		return dicSprite[itemType];	
	}
	public int IsNoDataInt()						//获取没有赋值的第一个i
	{
		for (int i = 0; i < dicData.Count; i++)
		{
			if (dicData[i].Type==ItemType.No)
			{
				return i;
			}
		}
		return -1;
	}

	public ItemData IsNoType()						//获取没有赋值的第一个脚本
	{
		for (int i = 0; i < dicData.Count; i++)
		{
			if (dicData[i].Type==ItemType.No)
			{
				return dicData[i];
			}
		}
		return null;
	}
	public ItemData IsOnType()						//获取已经赋值的第一个脚本
	{
		for (int i = 0; i < dicData.Count; i++)
		{
			if (dicData[i].Type!=ItemType.No)
			{
				return dicData[i];
			}
		}
		return null;
	}
	public ItemData AddRandOne()					//随机添加一个
	{
		ItemData data2 =IsNoType();
		if (data2!=null)
		{
			Debug.Log("随机添加一个");
			System.Random a =new System.Random();
			int i = a.Next(0, 4);
			ItemData itemCopy=CfgManager.Instance.JsonAddItemData(i);
			data2.Id=itemCopy.Id;
			data2.Goodsname=itemCopy.Goodsname;
			data2.Effect=itemCopy.Effect;
			data2.Coins=itemCopy.Coins;
			data2.Type=itemCopy.Type;
			data2.Count=1;
			data2.Sprite=DicSpriteGet(itemCopy.Type);
			return data2;
		}
			return null;
	}
	private void DelRandOne()					
	{
		Debug.Log("清除第一个获取的数据");
		ItemData data2=IsOnType();
		DicDataResert(data2);
	}
	public void CleanCount()					//整理格子
	{
		CleanDicCount();
		for (int i = 0; i < dicData.Count; i++)
		{
			if (dicData[i].Type!=ItemType.No)
			{
			dicCount[dicData[i].Type]+=dicData[i].Count;
			DicDataResert(dicData[i]);
			}
		}
		for (int i = 0; i < dicCount.Count; i++)
		{
			if (dicCount[(ItemType)(i+1)]>0)
			{
				CopyItem(i);
			}
		}
		PrintAll();
	}
	public void CleanDicCount()
	{
		for (int i = 0; i < dicCount.Count; i++)
		{
			dicCount[(ItemType)(i+1)]=0;
		}
	}

	public void DelCount()
	{
		if (NowItemdata!=-1&&dicData[NowItemdata].Type!=ItemType.No)	//选中框并且里面有赋值
		{
			if (dicData[NowItemdata].Count!=0)
			{
				dicCount[dicData[NowItemdata].Type]--;
				dicData[NowItemdata].Count--;
			}
		}
	}
		public void AddCount()
	{
		if (NowItemdata!=-1&&dicData[NowItemdata].Type!=ItemType.No)	//选中框并且里面有赋值
		{
				dicCount[dicData[NowItemdata].Type]++;
				dicData[NowItemdata].Count++;
		}
	}
	
}
