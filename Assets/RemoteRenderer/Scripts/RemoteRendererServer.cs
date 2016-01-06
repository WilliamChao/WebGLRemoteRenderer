using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


[AddComponentMenu("RemoteRenderer/RemoteRendererServer")]
public class RemoteRendererServer : MonoBehaviour
{
    public enum Type
    {
        WebSocket,
        Normal,
    }
    Type m_type = Type.WebSocket;
    public int m_port = 14841;
    public int m_maxConnections = 20;

    int m_chID;
    int m_sockID;
    int m_connectionID;
    bool m_started = false;

    MemoryStream m_stream;
    BinaryWriter m_serializer;


    public bool RRStartServer()
    {
        if(m_started)
        {
            Debug.Log("RemoteRendererServer: already started.");
            return false;
        }

        NetworkTransport.Init();
        ConnectionConfig config = new ConnectionConfig();
        m_chID = config.AddChannel(QosType.Reliable);
        HostTopology topology = new HostTopology(config, m_maxConnections);
        if(m_type == Type.Normal)
        {
            m_sockID = NetworkTransport.AddHost(topology, m_port);
        }
        else if(m_type == Type.WebSocket)
        {
            m_sockID = NetworkTransport.AddWebsocketHost(topology, m_port);
        }

        byte error;
        m_connectionID = NetworkTransport.Connect(m_sockID, "localhost", m_port, 0, out error);
        Debug.Log("Connected to server. ConnectionId: " + m_connectionID);

        m_stream = new MemoryStream(1024*64);
        m_serializer = new BinaryWriter(m_stream);
        m_started = true;
        return true;
    }

    public void RRStopServer()
    {
        // todo
        //NetworkTransport.DisconnectNetworkHost();
    }

    public void RRUpdate()
    {
        if(!m_started) { return; }

        foreach(var e in RemoteRendererEntity.GetInstances())
        {
            e.RRSerialize(m_serializer);
        }
        m_serializer.Flush();

        var buf = m_stream.ToArray();
        byte error;
        NetworkTransport.Send(m_sockID, m_connectionID, m_chID, buf, buf.Length, out error);
    }


    void Start()
    {
        RRStartServer();
    }
}
