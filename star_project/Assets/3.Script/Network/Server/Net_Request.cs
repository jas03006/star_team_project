using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net_Request
{
    public Client_Handler client;
    public string msg;
    public Net_Request(Client_Handler client_, string msg_)
    {
        client = client_;
        msg = msg_;
    }
}
