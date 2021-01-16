using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCom  {
	
	private int ItemComId;
	private   Text txt ;
	private   Image image;
	private   Button button;
	private   Transform thistransform;
	
	public  ItemCom(Transform transform,int i)
	{
		ItemComId=i;
		thistransform=transform;
		txt=transform.Find("Itemtxt").GetComponent<Text>();
		image= transform.GetComponent<Image>();
		button=transform.GetComponent<Button>();

		button.onClick.AddListener(UpdataData);

	}
	private void UpdataData()										
	{
		DataManager.Instance.NowItemdata=ItemComId;					//传入编号
		PanelManager.Instance.SetCheckBox(thistransform);			//找到选中框
		PanelManager.Instance.UpdataPanel(ItemComId);				//重置画面
	}

	public void SetData(ItemData itemData)
	{	
		CleanData();
		txt.text=itemData.Count.ToString();
		image.sprite=itemData.Sprite;
		IsHied();
	}
	
	public void CleanData()
	{
		txt.text=null;		
		image.sprite =null;
	}

	public void Hide()
	{
		thistransform.gameObject.SetActive(false);
	}
	public void Show()
	{
		thistransform.gameObject.SetActive(true);
	}
	public void IsHied()
	{
		if (txt.text==0.ToString())
		{
			Hide();
			DataManager.Instance.DicDataResert(ItemComId);
		}
		else
		{
			Show();
		}
	}

}
