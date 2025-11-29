using TMPro;
using UnityEngine;

public class ObjectCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text count;
    private int objectCount;

    private void OnCollisionEnter(Collision collision)
    {
        count.text = $"Number of objects on table is {++objectCount}";
    }

    private void OnCollisionExit(Collision collision)
    {
        count.text = $"Number of objects on table is {--objectCount}";
    }
}
