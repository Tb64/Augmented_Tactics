using System;
using UnityEngine;

namespace CharacterEditor
{

    [Serializable]
    public class Config : ScriptableObject
    {
        public string folderName = "";
        public string prefabPath;
        public GameObject prefab;

        public string headBone;
        private Transform _head;

        public string[] skinnedMeshes;
        public string[] shortRobeMeshes;
        public string[] longRobeMeshes;
        public string[] cloakMeshes;

        public TextureType[] availableTextures;
        public MeshTypeBone[] availableMeshes;
        public FxMeshTypeBone[] availableFxMeshes;

        private GameObject _character;
        public GameObject GetCharacter() {
            return _character;
        }

        public void SetCharacter(GameObject character)
        {
            _skinnedMeshes = new SkinnedMeshRenderer[skinnedMeshes.Length];
            _shortRobeMeshes = new SkinnedMeshRenderer[shortRobeMeshes.Length];
            _longRobeMeshes = new SkinnedMeshRenderer[longRobeMeshes.Length];
            _cloakMeshes = new SkinnedMeshRenderer[cloakMeshes.Length];

            _character = character;
            _head = Helper.FindTransform(character.transform, headBone);

            for (int i = 0; i < skinnedMeshes.Length; i++)
                _skinnedMeshes[i] = character.transform.Find(skinnedMeshes[i]).GetComponent<SkinnedMeshRenderer>();
            
            for (int i = 0; i < longRobeMeshes.Length; i++)
                _longRobeMeshes[i] = character.transform.Find(longRobeMeshes[i]).GetComponent<SkinnedMeshRenderer>();
            
            for (int i = 0; i < shortRobeMeshes.Length; i++)
                _shortRobeMeshes[i] = character.transform.Find(shortRobeMeshes[i]).GetComponent<SkinnedMeshRenderer>();
            
            for (int i = 0; i < cloakMeshes.Length; i++)
                _cloakMeshes[i] = character.transform.Find(cloakMeshes[i]).GetComponent<SkinnedMeshRenderer>();
            
        }

        private SkinnedMeshRenderer[] _skinnedMeshes;
        public SkinnedMeshRenderer[] GetSkinMesshes() {
            return _skinnedMeshes;
        }

        private SkinnedMeshRenderer[] _shortRobeMeshes;
        public SkinnedMeshRenderer[] GetShortRobeMeshes() {
            return _shortRobeMeshes;
        }

        private SkinnedMeshRenderer[] _longRobeMeshes;
        public SkinnedMeshRenderer[] GetLongRobeMeshes() {
            return _longRobeMeshes;
        }

        private SkinnedMeshRenderer[] _cloakMeshes;
        public SkinnedMeshRenderer[] GetCloakMesshes() {
            return _cloakMeshes;
        }

        public Transform GetHead() {
            return _head;
        }
    }
}
