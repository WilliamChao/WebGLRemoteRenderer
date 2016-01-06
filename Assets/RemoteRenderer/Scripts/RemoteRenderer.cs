using System.Collections;
using UnityEngine;


public static class RemoteRenderer
{
    public enum EntityType
    {
        Camera,
        MeshRenderer,
        SkinnedMeshRenderer,
    }

    public struct EntityData
    {
        public EntityType type;
        public int entityID;
        public int meshID;
        public int materialID;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }
}
