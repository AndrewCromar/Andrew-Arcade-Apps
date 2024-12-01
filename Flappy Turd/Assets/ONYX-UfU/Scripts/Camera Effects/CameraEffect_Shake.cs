using UnityEngine;

namespace ONYX{
    public class CameraEffect_Shake : MonoBehaviour
    {
        [HideInInspector] public static CameraEffect_Shake instance;

        [Header("POSITION ----------------------------------------")]
        [Header("Settings")]
        [SerializeField] private bool position = true;
        [SerializeField] private bool position_x = true;
        [SerializeField] private bool position_y = true;
        [SerializeField] private bool position_z = true;
        [Space]
        [SerializeField] private float position_shakeSmoothing = 1f;
        [SerializeField] private float position_shakeAmplitude = 0.2f;
        [SerializeField] private float position_shakeFrequency = 0.1f;
        [SerializeField] private float position_punchSmoothing = 25f;

        [Header("Debug")]
        [SerializeField] private Vector3 position_zero;
        [SerializeField] private Vector3 position_offset;
        [SerializeField] private Vector3 position_punch;
        [SerializeField] private float position_frequencyCounter;

        [Header("ROTATION ----------------------------------------")]
        [Header("Settings")]
        [SerializeField] private bool rotation = true;
        [SerializeField] private bool rotation_x = true;
        [SerializeField] private bool rotation_y = true;
        [SerializeField] private bool rotation_z = true;
        [Space]
        [SerializeField] private float rotation_shakeSmoothing = 5f;
        [SerializeField] private float rotation_shakeAmplitude = 0.3f;
        [SerializeField] private float rotation_shakeFrequency = 0.1f;
        [SerializeField] private float rotation_punchSmoothing = 10f;

        [Header("Debug - Rotation")]
        [SerializeField] private Vector3 rotation_zero;
        [SerializeField] private Vector3 rotation_offset;
        [SerializeField] private Vector3 rotation_punch;
        [SerializeField] private float rotation_frequencyCounter;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            position_zero = transform.position;
            rotation_zero = transform.rotation.eulerAngles;
        }

        private void Update()
        {
            position_frequencyCounter -= Time.deltaTime;
            rotation_frequencyCounter -= Time.deltaTime;

            if(position){
                if(position_frequencyCounter <= 0){
                    position_frequencyCounter = position_shakeFrequency;
                    position_offset = new Vector3(Random.Range(-position_shakeAmplitude, position_shakeAmplitude), Random.Range(-position_shakeAmplitude, position_shakeAmplitude), Random.Range(-position_shakeAmplitude, position_shakeAmplitude));
                    if(!position_x) position_offset.x = 0;
                    if(!position_y) position_offset.y = 0;
                    if(!position_z) position_offset.z = 0;
                }
                transform.position = Vector3.Lerp(transform.position, position_zero + position_offset + position_punch, position_shakeSmoothing * Time.deltaTime);
                position_punch = Vector3.Lerp(position_punch, Vector3.zero, position_punchSmoothing * Time.deltaTime);
            }

            if(rotation){
                if(rotation_frequencyCounter <= 0){
                    rotation_frequencyCounter = rotation_shakeFrequency;
                    rotation_offset = new Vector3(Random.Range(-rotation_shakeAmplitude, rotation_shakeAmplitude), Random.Range(-rotation_shakeAmplitude, rotation_shakeAmplitude), Random.Range(-rotation_shakeAmplitude, rotation_shakeAmplitude));
                    if(!rotation_x) rotation_offset.x = 0;
                    if(!rotation_y) rotation_offset.y = 0;
                    if(!rotation_z) rotation_offset.z = 0;
                }
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation_zero + rotation_offset + rotation_punch), rotation_shakeSmoothing * Time.deltaTime);
                rotation_punch = Vector3.Lerp(rotation_punch, Vector3.zero, rotation_punchSmoothing * Time.deltaTime);
            }
        }

        public void PunchPosition(Vector3 _punch){ position_punch = _punch; }
        public void PunchRotation(Vector3 _punch){ rotation_punch = _punch; }
    }
}