using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

namespace SoftwareModeling.UI.Title
{
    public class PlayBtn : TitleSceneBtn
    {
        protected override void onClick()
        {
            base.onClick();

            SceneManager.LoadScene(1);
        }
    }
}
