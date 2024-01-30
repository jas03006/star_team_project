using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net_Room_Info
{
    public int host_id;
    public List<Client_Handler> guest_list;
    public Client_Handler host;
    public string data;


    public Net_Room_Info(int host_id_, Client_Handler client)
    {

        host_id = host_id_;
        guest_list = new List<Client_Handler>();
        add_client(client);
    }

    public void add_client(Client_Handler client) {
        if (client.id == host_id)
        {
            host = client;
            return;
        }
        guest_list.Add(client);
    }
    public void remove_client(Client_Handler client)
    {
        if (client.id == host_id) {
            host = null;
            return;
        }
        guest_list.Remove(client);
    }

    public void every_RPC(string msg) {
        if (host !=null) {
            host.writer.WriteLine(msg);
        }        
        guest_RPC(msg);
    }
    public void guest_RPC(string msg) {
        for (int i =0; i < guest_list.Count; i++) {
            guest_list[i].writer.WriteLine(msg);
        }
    }

    
}
