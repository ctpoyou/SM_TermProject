using System;
using UnityEngine;

namespace SoftwareModeling.Managers
{
    class SMTimeManager
    {
        static private SMTimeManager _instance = null;
        private float _timescale;

        static public SMTimeManager getInstance()
        {
            if( _instance == null )
            {
                _instance = new SMTimeManager();
            }

            return _instance;
        }

        private SMTimeManager()
        {

        }

        public float currentTime
        {
            get
            {
                return Time.time;
            }
        }

        public float deltaTime
        {
            get
            {
                return Time.deltaTime * timescale;
            }
        }

        public float timescale
        {
            get
            {
                return _timescale;
            }

            set
            {
                _timescale = value;
            }
        }

        public float unscaledDeltaTime
        {
            get
            {
                return Time.deltaTime;
            }
        }
    }
}
