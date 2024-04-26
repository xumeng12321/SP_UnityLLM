using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using Unity.VisualScripting;
using UnityEngine;

public class nav : MonoBehaviour
{
    GameObject obj;
    private BipedIK bipedIK;
    private AvatarIKGoal handIKGoal = AvatarIKGoal.RightHand;
    private bool isPickingUp = false;
    private bool transformParentSet = false;
    void Start()
    {   
        obj = GameObject.Find("Strawberry");
        UnityEngine.AI.NavMeshAgent agent = FindObjectOfType<UnityEngine.AI.NavMeshAgent>();

        agent.SetDestination(GameObject.Find("Strawberry").transform.position);

        bipedIK = agent.gameObject.GetComponent<BipedIK>();
        bipedIK.solvers.lookAt.target = GameObject.Find("Strawberry").transform;
        bipedIK.solvers.lookAt.SetLookAtWeight(0.3f, 0.5f, 1f, 1f, 0.5f, 0.5f, 0.5f);
    }

    void FixedUpdate()
    {
        
        if (!isPickingUp && Vector3.Distance(transform.position, obj.transform.position) < 1.15f)
        {
            isPickingUp = true;
           
            // Start picking up the object
            PickUpObject();
        }

        if (!transformParentSet && isPickingUp)
        {
            StartCoroutine(SetTransformParent());
        }

    }

    private void PickUpObject()
    {
        bipedIK.solvers.rightHand.target = obj.transform;
        bipedIK.solvers.pelvis.target = obj.transform;
        bipedIK.solvers.rightHand.IKPositionWeight = 1f;
        bipedIK.solvers.pelvis.positionWeight = 0.3f;
        
        Debug.Log("pickedUp");
    }

    IEnumerator SetTransformParent()
    {
        yield return new WaitForSeconds(1f);
        bipedIK.solvers.rightHand.target = null;
        bipedIK.solvers.pelvis.target = null;
        obj.transform.parent = bipedIK.references.rightHand.transform;
        obj.transform.localPosition = new Vector3(-0.1f,0.05f,0f);
        transformParentSet = true;
    }

}
