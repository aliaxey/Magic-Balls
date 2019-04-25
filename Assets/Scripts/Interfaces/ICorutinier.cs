using System.Collections;
using UnityEngine;

public interface ICorutinier{
    Coroutine StartCoroutine(IEnumerator routine);
    void StopCoroutine(IEnumerator enumerator);

}