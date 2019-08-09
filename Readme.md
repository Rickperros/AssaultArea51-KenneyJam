# Assault Area 51 - Kenney Jam
Below are all the agreements on how to develop on this repository, so please make sure that your work checks the following rules behore merging it to the master branch.

## **Repository Rules**
1. **Never** work directly on the **master** or **develop** branch
2. All branches have to be named following the next format **Feature_Release_Developer** for example: 
_Player_Proto_Ricard_
3. Just **one user per branch**
4. **Do not** create more than one branch per task
5. **Describe** what you do on every **commit**
6. Only **Push** a **feature branch** when its feature is completly implemented.
7. The Branch system will be described on this article: https://nvie.com/posts/a-successful-git-branching-model/
	1. **Master branch** will only be updated with releases.
	2. **Develop branch** will be updated with every finished feature.
	3. **Hotfix branch** will only be created and used to fix major issues of a release.
	4. **Release branch** will be created before every release and it will be used to make the last adjustments (no feature will be added here!)

## **Naming and Normative**
1. All assets have to be named as **FeatureName_Type_Release** _(version only if necessary)_ for example: _Player_Model_Proto_ or _MetalDetector_Blueprint_Beta_
	1. _Blueprints that **will not** be used directly by design team doesn't need to follow that rule_
2. **Update Trello board**

## **Art Considerations**
1. **Do not push any work file**
2. **Really, any work file**
3. Textures should be **PNG** format not **TGA**
4. Try to make everything as **light** as possible
5. Keep project **clean**
	1. _**Erase** all obsolete work_
	2. _Follow a **comprehensible** folder **structure**_
	3. _**Stick to naming rules!**_

## **Code Pipeline**
1. **Make it work**
2. **Debug** and **validate** behaviours with **designer**
3. Clean up and **Refactory**
4. **Validate** with **code team**
5. **Merge** to master
