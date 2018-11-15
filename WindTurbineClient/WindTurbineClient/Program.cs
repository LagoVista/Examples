using OpenNETCF.MQTT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindTurbineClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new OpenNETCF.MQTT.MQTTClient("mqttdev.nuviot.com", 1883);
            client.Connect("statreader", "kevinw", "XXXX");
            client.Connected += Client_Connected;
            client.MessageReceived += Client_MessageReceived;
            Console.WriteLine("CTRL+C = ENd");
            Console.ReadKey();
        }

        private static void Client_MessageReceived(string topic, QoS qos, byte[] payload)
        {
            var msg = System.Text.ASCIIEncoding.ASCII.GetString(payload);
            Console.WriteLine(topic);
            Console.WriteLine(msg);
        }

        private static void Client_Connected(object sender, EventArgs e)
        {
            var client = sender as MQTTClient;
            client.Subscriptions.Add("#", QoS.FireAndForget);
            
        }
    }
}
