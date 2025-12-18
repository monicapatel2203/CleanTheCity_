using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DummyModelScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine("UpdatePath");
        LevelUpScript _levelScript = FindObjectOfType<LevelUpScript>();
        _levelScript.targetPos.x = _levelScript.Destination_Pos.transform.position.x;
        _levelScript.targetPos.y = _levelScript.Destination_Pos.transform.position.y;
        _levelScript.targetPos.z = _levelScript.Destination_Pos.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        LevelUpScript _levelScript = FindObjectOfType<LevelUpScript>();
        Vector3 targetPosition = new Vector3(_levelScript.Destination_Pos.transform.position.x, _levelScript.Destination_Pos.transform.position.y, _levelScript.Destination_Pos.transform.position.z);
        // transform.GetComponent<NavMeshAgent>().SetDestination(targetPosition);
        transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, 5.0f * Time.deltaTime);
        if( Vector3.Distance( _levelScript.Destination_Pos.transform.position, this.transform.position ) < 0.1f )
        {
            // gameObject.GetComponent<Animator>().Play("Walking");
            // Debug.Log("Rumba dance...");
            gameObject.GetComponent<Animator>().Play("RumbaDance");
        }
        else
        {
            // Debug.Log("Walk...");
            gameObject.GetComponent<Animator>().Play("Walk");
        }
    }

    IEnumerator UpdatePath()
    {
        LevelUpScript _levelScript = FindObjectOfType<LevelUpScript>();
        Vector3 targetPosition = new Vector3(_levelScript.Destination_Pos.transform.position.x, _levelScript.Destination_Pos.transform.position.y, _levelScript.Destination_Pos.transform.position.z);
        // transform.GetComponent<NavMeshAgent>().SetDestination(targetPosition);
        transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, 3.0f * Time.deltaTime);
        yield return new WaitForSeconds(0.25f);

        if( Vector3.Distance( _levelScript.Destination_Pos.transform.position, this.transform.position ) > 0.5f )
        {
            gameObject.GetComponent<Animator>().Play("Walking");
        }
        else
        {
            Debug.Log("Rumba dance...");
            gameObject.GetComponent<Animator>().Play("RumbaDance");
        }
        Debug.Log("UpdatePath called...");
        if( Vector3.Distance( this.transform.position, targetPosition ) > 0.1f )
        {
            StartCoroutine("UpdatePath");
        }
    }
}
