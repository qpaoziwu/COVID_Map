using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cs_TimelineTicks : MonoBehaviour
{

    public Transform[] m_tickPoints; // Two points to spawn bettwen, place at beginning and end of timeline
    public Sprite m_sprite; // Sprite to duplicate
    public int m_tickAmount = 2; // amount of tick points FIX LATER
    void Start()
    {
        duplicateObject(m_sprite, m_tickAmount);
    }
    public void duplicateObject(Sprite original, int amount)
    {
        amount++;
        for (int i = 0; i < m_tickPoints.Length - 1; i++)
        {
            for (int x = 1; x < amount; x++)
            {
                Vector3 position = m_tickPoints[i].position + x * (m_tickPoints[i + 1].position - m_tickPoints[i].position) / amount;
                Instantiate(original, position, Quaternion.identity);
            }
        }
    }
}
