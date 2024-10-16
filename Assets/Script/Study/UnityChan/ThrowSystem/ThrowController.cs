using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThrowController : MonoBehaviour
{
    public LineRenderer trajectoryRenderer;

    public Collider[] physicsObjects;

    public Rigidbody bulletPrefab;
    public Transform firePosition;

    public float firePower = 10f;
    public int simulationStep = 100;

    Scene simulationScene;
    PhysicsScene physicsScene;

    private void Start()
    {
        CreatePhysicsScene();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Simulation();
        }
    }

    void CreatePhysicsScene()
    {
        simulationScene = SceneManager.CreateScene("SimulationScene", new CreateSceneParameters(LocalPhysicsMode.Physics3D));

        physicsScene = simulationScene.GetPhysicsScene();

        foreach (var physicsObject in physicsObjects)
        {
            var ghost = Instantiate(
                physicsObject.gameObject,
                physicsObject.transform.position,
                physicsObject.transform.rotation);

            ghost.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghost.gameObject, simulationScene);
        }
    }

    public void Simulation()
    {
        Rigidbody ghostBullet = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
        ghostBullet.gameObject.SetActive(true);
        ghostBullet.GetComponent<Renderer>().enabled = false;

        SceneManager.MoveGameObjectToScene(ghostBullet.gameObject, simulationScene);
        ghostBullet.AddForce(firePosition.forward * firePower, ForceMode.Impulse);

        trajectoryRenderer.positionCount = simulationStep;

        for (int i = 0; i < simulationStep; i++) 
        {
            physicsScene.Simulate(Time.fixedDeltaTime);
            trajectoryRenderer.SetPosition(i, ghostBullet.position);
        }

        Destroy(ghostBullet.gameObject);
    }
}
