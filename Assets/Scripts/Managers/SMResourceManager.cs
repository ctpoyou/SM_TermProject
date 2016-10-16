using UnityEngine;
using System.Collections.Generic;

namespace SoftwareModeling.Managers
{
    public class SMResourceManager
    {
        static private SMResourceManager _instance = null;

        private Dictionary<string, GameObject> _prefabHash;
        
        static public SMResourceManager getInstance()
        {
            if( _instance == null )
            {
                _instance = new SMResourceManager();
            }

            return _instance;
        }

        private SMResourceManager()
        {
            _prefabHash = new Dictionary<string, GameObject>();
        }

        public void preload( string path_ )
        {
            _prefabHash.Add(path_, Resources.Load<GameObject>(path_));
        }

        public GameObject findResource( string path_ )
        {
            return _prefabHash[path_];
        }
    }
}
