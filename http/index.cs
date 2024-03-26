using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class index : MonoBehaviour
{
    public static index instance;
    public string username;
    public string password;
    public string passwordVerify;
    public string phone;
    public string qq;
    public Hashtable param;
    public GameObject login;
    public GameObject regist;
    public GameObject Tips;
    public bool isLogin = true;
    // Start is called before the first frame update
    public Dictionary<string, int> scene = new Dictionary<string, int>()
    {
        {"login",0 },
        {"home",1 },
        {"game1",2 }
    };
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (login == null)
            return;
        if (isLogin)
        {
            phone = login.transform.GetChild(0).GetComponent<InputField>().text;
            password = login.transform.GetChild(1).GetComponent<InputField>().text;
        }
        else
        {
            username = regist.transform.GetChild(0).GetComponent<InputField>().text;
            phone = regist.transform.GetChild(1).GetComponent<InputField>().text;
            qq = regist.transform.GetChild(2).GetComponent<InputField>().text;
            password = regist.transform.GetChild(3).GetComponent<InputField>().text;
            passwordVerify = regist.transform.GetChild(4).GetComponent<InputField>().text;
        }
    }
    //public void SaveDate()


    public void Login()
    {
        Hashtable param = new Hashtable();
        param["phone"] = phone;
        param["password"] = password;
        StartCoroutine(WebRequest.instance.Post("unity_login/", param, LoginAnsShow));
    }
    void LoadHome()
    {
        SceneManager.LoadScene(scene["home"]);
    }
    void LoginAnsShow(string s)
    {
        if (s == "ok")
        {
            Debug.Log("success"); ShowTips("��¼�ɹ�");
            Invoke("LoadHome", 2f);
        }

        else if (s == "wu")
        {
            Debug.Log("error");
            ShowTips("��¼ʧ��,���޴��û�,��ע��");
        }
        else if (s == "wrong")
        {
            ShowTips("�����������������");
        }
        else
            ShowTips("����ʧ��,����й���Ա");
    }

    
    public void Regist()
    {
        int x;
        Hashtable param = new Hashtable();
        if (phone == "" || password == "" || username == "" || passwordVerify == "" || qq == "")
            ShowTips("����������");
        else if (!int.TryParse(phone.Substring(0,5), out x)|| !int.TryParse(phone.Substring(5), out x) || phone.Length != 11)
        {
            ShowTips("��������ȷ���ֻ���");
        }
            
        else if (!int.TryParse(qq.Substring(0,5), out x) || !int.TryParse(qq.Substring(0, 5), out x) || qq.Length < 5)
            ShowTips("��������ȷ��QQ");
        else if (password != passwordVerify)
            ShowTips("�������벻һ��");
        else
        {
            param["phone"] = phone;
            param["password"] = password;
            param["username"] = username;
            param["qq"] = qq;
            StartCoroutine(WebRequest.instance.Post("unity_regist/", param, RegistAnsShow));
        }
    }
    void RegistAnsShow(string s)
    {
        if (s == "ok")
            ShowTips("��ӳɹ�");
        else if (s == "had")
            ShowTips("�˺����ѱ�ע��");
        else
            ShowTips("����ʧ��");
    }
    public void ShowTips(string s)
    {
        Tips.transform.GetChild(0).GetComponent<Text>().text = s;
        Tips.SetActive(true);
        Invoke("CloseTips", 1.5f);

    }
    public void CloseTips()
    {
        Tips.SetActive(false);
    }
    public void ChangeModel()
    {
        isLogin = !isLogin;
        login.SetActive(isLogin);
        regist.SetActive(!isLogin);
    }
}
