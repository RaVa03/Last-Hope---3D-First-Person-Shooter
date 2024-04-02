using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingZombies : MonoBehaviour
{
    float destroyHeight;

    void Start()
    {
        if (this.gameObject.tag == "RagdollTag")
        {
            Invoke("StartSinking", 15); 
        }
    }
    void SinkZombie()
    {
        this.transform.Translate(0, -0.005f, 0);
        if (this.transform.position.y < destroyHeight)
        {
            Destroy(this.gameObject);
        }
    }
    public void StartSinking()
    {
        destroyHeight = Terrain.activeTerrain.SampleHeight(this.transform.position) - 6;
        Collider[] colliderList = this.transform.GetComponentsInChildren<Collider>();
        foreach (Collider c in colliderList)
        {
            Destroy(c);
        }
        InvokeRepeating("SinkZombie", 5, 0.005f);
    }

}
