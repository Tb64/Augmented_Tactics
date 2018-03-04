using System;

namespace CharacterEditor
{
    [Serializable]
    public class FxMeshTypeBone
    {
        public FXType mesh;
        public string boneName;

        public FxMeshTypeBone(FXType type, string bone)
        {
            mesh = type;
            boneName = bone;
        }
    }
}