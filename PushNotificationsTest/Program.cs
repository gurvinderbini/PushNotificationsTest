using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PushNotificationsTest
{
    class Program
    {
       
        static void Main(string[] args)
        {
            string token = "fSFxGslRjLU:APA91bHzTT4sk5cwVpYOPAmlzzScIiB_UxoCuw_O0FoB1hDX7gnM48dYLY4ikqqzDpstOhDVrb5HtumsEnGQ-A6vBAspujTDP0OKq52cbp86ervyQGtb6Ex48346utnBfN5ihzePNcdZ";
            new PushNotifications().SendPushNotification(token);
            Console.ReadKey();
        }
    }

    public  class PushNotifications
    {
        string ServerKey = "AAAA39zcNCM:APA91bHMENlXY3HEUFFO9ZgK-QK1bJh7_YqdqOMkYWO6rC-YTrsxdvezmJtUUCsrvWXSKugPouoDGs5ZTnIOVvw29LYcqWxbWql3Qi6YZ0DzY2VDC-GtxINx0lOfxGRTRSLkptPTz4to";
        public async Task SendPushNotification(string token)
        {

            var jGcmData = new JObject();
            var jData = new JObject();

            jData.Add("message", "yoo");
            jGcmData.Add("to", token);
            jGcmData.Add("data", jData);
            var url = new Uri("https://fcm.googleapis.com/fcm/send");
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.TryAddWithoutValidation(
                        "Authorization", "key=" + ServerKey);

                    Task.WaitAll(client.PostAsync(url,
                            new StringContent(jGcmData.ToString(), Encoding.Default, "application/json"))
                        .ContinueWith(response =>
                        {
                            Console.WriteLine(response);
                            Console.WriteLine("Message sent: check the client device notification tray.");
                        }));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to send GCM message:");
                Console.Error.WriteLine(e.StackTrace);
            }
        }

    }
}
