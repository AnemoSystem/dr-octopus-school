using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementTest
{
    [UnityTest]
    public IEnumerator FollowTarget()
    {
        var gameObject = new GameObject();
        var p = gameObject.AddComponent<MovimentationTest>();

        p.setTargetPos(10f, 10f);
        yield return new WaitForSeconds(5f);

        Assert.AreEqual(p.getTargetPos(), p.transform.position);
    }

    [UnityTest]
    public IEnumerator FollowMouse()
    {   
        Vector2 mousePos = new Vector2();
        Camera cam = new Camera();

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        var gameObject = new GameObject();
        var p = gameObject.AddComponent<MovimentationTest>();

        p.setTargetPos(mousePos.x, mousePos.y);
        yield return new WaitForSeconds(5f);

        Assert.AreEqual(p.getTargetPos(), p.transform.position);
    }
}