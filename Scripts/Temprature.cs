using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public class Temprature : MonoBehaviour
{
    public string msg;
    MqttClient client;
    string clientId;
    // Start is called before the first frame update
    void Start()
    {
        string Topic = "sensor/temp";
        string BrokerAddress = "192.168.43.248";
        client = new MqttClient(BrokerAddress);
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
        clientId = Guid.NewGuid().ToString();

        client.Connect(clientId, "dedsec995", "amadeus");
        Debug.Log(client.Subscribe(new string[] { Topic }, new byte[] { 2 }));


        
    }
    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        msg = System.Text.Encoding.UTF8.GetString(e.Message);
        Debug.Log("Received message from " + e.Topic + " : " + msg);
    //  GetComponent<TextMesh>().text = msg;
    }
    // Update is called once per frame
    void Update()
    {
        string Topic = "hab";

        // subscribe to the topic with QoS 2
        client.Subscribe(new string[] { Topic }, new byte[] { 2 });   // we need arrays as parameters because we can subscribe to different topics with one call
       // GetComponent<TextMesh>().text = msg;
       GetComponent<TextMesh>().text = msg;

    }
}
