using System.Collections;
using UnityEngine;

public class InfoAbovePlayer : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(InfoDeactivateDelay());
    }

    IEnumerator InfoDeactivateDelay()
    {
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
    }
}
