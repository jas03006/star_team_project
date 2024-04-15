using System.Collections;
using System.Collections.Generic;

//클라이언트 요청 정보를 담는 class
public class Net_Request
{
    public Client_Handler client; //요청한 클라이언트
    public string msg; //요청의 원본 메세지
    public Net_Request(Client_Handler client_, string msg_)
    {
        client = client_;
        msg = msg_;
    }
}
