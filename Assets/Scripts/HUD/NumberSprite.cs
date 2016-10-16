using UnityEngine;
using System.Collections;

using SoftwareModeling.Constants;
using SoftwareModeling.Managers;

namespace SoftwareModeling.HUD
{
    public class NumberSprite : AbstractHUD
    {
        public int _number;
        public float _visibleTime = 1f;

        protected override void Awake()
        {
        }

        protected override void Start()
        {
            int number = _number;
            int digit;
            int digitCount = 0;

            GameObject digitSprite;

            do
            {
                digit = number % 10;
                number = (int)(number / 10);

                digitSprite = Instantiate(SMResourceManager.getInstance().findResource(ResourcePathConst.getNumberPath(digit)));
                digitSprite.transform.SetParent(transform);
                digitSprite.transform.localPosition = new Vector3( -0.07f * digitCount, 0, 0 );
                ++digitCount;
            } while (number > 0);

            StartCoroutine(hideAfterTime());
        }

        private IEnumerator hideAfterTime()
        {
            yield return new WaitForSeconds(_visibleTime);

            Destroy(this.gameObject);
        }

        protected override void Update()
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
