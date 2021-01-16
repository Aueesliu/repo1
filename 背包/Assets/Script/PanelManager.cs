using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour {

	private static PanelManager instance;
	private GameObject item;
	private GameObject box;
	private Transform checkbox;
	private Transform content;			

	private Image showImg;
	private Text showEffect;
	private Text showCoin;
	public Dictionary<int,ItemCom> dicCom=new Dictionary<int, ItemCom>();				

	void Start () 
	{
		item =Resources.Load<GameObject>("Item");
		box=Resources.Load<GameObject>("Box");
		content=GameTool.FindTheChild(transform.parent.gameObject,"Content");
		showImg = GameTool.FindTheChild(transform.root.gameObject, "InforImg").GetComponent<Image>();
        showEffect = GameTool.FindTheChild(transform.root.gameObject, "InforEffect").GetComponent<Text>();
        showCoin = GameTool.FindTheChild(transform.root.gameObject, "InforCoin").GetComponent<Text>();
        checkbox= Instantiate( Resources.Load<GameObject>("CheckBox")).transform;
        checkbox.SetParent(this.transform.parent.Find("ObjectPool"));


	}

	public void CreateItem()
	{

		for (int i = 0; i < content.childCount; i++)
		{
			DataManager.Instance.CreatItemData(i);			//创建数据类
			GameObject itemobj=Instantiate(item);			//创建实体
			itemobj.transform.SetParent(content.GetChild(i));
            itemobj.transform.localPosition = Vector3.zero;
            itemobj.transform.localScale = Vector3.one;
			ItemCom itemCom =new ItemCom(itemobj.transform,i);	
			itemCom.SetData((DataManager.Instance.DicDataItemData(i)));
			dicCom.Add(i,itemCom);
		}
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))					//随机生成一个物品
		{
			int i=DataManager.Instance.IsNoDataInt();		
			DataManager.Instance.AddRandOne();
			UpdataPanel(DataManager.Instance.NowItemdata);
			
		}
		if (Input.GetKeyDown(KeyCode.C))					//整理功能
		{
			DataManager.Instance.CleanCount();				//数据整理
			CleanAndSet();									//图片清空 , 重新赋值												
			int i=DataManager.Instance.NowItemdata;
			UpdataPanel(i);
		}

		if (Input.GetKeyDown(KeyCode.D))					//减少数量
		{
			int i=DataManager.Instance.NowItemdata;
			if (i !=-1)
			{
				DataManager.Instance.DelCount();
				UpdataPanel(i);
			}		
		}
		if (Input.GetKeyDown(KeyCode.S))					//增加数量
		{
			int i=DataManager.Instance.NowItemdata;
			if (i!=-1)
			{
			DataManager.Instance.AddCount();
			UpdataPanel(i);
			}
		}
	}
	public void CleanAndSet()
	{
		for (int i = 0; i < dicCom.Count; i++)
		{
			dicCom[i].SetData(DataManager.Instance.DicDataItemData(i));							
		}
	}

	public void ShowToPanel(int i)
	{
		ItemData itemData2=DataManager.Instance.DicDataItemData(i);
		showImg.color=new Color(255,255,255,255);
		showImg.sprite=itemData2.Sprite;
		showEffect.text=itemData2.Effect;
		showCoin.text=itemData2.Coins.ToString();
		
	}
	public void UpdataPanel(int index = -1)													//刷新数据
	{						
		for (int i = 0; i < PanelManager.Instance.dicCom.Count; i++)
		{
			ItemCom itemCom = dicCom[i];
			ItemData itemData = DataManager.Instance.DicDataItemData(i);			
			itemCom.SetData(itemData);									
		}
		if ( index==-1 || DataManager.Instance.DicDataGetImg(index)==null)
		{
			showImg.color=new Color(255,255,255,0);
			showImg.sprite=null;
			showEffect.text=null;
			showCoin.text=null;
			DataManager.Instance.NowItemdata=-1;
			SetCheckBoxToPool();
			return;
		}
		ShowToPanel(index);
	}
	
	public void SetCheckBox(Transform itemTransform)
	{
		checkbox.SetParent(itemTransform);
		checkbox.localPosition=Vector3.zero;
		checkbox.localScale=Vector3.one;
	}
	public void SetCheckBoxToPool()
	{
		checkbox.SetParent(this.transform.parent.Find("ObjectPool"));
		checkbox.localPosition=Vector3.zero;
		checkbox.localScale=Vector3.one;
	}
	public static PanelManager Instance
    {
        get
        {
            if (instance==null)
            {
                GameObject go =GameObject.FindWithTag("MainManager");
                instance=go.GetComponent<PanelManager>();
            }
            return instance;
        }
    }
	
}
