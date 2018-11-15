using OpenNETCF.MQTT;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindTurbineClient
{
    public class Program
    {
        static void Main(string[] args)
        {

            var client = new OpenNETCF.MQTT.MQTTClient("cyclone-mqtt.iothost.net", 1883);
            client.Connect("statreader", "cyclone", "XXXXXXX");
            client.Connected += Client_Connected;
            client.MessageReceived += Client_MessageReceived;
            Console.WriteLine("CTRL+C = ENd");
            Console.ReadKey();
        }

        private static void Client_MessageReceived(string topic, QoS qos, byte[] payload)
        {
            if (topic.StartsWith("turbine/status"))
            {
                var msg = System.Text.ASCIIEncoding.ASCII.GetString(payload);
                var obj = JsonConvert.DeserializeObject<StatusMessage>(msg);
                Console.WriteLine($"Turbine Id: {obj.TurbineId}, RPM: {obj.RPM}, Power: {obj.Power}");
            }
            else if(topic.StartsWith("turbine/power"))
            {
                var parts = topic.Split('/');
                if (parts.Length == 4)
                {
                    var turbineId = parts[2];
                    var powerStatus = parts[3];
                    Console.WriteLine($"Turbine Id: {turbineId}, Status: {powerStatus}");
                }
            }
        }

        private static void Client_Connected(object sender, EventArgs e)
        {
            var client = sender as MQTTClient;
            client.Subscriptions.Add("#", QoS.FireAndForget);
            
        }
    }

    class StatusMessage
    {
        [JsonProperty("turbineId")]
        public String TurbineId { get; set; }
        [JsonProperty("rpm")]
        public double RPM { get; set; }
        [JsonProperty("power")]
        public double Power { get; set; }
        [JsonProperty("timeStamp")]
        public string TimeStamp { get; set; }


    }
}
