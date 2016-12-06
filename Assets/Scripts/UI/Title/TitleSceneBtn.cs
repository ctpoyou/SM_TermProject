using SoftwareModeling.Cams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SoftwareModeling.UI.Title
{
    public abstract class TitleSceneBtn : AbstractUI
    {
        Vector3 _dest;
        protected override void Awake()
        {
            _dest = transform.GetChild(0).transform.position;
        }

        protected override void Start()
        {
        }

        protected override void Update()
        {
            if( Input.GetMouseButtonUp(0))
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if(Physics.Raycast(mouseRay, out hitInfo ))
                {
                    if( hitInfo.collider.gameObject == gameObject)
                    {
                        onClick();
                    }
                }
            }
        }

        protected virtual void onClick()
        {
            Debug.Log(this + " is clicked");
            Camera.main.GetComponent<TitleCamera>().setDest(_dest);
        }
    }
}
