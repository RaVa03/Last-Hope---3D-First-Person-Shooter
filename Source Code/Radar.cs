using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarObject
{
    public Image icon;
    public GameObject owner;
}

public class Radar : MonoBehaviour
{
    public Transform playerPos;
    public float mapScale = 2.0f;
    public static List<RadarObject> radarObjects = new List<RadarObject>();


    public static void RegisterRadarObject(GameObject obj, Image img)
    {
        Image image = Instantiate(img);
        radarObjects.Add(new RadarObject() { owner = obj, icon = image });
    }

    public static void RemoveRadarObject(GameObject obj)
    {
        List<RadarObject> updatedList = new List<RadarObject>();
        for (int i = 0; i < radarObjects.Count; i++)
        {
            if (radarObjects[i].owner == obj)
            {
                Destroy(radarObjects[i].icon);
                continue;
            }
            else
                updatedList.Add(radarObjects[i]);
        }

        radarObjects.RemoveRange(0, radarObjects.Count);
        radarObjects.AddRange(updatedList);
    }

    void Start()
    {

        if (GameController.instance != null)
        {
            GameController.instance.varDest1 = true;
            GameController.instance.varDest2 = false;
            GameController.instance.varDest3 = false;
        }

    }
    void Update()
    {
        if (playerPos == null) return;


        foreach (RadarObject ro in radarObjects)
        {
            Vector3 objectPosition = ro.owner.transform.position - playerPos.position;
            float distToOwner = Vector3.Distance(playerPos.position, ro.owner.transform.position) * mapScale;
            float newAngle = Mathf.Atan2(objectPosition.z, objectPosition.x) * Mathf.Rad2Deg + playerPos.eulerAngles.y;
            objectPosition.x = distToOwner * Mathf.Cos(newAngle * Mathf.Deg2Rad) ; 
            objectPosition.z = distToOwner * Mathf.Sin(newAngle * Mathf.Deg2Rad);

            ro.icon.transform.SetParent(this.transform);
            RectTransform rt = this.GetComponent<RectTransform>();

            if (GameController.instance.varDest1)
            {
                if (ro.owner.tag == "Dest1Tag")
                    ro.icon.enabled = true;
                if (ro.owner.tag == "Dest2Tag")
                    ro.icon.enabled = false;
                if (ro.owner.tag == "HomeTag")
                    ro.icon.enabled = false;
                if (ro.owner.tag == "Dest1Tag" && distToOwner > 160)
                {
                    SetIconPosGreatDistance(newAngle, rt,ro);
                }
                else if (ro.owner.tag == "Dest1Tag" && distToOwner < 10 && GameController.instance.enemies.Count == 0)
                {
                    GameController.instance.varDest1 = false;
                    GameController.instance.varDest2 = true;
                    GameController.instance.checkPoint = 1;
                    GameController.instance.menuFunctions.GetComponent<MenuFunctions>().SaveState();
                    ro.icon.enabled = false;
                }
                else
                {
                    SetIconPosDistance(objectPosition, rt, ro);
                }

            }
            else if (GameController.instance.varDest2)
            {
                if (ro.owner.tag == "Dest1Tag")
                    ro.icon.enabled = false;
                if (ro.owner.tag == "Dest2Tag")
                    ro.icon.enabled = true;
                if (ro.owner.tag == "HomeTag")
                    ro.icon.enabled = false;
                if (ro.owner.tag == "Dest2Tag" && distToOwner > 160)
                {
                    SetIconPosGreatDistance(newAngle, rt, ro);
                }
                else if (ro.owner.tag == "Dest2Tag" && distToOwner < 10 && GameController.instance.enemies.Count == 0)
                {
                    GameController.instance.varDest2 = false;
                    GameController.instance.varDest3 = true;
                    GameController.instance.checkPoint = 2; 
                    GameController.instance.menuFunctions.GetComponent<MenuFunctions>().SaveState();
                    ro.icon.enabled = false;

                }
                else
                {
                    SetIconPosDistance(objectPosition, rt, ro);
                }
            }
            else if (GameController.instance.varDest3)
            {
                if (ro.owner.tag == "Dest1Tag")
                    ro.icon.enabled = false;
                if (ro.owner.tag == "Dest2Tag")
                    ro.icon.enabled = false;
                if (ro.owner.tag == "HomeTag")
                    ro.icon.enabled = true;
                if (ro.owner.tag == "HomeTag" && distToOwner > 160)
                {
                    SetIconPosGreatDistance(newAngle, rt, ro);
                }
                else if (ro.owner.tag == "HomeTag" && distToOwner < 3 && GameController.instance.enemies.Count == 0)
                {
                    GameController.instance.checkPoint = 3;
                    GameController.instance.menuFunctions.GetComponent<MenuFunctions>().SaveState();
                    ro.icon.enabled = false;

                }
                else
                {
                    SetIconPosDistance(objectPosition,rt,ro);
                }
            }

        }
    }
    void SetIconPosGreatDistance(float newAngle, RectTransform rt, RadarObject ro)
    {

        float newX = 160 * Mathf.Cos(newAngle * Mathf.Deg2Rad);
        float newZ = 160 * Mathf.Sin(newAngle * Mathf.Deg2Rad);
        ro.icon.transform.position = this.transform.position + new Vector3(newX + rt.pivot.x, newZ + rt.pivot.y, 0) + new Vector3(178.158f, 179.16f, 0f);
    }
    void SetIconPosDistance(Vector3 objectPosition, RectTransform rt, RadarObject ro)
    {
        ro.icon.transform.position = this.transform.position + new Vector3(objectPosition.x, objectPosition.z , 0) + new Vector3(178.158f, 179.16f, 0f);
    }
}
