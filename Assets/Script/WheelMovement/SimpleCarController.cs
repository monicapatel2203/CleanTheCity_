using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour {

    [SerializeField] private float m_horizontalInput;
    [SerializeField] private float m_verticalInput;
    [SerializeField] private float m_steeringAngle;
    [SerializeField] private Vector3 newVelocity, newTensor;

    public WheelCollider frontDriverW, frontPassengerW;     //Pass- left drive- right
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public float maxSteerAngle = 30f;
    public float motorForce = 5000f;

    [SerializeField] private float massOFCar;
    public float pickupSpeed = 10f;

    public float maxSpeed = 1f, maxtensor = 1f;
    private Rigidbody rb;
    public GameObject Controller;
    public Transform CM;


    private void Start() 
    {
        GameController _gcScript = FindObjectOfType<GameController>();
        Controller = _gcScript.Controller;
        rb = this.GetComponent<Rigidbody>();
        massOFCar = rb.mass;
        // RegulateInertiaTensor();
        if(CM)
        {
            rb.centerOfMass = CM.localPosition;
            // Debug.Log(rb.centerOfMass + "  " + this.name);
        }
    }

    private void FixedUpdate() {
            GetInput();//
            Steer();//
            RegulateSpeed();//
            UpdateWheelPoses();//
            // RegulateInertiaTensor();
            // Accelerate();//
    }


    public void GetInput() {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer() {
        // if( Controller.transform.rotation.y > -0.1f && Controller.transform.rotation.y < 0.1f )
        // {
            // Debug.Log(Controller.transform.rotation.y);
            // m_steeringAngle = maxSteerAngle * m_horizontalInput;
            m_steeringAngle = maxSteerAngle * Controller.transform.rotation.y;
            frontDriverW.steerAngle = m_steeringAngle;//
            frontPassengerW.steerAngle = m_steeringAngle;//   
        // }
    }

    void RegulateSpeed()
    {
        if (rb.velocity.z >= maxSpeed) {
            m_verticalInput = 0f;
            newVelocity = rb.velocity;
            newVelocity.z = maxSpeed;
            rb.velocity = newVelocity;
        } else {
            m_verticalInput = 1f;
        }
    }

    void RegulateInertiaTensor()
    {
        if(rb.inertiaTensor.y >= maxtensor)
        {

            // newTensor = rb.inertiaTensor;
            // newTensor.y = maxtensor;
            rb.inertiaTensor = new Vector3(1, 100, 1);
            // Debug.Log(rb.inertiaTensor + "    " + newTensor + "    " + maxtensor);
        }
    }

    private void Accelerate() {
        frontDriverW.motorTorque = m_verticalInput * motorForce;
        frontPassengerW.motorTorque = m_verticalInput * motorForce;
    }

    private void UpdateWheelPoses() {
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform) {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;
        _collider.GetWorldPose(out _pos, out _quat);

        // _transform.position = _pos;
        _transform.rotation = _quat;
    }

    public void SteerCar(float steerValue) {
        m_horizontalInput = steerValue;
    }

}
