using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class FollowCarCamera : MonoBehaviour
{
    public Transform followingGO;
    private Vector3 followPosition;
    private float backOffSet = -8f;
    [SerializeField]
    private Vector3 positionOffSet ;
    private Camera followCamera;
    private float rotationSpeed = 5f;
    Vector3 maxBoundPosition;
    private void Awake()
    {
        if(followCamera == null)
        this.followCamera = transform.GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError(followingGO.transform.forward);
        this.followCamera.transform.position = this.followingGO.transform.position +  followingGO.transform.forward * backOffSet + positionOffSet;
        // Vector3 newDirectio =  Vector3.RotateTowards(transform.forward, followingGO.forward, 0.5f,0.5f);
        transform.rotation = Quaternion.LookRotation(followingGO.transform.forward);

    }


    
    
    public void Reset()
    {
        Debug.LogError("Reset called");
        Debug.LogError(followingGO.transform.forward);
        this.followCamera.transform.position = this.followingGO.transform.position + followingGO.transform.forward * backOffSet;
    }

    Vector3 targetPos;
    Vector3 initPos;
    private void Update()
    {
        targetPos = this.followingGO.transform.position + (followingGO.transform.forward * backOffSet) + positionOffSet;
        initPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
     
     
        float step = rotationSpeed * Time.deltaTime;
        this.followCamera.transform.position =  Vector3.Slerp(initPos, targetPos, step);
        return;
        Vector3 towardsCar = (followingGO.transform.position - transform.position).normalized;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, towardsCar, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
