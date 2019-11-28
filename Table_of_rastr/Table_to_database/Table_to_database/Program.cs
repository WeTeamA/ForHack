using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace ConsoleApp8
{
    class Program
    {
        static void Main(string[] args)
        {
            string URL = "http://www.hemi.nsu.ru/rastab.htm";
            if (URL == "" || URL == " ")
            {
                Console.WriteLine("Невозможно отправить GET запрос к адресу", URL);
            }
            else
            {
                //Отправляем запрос,где textBox1 - строка с адресом

                System.Net.WebRequest reqGET = System.Net.WebRequest.Create(URL);
                System.Net.WebResponse resp = reqGET.GetResponse();
                System.IO.Stream stream = resp.GetResponseStream();
                //Получаем ответ в переменную sr и считываем его до конца
                System.IO.StreamReader sr = new System.IO.StreamReader(stream, Encoding.Default);
                string s = sr.ReadToEnd();
                //Выводим всю лабуду в richTextBox1
                URL = s;
            }
            Console.WriteLine(URL);
           // Console.ReadLine();

            // Строка для хранения результата
            string[] storageForResponse = new ;
            // Читаем HTML-код сайта
            string html = URL;
            // Разбиваем строку на массив строк
            string[] words = html.Split(new[] { ' ', ',', ':', '?', '\n', '\t', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] == "<tr>")
                {
                    if (i < words.Length - 1)
                    {
                        storageForResponse[i] += words[i + 1];
                    }
                    break;
                }
            }

            Console.WriteLine("Начал отражать строчный массив"+ "\n" + "\n");

            for (int i = 0; i < storageForResponse.Length; i++)
            {
                Console.WriteLine(storageForResponse[i] + "\n");

            }
            Console.ReadLine();
        }
    }
}
