// using UnityEngine;
//
// namespace Rotation
// {
//     public class Rotation
//     {
//         public static void Rotate(Transform current, Transform target)
//         {
//             Vector3 direction = target.transform.position - current.position;
//             float radians = Mathf.Atan2(direction.x, direction.y) * -1;
//             float degrees = radians * Mathf.Rad2Deg;
//             Quaternion aim = Quaternion.Euler(0, 0, degrees);
//             target.rotation = aim;
//         }
//             
//     }
// }