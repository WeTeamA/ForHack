using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Net;
using System.IO;

public class Substance
{
    public string name;
    public bool solubility;
    public double temp;
    public bool aggregate;

    public Substance(string Name, bool Solubility, double Temp, bool Aggregate)
    {
        name = Name;
        solubility = Solubility;
        temp = Temp;
        aggregate = Aggregate;
    }
}
public class Request : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        List <Substance> Substances = new List<Substance>();
        //Substances.Add(new Substance("asdf", true, 454, false));
        //APP NAME: ChemicalLab
        //APPID: K58ETV - GTPAJVATGW
        // var url = "http://api.wolframalpha.com/v2/query?input=NaCl&appid=K58ETV-GTPAJVATGW";
        get_http_write("http://api.wolframalpha.com/v2/query?input=NaCl&appid=K58ETV-GTPAJVATGW");
        var doc = new XmlDocument();
        doc.Load("C:/ForHack/ForHack/page.xml");
       //print(get_http("http://api.wolframalpha.com/v2/query?input=NaCl&appid=HP44EW-XHHUV3698T"));
    }

    /*
    //CookieContainer cookies = new CookieContainer();
    private string get_http(string url)
    {
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        //req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:17.0) Gecko/20100101 Firefox/17.0";
        //req.CookieContainer = cookies;
        //req.Headers.Add("DNT", "1");
        
        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        Stream stream = resp.GetResponseStream();
        StreamReader sr = new StreamReader(stream);
        resp.Close();
        string text = sr.ReadToEnd();
        sr.Close();
        return text;
        
    }
    */
    
    private void get_http_write(string url)
    {
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        //req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:17.0) Gecko/20100101 Firefox/17.0";
        //req.CookieContainer = cookies;
        //req.Headers.Add("DNT", "1");

        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        Stream stream = resp.GetResponseStream();
        StreamReader sr = new StreamReader(stream);
        //resp.Close();
        string text = sr.ReadToEnd();
       // sr.Close();
        using (var sw = new StreamWriter("page.xml"))
            sw.Write(text); //пишем на комп
        resp.Close();
        sr.Close();
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
