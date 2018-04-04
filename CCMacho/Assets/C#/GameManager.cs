//全ての処理の統括

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    string machoAPath = "Prefab/MachoA";
    string machoAMaterialPath = "Material/r255";


    int machoCount = 0;
    int maxMachoCount = 1000;
    GameObject machoA = null;
    GameObject[] machoAs;

    Vector2 stageRange = new Vector2(320f, 180f);

    GameObject[] ball;

    // Use this for initialization
    void Start()
    {
        System.Array.Resize(ref machoAs, maxMachoCount);

        machoA = Resources.Load(machoAPath) as GameObject;
        ball = GameObject.FindGameObjectsWithTag("Ball");


        while (machoCount < maxMachoCount)
        {
            float xPosition = (float)(machoCount % 25) * 2f;
            float zPosition = (float)(machoCount / 25) * 2f;

            GameObject obj = (GameObject)Instantiate(machoA, new Vector3(xPosition, 0, zPosition), Quaternion.identity);

            if (machoCount < maxMachoCount / 2)
            {
                Material[] material = new Material[] { (Material)Resources.Load(machoAMaterialPath) as Material };
                obj.GetComponent<Renderer>().materials = material;

            }

            machoAs[machoCount] = obj;
            ++machoCount;
        }
    }

    // Update is called once per frame
    void Update()
    {



        for (int i = 0; i < machoAs.Length; ++i)
        {


            Vector3 machoAForce = new Vector3(0, 0, 0);

            if (i < maxMachoCount / 2)
            {
                machoAForce = TeamAutoMove(machoAs[i], true);

                //machoAForce.x += 1f;
            }
            else
            {

                machoAForce += TeamAutoMove(machoAs[i], false);
                //machoAForce.z -= 1f;
            }

            //machoAForce.x += Random.Range(-1f, 1f);
            //machoAForce.z += Random.Range(-1f, 1f);


            Rigidbody rigidbodyComponent = machoAs[i].GetComponent<Rigidbody>();

            rigidbodyComponent.AddForce(machoAForce, ForceMode.Impulse);
            /*ランダム
			Vector3 machoAPosition = machoAs[i].transform.position;
		
			machoAPosition.x += Random.Range(-1f, 1f);
			machoAPosition.z += Random.Range(-1f, 1f);

			if(machoAPosition.z < StageRange.x){
				
			}

			machoAs[i].transform.position = machoAPosition;
			*/
        }
    }

    Vector3 TeamAutoMove(GameObject macho_, bool ATeam_)
    {
        Vector3 force = new Vector3(0f, 0f, 0f);

        //押す
        if (ATeam_)
        {
            if (ball[0].transform.position.x > macho_.transform.position.x + 10f)
            {
                force += new Vector3(1f, 0f, 0f);
                if (ball[0].transform.position.z < macho_.transform.position.z)
                {
                    force += new Vector3(0f, 0f, -1f);

                }
                else
                {
                    force += new Vector3(0f, 0f, 1f);

                }
            }
            else
            {   //押せる位置に下がる
                force += new Vector3(-2f, 0f, 0f);
                if (ball[0].transform.position.z < macho_.transform.position.z)
                {
                    force += new Vector3(0f, 0f, 0.5f);
                }
                else
                {
                    force += new Vector3(0f, 0f, -0.5f);
                }
            }
        }
        else
        {
            if (ball[0].transform.position.x < macho_.transform.position.x - 10f)
            {
                force += new Vector3(-1f, 0f, 0f);
                if (ball[0].transform.position.z < macho_.transform.position.z)
                {
                    force += new Vector3(0f, 0f, -1f);

                }
                else
                {
                    force += new Vector3(0f, 0f, 1f);

                }
            }
            else
            {   //押せる位置に下がる
                force += new Vector3(2f, 0f, 0f);
                if (ball[0].transform.position.z < macho_.transform.position.z)
                {
                    force += new Vector3(0f, 0f, 0.5f);
                }
                else
                {
                    force += new Vector3(0f, 0f, -0.5f);
                }
            }
        }

        return force;
    }
}
