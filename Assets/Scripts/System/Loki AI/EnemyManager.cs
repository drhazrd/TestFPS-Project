using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic;

[Serializable]

public class EnemyManager
{

    public bool isAI = true;
    public Color m_PlayerColor;                             // This is the color this tank will be tinted.
    public Transform m_SpawnPoint;                          // The position and direction the tank will have when it spawns.
    [HideInInspector] public int m_PlayerNumber;            // This specifies which player this the manager for.
    [HideInInspector] public string m_ColoredPlayerText;    // A string that represents the player with their number colored to match their tank.
    [HideInInspector] public GameObject m_Instance;         // A reference to the instance of the tank when it is created.
    [HideInInspector] public int m_Wins;                    // The number of wins this player has so far.
    [HideInInspector] public List<Transform> m_WayPointList;

    //Grab Movment and Attack Scripts
    private StateController m_StateController;
	public void SetupAI(List<Transform> wayPointList)
	{
		m_StateController = m_Instance.GetComponent<StateController>();
		m_StateController.SetupAI(true, wayPointList);
		m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";

		// Get all of the renderers of the tank.
		MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();

		// Go through all the renderers...
		for (int i = 0; i < renderers.Length; i++)
		{
			// ... set their material color to the color specific to this tank.
			renderers[i].material.color = m_PlayerColor;
		}
	}
    public void SetupPlayerTank()
    {
        // Create a string using the correct color that says 'PLAYER 1' etc based on the tank's color and the player's number.
        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";
        // Get all of the renderers of the tank.
        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();
        // Go through all the renderers...
        for (int i = 0; i < renderers.Length; i++)
        {
            // ... set their material color to the color specific to this tank.
            renderers[i].material.color = m_PlayerColor;
        }
    }
}
