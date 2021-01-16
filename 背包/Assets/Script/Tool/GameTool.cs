using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

//游戏工具类
//会把一些被外界经常调用到的方法写在这个类里面
//为了方便外界调用，工具类里面的方法都是静态的
public class GameTool:MonoBehaviour
{
    //清理内存的方法（一般在场景切换的时候调用）
    public static void ClearMemory()
    {
       //垃圾回收自动调用的时间点具有不确定性，所有可以去预估，当某个时间点可能产生很多垃圾的时候，就可以手动去调用
       //垃圾回收机制不能频繁的去调用，会消耗很大的性能，频繁调用可能会卡顿
        GC.Collect();
        //卸载内存中没用的资源
        Resources.UnloadUnusedAssets();
    }
    //操作内存，数据持久化
    //判断系统里面是否有包含某个键
    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
    //去内存里面根据键来取值
    public static int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }
    public static float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }
    public static string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }
    //去系统里面存值
    public static void SetInt(string key,int value)
    {
        PlayerPrefs.SetInt(key,value);
    }
    public static void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }
    public static void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
    //在内存里面删除指定的数据（键值对）
    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }
    //删除内存里面所有的数据（键值对）
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
    //分割字符串
    public static string[] SplitString(string str, char c)
    {
        return str.Split(c);
    }
    //查找子物体
    public static Transform FindTheChild(GameObject goParent,string childName)
    {
      Transform searchTrans=  goParent.transform.Find(childName);
      if (searchTrans==null)
      {
          //遍历goParent的所有的一级子物体
          foreach (Transform trans in goParent.transform)
          {
              //递归操作
              searchTrans = FindTheChild(trans.gameObject, childName);
              if (searchTrans!=null)
              {
                  return searchTrans;
              }
          }
      }
      return searchTrans;
    }
    //获取子物体上面的组件（T代表泛型）
    public static T GetTheChildComponent<T>(GameObject goParent, string childName) where T:Component
    {
         Transform searchTrans= FindTheChild(goParent, childName);
         if (searchTrans != null)
         {
             return searchTrans.GetComponent<T>();
         }       
         return null;      
    }
    //给子物体添加组件
    public static T AddTheChildComponent<T>(GameObject goParent, string childName) where T : Component
    {
        Transform searchTrans = FindTheChild(goParent, childName);
        if (searchTrans != null)
        {
            T[] arr = searchTrans.GetComponents<T>();
            if (arr.Length > 0)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    DestroyImmediate(arr[i], true);
                }
            }
            return searchTrans.gameObject.AddComponent<T>();
        }
     
       return null;
       
    }
    //添加子物体
    public static void AddChildToParent(Transform parentTrans,Transform childTrans)
    {
        childTrans.SetParent(parentTrans);
        childTrans.localPosition = Vector3.zero;
        childTrans.localScale = Vector3.one;
    }
    private static GameTool instance;
     public static GameTool Instance
    {
        get
        {
            if (instance==null)
            {
                GameObject go =GameObject.FindWithTag("MainManager");
                instance=go.GetComponent<GameTool>();
            }
            return instance;
        }
    }

   //挂在Unity的单例物体
    // public static GameObject unitySingletonObj=null;
    // public static T AddUnitySingleton<T>() where T : UnitySingleton<T>
    // {
    //     return unitySingletonObj.AddComponent<T>();
    // }
    // public static void RemoveUnitySingleton<T>() where T : UnitySingleton<T>
    // {
    //    T t=  unitySingletonObj.GetComponent<T>();
    //    if (t!=null)
    //    {
    //        DestroyImmediate(t);
    //    }
    // }
}
