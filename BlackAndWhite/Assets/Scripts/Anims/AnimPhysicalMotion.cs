using UnityEngine;

public class AnimPhysicalMotion : AnimMotion
{
    public enum MotionType
    {
        Linear = 0,
        Accelerated,
        Decelerated,
    }

    [SerializeField] bool isWaiting = false;

    [SerializeField] MotionType normal = MotionType.Linear;
    [SerializeField] MotionType reverse = MotionType.Linear;

    private float restTime = 0f;
    private bool clockFlag = true;


    private Vector2 acceleration;           //加速度
    private Vector2 linearSpeed;            //线性速度

    void Start()
    {
        linearSpeed = targetVector / duration * Time.fixedDeltaTime;
        acceleration = 2 * targetVector / (duration * duration) * Time.fixedDeltaTime * Time.fixedDeltaTime;
    }

    private Vector2 speed;
    private Vector2 acc;
    private MotionType mode;

    private void FixedUpdate()
    {
        //开始之前
        if (startTime > 0f)
        {
            startTime -= Time.fixedDeltaTime;
            return;
        }

        if (isWaiting)
        {
            //间隔
            if (clockFlag)
            {
                restTime = interval;
                clockFlag = false;
            }

            if (restTime > 0f)
            {
                restTime -= Time.fixedDeltaTime;
                return;
            }

            direction *= -1f;
            transform.localScale = transform.localScale * flipScale;
            isWaiting = false;
            clockFlag = true;
        }
        else
        {
            //变换        
            if (clockFlag)
            {
                restTime = duration;
                clockFlag = false;
                if (direction > 0f) mode = normal;
                else mode = reverse;

                switch (mode)
                {
                    case MotionType.Linear:
                        {
                            speed = linearSpeed;
                            acc = Vector2.zero;
                            break;
                        }
                    case MotionType.Accelerated:
                        {
                            speed = Vector2.zero;
                            acc = acceleration;
                            break;
                        }
                    case MotionType.Decelerated:
                        {
                            speed = acceleration * duration / Time.fixedDeltaTime;
                            acc = -acceleration;
                            break;
                        }
                }
            }

            if (restTime > 0f)
            {
                restTime -= Time.fixedDeltaTime;
                transform.position += (Vector3)speed * direction;
                speed += acc;
                return;
            }

            isWaiting = true;
            clockFlag = true;
        }
    }
}