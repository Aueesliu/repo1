using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateMesh : MonoBehaviour {

    private int boxcount=12;        //物品栏
    private int boxId=0;           //物品栏id
    private  Dictionary<int,Transform> dicBox=new Dictionary<int, Transform>();
    public   Dictionary<int ,Button > dicBoxBtn=new Dictionary<int, Button>();
    private int x=70;               
    private int y=-50;
    private int high = 50;          //content长度   
    private int Xreal;   
    private GameObject box;
    private GameObject item; 
    private GameObject  checkbox;
    private Transform parentTranform;
    private RectTransform contentParent;
    private static CreateMesh instance;
    //按钮
    
    void Start()
    {
        item = Resources.Load<GameObject>("Item");
        box = Resources.Load<GameObject>("Box");
        parentTranform = GameObject.Find("SvPanel/Viewport/Content").transform;
        contentParent = parentTranform.GetComponent<RectTransform>();
        
        CreatBox(boxcount);                                          //先创建背包格子
        PanelManager.Instance.CreateItem();                          //再创建物品
        
    }

    public static CreateMesh Instance
    {
        get
        {
            if (instance==null)
            {
                GameObject go =GameObject.FindWithTag("MainManager");
                instance=go.GetComponent<CreateMesh>();
            }
            return instance;
        }
    }

    private void CreatBox(int a)                                //可优化
    {
        int line = a / 3;
        int remain = a % 3;
        
        for (int i = 0; i < line; i++)
        {
            Xreal =x;
            for (int j = 0; j < 3; j++)
            {
                SetMesh();
                Xreal += 480;
            }
            y -= 450;
            high += 450;
        }
        if (remain!=0)
        {
            Xreal =x;
            for (int j = 0; j < remain; j++)
            {
                SetMesh();
                Xreal += 480;
            }
            high += 450;
        }
        if (remain==0)
        {
            y +=450;
        }
        contentParent.sizeDelta = new Vector2(1500, high); 
    }
    public GameObject SetMesh()
    {
        GameObject boxObj = Instantiate(box);
        Button button=boxObj.GetComponent<Button>();
        boxObj.transform.SetParent(parentTranform);
        boxObj.transform.localPosition = new Vector3(Xreal, y, 0);
        dicBox.Add(boxId,boxObj.transform);
        dicBoxBtn.Add(boxId,button);
        boxId++;
        return boxObj;
    }

    public GameObject AddBox()
    {
        switch (parentTranform.childCount%3)
            {
                case 0:
                    y -= 450;
                    Xreal =x;              
                    high += 450;
                    contentParent.sizeDelta = new Vector2(1500, high);
                    return SetMesh();
                case 1:
                    Xreal=x+480;
                    return SetMesh();
                case 2:
                    Xreal =x+480*2;
                    return SetMesh();
                default:
                    Debug.LogError("无法生成box");
                    return null;
            }
    }


}
