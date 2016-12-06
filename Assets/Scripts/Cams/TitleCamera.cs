using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SoftwareModeling.Cams
{
    class TitleCamera : AbstractGameObject
    {
        private Vector3 _dest;

        protected override void Awake()
        {
            _dest = transform.position;
        }

        protected override void Start()
        {
        }

        protected override void Update()
        {
            if( Vector2.Distance( transform.position, _dest) > 0.5f)
            {
                Vector2 movement = (_dest - transform.position).normalized * 0.5f;

                transform.position += (Vector3)movement;
            }
            else
            {
                transform.position = _dest;
            }
        }

        public void setDest( Vector2 dest_ )
        {
            _dest = new Vector3( dest_.x, dest_.y, transform.position.z);
        }
    }
}
