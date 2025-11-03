using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK.WinForms;

namespace AxialCompressorDesigner
{
    /// <summary>
    /// Panel for 3D visualization of blades and compressor stages
    /// </summary>
    public class Blade3DViewerPanel : UserControl
    {
        private GLControl glControl;
        private Panel controlPanel;
        
        // Controls
        private GroupBox viewControlGroup;
        private Label lblRotationX;
        private TrackBar trackRotationX;
        private Label lblRotationY;
        private TrackBar trackRotationY;
        private Label lblRotationZ;
        private TrackBar trackRotationZ;
        private Label lblZoom;
        private TrackBar trackZoom;
        private Button btnResetView;
        
        private GroupBox stageControlGroup;
        private Label lblNumBlades;
        private NumericUpDown nudNumBlades;
        private Label lblStageHeight;
        private NumericUpDown nudStageHeight;
        private Button btnGenerate3D;
        private CheckBox chkShowGrid;
        private CheckBox chkShowAxes;
        
        // 3D rendering state
        private float rotationX = 30f;
        private float rotationY = 30f;
        private float rotationZ = 0f;
        private float zoom = -5f;
        private bool isLoaded = false;
        
        // Blade geometry data
        private List<Vector3> bladeVertices = new List<Vector3>();
        private List<int> bladeIndices = new List<int>();
        
        public Blade3DViewerPanel()
        {
            InitializeComponents();
            SetupLayout();
        }
        
        private void InitializeComponents()
        {
            this.BackColor = Color.White;
            
            // Create OpenTK control
            var settings = new OpenTK.WinForms.GLControlSettings
            {
                APIVersion = new Version(3, 3),
                Profile = OpenTK.Windowing.Common.ContextProfile.Compatibility
            };
            
            glControl = new GLControl(settings)
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Black
            };
            glControl.Load += GlControl_Load;
            glControl.Paint += GlControl_Paint;
            glControl.Resize += GlControl_Resize;
            
            // Control panel
            controlPanel = new Panel
            {
                Width = 300,
                Dock = DockStyle.Right,
                BackColor = SystemColors.Control,
                Padding = new Padding(10),
                AutoScroll = true
            };
            
            // View control group
            viewControlGroup = new GroupBox
            {
                Text = "View Control",
                Width = 280,
                Height = 280,
                Location = new Point(0, 0)
            };
            
            lblRotationX = new Label { Text = "Rotation X: 30°", Location = new Point(10, 25), Width = 260 };
            trackRotationX = new TrackBar { Location = new Point(10, 45), Width = 260, Minimum = -180, Maximum = 180, Value = 30, TickFrequency = 30 };
            trackRotationX.ValueChanged += TrackRotation_ValueChanged;
            
            lblRotationY = new Label { Text = "Rotation Y: 30°", Location = new Point(10, 95), Width = 260 };
            trackRotationY = new TrackBar { Location = new Point(10, 115), Width = 260, Minimum = -180, Maximum = 180, Value = 30, TickFrequency = 30 };
            trackRotationY.ValueChanged += TrackRotation_ValueChanged;
            
            lblRotationZ = new Label { Text = "Rotation Z: 0°", Location = new Point(10, 165), Width = 260 };
            trackRotationZ = new TrackBar { Location = new Point(10, 185), Width = 260, Minimum = -180, Maximum = 180, Value = 0, TickFrequency = 30 };
            trackRotationZ.ValueChanged += TrackRotation_ValueChanged;
            
            lblZoom = new Label { Text = "Zoom: 5.0", Location = new Point(10, 220), Width = 260 };
            trackZoom = new TrackBar { Location = new Point(10, 240), Width = 180, Minimum = 10, Maximum = 200, Value = 50, TickFrequency = 20 };
            trackZoom.ValueChanged += TrackZoom_ValueChanged;
            
            btnResetView = new Button { Text = "Reset View", Location = new Point(200, 240), Width = 70, Height = 25 };
            btnResetView.Click += BtnResetView_Click;
            
            viewControlGroup.Controls.AddRange(new Control[] {
                lblRotationX, trackRotationX,
                lblRotationY, trackRotationY,
                lblRotationZ, trackRotationZ,
                lblZoom, trackZoom,
                btnResetView
            });
            
            // Stage control group
            stageControlGroup = new GroupBox
            {
                Text = "Stage Configuration",
                Width = 280,
                Height = 200,
                Location = new Point(0, 290)
            };
            
            lblNumBlades = new Label { Text = "Number of Blades:", Location = new Point(10, 25), Width = 140 };
            nudNumBlades = new NumericUpDown { Location = new Point(160, 23), Width = 100, Minimum = 3, Maximum = 100, Value = 20 };
            
            lblStageHeight = new Label { Text = "Stage Height (mm):", Location = new Point(10, 55), Width = 140 };
            nudStageHeight = new NumericUpDown { Location = new Point(160, 53), Width = 100, Minimum = 10, Maximum = 1000, Value = 200 };
            
            btnGenerate3D = new Button { Text = "Generate 3D Model", Location = new Point(10, 90), Width = 250 };
            btnGenerate3D.Click += BtnGenerate3D_Click;
            
            chkShowGrid = new CheckBox { Text = "Show Grid", Location = new Point(10, 130), Width = 120, Checked = true };
            chkShowGrid.CheckedChanged += ChkShowGrid_CheckedChanged;
            
            chkShowAxes = new CheckBox { Text = "Show Axes", Location = new Point(140, 130), Width = 120, Checked = true };
            chkShowAxes.CheckedChanged += ChkShowAxes_CheckedChanged;
            
            stageControlGroup.Controls.AddRange(new Control[] {
                lblNumBlades, nudNumBlades,
                lblStageHeight, nudStageHeight,
                btnGenerate3D,
                chkShowGrid, chkShowAxes
            });
            
            controlPanel.Controls.Add(viewControlGroup);
            controlPanel.Controls.Add(stageControlGroup);
        }
        
        private void SetupLayout()
        {
            this.Controls.Add(glControl);
            this.Controls.Add(controlPanel);
        }
        
        private void GlControl_Load(object? sender, EventArgs e)
        {
            if (glControl == null) return;
            
            glControl.MakeCurrent();
            
            // Set up OpenGL
            GL.ClearColor(0.2f, 0.2f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.ColorMaterial);
            
            // Set up lighting
            float[] lightPosition = { 1.0f, 1.0f, 1.0f, 0.0f };
            float[] lightAmbient = { 0.3f, 0.3f, 0.3f, 1.0f };
            float[] lightDiffuse = { 0.8f, 0.8f, 0.8f, 1.0f };
            
            GL.Light(LightName.Light0, LightParameter.Position, lightPosition);
            GL.Light(LightName.Light0, LightParameter.Ambient, lightAmbient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, lightDiffuse);
            
            isLoaded = true;
            GenerateSimpleBlade();
        }
        
        private void GlControl_Resize(object? sender, EventArgs e)
        {
            if (!isLoaded || glControl == null) return;
            
            glControl.MakeCurrent();
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
            
            double aspectRatio = (double)glControl.Width / glControl.Height;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            
            // Set up perspective projection
            double fov = 45.0;
            double near = 0.1;
            double far = 100.0;
            double top = near * Math.Tan(fov * Math.PI / 360.0);
            double bottom = -top;
            double left = bottom * aspectRatio;
            double right = top * aspectRatio;
            
            GL.Frustum(left, right, bottom, top, near, far);
            GL.MatrixMode(MatrixMode.Modelview);
        }
        
        private void GlControl_Paint(object? sender, PaintEventArgs e)
        {
            if (!isLoaded || glControl == null) return;
            
            glControl.MakeCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            GL.LoadIdentity();
            GL.Translate(0f, 0f, zoom);
            GL.Rotate(rotationX, 1f, 0f, 0f);
            GL.Rotate(rotationY, 0f, 1f, 0f);
            GL.Rotate(rotationZ, 0f, 0f, 1f);
            
            // Draw grid if enabled
            if (chkShowGrid.Checked)
            {
                DrawGrid();
            }
            
            // Draw axes if enabled
            if (chkShowAxes.Checked)
            {
                DrawAxes();
            }
            
            // Draw blade
            DrawBlade();
            
            glControl.SwapBuffers();
        }
        
        private void DrawGrid()
        {
            GL.Disable(EnableCap.Lighting);
            GL.Color3(0.3f, 0.3f, 0.3f);
            GL.Begin(PrimitiveType.Lines);
            
            for (int i = -10; i <= 10; i++)
            {
                GL.Vertex3(i * 0.5f, 0f, -5f);
                GL.Vertex3(i * 0.5f, 0f, 5f);
                
                GL.Vertex3(-5f, 0f, i * 0.5f);
                GL.Vertex3(5f, 0f, i * 0.5f);
            }
            
            GL.End();
            GL.Enable(EnableCap.Lighting);
        }
        
        private void DrawAxes()
        {
            GL.Disable(EnableCap.Lighting);
            GL.Begin(PrimitiveType.Lines);
            
            // X axis - red
            GL.Color3(1f, 0f, 0f);
            GL.Vertex3(0f, 0f, 0f);
            GL.Vertex3(2f, 0f, 0f);
            
            // Y axis - green
            GL.Color3(0f, 1f, 0f);
            GL.Vertex3(0f, 0f, 0f);
            GL.Vertex3(0f, 2f, 0f);
            
            // Z axis - blue
            GL.Color3(0f, 0f, 1f);
            GL.Vertex3(0f, 0f, 0f);
            GL.Vertex3(0f, 0f, 2f);
            
            GL.End();
            GL.Enable(EnableCap.Lighting);
        }
        
        private void DrawBlade()
        {
            if (bladeVertices.Count == 0) return;
            
            GL.Color3(0.7f, 0.7f, 0.9f);
            
            // Draw blade as triangles
            GL.Begin(PrimitiveType.Triangles);
            for (int i = 0; i < bladeIndices.Count; i += 3)
            {
                Vector3 v1 = bladeVertices[bladeIndices[i]];
                Vector3 v2 = bladeVertices[bladeIndices[i + 1]];
                Vector3 v3 = bladeVertices[bladeIndices[i + 2]];
                
                // Calculate normal
                Vector3 normal = CalculateNormal(v1, v2, v3);
                GL.Normal3(normal.X, normal.Y, normal.Z);
                
                GL.Vertex3(v1.X, v1.Y, v1.Z);
                GL.Vertex3(v2.X, v2.Y, v2.Z);
                GL.Vertex3(v3.X, v3.Y, v3.Z);
            }
            GL.End();
            
            // Draw wireframe
            GL.Disable(EnableCap.Lighting);
            GL.Color3(0.2f, 0.2f, 0.4f);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            
            GL.Begin(PrimitiveType.Triangles);
            for (int i = 0; i < bladeIndices.Count; i += 3)
            {
                Vector3 v1 = bladeVertices[bladeIndices[i]];
                Vector3 v2 = bladeVertices[bladeIndices[i + 1]];
                Vector3 v3 = bladeVertices[bladeIndices[i + 2]];
                
                GL.Vertex3(v1.X, v1.Y, v1.Z);
                GL.Vertex3(v2.X, v2.Y, v2.Z);
                GL.Vertex3(v3.X, v3.Y, v3.Z);
            }
            GL.End();
            
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.Enable(EnableCap.Lighting);
        }
        
        private void GenerateSimpleBlade()
        {
            bladeVertices.Clear();
            bladeIndices.Clear();
            
            int numBlades = (int)nudNumBlades.Value;
            float stageHeight = (float)nudStageHeight.Value / 100f;
            
            // Generate a simple airfoil profile
            int profilePoints = 20;
            float chordLength = 1.0f;
            float maxThickness = 0.1f;
            
            for (int blade = 0; blade < numBlades; blade++)
            {
                float angleStep = 360f / numBlades;
                float angle = blade * angleStep * (float)Math.PI / 180f;
                
                int baseIndex = bladeVertices.Count;
                
                // Generate blade profile at root and tip
                for (int h = 0; h <= 1; h++) // 0 = root, 1 = tip
                {
                    float height = h * stageHeight;
                    float radius = 1.0f + height * 0.1f; // Slight taper
                    
                    for (int i = 0; i < profilePoints; i++)
                    {
                        float t = (float)i / (profilePoints - 1);
                        float x = t * chordLength - chordLength / 2f;
                        
                        // Simple symmetric airfoil
                        float thickness = maxThickness * (float)(4 * t * (1 - t));
                        
                        // Upper surface
                        float y = thickness;
                        float z = x;
                        
                        // Rotate around Y axis for circular arrangement
                        float xRot = radius * (float)Math.Cos(angle);
                        float zRot = radius * (float)Math.Sin(angle);
                        
                        bladeVertices.Add(new Vector3(xRot + z * (float)Math.Cos(angle), y + height, zRot + z * (float)Math.Sin(angle)));
                    }
                    
                    // Lower surface
                    for (int i = profilePoints - 1; i >= 0; i--)
                    {
                        float t = (float)i / (profilePoints - 1);
                        float x = t * chordLength - chordLength / 2f;
                        float thickness = maxThickness * (float)(4 * t * (1 - t));
                        
                        float y = -thickness;
                        float z = x;
                        
                        float xRot = radius * (float)Math.Cos(angle);
                        float zRot = radius * (float)Math.Sin(angle);
                        
                        bladeVertices.Add(new Vector3(xRot + z * (float)Math.Cos(angle), y + height, zRot + z * (float)Math.Sin(angle)));
                    }
                }
                
                // Create triangles connecting root and tip
                int pointsPerSection = profilePoints * 2;
                for (int i = 0; i < pointsPerSection - 1; i++)
                {
                    int p1 = baseIndex + i;
                    int p2 = baseIndex + i + 1;
                    int p3 = baseIndex + i + pointsPerSection;
                    int p4 = baseIndex + i + pointsPerSection + 1;
                    
                    // Triangle 1
                    bladeIndices.Add(p1);
                    bladeIndices.Add(p3);
                    bladeIndices.Add(p2);
                    
                    // Triangle 2
                    bladeIndices.Add(p2);
                    bladeIndices.Add(p3);
                    bladeIndices.Add(p4);
                }
            }
        }
        
        private Vector3 CalculateNormal(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            Vector3 u = new Vector3(v2.X - v1.X, v2.Y - v1.Y, v2.Z - v1.Z);
            Vector3 v = new Vector3(v3.X - v1.X, v3.Y - v1.Y, v3.Z - v1.Z);
            
            Vector3 normal = new Vector3(
                u.Y * v.Z - u.Z * v.Y,
                u.Z * v.X - u.X * v.Z,
                u.X * v.Y - u.Y * v.X
            );
            
            float length = (float)Math.Sqrt(normal.X * normal.X + normal.Y * normal.Y + normal.Z * normal.Z);
            if (length > 0)
            {
                normal.X /= length;
                normal.Y /= length;
                normal.Z /= length;
            }
            
            return normal;
        }
        
        private void TrackRotation_ValueChanged(object? sender, EventArgs e)
        {
            rotationX = trackRotationX.Value;
            rotationY = trackRotationY.Value;
            rotationZ = trackRotationZ.Value;
            
            lblRotationX.Text = $"Rotation X: {rotationX}°";
            lblRotationY.Text = $"Rotation Y: {rotationY}°";
            lblRotationZ.Text = $"Rotation Z: {rotationZ}°";
            
            glControl?.Invalidate();
        }
        
        private void TrackZoom_ValueChanged(object? sender, EventArgs e)
        {
            zoom = -trackZoom.Value / 10f;
            lblZoom.Text = $"Zoom: {trackZoom.Value / 10f:F1}";
            glControl?.Invalidate();
        }
        
        private void BtnResetView_Click(object? sender, EventArgs e)
        {
            trackRotationX.Value = 30;
            trackRotationY.Value = 30;
            trackRotationZ.Value = 0;
            trackZoom.Value = 50;
        }
        
        private void BtnGenerate3D_Click(object? sender, EventArgs e)
        {
            GenerateSimpleBlade();
            glControl?.Invalidate();
        }
        
        private void ChkShowGrid_CheckedChanged(object? sender, EventArgs e)
        {
            glControl?.Invalidate();
        }
        
        private void ChkShowAxes_CheckedChanged(object? sender, EventArgs e)
        {
            glControl?.Invalidate();
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                glControl?.Dispose();
                controlPanel?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
    
    // Simple Vector3 structure for 3D coordinates
    public struct Vector3
    {
        public float X, Y, Z;
        
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
