using System.Collections;
using UnityEngine;


[AddComponentMenu("RemoteRenderer/RemoteRendererEntity")]
public class RemoteRendererEntity : MonoBehaviour
{
    Transform m_trans;
    Camera m_camera;
    MeshRenderer m_mesh_renderer;
    SkinnedMeshRenderer m_skmesh_renderer;

    void Start()
    {
        m_trans = GetComponent<Transform>();
        m_camera = GetComponent<Camera>();
        m_mesh_renderer = GetComponent<MeshRenderer>();
        m_skmesh_renderer = GetComponent<SkinnedMeshRenderer>();
    }
}
