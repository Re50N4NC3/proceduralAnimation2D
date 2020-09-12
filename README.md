## proceduralAnimation2D
Unity 2D humanoid procedural animation

## 1. Overview
  This is the explanation of a simple 2D procedural leg animation for bipedal creature. Project uses inverse kinematics (IK) to calculate position of the leg, which adapts to the terrain.
  
  ![Alt Text](https://media.giphy.com/media/Y3S9FdlkHIynsJgPaj/giphy.gif)

**Results**

## 2. Idea
  Legs of the rigged character are chain of the two bones, end of which (feet) are always trying to reach for the raycasted target after it reaches certain range. To prevent unnatural, direct movement from point to point of both legs, there is added logic, which controls and syncs leg and body movement depending on the environment and current placement of the feet.
  
## 3. Character rig
  Bone rig for legs is simplified to make later calculations easier and bone affects one sprite assigned to it. Top of the body could have no bones, but those will be important in further developement for arms and head animations. 
  
  ![Alt Text](https://github.com/Re50N4NC3/proceduralAnimation2D/blob/master/ikCharacterRig.PNG)
