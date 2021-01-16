using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public  class CfgManager:MonoBehaviour {

	List<ItemData> ListItemData=new List<ItemData>();
	TextAsset goodText;
	TextAsset goodText2;
	                                                                                                                 
	private static CfgManager instance;
	public static CfgManager Instance
 	{
        get
        {
            if (instance==null)
            {
                GameObject go =GameObject.FindWithTag("MainManager");
                instance=go.GetComponent<CfgManager>();
            }
            return instance;
        }
	}
	void Awake () 
	{
		JsonCreateItemData();
	}
	
	public void JsonCreateItemData()								//将Json导入内容导入链表
	{
		goodText2 =Resources.Load<TextAsset>("Txt/Goods2");
		JsonData goodjson = JsonMapper.ToObject(goodText2.text);
		JsonData goodsdata =goodjson["Goods"];
		
		foreach (JsonData Goods in goodsdata)
		{
			ItemData itemdata=new ItemData();
			itemdata.Id =  (int)Goods["GoodsId"]; 
			itemdata.Goodsname = Goods["GoodsName"].ToString();
			itemdata.Effect =Goods["GoodsEffect"].ToString();
			itemdata.Coins = (int)Goods["GoodCoins"];
            itemdata.Type = (ItemType)itemdata.Id;    
			ListItemData.Add(itemdata);	
		}
	}
	public ItemData JsonAddItemData(int index)
	{
		for (int i = 0; i < ListItemData.Count; i++)
		{
			if (ListItemData[i].Id==index+1)
			{
				return ListItemData[i];
			}
		}
		
		return new ItemData();
	}
	private void TestSHowList()
	{
		foreach (var item in ListItemData)
		{
			Debug.Log(item.Type);
		}
	}

}
