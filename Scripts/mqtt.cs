using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public class mqtt : MonoBehaviour {
    private MqttClient client;
    // The connection information
    public string brokerHostname = "192.168.43.248";
    public int brokerPort = 1883;
    public string userName = "dedsec995";
    public string password = "amadeus";
    //public TextAsset certificate;
    // listen on all the Topic
    static string subTopic = "#";

    private void Connect()
    {
        Debug.Log("about to connect on '" + brokerHostname + "'");
        // Forming a certificate based on a TextAsset
        X509Certificate cert = new X509Certificate();
        //cert.Import(certificate.bytes);
        Debug.Log("Using the certificate '" + cert + "'");
        client = new MqttClient(brokerHostname, brokerPort, true, cert, null, MqttSslProtocols.TLSv1_0, MyRemoteCertificateValidationCallback);
        string clientId = Guid.NewGuid().ToString();
        Debug.Log("About to connect using '" + userName + "' / '" + password + "'");
        try
        {
            client.Connect(clientId, userName, password);
        }
        catch (Exception e)
        {
            Debug.LogError("Connection error: " + e);
        }
    }

    public static bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string msg = System.Text.Encoding.UTF8.GetString(e.Message);
        Debug.Log("Received message from " + e.Topic + " : " + msg);
    }

    public void Publish(string _topic, string msg)
    {
        client.Publish(
            _topic, Encoding.UTF8.GetBytes(msg),
            MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
    }
    // Use this for initialization
    void Start () {
        // if (brokerHostname != null && userName != null && password != null)
        // {
        //     Debug.Log("connecting to " + brokerHostname + ":" + brokerPort);
        //     Connect();
        //     client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
        //     byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE };
        //     client.Subscribe(new string[] { subTopic }, qosLevels);
        // }
        // Publish("Dev/test", "Hello from usnig");
        Debug.Log("Published");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
