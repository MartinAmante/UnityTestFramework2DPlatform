using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class testSuite
{
    private PlayerController controller;
    private GameManager gameManager;

    [SetUp]
    public void Setup()
    {
        // Cargar el prefab o instancia de GameManager para las pruebas
        GameObject gameManagerObject = Object.Instantiate(Resources.Load<GameObject>("Prefabs/GameManager"));
        gameManager = gameManagerObject.GetComponent<GameManager>();


        GameObject playerObject = GameObject.Find("Player");
        controller = playerObject.GetComponent<PlayerController>();

        GameObject map = GameObject.Find("Tilemap");
    }

    [UnityTest]
    public IEnumerator IncrementCoin()
    {
        GameObject controller = GameObject.Find("Player");
        Assert.IsNotNull(controller);
        int Coin = gameManager.coinCount;
        int InitialCoin = Coin;
        controller.transform.position = new Vector3(0, -6, 0);
        controller.transform.Translate(Vector3.right * Time.deltaTime * 0.5f);
        gameManager.IncrementCoinCount();
        int NewCoin = gameManager.coinCount;
        yield return new WaitForSeconds(5f);
        Assert.AreNotEqual(Coin, NewCoin);
        Debug.Log("la cantidad anterior es" + Coin + "y ahora es" + NewCoin);
    }
   
    [UnityTest]
    public IEnumerator Jump()
    {
        Vector3 initialPos = controller.transform.position;  
        controller.Jump(50f);  
        yield return new WaitForSeconds(1f);
        Assert.AreNotEqual(initialPos, controller.transform.position);  
    }
    [UnityTest]
    public IEnumerator MoveRight()
    {
        int destroy = 0;
        float maxX = 3f;
        while (controller != null && controller.transform.position.x >= maxX)
        {
            controller.transform.Translate(Vector3.left * Time.deltaTime * 1);
            yield return null;
        }
        if (controller != null)
        {
            UnityEngine.Object.Destroy(controller);
            destroy = 1;
        }
        yield return new WaitForSeconds(1f);

        Assert.IsTrue(destroy > 0);
    }
    [UnityTest]
    public IEnumerator ControllerDoesNotPassThroughMap()
    {
        GameObject controller = GameObject.Find("Player");
        Assert.IsNotNull(controller);
        controller.transform.position = new Vector3(-11, 0, 0);
        Vector3 initialPosition = controller.transform.position;
        yield return new WaitForSeconds(1f);
        Assert.LessOrEqual(controller.transform.position.y, -6f);        
    }


}
