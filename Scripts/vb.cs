using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

using System;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;


public class vb : MonoBehaviour, IVirtualButtonEventHandler
{

    MqttClient client;
    string clientId;


    public GameObject virtualbutton;
    public bool horse = false;

    public GameObject Virtualbutton
    {
        get
        {
            return virtualbutton;
        }

        set
        {
            virtualbutton = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        virtualbutton = GameObject.Find("butt");
        virtualbutton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        string BrokerAddress = "192.168.43.248";

        client = new MqttClient(BrokerAddress);

        // register a callback-function (we have to implement, see below) which is called by the library when a message was received
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

        // use a unique id as client id, each time we start the application
        clientId = Guid.NewGuid().ToString();

        client.Connect(clientId, "dedsec995", "amadeus");

        string Topic = "sensor/522";

        // subscribe to the topic with QoS 2
        Debug.Log(client.Subscribe(new string[] { Topic }, new byte[] { 2 }));   // we need arrays as parameters because we can subscribe to different topics with one call
                                                                                 //  txtReceived.Text = "";
    }

    // private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    // {
    //     throw new NotImplementedException();
    // }



    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {




        if (horse == false)
        {
            Debug.Log("OFF");
            string Topic = "hub";

            // publish a message with QoS 2
            client.Publish(Topic, Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        }
        else
        {
            Debug.Log("ON");
            string Topic = "hub";

            // publish a message with QoS 2
            client.Publish(Topic, Encoding.UTF8.GetBytes("1"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
           // string msg = System.Text.Encoding.UTF8.GetString(e.Message);
        //Debug.Log ("Received message from " + e.Topic + " : " + msg);
        }
    }
    
    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
	{
        //string Topic = "hub";
		string msg = System.Text.Encoding.UTF8.GetString(e.Message);
        Debug.Log ("Received message from " + e.Topic + " : " + msg);
	}
    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        if (horse == true)
        {
            horse = false;
        }
        else
        {
            horse = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        string Topic = "hub/listen  ";

        // subscribe to the topic with QoS 2
        client.Subscribe(new string[] { Topic }, new byte[] { 2 });   // we need arrays as parameters because we can subscribe to different topics with one call
    }
}