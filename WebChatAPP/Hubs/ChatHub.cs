using ChoETL;
using CsvHelper;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebChatAPP.Data;
using WebChatAPP.Data.Interfaces;
using WebChatAPP.Data.Models;

namespace WebChatAPP.Hubs
{
    public class ChatHub : Hub
    {
        //depdency injection didn't work in here (SignalR typescript crashing).. why?
        //public readonly ApplicationDbContext _context;

        //public ChatHub(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        public async Task SendMessage(Message message)
        {
            if (message.Text.Contains("/stock="))
            {
                var result = message.Text.Substring(message.Text.LastIndexOf('=') + 1);

                await Clients.All.SendAsync("receiveMessage", new { message.UserName, message.Text, message.MessageDate });

                DateTime dateTime = DateTime.UtcNow.Date;


                //Hardcoded bot id b0fb7f6b-a583-4c6b-943e-5713fd681211

                var splitted = getStockData(result);

                if (splitted.Contains("N/D"))
                {
                    message.UserName = "RabbitMQ";
                    message.Text = "Quote not found";
                    message.MessageDate = dateTime;
                }
                else
                {
                    var csvobject = buildModel(splitted);

                    //how can i call the add record method without using injection in here???


                    //temporary solution, not clean..

                    //Helper helper = new Helper(message);
                    



                    //helper.insertrecord(message);

                    message.UserName = "RabbitMQ";
                    message.Text = csvobject.Symbol + " quote is " + "$" + csvobject.Open + " per share";
                    message.MessageDate = dateTime;
                    message.UserID = "b0fb7f6b-a583-4c6b-943e-5713fd681211";

                    //_context.Messages.Add(message);
                    //_context.SaveChanges();


                    //helper._context.Add(message);
                    //helper._context.SaveChanges();
                }

                await Clients.All.SendAsync("receiveMessage", new { message.UserName, message.Text, message.MessageDate });
            }
            else
            {
                await Clients.All.SendAsync("receiveMessage", new { message.UserName, message.Text, message.MessageDate });
            }
            
        }

        public List<string> getStockData(string result)
        {
            //list with data on the csv
            List<string> splitted = new List<string>();
            //pure csv 
            string fileList = GetCSV("https://stooq.com/q/l/?s="+result+"&f=sd2t2ohlcv&h&e=csv");
            
            string[] tempStr;

            tempStr = fileList.Split(',');

            foreach (string item in tempStr)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    splitted.Add(item);
                }
            }
            return splitted;
        }

        public CsvModel buildModel(List<string> splitted)
        {
            CsvModel csvmodel = new CsvModel
            {
                Symbol = splitted[7],
                Date = Convert.ToDateTime(splitted[8]),
                Time = splitted[9],
                Open = splitted[10],
                High = splitted[11],
                Low = splitted[12],
                Close = splitted[13],
                Volume = splitted[14]

            };

            return csvmodel;

        }
        public string GetCSV(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();

            return results;
        }
    }
}
