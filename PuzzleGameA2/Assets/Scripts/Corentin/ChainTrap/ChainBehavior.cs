using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class ChainBehavior : MonoBehaviour
{
    [SerializeField] private List<GameObject> _links;

    [SerializeField] private GameObject _objectChained;

    [SerializeField] private float _detectionRange;

    [SerializeField] private LayerMask _detectedLayerMask;

    private bool _isBroken;

    private void CheckBroken(GameObject gameObject)
    {
        if (gameObject.CompareTag("Projectile"))
        {
            BreakChain();
        }
    }

    [Button]
    private void BreakChain()
    {
        if (!_isBroken)
        {
            _isBroken = true;

            _objectChained.transform.parent = null;

            StartCoroutine(SpikeChainDeathTagCooldown());

            _links[_links.Count - 1].GetComponent<HingeJoint2D>().connectedBody = null;

            gameObject.GetComponent<DistanceJoint2D>().connectedBody = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var link in _links)
        {
            link.GetComponent<TriggerCollisionDetection>().OnTriggerEnterEvent += CheckBroken;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), _detectionRange, _detectedLayerMask);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * _detectionRange, Color.yellow, 5f);
        if (hit && !_isBroken)
        {
            Debug.Log("Detect Player Under spike");

            BreakChain();
        }
    }

    IEnumerator SpikeChainDeathTagCooldown()
    {
        yield return new WaitForSeconds(4f);

        _objectChained.tag = "Untagged";

        yield return null;
    }
}
