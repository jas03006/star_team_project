using System.Collections;
using System.Collections.Generic;
using System.Numerics;

//마이플래닛 방의 정보를 표현하는 class
public class Net_Room_Info
{
    public string host_id;
    public List<Client_Handler> guest_list;
    public Client_Handler host;
    public string data; //기타 정보를 담을 string (위치 정보)


    public Net_Room_Info(string host_id_, Client_Handler client)
    {

        host_id = host_id_;
        guest_list = new List<Client_Handler>();
        add_client(client);
    }

    public bool is_empty() {
        if (host != null) {
            return false;
        }
        if (guest_list.Count != 0) {
            return false;
        }
        return true;
    }

    public void add_client(Client_Handler client)
    {
        client.room_id = host_id;
        if (client.uuid == host_id)
        {
            host = client;
            return;
        }
        guest_list.Add(client);
    }
    public void remove_client(Client_Handler client)
    {
        Console.WriteLine($"room {host_id}: remove {client.uuid}");
        if (client.uuid == host_id)
        {
            host = null;
            client.room_id = "-";
            string msg = $"{(int)command_flag.exit} {host_id} {client.uuid}";
            every_RPC(msg);
            return;
        }
        if (guest_list.Remove(client)){
            client.room_id = "-";
            string msg = $"{(int)command_flag.exit} {host_id} {client.uuid}";
            every_RPC(msg);
        }        
    }

    public void every_RPC(string msg)
    {
        host_RPC(msg);
        guest_RPC(msg);
    }
    public void host_RPC(string msg)
    {
        if (host != null)
        {
            host.writer.WriteLine(msg);
        }
    }
    public void guest_RPC(string msg)
    {
        for (int i = 0; i < guest_list.Count; i++)
        {
            guest_list[i].writer.WriteLine(msg);
        }
    }

    public string get_people_positions()
    { // uuid:x:y:z|uuid:x:y:z|...
        string data = "";
        if (host != null)
        {
            data += host.uuid + ":" + host.position.X + ":" + host.position.Y + ":" + host.position.Z + "|";
        }
        for (int i = 0; i < guest_list.Count; i++)
        {
            data += guest_list[i].uuid + ":" + guest_list[i].position.X + ":" + guest_list[i].position.Y + ":" + guest_list[i].position.Z + "|";
        }
        return data;
    }


}
