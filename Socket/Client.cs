using System.Runtime.Serialization.Json;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class Client
{
    public static bool ifrun = true;
    public static string name;
    public static Socket _ClientSocket;                       //客户端通讯套接字
    public static IPEndPoint SeverEndPoint;                   //连接到服务器端IP和端口

    public const int HEADLEN = 3;
        
    public static int REGISTEREDTYPE = 1;
    public static int LOGINTYPE = 2;
    public static int OPERATETYPE = 3;
    public static int POSSYNTYPE = 4;
    public static int FRAMHEADTYPE = 5;
    public static int FRAMOPTYPE = 6;
    public static int FRAMPACKAGETYPE = 7;


    public static int MOVETYPE = 1;
    public static int SKILLTYPE = 2;

    public static List<byte> dataList;
    public static Queue<ValueTuple<int,byte[]>> dataQueue;

    public static void Init(IPEndPoint serverEndPoint = null)
    {
        name = "";
        dataList = new List<byte>();
        dataQueue = new Queue<(int, byte[])>();
        //服务器通信地址
        if(serverEndPoint==null)
            SeverEndPoint = new IPEndPoint(IPAddress.Parse("117.78.9.170"), 10009);
        else 
            SeverEndPoint = serverEndPoint;
        //建立客户端Socket
        _ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            _ClientSocket.Connect(SeverEndPoint);
        }
        catch (Exception)
        {

        }
        _ClientSocket.Blocking = true;
    }

    public static byte[] Serialize<T>(T obj) where T : IMessage {
        return obj.ToByteArray();
    }

    public static T Deserialize<T>(byte[] data, int offset, int len) where T : class, IMessage, new(){
        T obj = new T();
        Google.Protobuf.IMessage message = obj.Descriptor.Parser.ParseFrom(data,offset,len);
        return message as T;
    }

    public static void Send(int type, int len, byte[] msg) {
        char[] c = new Char[3];
        c[0] = (char)type;
        c[1] = (char)((len>>6)+1);
        c[2] = (char)(len-(c[1]-1<<6)+1);
        byte[] head = Encoding.Default.GetBytes(c);
        byte[] datapackage = new byte[head.Length+msg.Length];
        head.CopyTo(datapackage,0);
        msg.CopyTo(datapackage,head.Length);

        _ClientSocket.Send(datapackage);
        Console.WriteLine("send ok");
    }
    public static void Send<T>(int type, T t) where T : class, IMessage, new() {
        byte[] msg;
        msg = Serialize<T>(t);
        char[] c = new Char[3];
        int len = msg.Length;
        c[0] = (char)type;
        c[1] = (char)((len>>6)+1);
        c[2] = (char)(len-(c[1]-1<<6)+1);
        byte[] head = Encoding.Default.GetBytes(c);
        byte[] datapackage = new byte[head.Length+msg.Length];
        head.CopyTo(datapackage,0);
        msg.CopyTo(datapackage,head.Length);
         _ClientSocket.Send(datapackage);
        Console.WriteLine("send T ok");
    }

    const int MAXBUFF = 40960;
    const int LEASTBUFF = 10240;
    const int ONEBUFF = 2048;
    public static int tailp = 0;
    public static byte[] buff = new byte[40960];
    public static void Recv() {
        
        //Debug.Log("rec zu se b" + " tailp " + tailp);
        int len = _ClientSocket.Receive(buff,tailp,MAXBUFF-tailp,SocketFlags.None);
        //Debug.Log("rec zu se e " + tailp);
        if(len <= 0) {
            Debug.Log("recv error " + len);
        }
        tailp += len;

        int type, headp;
        //Debug.Log("tail " + tailp);
        while(true) {
            headp = 0;
            //够拿头
            while(tailp-headp>=Client.HEADLEN) {
                type = (int)buff[headp];
                len = (int)buff[headp+1]-1;
                len <<= 6;
                len += (int)buff[headp+2]-1;
                //Debug.Log("tailp " + tailp + " headp " + headp + " alllen " + (len+Client.HEADLEN));
                if(tailp-headp>=len+Client.HEADLEN) {
                    /*string debug = "";
                    for(int j = 0;j < len + Client.HEADLEN;j++) {
                        debug += buff[j+headp] + " ";
                    } Debug.Log(len + " Debug " + debug);*/
                    byte[] b = new byte[len];
                    for(int i = 0;i < len;i++) {
                        b[i] = buff[i+Client.HEADLEN+headp];
                    }
                    dataQueue.Enqueue((type,b));
                    headp += len+HEADLEN;
                }
                else {
                    break;
                }
            }
            if(headp!=0) {
                for(int i = 0;i < tailp - headp;i++) {
                    buff[i] = buff[i+headp];
                }
            }
            tailp = tailp - headp;
            break;
        }

        //Debug.Log("tail p e" + tailp);
       
    }
    public void Close() {
        _ClientSocket.Shutdown(SocketShutdown.Both);
        _ClientSocket.Close();
    }

}
