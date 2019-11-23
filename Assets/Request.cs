using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

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
    /// <summary>
    /// Если Solid -> true, Если Liquid -> false
    /// </summary>
    /// <param name="childnode"></param>
    /// <returns></returns>
    public bool Aggregate(XmlNode childnode)
    {
        Regex regex = new Regex(@"solid");
        MatchCollection matches = regex.Matches(childnode.InnerText);
        if (matches.Count > 0)
        {
            if (matches[0].Value == "solid")
                return true;
            else
            {
                return false;
            }
        }
        else
            return false;
    }

    /// <summary>
    /// Soluble -> true, Если Unsoluble -> false
    /// </summary>
    /// <param name="childnode"></param>
    /// <returns></returns>
    public bool Solubility(XmlNode childnode)
    {
        Regex regex = new Regex(@"soluble");
        MatchCollection matches = regex.Matches(childnode.InnerText);
        if (matches.Count > 0)
        {
            if (matches[0].Value == "soluble")
                return true;
            else
            {
                return false;
            }
        }
        else
            return false;
    }

    public double Temp(XmlNode childnode)
    {
        Regex regex = new Regex(@"\d*\s°C");
        MatchCollection matches = regex.Matches(childnode.InnerText);
        if (matches.Count > 0)
        {
            string x = matches[0].Value;
            int x1 = x.Length - 3;
            x = x.Remove(x1);
            return double.Parse(x);
        }
        else
            return 100;
    }

    public void get_http_write(string url)
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

    public void FillArray(List<Substance> Subs)
    {
        var doc = new XmlDocument();
        doc.Load("C:/ForHack/ForHack/page.xml");

        XmlElement xRoot = doc.DocumentElement;
        foreach (XmlNode childnode in xRoot)
        {
            if (childnode.Name == "pod" && childnode.Attributes.GetNamedItem("title").Value == "Basic properties")
            {
                foreach (XmlNode childnode2 in childnode.ChildNodes)
                {
                    if (childnode2.Name == "subpod")
                    {
                        foreach (XmlNode childnode3 in childnode.ChildNodes)
                        {
                            if (childnode3.Name == "subpod")
                            {
                                foreach (XmlNode childnode4 in childnode3.ChildNodes)
                                {
                                    if (childnode4.Name == "plaintext")
                                    {
                                        Subs.Add(new Substance(null, Solubility(childnode4), Temp(childnode4), Aggregate(childnode4)));
                                    }
                                }
                            }
                        }
                    }
                }
            }
    // Start is called before the first frame update
    void Start()
    {
        List<Substance> Substances = new List<Substance>();
        //APP NAME: ChemicalLab
        //APPID: K58ETV - GTPAJVATGW
        // var url = "http://api.wolframalpha.com/v2/query?input=NaCl&appid=K58ETV-GTPAJVATGW";


        //get_http_write("http://api.wolframalpha.com/v2/query?input=NaCl&appid=K58ETV-GTPAJVATGW"); ЭТО ПОКА НЕ НУЖНОООООО





                /*
                for (int i = 0; i < Substances.Count; i++) //Заполнение массива
                {
                    //Substances[i].temp = 
                }
                //print(get_http("http://api.wolframalpha.com/v2/query?input=NaCl&appid=HP44EW-XHHUV3698T"));
                */
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


           

            // Update is called once per frame
            void Update()
            {

            }
        }
    }
}
