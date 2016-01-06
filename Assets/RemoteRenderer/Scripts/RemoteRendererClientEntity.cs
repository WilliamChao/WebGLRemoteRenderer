using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Renderer))]
public class RemoteRendererClientEntity : MonoBehaviour
{
    bool m_dirty;
    Transform m_trans;
    Camera m_camera;
    MeshRenderer m_mesh_renderer;
    SkinnedMeshRenderer m_skmesh_renderer;

    public bool dirty
    {
        get { return m_dirty; }
        set { m_dirty = value; }
    }

    public void RRStart(ref RemoteRenderer.EntityData data)
    {
        m_trans = GetComponent<Transform>();
        if (data.type == RemoteRenderer.EntityType.Camera)
        {
            m_camera = gameObject.AddComponent<Camera>();
        }
        else if (data.type == RemoteRenderer.EntityType.MeshRenderer)
        {
            var mesh_filter = gameObject.AddComponent<MeshFilter>();
            mesh_filter.sharedMesh = RemoteRendererAssets.GetInstance().GetMesh(data.meshID);
            m_mesh_renderer = gameObject.AddComponent<MeshRenderer>();
            m_mesh_renderer.sharedMaterial = RemoteRendererAssets.GetInstance().GetMaterial(data.materialID);
        }
        else if(data.type == RemoteRenderer.EntityType.SkinnedMeshRenderer)
        {
            m_skmesh_renderer = gameObject.AddComponent<SkinnedMeshRenderer>();
            m_skmesh_renderer.sharedMesh = RemoteRendererAssets.GetInstance().GetMesh(data.meshID);
            m_skmesh_renderer.sharedMaterial = RemoteRendererAssets.GetInstance().GetMaterial(data.materialID);
        }
    }

    public void RRUpdate(ref RemoteRenderer.EntityData data)
    {
        dirty = true;

        m_trans.position = data.position;
        m_trans.rotation = data.rotation;
        m_trans.localScale = data.scale;
    }
}
