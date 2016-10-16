using System;
using UnityEngine;

namespace SoftwareModeling.GameCharacter
{
    abstract public class AbstractCharacter : AbstractGameObject
    {
        private Transform _transform;
        protected override void Awake()
        {
            _transform = transform;
        }

        public Vector3 position
        {
            get
            {
                return _transform.position;
            }

            set
            {
                _transform.position = value;
            }
        }
    }
}
