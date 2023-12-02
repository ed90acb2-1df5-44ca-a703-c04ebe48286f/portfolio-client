using UnityEngine;

namespace Portfolio.Unity.Systems
{
    public class CameraSystem : MonoBehaviour
    {
        [SerializeField] private Camera _camera = null!;
        [SerializeField] private Vector3 _followOffset;
        [SerializeField] private float _followSmoothFactor;

        private Transform? _target;

        public void StartFollow(Transform target)
        {
            _target = target;
        }

        public void StopFollow()
        {
            _target = null;
        }

        private void Update()
        {
            if (_target is null)
            {
                return;
            }

            _camera.transform.position = Vector3.Lerp(
                _camera.transform.position, _target.position + _followOffset, _followSmoothFactor * Time.deltaTime);
        }
    }
}
