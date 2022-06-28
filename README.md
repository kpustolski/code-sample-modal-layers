# Table of Contents
- [Table of Contents](#table-of-contents)
- [Overview](#overview)
- [Tools](#tools)
- [Quick Links](#quick-links)
- [Resources](#resources)
  - [Images used](#images-used)
  
# Overview
*Last Updated: 6.27.2022*

* Unity version: 2020.3.14f1
* **Design goal:** Create a system that layers modal popups so that only one is seen at a time. Keep the overall project simple.
* **Experience goal:** Choose which 10 items you want to pack in your backpack for a camping trip.
* The `Start()` function is located in [AppManager.cs](https://github.com/moose15/code-sample-modal-layers/blob/main/ModalLayeringSample/Assets/_Scripts/Managers/AppManager.cs).
* This project is intended to be viewed in the 16:9 aspect ratio.

In this example, you can choose which 10 items from your inventory that you would like to bring with you on a camping trip. There are a total of 11 different types of items and there can be multiples of an item available in the inventory. As far as interactions go, you can:
* Tap through the tab navigation to filter items by category
* Tap on items for more information and to add them to your bag
* You can remove one item or all items from your backpack

Modal layering code can be found in the [UIManager.cs](https://github.com/moose15/code-sample-modal-layers/blob/main/ModalLayeringSample/Assets/_Scripts/Managers/UIManager.cs) script. Also, for the sake of this example, all data is stored locally with the project. JSON files are stored in the `Assets/Resources` folder.

<p float="left" align="center">
  <img width="400" alt="codesample_modallayer_05" src="https://user-images.githubusercontent.com/4196059/130309181-e733d9b7-de4a-4db8-adb2-0e07c1cc90eb.png">
  <img width="400" alt="codesample_modallayer_02" src="https://user-images.githubusercontent.com/4196059/130309113-6d9d31f0-53cd-4741-9366-25f680b703ca.png">
  <img width="400" alt="codesample_modallayer_01" src="https://user-images.githubusercontent.com/4196059/130309118-15f4d67b-8016-4547-a50b-3969adaa2a3a.gif">
  <img width="400" alt="codesample_modallayer_01" src="https://user-images.githubusercontent.com/4196059/130309115-b2200919-61b4-4c50-9f55-ea691b2fd0d9.png">
</p>

# Tools
I made the **Copy Data** and **Item Data** tools to help me modify the JSON data for the items and project copy.

<p float="left" align="center">
  <img width="400" alt="codesample_modallayer_04" src="https://user-images.githubusercontent.com/4196059/130308788-344596b7-828e-4b9d-a34e-679d19747461.png">
  <img width="400" alt="codesample_modallayer_03" src="https://user-images.githubusercontent.com/4196059/130308787-ec4f120d-aec7-4e1e-8e3e-18ce21322bbe.png">
</p>

# Quick Links
* [Scripts folder](https://github.com/moose15/code-sample-modal-layers/tree/main/ModalLayeringSample/Assets/_Scripts)

# Resources
* [Unity UI Extensions](https://bitbucket.org/UnityUIExtensions/unity-ui-extensions/wiki/Home)
   * `Gradient2.cs` is from this repository.
* [DOTween library](http://dotween.demigiant.com/) 
   * I used this library to animate the buttons and modal. 
* [Kate Maldjian's the noun project page](https://thenounproject.com/katemaldjian/)
* [Kate Maldjian's website](http://katemaldjian.com/)

## Images used
* Tiled Trees Background is from [www.subtlepatterns.com ](www.subtlepatterns.com)

The following icons are used in this project. The ones listed here were all found on [www.thenounproject.com](https://thenounproject.com/)

* baseball t-shirt by Kate Maldjian from the Noun Project
* Rope by Kate Maldjian from the Noun Project
* Binoculars by Kate Maldjian from the Noun Project
* campground map by Kate Maldjian from the Noun Project
* Cargo Shorts by Kate Maldjian from the Noun Project
* Guitar by Kate Maldjian from the Noun Project
* marshmallow stick by Kate Maldjian from the Noun Project
* Camping Hat by Kate Maldjian from the Noun Project
* first aid kit by Kate Maldjian from the Noun Project
* Pickaxe by Kate Maldjian from the Noun Project
* Tent by Kate Maldjian from the Noun Project
* Backpack by Kate Maldjian from the Noun Project
