## proceduralAnimation2D
Unity 2D humanoid procedural animation

## 1. Overview
  This is the explanation of a simple 2D procedural leg animation for bipedal character. Project uses inverse kinematics (IK) to calculate position of the leg, which adapts to the terrain.
  
  ![Alt Text](https://media.giphy.com/media/Y3S9FdlkHIynsJgPaj/giphy.gif)

**Results**

## 2. Idea
  Legs of the rigged character are chain of the two bones, end of which (feet) are always trying to reach for the raycasted target after it reaches certain range. To prevent unnatural, direct movement from point to point of both legs, there is added logic, which controls and syncs leg and body movement depending on the environment and current placement of the feet.
  
## 3. Character rig
  Bone rig for legs is simplified to make later calculations easier and bone affects one sprite assigned to it. Top of the body could have no bones, but those will be important in further developement for arms and head animations. 
  
  ![Alt Text](https://github.com/Re50N4NC3/proceduralAnimation2D/blob/master/ikCharacterRig.PNG)

## 4. Inverse kinematics
  It makes possible for us to calculate possible rotations of the bones, that will allow feet to reach desired point.
  
  ![Alt Text](https://github.com/Re50N4NC3/proceduralAnimation2D/blob/master/ikFeet.gif)
  
  Nearly all complicated calculations are diminished thanks to the simplified bone structure and reduction of the problem to the two dimensions. It allows us to use basic trigonometry instead of the iterating over matrix operations.
  
  [ ]picture of the trigonometry here

## 5. Raycasting desired position
  There is an raycaster attached to the body which adjusts y coordinate of the desired position, represented here by the diamond icon, depending on the distance from the ground. It shows where leg will be placed if controller will be in certain range from current leg position.

```C#
using UnityEngine;

public class RaycastTarget : MonoBehaviour {
    float desiredYPosition;
    public Transform desiredTarget;

    void Update() {
        RaycastCollider();
        desiredTarget.position = new Vector2(desiredTarget.position.x, desiredYPosition);
    }
    
    void RaycastCollider() {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(desiredTarget.position.x, transform.position.y + 5), -Vector2.up, 12f);

        if (hit.collider != null) {
            desiredYPosition = hit.point.y;
        }
        else {
            desiredYPosition = transform.position.y;
        }
    }
}

```
  
  
  
  
  
  
  
  
