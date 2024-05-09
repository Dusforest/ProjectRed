using Unity.VisualScripting;
using UnityEngine;

namespace Elvenwood
{
    public class Mirror : AbstractController
    {
        // 定义左和右的枚举
        public enum Direction
        {
            Left,
            Right
        }
        [Tooltip("如果分身被传送到这个镜子，传过来后的朝向是左还是右")] 
        public Direction exitDirection;
        [Tooltip("向朝向的方向多出的位移")]
        public float extraDistance = 1.0f;
        [Tooltip("目标镜子")]
        public Mirror targetMirror;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Clone")) 
            {
                var cloneTrans = other.transform;
                var cloneRotation = cloneTrans.rotation;
                if (targetMirror.exitDirection == Direction.Left)
                {
                    cloneRotation = new Quaternion(cloneRotation.x, -180f,
                        cloneRotation.z, cloneRotation.w);
                    cloneTrans.rotation = cloneRotation;
                    cloneTrans.position = targetMirror.transform.position + Vector3.left * targetMirror.extraDistance;
                    if (cloneTrans.GetComponent<Rigidbody2D>().velocity.x > 0)
                        cloneTrans.GetComponent<CloneController>().facingDir *= -1;
                }
                else if (targetMirror.exitDirection == Direction.Right)
                {
                    cloneRotation = new Quaternion(cloneRotation.x, 0,
                        cloneRotation.z, cloneRotation.w);
                    cloneTrans.rotation = cloneRotation;
                    cloneTrans.position = targetMirror.transform.position + Vector3.right * targetMirror.extraDistance;
                    if (cloneTrans.GetComponent<Rigidbody2D>().velocity.x < 0)
                        cloneTrans.GetComponent<CloneController>().facingDir *= -1;

                }
                
            }

            if (other.CompareTag("MagicArrow"))
            {
                var arrowTrans = other.transform;
                var arrowRotation = arrowTrans.rotation;
                if (targetMirror.exitDirection == Direction.Left)
                {
                    arrowRotation = new Quaternion(arrowRotation.x, -180f,
                        arrowRotation.z, arrowRotation.w);
                    arrowTrans.rotation = arrowRotation;
                    arrowTrans.position = targetMirror.transform.position + Vector3.left * targetMirror.extraDistance;
                    if (arrowTrans.GetComponent<Rigidbody2D>().velocity.x > 0)
                        arrowTrans.GetComponent<Rigidbody2D>().velocity = new Vector2(
                            arrowTrans.GetComponent<Rigidbody2D>().velocity.x * -1,
                            arrowTrans.GetComponent<Rigidbody2D>().velocity.y);
                }
                else if (targetMirror.exitDirection == Direction.Right)
                {
                    arrowRotation = new Quaternion(arrowRotation.x, 0,
                        arrowRotation.z, arrowRotation.w);
                    arrowTrans.rotation = arrowRotation;
                    arrowTrans.position = targetMirror.transform.position + Vector3.right * targetMirror.extraDistance;
                    if (arrowTrans.GetComponent<Rigidbody2D>().velocity.x < 0)
                        arrowTrans.GetComponent<Rigidbody2D>().velocity = new Vector2(
                            arrowTrans.GetComponent<Rigidbody2D>().velocity.x * -1,
                            arrowTrans.GetComponent<Rigidbody2D>().velocity.y);
                }
            }
        }
    }
}