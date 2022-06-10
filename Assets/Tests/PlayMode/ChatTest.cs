using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ChatTest
{
    [UnityTest]
    public IEnumerator TextProcess()
    {
        var gameObject = new GameObject();
        var p = gameObject.AddComponent<ChatManagerTest>();

        p.startMessage("this is a test");

        Assert.AreNotEqual("", p.playerText);
        
        yield return new WaitForSeconds(4f);

        Assert.AreEqual("", p.playerText);
    }
}