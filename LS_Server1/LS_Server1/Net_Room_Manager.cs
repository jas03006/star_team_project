using System.Collections;
using System.Collections.Generic;

public class Net_Room_Manager
{
    private Dictionary<string, Net_Room_Info> room_dict; // host_id, net_room_info

    public Net_Room_Manager()
    {
        room_dict = new Dictionary<string, Net_Room_Info>();
    }

    public bool join_room(Client_Handler client, string host_id)
    {
        Net_Room_Info net_room_info = null;
        if (room_dict.ContainsKey(host_id))
        {
            net_room_info = room_dict[host_id];
        }
        if (net_room_info == null)
        {
            net_room_info = create_room(host_id, client);
            room_dict[host_id] = net_room_info;
        }
        else{
            net_room_info.add_client(client);
        }
        return true;
    }

    public void remove_from_room(Client_Handler client, string host_id)
    {
        if (host_id == "-") {
            return;
        }
        Net_Room_Info net_room_info = null;
        if (room_dict.ContainsKey(host_id))
        {
            net_room_info = room_dict[host_id];
        }
        if (net_room_info == null)
        {
            return;
        }
        else
        {            
            net_room_info.remove_client(client);
            if (net_room_info.is_empty()) {
                room_dict.Remove(host_id);
            }
        }
    }

    public Net_Room_Info create_room(string host_id, Client_Handler client)
    {
        Net_Room_Info net_room_info = new Net_Room_Info(host_id, client);
        return net_room_info;
    }

    public void room_RPC(string host_id, string msg)
    {
        if (host_id == "-") {
            return;
        }
        Net_Room_Info net_room_info = null;
        if (room_dict.ContainsKey(host_id))
        {
            net_room_info = room_dict[host_id];
        }
        if (net_room_info != null)
        {
            net_room_info.every_RPC(msg);
            //net_room_info.guest_RPC(msg);
        }
        else {
            Console.WriteLine($"No Room: {host_id}");
        }
        
    }
    public void room_host_RPC(string host_id, string msg)
    {
        Net_Room_Info net_room_info = null;
        if (room_dict.ContainsKey(host_id))
        {
            net_room_info = room_dict[host_id];
        }
        if (net_room_info != null)
        {
            net_room_info.host_RPC(msg);
            //net_room_info.guest_RPC(msg);
        }
    }

    public string get_people_positions(string host_id)
    {

        if (room_dict.ContainsKey(host_id))
        {
            Net_Room_Info net_room_info = room_dict[host_id];
            return net_room_info.get_people_positions();
        }
        return string.Empty;
    }
}
