using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


[AddComponentMenu("RemoteRenderer/RemoteRendererAssets")]
public class RemoteRendererAssets : MonoBehaviour
{
    static RemoteRendererAssets s_instance;
    public static RemoteRendererAssets GetInstance()
    {
        return s_instance;
    }

    public Mesh[] m_meshes;
    public Material[] m_materials;


#if UNITY_EDITOR
    // thanks to http://answers.unity3d.com/questions/223434/find-all-assets-by-type.html
    //
    // fileExtension: ".prefab" etc.
    public static T[] GetAssetsOfType<T>(string fileExtension) where T : Object
    {
        var ret = new List<T>();
        var directory = new DirectoryInfo(Application.dataPath);
        var files = directory.GetFiles("*" + fileExtension, SearchOption.AllDirectories);

        foreach (var info in files)
        {
            string path = info.FullName;
            path = path.Replace(@"\", "/").Replace(Application.dataPath, "Assets");

            var asset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (asset != null && asset.GetType() == typeof(T))
            {
                Debug.Log(path + "\n" + Application.dataPath);
                ret.Add(asset);
            }
        }
        return ret.ToArray();
    }

    public void UpdateAssetList()
    {
        m_meshes = GetAssetsOfType<Mesh>(".asset");
        m_materials = GetAssetsOfType<Material>(".mat");
    }
#endif

    public Mesh GetMesh(int id)
    {
        return m_meshes[id];
    }

    public Material GetMaterial(int id)
    {
        return m_materials[id];
    }

    public int GetMeshID(Mesh v)
    {
        return System.Array.IndexOf(m_meshes, v);
    }

    public int GetMaterialID(Material v)
    {
        return System.Array.IndexOf(m_materials, v);
    }


#if UNITY_EDITOR
    void Reset()
    {
        UpdateAssetList();
    }
#endif

    void Awake()
    {
        if(s_instance != null)
        {
            Debug.LogWarning("RemoteRendererAssets: multiple instances");
        }
        else
        {
            s_instance = this;
        }
    }

    void OnDestroy()
    {
        if(s_instance == this)
        {
            s_instance = null;
        }
    }
}
