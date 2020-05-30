using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parabola_teach : MonoBehaviour
{
    public Rigidbody origin;
    public Transform target;
    public float Height;
    public float h;
    public float g = -9.8f;
    // Start is called before the first frame update
    void Start()
    {
        origin.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
        drawLine();
    }

    void Launch()
    {
        Physics.gravity = Vector3.up*g;
        origin.useGravity = true;
        origin.velocity = calculateVelocity().Initial_Velocity;
    }

    void drawLine()
    {
        if(target.position.y <= 0)
        {
            h = Height;
        }
        else
        {
            h = target.position.y + Height;
        }
        calculateData data = calculateVelocity();

        int result = 10;
        Vector3 P_point = origin.position;
        for(int i = 0; i <= result; i++)
        {
            float draw_time = (i / (float)result) * data.totalTime;
            Vector3 displacement = data.Initial_Velocity * draw_time + (Vector3.up*g * draw_time * draw_time / 2);
            Vector3 next_point = origin.position + displacement; ;
            Debug.DrawLine(P_point, next_point);
            P_point = next_point;
        }
    }

    calculateData calculateVelocity()
    {
        Vector3 Px = new Vector3(target.position.x - origin.position.x, 0, target.position.z - origin.position.z);
        float Py = target.position.y - origin.position.y;
        float spent_time = Mathf.Sqrt(-2 * h / g) + Mathf.Sqrt(2 * (Py - h) / g);
        //Get Velocity
        Vector3 VelocityRight = Px / spent_time;
        Vector3 VelocityUp = Vector3.up * Mathf.Sqrt(-2 * g * h);
        //Combine together
        Vector3 finalVelocity = VelocityRight + VelocityUp;

        return new calculateData(spent_time, finalVelocity);
    }

    struct calculateData
    {
        public readonly float totalTime;
        public readonly Vector3 Initial_Velocity;

        public calculateData(float time,Vector3 Velocity)
        {
            this.totalTime = time;
            this.Initial_Velocity = Velocity;
        }
    }
}
