using UnityEngine;

using SoftwareModeling.Managers;
using SoftwareModeling.SMInput;

namespace SoftwareModeling.Cams
{
    public class PlayerCamera : AbstractGameObject
    {
        private Camera _cam;
        private Transform _transform;
        #region initialize
        protected override void Awake()
        {
            _cam = GetComponent<Camera>();
            _transform = transform;
        }

        protected override void Start()
        {
            SMInputManager.getInstance().onInput += onInput;
        }
        #endregion

        private void onInput( SMInputTypes type_ )
        {
            switch( type_ )
            {
                case SMInputTypes.UP:
                    _transform.position += new Vector3(0, 0, 0.1f);
                    break;
                case SMInputTypes.DOWN:
                    _transform.position += new Vector3(0, 0, -0.1f);
                    break;
                case SMInputTypes.RIGHT:
                    _transform.position += new Vector3(0.1f, 0, 0);
                    break;
                case SMInputTypes.LEFT:
                    _transform.position += new Vector3(-0.1f, 0, 0);
                    break;
                case SMInputTypes.ZOOM_IN:
                    _transform.position += new Vector3(0, -1, 0);
                    break;
                case SMInputTypes.ZOOM_OUT:
                    _transform.position += new Vector3(0, 1, 0);
                    break;
            }
        }

        protected override void Update()
        {
        }
    }
}
