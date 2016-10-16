using UnityEngine;

using SoftwareModeling.Constants;
using SoftwareModeling.Managers;

namespace SoftwareModeling.HUD
{
    public class CharacterHUDRoot : AbstractHUD
    {
        protected override void Awake()
        {
        }

        protected override void Start()
        {
        }

        protected override void Update()
        {
        }

        public void showNumber(int number_)
        {
            NumberSprite digitSprite;

            digitSprite = Instantiate(SMResourceManager.getInstance().findResource( ResourcePathConst.NumberSpritePath)).GetComponent<NumberSprite>();
            digitSprite._number = number_;
            digitSprite.transform.SetParent(transform);
            digitSprite.transform.localPosition = Vector3.zero;
        }
    }
}
