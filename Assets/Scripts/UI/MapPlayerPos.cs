using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayerPos : MonoBehaviour
{
    [SerializeField] private GameObject doorUp;
    [SerializeField] private GameObject doorMiddle;
    [SerializeField] private GameObject playerPosMap;

    [SerializeField] private GameObject[] Map;

    // Start is called before the first frame update
    void Start()
    {
        Map = GameObject.FindGameObjectsWithTag("MapPart");
    }

    // Update is called once per frame
    void Update()
    {
        if(GlobalController.Instance.actualLevel == GlobalController.Level.OUTSIDE)
        {
            if (GlobalController.Instance.nameOfPartLevel == "Start")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Outside BC")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            else if(GlobalController.Instance.nameOfPartLevel == "AC")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Outside AC")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
        }

        if(GlobalController.Instance.actualLevel == GlobalController.Level.CAVE)
        {
            for (int i = 0; i < Map.Length; i++)
            {
                if (Map[i].name == "Cave")
                {
                    playerPosMap.transform.position = Map[i].transform.position;
                }
            }
        }

        if (GlobalController.Instance.actualLevel == GlobalController.Level.INSIDE)
        {
            if (GlobalController.Instance.nameOfPartLevel == "Start")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Inside Start")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Middle")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Inside SUp")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Up")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Inside Up")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Down BD")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Inside Down BD")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Down AD")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Inside Down AD")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Down Middle AD")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Inside Down AD Middle")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Down Down AD")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Inside Down AD Down")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Down Down AP")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Inside Down AP")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
        }

        if (GlobalController.Instance.actualLevel == GlobalController.Level.ROOF)
        {
            for (int i = 0; i < Map.Length; i++)
            {
                if (Map[i].name == "Roof")
                {
                    playerPosMap.transform.position = Map[i].transform.position;
                }
            }
        }

        if (GlobalController.Instance.actualLevel == GlobalController.Level.STORAGE)
        {
            if (GlobalController.Instance.nameOfPartLevel == "Storage Middle")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Storage Middle")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Storage Up")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Storage Up")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
        }

        if (GlobalController.Instance.actualLevel == GlobalController.Level.PRISON)
        {
            if (GlobalController.Instance.nameOfPartLevel == "Start")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Prison Start")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Middle BD")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Prison Middle BD")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Middle AD")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Prison Middle AD")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Middle Up BD")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Prison Middle Up BD")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Middle Up AD")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Prison Middle Up AD")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Middle Down")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Prison Down")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Middle Far Down")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Prison Middle Far Down")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "End")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Prison End")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Right Start")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Prison Right Start")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Right Middle")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Prison Right Middle")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Right Right")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Prison Right Right")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Right Start Up")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Prison Start Up")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Right Middle Up")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Prison Right Middle Up")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
            if (GlobalController.Instance.nameOfPartLevel == "Right Right Up")
            {
                for (int i = 0; i < Map.Length; i++)
                {
                    if (Map[i].name == "Prison Right Right Up")
                    {
                        playerPosMap.transform.position = Map[i].transform.position;
                    }
                }
            }
        }
    }
}
