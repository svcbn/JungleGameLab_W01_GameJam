using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public GroundManager GroundManager { get; set; }
    public Transform myTr;
    public Transform crossTr;
    public Transform horizonTr;
    public Transform verticalTr;

    public void MovePlayerOnLeftUp()
    {
        if (crossTr.position.x > myTr.position.x || crossTr.position.y < myTr.position.y)
        {
            crossTr.position = myTr.position +  new Vector3(-GroundManager.valueX, GroundManager.valueY, 0);
        }
        
        if (myTr.position.x < horizonTr.position.x  )
        {
            horizonTr.position = myTr.position +  new Vector3(-GroundManager.valueX, 0, 0);
        }

        if (myTr.position.y > verticalTr.position.y )
        {
            verticalTr.position = myTr.position +  new Vector3(0, GroundManager.valueY, 0);
        }
    }
    public void MovePlayerOnLeftDown()
    {
        if (crossTr.position.x > myTr.position.x || crossTr.position.y > myTr.position.y )
        {
            crossTr.position = myTr.position +  new Vector3(-GroundManager.valueX, -GroundManager.valueY, 0);
        }
        
        if (horizonTr.position.x > myTr.position.x)
        {
            horizonTr.position = myTr.position +  new Vector3(-GroundManager.valueX, 0, 0);
        }

        if (verticalTr.position.y > myTr.position.y)
        {
            verticalTr.position = myTr.position + new Vector3(0, -GroundManager.valueY, 0);
        }
    }
    public void MovePlayerOnRightUp()
    {
        
        if (crossTr.position.x < myTr.position.x || crossTr.position.y < myTr.position.y )
        {
            crossTr.position = myTr.position + new Vector3(GroundManager.valueX, GroundManager.valueY, 0);
        }
        
        if (horizonTr.position.x < myTr.position.x)
        {
            horizonTr.position = myTr.position + new Vector3(GroundManager.valueX, 0, 0);
        }

        if (verticalTr.position.y < myTr.position.y)
        {
            verticalTr.position = myTr.position + new Vector3(0, GroundManager.valueY, 0);
        }
    }
    public void MovePlayerOnRightDown()
    {
        if (crossTr.position.x < myTr.position.x || crossTr.position.y > myTr.position.y)
        {
            crossTr.position = myTr.position + new Vector3(GroundManager.valueX, -GroundManager.valueY, 0);
        }
        
        if (horizonTr.position.x < myTr.position.x)
        {
            horizonTr.position = myTr.position + new Vector3(GroundManager.valueX, 0, 0);
        }

        if (verticalTr.position.y > myTr.position.y)
        {
            verticalTr.position = myTr.position + new Vector3(0, -GroundManager.valueY, 0);
        }
    }
    
    
}
