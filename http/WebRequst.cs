using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
public enum NetWorkState
{
    NETWORK_WARNING,//警告
    NETWORK_ERROE,//连接错误
    NETWORK_SUCCESS//连接成功
}
public class WebRequest : MonoBehaviour
{
    public static WebRequest instance;
    //readonly string baseUrl = "http://121.199.62.111:4001/";
    readonly string baseUrl = "http://localhost:4001/";
    //readonly string baseUrl= "https://connect-mongodb.vercel.app/";
    public IEnumerator Post(string url,Hashtable map,Action<string>callpack)
    {
        Debug.Log("请求服务器");
        var request = new UnityWebRequest(baseUrl+url, "post");
        //Hashtable data = new Hashtable();
        Dictionary<string,string> data= new Dictionary<string,string>();
        foreach (DictionaryEntry entry in map)
            data[(string)entry.Key] = (string)entry.Value;

        //string js=JsonUtility.ToJson(data);;
        string js = JsonUtility.ToJson(new Serialization<string,string>(data));
        byte[] dataBytes = Encoding.UTF8.GetBytes(js);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(dataBytes);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        if (request.responseCode == 200)
            callpack(request.downloadHandler.text);
        else
            callpack("连接失败");
    }
    public void SaveDate()
    {

        Hashtable param = new Hashtable();
        param["rank"] = Map.instance.map["lv"].ToString();
        param["health"] = Map.instance.map["health"].ToString();
        param["magic"] = Map.instance.map["magic"].ToString();
        param["skillsNum"] = Map.instance.map["skillsNum"].ToString();
        param["attack"] = Map.instance.map["attack"].ToString();
        param["defense"] = Map.instance.map["defense"].ToString();
        param["cirt"] = (Map.instance.map["cirt"]*100).ToString();
        param["cirtEffect"] = (Map.instance.map["cirtEffect"]*100).ToString();
        StartCoroutine(WebRequest.instance.Post("unity_saveDate/", param, NullString));
    }
    public void NullString(string s) { }
    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

}
