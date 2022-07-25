using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ConsoleApp4
{

    public class LatestSlotData
    {
        public static DateTime DateTime { get; set; }
        public static string Consulate { get; set; }
    }

    public static class CheckVisaSlotRequestHandler
    {

        public const string AccessCode = "3JRXQ1";
        public static CheckMyVisaSlotResponse Get()
        {
            return JsonConvert.DeserializeObject<CheckMyVisaSlotResponse>(_Get());
        }

        private static string _Get()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://app.checkvisaslots.com/slots/v1");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Headers.Add("origin", "chrome-extension://beepaenfejnphdgnkmccjcfiieihhogl");
            request.Headers.Add("sec-ch-ua", "\" Not A;Brand\";v=\"99\", \"Chromium\";v=\"102\", \"Google Chrome\";v=\"102\"");
            request.Headers.Add("sec-ch-ua-mobile", "?1");
            request.Headers.Add("sec-ch-ua-platform", "\"Android\"");
            request.Headers.Add("x-api-key", AccessCode);
            request.UserAgent =
                "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/102.0.0.0 Mobile Safari/537.36";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        public static void Send()
        {
            using var httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            var bytefile = AuthGetFileData("2022-06-23T12.22.43.png");
            var fileContent = new ByteArrayContent(bytefile);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");
            form.Add(fileContent, "input", DateTime.Now.ToString("s").Replace(':', '.') + ".png");
            form.Add(new StringContent("Visa InformationCHENNAIStudents and Exchange VisitorsF-1"), "visaDetails");
            form.Add(new StringContent("Logged in as  memyselfandiseries@gmail.com (42288016)"), "userDetails");
            form.Add(new StringContent("No appointment is currently scheduled."), "apntDetails");
            form.Add(new StringContent("{\"top\":177,\"right\":1206.6000366210938,\"bottom\":549,\"left\":559.6000366210938,\"width\":647,\"height\":372,\"x\":559.6000366210938,\"y\":177,\"pixel_scale\":2.5}"), "boundaries");
            form.Add(new StringContent("[{\"location\":\"CHENNAI VAC\"}]"), "slotDetails");
            form.Add(new StringContent("[]"), "appointmentTimes");
            httpClient.DefaultRequestHeaders.Add("x-api-key", "3JRXQ1");
            var x = httpClient.PostAsync("https://app.checkvisaslots.com/push/v3", form).Result;
        }
        public static byte[] AuthGetFileData(string fileUrl)
        {
            using (FileStream fs = new FileStream(fileUrl, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] buffur = new byte[fs.Length];
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(buffur);
                    bw.Close();
                }
                return buffur;
            }
        }
    }
}
