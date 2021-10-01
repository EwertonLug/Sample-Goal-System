# Sample-Goal-System-Unity
Sample Goal System Unity
# How to use

### Add Prefab GoalManager in Scene
### Add Prefab Panel [GoalSystemSample] in Canvas


### Create a script example name Objetive1 what will extend of Goal

### Implement method IsRunning with your logic. When Goal completed return GoalStatus.COMPLETED or GoalStatus.FAILED to break execution. By default leaves the GoalStatus.RUNNING status.

### Use method Activate to enable or Deactivate to disable

### The method IsRunning is only executed when the objective is enabled

### To check if all goals have been completed use GoalManager.Instance.IsCompletedGoals
