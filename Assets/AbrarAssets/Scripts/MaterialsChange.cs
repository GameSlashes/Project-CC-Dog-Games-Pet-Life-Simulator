using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsChange : MonoBehaviour
{
    public static MaterialsChange instance;
    public SkinnedMeshRenderer SichuMesh;
    public Texture[] SichueTexture;
    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {

        ActivePlayers();
    }
    public void ActivePlayers()
    {

        for (int i = 0; i < GR_GameController.instance.players.Length; i++)
        {

            if (i == GR_SaveData.instance.finalPlayer)
            {
                if (GR_GameController.instance.levels[GR_SaveData.instance.CurrentLevel].SpawnGirlActivation)
                {
                    SichuMesh.materials[0].mainTexture = SichueTexture[GR_SaveData.instance.finalPlayer];
                    SichuMesh.materials[0].mainTexture = SichueTexture[GR_SaveData.instance.finalPlayer];
                }
                else
                {
                    SichuMesh.materials[0].mainTexture = SichueTexture[GR_SaveData.instance.finalPlayer];
                    SichuMesh.materials[0].mainTexture = SichueTexture[GR_SaveData.instance.finalPlayer];
                }
            }
        }
    }
}
