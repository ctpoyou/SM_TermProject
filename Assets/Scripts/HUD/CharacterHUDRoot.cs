using UnityEngine;

using SoftwareModeling.Constants;
using SoftwareModeling.Managers;
using System.Collections.Generic;
using System.Collections;

namespace SoftwareModeling.HUD
{
    public class CharacterHUDRoot : AbstractHUD
    {
        private List<NumberSprite> _numberQueue;
        private bool _HUDReady;

        protected override void Awake()
        {
            _numberQueue = new List<NumberSprite>();
            _HUDReady = true;
        }

        protected override void Start()
        {
        }

        protected override void Update()
        {
            if( _HUDReady )
            {
                StartCoroutine(checkNumber());
            }
        }

        private IEnumerator checkNumber()
        {
            if( _numberQueue.Count > 0 )
            {
                _HUDReady = false;
                NumberSprite numSprite = _numberQueue[0];
                _numberQueue.RemoveAt(0);

                numSprite.gameObject.SetActive(true);

                yield return new WaitForSeconds(0.3f);
                _HUDReady = true;
            }
        }

        public void showNumber(int number_, Color c_)
        {
            NumberSprite digitSprite;

            digitSprite = Instantiate(SMResourceManager.getInstance().findResource( ResourcePathConst.NumberSpritePath)).GetComponent<NumberSprite>();
            digitSprite._number = number_;
            digitSprite.setColor(c_);
            digitSprite.transform.SetParent(transform);
            digitSprite.transform.localPosition = Vector3.zero;
            digitSprite.gameObject.SetActive(false);

            _numberQueue.Add(digitSprite);
        }
    }
}
