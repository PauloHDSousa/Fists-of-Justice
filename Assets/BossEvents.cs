using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvents : MonoBehaviour
{

    [Header("General")]
    [SerializeField] int boss_number = 1;

    [Header("First Boss")]
    [SerializeField] GameObject[] first_boss_invisible_walls;
    [SerializeField] GameObject first_boss_ui;

    bool areaStarted = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    #region First Boss
    public void FirstBossArea()
    {
        EnableWalls(first_boss_invisible_walls);
    }

    public void FirstBossKilled()
    {
        DestroyWalls(first_boss_invisible_walls);
        first_boss_ui.SetActive(false);
    }

    #endregion
    void EnableWalls(GameObject[] walls)
    {
        foreach (GameObject wall in walls)
            wall.SetActive(true);
    }

    void DestroyWalls(GameObject[] walls)
    {
        foreach (GameObject wall in walls)
            Destroy(wall);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !areaStarted)
        {
            areaStarted = true;

            if (boss_number == 1)
                FirstBossArea();
        }
    }
}
