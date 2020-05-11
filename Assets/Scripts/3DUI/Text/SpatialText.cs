using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARQ.UIKit.TD 
{
    [ExecuteInEditMode]
    public class SpatialText : MonoBehaviour
    {
        [SerializeField]
        private TextMesh _textMesh;
        [TextArea(1, 20)]
        [SerializeField]
        private string _text;
        [SerializeField]
        private bool _autoBackground;
        [Tooltip("Padding of background field in Unity units")]
        [SerializeField]
        private float _backgroundPadding;
        
        private SpatialTextBackground _bg;
        private SpatialTextMesh _spTextMesh;

        private void Awake()
        {
            InitFields();
            _textMesh.text = _text;
            if (_autoBackground)
            {
                SetBackgroudSize();
            }
        }
        public void OnValidate()
        {
            InitFields();
            _textMesh.text = _text;
            if (_autoBackground)
            {
                SetBackgroudSize();
            }
        }
        public void InitFields()
        {
            _spTextMesh = GetComponentInChildren<SpatialTextMesh>();
            _bg = GetComponentInChildren<SpatialTextBackground>();
        }
        public void SetBackgroudSize()
        {
            var size = _spTextMesh.GetGetMeshSize();
            var rsize = new Vector3(size.x + _backgroundPadding, size.y + _backgroundPadding, size.z);
            _bg.SetBackgroundSize(rsize);
        }
    }
}