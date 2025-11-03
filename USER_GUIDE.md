# User Guide - Axial Compressor Designer

## Table of Contents
1. [Introduction](#introduction)
2. [Interface Overview](#interface-overview)
3. [2D Blade Profile Design](#2d-blade-profile-design)
4. [3D Visualization](#3d-visualization)
5. [OpenVOGEL Integration](#openvogel-integration)
6. [Workflow Examples](#workflow-examples)
7. [Tips and Best Practices](#tips-and-best-practices)

## Introduction

The Axial Compressor Designer is a specialized tool for designing and analyzing axial compressor blades with tandem blade configurations. This guide will walk you through all features and typical workflows.

## Interface Overview

The application has three main tabs:

1. **2D Blade Profile & Flowpath**: Design and calculate blade geometry
2. **3D Visualization**: View and interact with 3D blade models
3. **OpenVOGEL Solver**: Run aerodynamic analysis

## 2D Blade Profile Design

### Basic Blade Parameters

#### Chord Length
- **Range**: 10-1000 mm
- **Description**: The distance from the leading edge to trailing edge
- **Typical Values**: 50-200 mm for small compressors, 100-500 mm for larger ones
- **Effect**: Longer chords provide more area for work transfer but increase weight

#### Stagger Angle
- **Range**: -90° to 90°
- **Description**: Angle of the blade chord relative to the axial direction
- **Typical Values**: 20-50° for rotor blades, 10-30° for stator blades
- **Effect**: Affects the flow turning angle and blade loading

#### Max Thickness
- **Range**: 1-20% of chord
- **Description**: Maximum thickness as a percentage of chord length
- **Typical Values**: 8-15%
- **Effect**: Thicker blades are stronger but create more drag

#### Camber Angle
- **Range**: 0-60°
- **Description**: The amount of curvature in the blade
- **Typical Values**: 15-30° for moderate turning, 30-45° for high turning
- **Effect**: Higher camber increases flow turning but may cause flow separation

### Using the 2D Designer

1. **Set Blade Parameters**:
   - Enter desired values in the right panel
   - Start with default values if unsure

2. **Calculate Profile**:
   - Click "Calculate Profile" button
   - The blade profile appears in the drawing area
   - Red line shows the blade shape

3. **Enable Tandem Configuration**:
   - Check "Enable Tandem Blade" checkbox
   - Set gap ratio (distance between blades as % of first chord)
   - Set second chord ratio (size of second blade as % of first chord)
   - Recalculate to see both blades

4. **Design Flowpath**:
   - Set inlet radius (inner radius at inlet)
   - Set outlet radius (inner radius at outlet)
   - Set axial length (blade row length)
   - Click "Calculate Flowpath"
   - Blue lines show the flowpath boundaries

### Tandem Blade Configuration

Tandem blades are used for:
- Higher flow turning angles
- Reduced blade loading
- Better flow control at high incidence

**Gap Ratio Guidelines**:
- Small gaps (5-15%): Better for structural integrity
- Large gaps (15-30%): Better for flow control

**Second Chord Ratio Guidelines**:
- 60-80%: Most common range
- Smaller second blade: Less weight, specialized applications
- Larger second blade: More turning capability

## 3D Visualization

### View Controls

#### Rotation Controls
- **Rotation X**: Pitch (looking up/down)
- **Rotation Y**: Yaw (looking left/right)
- **Rotation Z**: Roll (rotating view)
- Use sliders to adjust each axis independently

#### Zoom Control
- Drag the zoom slider to move closer or farther
- Values: 1.0 (close) to 20.0 (far)

#### Reset View
- Click "Reset View" to return to default viewing angle

### Stage Configuration

#### Number of Blades
- **Range**: 3-100 blades
- **Typical Values**: 15-40 for rotors, 20-50 for stators
- **Considerations**: More blades = smoother flow, less blade loading

#### Stage Height
- **Range**: 10-1000 mm
- **Description**: Radial height of the blade (hub to tip)
- **Effect**: Affects aspect ratio and flow capacity

### Display Options

- **Show Grid**: Toggle ground reference grid
- **Show Axes**: Toggle X (red), Y (green), Z (blue) axes
- Both help with orientation and scale reference

### Generating 3D Model

1. Set number of blades and stage height
2. Click "Generate 3D Model"
3. The 3D model appears with all blades arranged in a circle
4. Use rotation and zoom to inspect from all angles

## OpenVOGEL Integration

### Flight Conditions

#### Velocity (m/s)
- **Range**: 0-500 m/s
- **Description**: Freestream velocity entering the compressor
- **Typical Values**: 50-200 m/s for subsonic compressors

#### Density (kg/m³)
- **Standard Sea Level**: 1.225 kg/m³
- **High Altitude**: Lower values (e.g., 0.5-1.0 kg/m³)
- **Effect**: Affects Reynolds number and aerodynamic forces

#### Viscosity (Pa·s)
- **Standard Air**: 1.81 × 10⁻⁵ Pa·s
- **Effect**: Affects boundary layer and skin friction drag

#### Mach Number
- **Subsonic**: < 0.8
- **Transonic**: 0.8-1.2
- **Supersonic**: > 1.2
- **Note**: VLM methods are most accurate for subsonic flows

### Solver Settings

#### Max Iterations
- **Range**: 10-10000
- **Typical**: 1000 for standard analysis
- **More iterations**: Better convergence but longer computation time

#### Convergence Criteria
- **Standard**: 0.0001
- **High Accuracy**: 0.00001
- **Quick Analysis**: 0.001

#### Flow Options
- **Unsteady Flow Analysis**: For time-dependent phenomena
- **Viscous Flow Effects**: Include boundary layer effects (recommended)

### Running Analysis

1. **Export Geometry**:
   - Click "Export Geometry to OpenVOGEL"
   - File is saved to Export directory
   - Contains all geometry and flow parameters

2. **Set OpenVOGEL Path**:
   - Click "Browse..." button
   - Navigate to OpenVOGEL installation folder
   - Click OK

3. **Run Solver**:
   - Click "Run OpenVOGEL Solver"
   - Note: Requires OpenVOGEL to be installed
   - Results appear in the results panel

4. **Import Results**:
   - If running OpenVOGEL separately
   - Click "Import Results"
   - Select the results file
   - View in the results panel

### Interpreting Results

The solver provides:
- **Lift Coefficient (CL)**: Measure of lift force
- **Drag Coefficient (CD)**: Measure of drag force
- **Moment Coefficient (CM)**: Pitching moment
- **L/D Ratio**: Efficiency metric (higher is better)

**Good Performance Indicators**:
- High L/D ratio (> 20)
- Stable pressure distribution
- No flow separation

## Workflow Examples

### Example 1: Basic Single Blade Design

1. Open application
2. Go to "2D Blade Profile & Flowpath" tab
3. Set parameters:
   - Chord Length: 100 mm
   - Stagger Angle: 30°
   - Max Thickness: 10%
   - Camber Angle: 20°
4. Click "Calculate Profile"
5. Review 2D profile
6. Go to "3D Visualization" tab
7. Set 20 blades, 200 mm height
8. Click "Generate 3D Model"
9. Inspect from multiple angles

### Example 2: Tandem Blade Design

1. Start with basic blade (Example 1, steps 1-4)
2. In 2D tab, check "Enable Tandem Blade"
3. Set Gap Ratio: 15%
4. Set Second Chord Ratio: 75%
5. Click "Calculate Profile"
6. Verify both blades appear correctly
7. Generate 3D model with tandem configuration
8. Export geometry for analysis

### Example 3: Complete Analysis Workflow

1. Design blade in 2D tab
2. Verify in 3D tab
3. Go to "OpenVOGEL Solver" tab
4. Set flight conditions:
   - Velocity: 100 m/s
   - Standard atmospheric conditions
5. Configure solver settings
6. Export geometry
7. Run solver (if installed)
8. Review results
9. Export results to file
10. Iterate design based on results

## Tips and Best Practices

### Design Tips

1. **Start Conservative**:
   - Use moderate camber angles initially
   - Increase gradually if needed

2. **Check Blade Loading**:
   - Very thin blades (< 5%) may be structurally weak
   - Very thick blades (> 15%) create excessive drag

3. **Tandem Blade Guidelines**:
   - Use for turning angles > 40°
   - Gap should be 10-20% of chord
   - Second blade typically 70-80% of first chord

4. **Stage Configuration**:
   - More blades reduce individual blade loading
   - Aspect ratio (height/chord) typically 1-3

### Analysis Tips

1. **Verify Geometry First**:
   - Always check 2D and 3D views before analysis
   - Ensure no intersecting surfaces

2. **Start with Coarse Settings**:
   - Use lower iteration counts for initial runs
   - Refine once design is close to target

3. **Document Your Work**:
   - Export geometry files regularly
   - Save results for each design iteration
   - Keep notes on what works

4. **Iterate Systematically**:
   - Change one parameter at a time
   - Compare results to understand effects
   - Build up complexity gradually

### Performance Optimization

1. **For High Efficiency**:
   - Optimize L/D ratio
   - Minimize flow separation
   - Use smooth transitions

2. **For High Turning**:
   - Consider tandem blades
   - Increase camber carefully
   - Check for flow separation

3. **For Wide Operating Range**:
   - Moderate blade loading
   - Conservative angles
   - Robust geometry

## Troubleshooting

### 2D View Issues

**Problem**: Profile doesn't appear after calculation
**Solution**: Check that chord length is not zero, click "Clear Drawing" then recalculate

**Problem**: Tandem blades overlap
**Solution**: Increase gap ratio or decrease second chord ratio

### 3D View Issues

**Problem**: Black screen or no display
**Solution**: Check graphics drivers, ensure OpenGL 3.3+ support

**Problem**: Model appears distorted
**Solution**: Reset view, adjust zoom level

### Solver Issues

**Problem**: Export fails
**Solution**: Check write permissions for Export directory

**Problem**: Solver not running
**Solution**: Verify OpenVOGEL installation path is correct

## Keyboard Shortcuts

Currently, the application uses mouse-based controls. Future versions may include:
- Ctrl+N: New design
- Ctrl+O: Open design
- Ctrl+S: Save design
- Ctrl+E: Export geometry

## Further Resources

- OpenVOGEL Documentation: https://github.com/npapnet/OpenVOGEL
- Compressor Design Theory: Academic textbooks on turbomachinery
- GitHub Repository: For latest updates and community support

## Getting Help

For additional assistance:
1. Check this user guide
2. Review INSTALLATION.md for setup issues
3. Open an issue on GitHub
4. Include screenshots when reporting problems

---

**Version**: 1.0
**Last Updated**: 2024
**Author**: Axial Compressor Designer Project
