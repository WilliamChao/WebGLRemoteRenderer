using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[AddComponentMenu("RemoteRenderer/RemoteRendererClient")]
public class RemoteRendererClient : MonoBehaviour
{
    Dictionary<int, RemoteRendererClientEntity> m_entities = new Dictionary<int, RemoteRendererClientEntity>();

    public RemoteRendererClientEntity CreateEntity(ref RemoteRenderer.EntityData data)
    {
        var obj = new GameObject();
        var rrr = obj.AddComponent<RemoteRendererClientEntity>();
        rrr.RRStart(ref data);
        return rrr;
    }

    public void RRUpdate(RemoteRenderer.EntityData[] data)
    {
        foreach(var i in m_entities) { i.Value.dirty = false; }

        for (int i=0; i<data.Length; ++i)
        {
            int eid = data[i].entityID;
            if (!m_entities.ContainsKey(eid))
            {
                m_entities.Add(eid, CreateEntity(ref data[i]));
            }
            var entity = m_entities[eid];
            entity.RRUpdate(ref data[i]);
        }

        foreach (var i in m_entities)
        {
            if(!i.Value.dirty)
            {
                m_entities.Remove(i.Key);
            }
        }
    }


    void Update()
    {

    }
}
