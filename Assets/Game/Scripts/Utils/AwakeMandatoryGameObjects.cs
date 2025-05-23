using UnityEngine;

public class AwakeMandatoryGameObjects : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _mandatoryGameObjects;

    private void Awake()
    {
        foreach (var go in _mandatoryGameObjects)
        {
            if (go != null)
            {
                go.SetActive(true);
            }
        }
    }
}
