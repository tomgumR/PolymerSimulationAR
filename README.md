# PolymerSimulationAR

An augmented reality Unity project for interactive polymer simulation and real-time molecular energy visualization.

This project was developed as part of the Summer Research Internship Program 2024 at IIT Gandhinagar. It presents a polymer simulation directly in augmented reality, where the polymer can be viewed and interacted with in 3D space rather than only through a conventional 2D screen. The project models a short polymer chain using a ball-and-spring representation, enables touch-based manipulation of the structure in AR, calculates bonded interaction energies in real time as the polymer is deformed, and reflects those changes in a real-time 3D graph projected beside the polymer. By placing both the molecular model and its live energy visualization into the user’s physical space, the system makes interaction more intuitive and spatially meaningful, thereby contributing to more effective educational and research tools in molecular dynamics.

## Demo Video

[Watch the demo video](./demoVid.MOV)


## Overview

The project focuses on visualizing polymer behavior in a more intuitive way than traditional 2D molecular tools. A polymer chain is represented using:

- Spheres for atoms
- Cylinders for bonds
- Unity physics for motion and constraints
- Custom C# scripts for energy calculations
- AR image tracking for marker-based placement in the real world

The simulation tracks key bonded interactions:

- Bond stretching potential
- Bond angle bending potential
- Dihedral or torsional energy
- Total energy of the modeled polymer segment

## Features

- Interactive 3D polymer chain built in Unity
- Real-time manipulation through drag and touch-based interaction
- Bond alignment and dynamic bond visualization
- Live energy readouts using TextMeshPro
- Marker-based AR placement using AR Foundation image tracking
- Functional real-time 3D graph projected in AR beside the polymer model
- Support for Android and iOS AR workflows through ARCore and ARKit packages

## Tech Stack

- Unity
- C#
- Unity Physics
- TextMeshPro
- AR Foundation
- ARCore XR Plugin
- ARKit XR Plugin

## Project Structure

Key files and folders:

- [Assets/BondManager.cs]: keeps bond cylinders aligned, positioned, and scaled between atoms
- [Assets/DragObject.cs]: user interaction for moving objects
- [Assets/PinchZoom.cs]: touch-based zoom interaction
- [Assets/ImageTrackingHandler.cs]: AR tracked-image response logic
- [Assets/TotalEnergyCalculator.cs]: sums bonded energy contributions
- [Assets/Scripts/Stretching/StretchingPotentialCalculator.cs]: bond stretching energy
- [Assets/Scripts/Bending/BendingPotentialCalculator.cs]: bending energy
- [Assets/Scripts/DihedralAngleCalculator.cs]: dihedral angle calculation
- [Assets/Scripts/TorsionalEnergyCalculator.cs]: torsional energy
- [Assets/Scenes/Scaled 1_forAndroid.unity]: one of the AR-ready scenes
- [Packages/manifest.json]: Unity package dependencies

## How It Works

### Polymer Model

The polymer is modeled as a tetramer-like chain of atoms connected by bonds. Spheres represent carbon atoms and cylinders visually represent covalent bonds.

### Bonded Interactions

The simulation computes three main bonded energy terms:

1. Stretching energy based on bond length deviation
2. Bending energy based on angle deviation between three connected atoms
3. Torsional energy based on the dihedral angle across four atoms

These values are combined into a total energy value that is displayed live in the scene.

### 3D Energy Graph

The project includes a working real-time 3D graph that is projected in AR beside the polymer model. This gives the user both a structural view of the polymer and a nearby data visualization view at the same time.

The graph is implemented through [Assets/GeneratePlotAR.cs] and [Assets/3D Plot AR 1.prefab]. It uses live simulation values to plot sampled points on a mesh-based 3D surface.

The plotting logic uses:

- dihedral angle for one axis
- end-to-end atom distance for one axis
- total energy for the vertical axis

This allows the graph to show how polymer configuration changes relate to energy during interaction in real time.

### AR Placement

The project currently includes AR Foundation packages in `Packages/manifest.json`. Marker-based image tracking is used to place the polymer object in AR space.

Note:
Legacy Vuforia-related assets and configuration files may still be present in the project assets from earlier experimentation, but the large local Vuforia package archives were removed from version control.

## Setup

### Requirements

- Unity 2022 or a compatible version that supports the included package set
- Android device with ARCore support or iOS device with ARKit support


### Recommended Package Check

Verify these packages are installed:

- `com.unity.xr.arfoundation`
- `com.unity.xr.arcore`
- `com.unity.xr.arkit`
- `com.unity.textmeshpro`

### Scene Setup

Open one of the scenes in [Assets/Scenes], especially:

- [Scaled 1_forAndroid.unity]
- [Scaled 1_Plt.unity]
- [Scaled.unity]
For the full AR visualization, the scene setup includes:

- the polymer model
- UI text for bonded and total energy values
- the AR-tracked 3D graph 

If AR image tracking is being used, make sure:

- An `XR Origin (AR)` exists in the scene
- `AR Tracked Image Manager` is configured
- A reference image library is assigned
- The tracked prefab references are assigned correctly in [Assets/ImageTrackingHandler.cs]

## Running the Project


### On Device

1. Switch build target to Android or iOS
2. Configure player settings and permissions as needed
3. Build and run on a compatible mobile device
4. Use the configured image target to place the model in AR



## Future Work

- Extend the polymer chain with more atoms and bonds
- Add non-bonded interactions
- Support dynamic chain growth at runtime
- Expand the current graphing system with richer analytics and visualization controls
- Expand support for VR and mixed reality headsets

## Research Context

This project originates from the report:
[Report(Tammy)_SRIP.pdf]

The work explores how AR can make molecular dynamics more intuitive by allowing users to directly manipulate a polymer model while observing structural and energetic changes in real time.
