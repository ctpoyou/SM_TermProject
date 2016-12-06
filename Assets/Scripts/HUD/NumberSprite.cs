using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using SoftwareModeling.Constants;
using SoftwareModeling.Managers;

namespace SoftwareModeling.HUD
{
    public class NumberSprite : AbstractHUD
    {
        public int _number;
        public float _visibleTime = 1f;
        private List<GameObject> _numbers;
        private Color _color;

        public void setColor(Color c_)
        {
            _color = c_;
        }

        protected override void Awake()
        {
        }

        protected override void Start()
        {
            int number = _number;
            int digit;
            int digitCount = 0;

            GameObject digitSprite;
            GameObject wrapper;

            _numbers = new List<GameObject>();

            do
            {
                digit = number % 10;
                number = (int)(number / 10);

                wrapper = new GameObject();
                digitSprite = Instantiate(SMResourceManager.getInstance().findResource(ResourcePathConst.getNumberPath(digit)));
                digitSprite.transform.SetParent(wrapper.transform);
                digitSprite.GetComponent<SpriteRenderer>().color = _color;
                wrapper.transform.SetParent(transform);
                wrapper.transform.localPosition = new Vector3(-0.07f * digitCount, 0, 0);
                digitSprite.transform.localPosition = Vector3.zero;
                _numbers.Add(digitSprite);
                digitSprite.transform.localScale = Vector3.zero;

                digitSprite.transform.rotation = Camera.main.transform.rotation;
                ++digitCount;
            } while (number > 0);

            _numbers.Reverse();

            StartCoroutine(hideAfterTime());
            StartCoroutine(playNumberAnimation());
        }

        private IEnumerator playNumberAnimation()
        {
            foreach( GameObject number in _numbers)
            {
                number.transform.localScale = new Vector3(1,1,1);
                number.GetComponent<Animation>().Play();
                yield return new WaitForSeconds(0.2f);
            }
            
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
