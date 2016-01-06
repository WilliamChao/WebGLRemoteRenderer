using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("RemoteRenderer/RemoteRendererEntity")]
public class RemoteRendererEntity : MonoBehaviour
{
    static HashSet<RemoteRendererEntity> s_instances;
    public static HashSet<RemoteRendererEntity> GetInstances()
    {
        if(s_instances == null)
        {
            s_instances = new HashSet<RemoteRendererEntity>();
        }
        return s_instances;
    }

    Transform m_trans;
    Camera m_camera;
    MeshRenderer m_mesh_renderer;
    SkinnedMeshRenderer m_skmesh_renderer;
    RemoteRenderer.EntityData m_data;


    public void RRSerialize(BinaryWriter io)
    {
        m_data.position = m_trans.position;
        m_data.rotation = m_trans.rotation;
        m_data.scale = m_trans.lossyScale;

        // todo write
    }


    void OnEnable()
    {
        GetInstances().Add(this);

        m_trans = GetComponent<Transform>();
        m_camera = GetComponent<Camera>();
        m_mesh_renderer = GetComponent<MeshRenderer>();
        m_skmesh_renderer = GetComponent<SkinnedMeshRenderer>();
    }

    void OnDisable()
    {
        GetInstances().Remove(this);
    }
}
