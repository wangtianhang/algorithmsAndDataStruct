﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

class TestWWW
{
    public static void Test()
    {
        CoroutineMgr coroutineMgr = new CoroutineMgr();
        coroutineMgr.StartCoroutine(coroutineMgr.TestWWWCoroutine());
        while (true)
        {
            coroutineMgr.Update();
            Thread.Sleep(33);
        }
    }


}

class CoroutineMgr
{
    public IEnumerator TestWWWCoroutine()
    {
        WWW www = new WWW("http://www.baidu.com");
        yield return www;

        Debug.Log("TestWWWCoroutine end");
    }

    public class CoroutineData
    {
        public IEnumerator m_coroutine = null;
        public System.Object m_current = null;
    }

    List<CoroutineData> m_coroutineList = new List<CoroutineData>();
    public void StartCoroutine(IEnumerator coroutine)
    {
        CoroutineData coroutineData = new CoroutineData();
        coroutineData.m_coroutine = coroutine;
        m_coroutineList.Add(coroutineData);
    }

    public void Update()
    {
        for (int i = m_coroutineList.Count - 1; i >= 0; --i )
        {
            CoroutineData iter = m_coroutineList[i];
            if(iter.m_current == null)
            {
                if (iter.m_coroutine.MoveNext())
                {
                    iter.m_current = iter.m_coroutine.Current;
                }
                else
                {
                    m_coroutineList.RemoveAt(i);
                }
            }
            else
            {
                if (iter.m_current is WWW)
                {
                    WWW current = iter.m_current as WWW;
                    if (current.isDone)
                    {
                        if (iter.m_coroutine.MoveNext())
                        {
                            iter.m_current = iter.m_coroutine.Current;
                        }
                        else
                        {
                            m_coroutineList.RemoveAt(i);
                        }
                    }
                    else
                    {
                        // 等待www完成
                    }
                }
                else
                {
                    // 暂时只有www一种
                }
            }
        }
    }
}

class WWW
{
    public class WebCommunication : WebClient
    {
        public int m_editorTimeOut = 3;

        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);

            request.Timeout = 1000 * 10;
            request.ReadWriteTimeout = 1000 * 10;

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            return request;
        }
    }

    WebCommunication m_webClient = null;
    bool m_isDone = false;
    string m_text = null;

    public WWW(string url)
    {
        m_webClient = new WebCommunication();

        m_webClient.DownloadDataCompleted += UploadDataCompletedEventHandler;
        m_webClient.Credentials = CredentialCache.DefaultCredentials;

        Uri uri = new Uri(url);
        m_webClient.DownloadDataAsync(uri, url);
    }

    void UploadDataCompletedEventHandler(object sender, DownloadDataCompletedEventArgs e)
    {
        string url = sender as string;

        if (e.Error == null
            || e.Error.Message == null
            || e.Error.Message == "")
        {
            m_text = System.Text.Encoding.UTF8.GetString(e.Result);
            Debug.Log("UploadDataCompletedEventHandler " + m_text);
        }
        else
        {
            string errorMsg = "";
            if (e.Error != null && e.Error.Message != null)
            {
                errorMsg = e.Error.Message;
            }
            Debug.LogError("UploadDataCompletedEventHandler error " + url + " " + errorMsg);
        }

        m_isDone = true;
    }

    public bool isDone
    {
        get 
        {
            return m_isDone;
        }
    }

    public string text 
    {
        get 
        { 
            return m_text; 
        }
    }
}

