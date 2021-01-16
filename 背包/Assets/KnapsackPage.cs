using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KnapsackPage : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public static KnapsackPage instance;       //单例模式
    public ScrollRect rect;                     //ScrollView那个
    public Text pageInfo;                       //页表信息
    public float[] index;                       //页表
    public float smooth = 5.0f;

    public Transform[] boxes;
    public GameObject itemPref;                 

    private RectTransform view, content;
    private bool isDrag;                        //是否拖拽
    private int pageIndex = 0;                  //页面

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //计算页数以及对应的 horizontalNormalizedPosition 值
        view = this.transform.Find("Viewport").GetComponent<RectTransform>();
        content = this.transform.Find("Viewport/Content").GetComponent<RectTransform>();
        float pages = content.childCount / 12.0f;
        float step = 1.0f / (pages - 1);
        index = new float[(int)(pages)];
        Debug.Log(step);    
        index[0] = 0;
        for (int i = 1; i < pages; i++)
        {
            index[i] = index[i - 1] + step;
        }
        pageInfo.text = (pageIndex + 1).ToString() + "/" + ((int)pages).ToString();
        isDrag = false;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //向左拉， pageindex增加
        if (index[pageIndex] > rect.horizontalNormalizedPosition)
        {
            pageIndex = pageIndex + 1 >= index.Length ? index.Length - 1 : pageIndex + 1;
        }
        else
        {
            pageIndex = pageIndex - 1 < 0 ? 0 : pageIndex - 1;
        }
        //计算释放时最近的页数对应的 horizontalNormalizedPosition
        float minDis = Mathf.Abs(index[pageIndex] - rect.horizontalNormalizedPosition);
        for (int i = 0; i < index.Length; i++)
        {
            if (minDis > Mathf.Abs(index[i] - rect.horizontalNormalizedPosition))
            {
                minDis = Mathf.Abs(index[i] - rect.horizontalNormalizedPosition);
                pageIndex = i;
            }
        }
        isDrag = false;
    }
    public void OnLeftBtnClicked()
    {
        pageIndex = pageIndex - 1 < 0 ? 0 : pageIndex - 1;
    }
    public void OnRightBtnClicked()
    {
        pageIndex = pageIndex + 1 >= index.Length ? index.Length - 1 : pageIndex + 1;
    }
    void Update()
    {
        //插值运算，达到缓动的目的
        //if (isDrag == false && Mathf.Abs(rect.horizontalNormalizedPosition - index[pageIndex]) > 0.0015f)
        //{
        //    rect.horizontalNormalizedPosition = Mathf.Lerp(rect.horizontalNormalizedPosition, index[pageIndex],
        //        Time.deltaTime * smooth);
        //    if (Mathf.Abs(rect.horizontalNormalizedPosition - index[pageIndex]) < 0.0015f)
        //    {
        //        rect.horizontalNormalizedPosition = index[pageIndex];
        //        pageInfo.text = (pageIndex + 1).ToString() + "/" + ((int)index.Length).ToString();
        //    }
        //}
    }

}
