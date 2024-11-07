using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public bool enableCameraShake = true;
    private CinemachineVirtualCamera cam;
    private Coroutine shaking;
    private void Start(){
        cam = this.GetComponent<CinemachineVirtualCamera>();
    }
    public void Shake(float intensity, float duration){
        if(shaking != null){
            StopCoroutine(shaking);
        }
        shaking = StartCoroutine(ShakingCamera(intensity, duration));
    }
    private IEnumerator ShakingCamera(float intensity, float duration){
        float elapsedTime = 0f;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        while(elapsedTime < duration){
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Mathf.Lerp(cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain, 0f, intensity * Time.deltaTime);
            yield return null;
        }
        //cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
    }
}
