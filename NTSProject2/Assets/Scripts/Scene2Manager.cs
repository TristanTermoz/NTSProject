using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class Scene2Manager : MonoBehaviour
{
    public ARRaycastManager RaycastManager;
    public TrackableType TypeToTrack = TrackableType.PlaneWithinBounds;
    public GameObject Cube;
    public PlayerInput PlayerInput;
    public List<Material> Materials;

    private InputAction touchPressAction;
    private InputAction touchPosAction;
    private List<GameObject> instantiatedCubes;
    private InputAction touchPhaseAction;
    private int cubeCount;
    [SerializeField] private TMP_Text countText;


    // Start is called before the first frame update
    void Start()
{
    if (PlayerInput == null)
    {
        Debug.LogError("PlayerInput is not assigned!");
        return;
    }

    PlayerInput.enabled = true;

    touchPressAction = PlayerInput.actions["TouchPress"];
    touchPosAction = PlayerInput.actions["TouchPos"];
    instantiatedCubes = new List<GameObject>();
    touchPhaseAction = PlayerInput.actions["TouchPhase"];
    cubeCount = 0;

    if (touchPressAction == null || touchPosAction == null)
    {
        Debug.LogError("TouchPress or TouchPos action is missing from PlayerInput!");
    }

}

    private void OnTouch() 
    {
        var touchPos = touchPosAction.ReadValue<Vector2>();
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        RaycastManager.Raycast(touchPos, hits, TypeToTrack);
        
        if (hits.Count > 0)
        {
            ARRaycastHit firstHit = hits[0];
            GameObject cube = Instantiate(Cube, firstHit.pose.position, firstHit.pose.rotation);
            instantiatedCubes.Add(cube);
            cubeCount += 1;
            countText.text = "Cubes: " + cubeCount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (touchPressAction.WasPerformedThisFrame())  
        {
            var touchPhase = touchPhaseAction.ReadValue<UnityEngine.InputSystem.TouchPhase>();
            if (touchPhase == UnityEngine.InputSystem.TouchPhase.Began)  
            {
                OnTouch();
            }
        }
    }

    public void ChangeColor()
        {
            foreach (GameObject cube in instantiatedCubes)
            {
                int randomIndex = Random.Range(0, Materials.Count);
                Material randomMaterial = Materials[randomIndex];
                cube.GetComponent<MeshRenderer>().material = randomMaterial;
            }
        }

    
}
