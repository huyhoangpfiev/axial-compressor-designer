using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AxialCompressorDesigner
{
    /// <summary>
    /// Panel for 2D blade profile design and flowpath calculation
    /// </summary>
    public class BladeProfile2DPanel : UserControl
    {
        private Panel drawingPanel;
        private Panel controlPanel;
        
        // Controls for blade parameters
        private GroupBox bladeParametersGroup;
        private Label lblChordLength;
        private NumericUpDown nudChordLength;
        private Label lblStaggerAngle;
        private NumericUpDown nudStaggerAngle;
        private Label lblMaxThickness;
        private NumericUpDown nudMaxThickness;
        private Label lblCamberAngle;
        private NumericUpDown nudCamberAngle;
        private Button btnCalculateProfile;
        private Button btnClearDrawing;
        
        // Flowpath parameters
        private GroupBox flowpathGroup;
        private Label lblInletRadius;
        private NumericUpDown nudInletRadius;
        private Label lblOutletRadius;
        private NumericUpDown nudOutletRadius;
        private Label lblAxialLength;
        private NumericUpDown nudAxialLength;
        private Button btnCalculateFlowpath;
        
        // Tandem blade parameters
        private GroupBox tandemGroup;
        private CheckBox chkEnableTandem;
        private Label lblGapRatio;
        private NumericUpDown nudGapRatio;
        private Label lblSecondChordRatio;
        private NumericUpDown nudSecondChordRatio;
        
        // Drawing data
        private List<PointF> bladeProfile1Points = new List<PointF>();
        private List<PointF> bladeProfile2Points = new List<PointF>();
        private List<PointF> flowpathInnerPoints = new List<PointF>();
        private List<PointF> flowpathOuterPoints = new List<PointF>();
        
        public BladeProfile2DPanel()
        {
            InitializeComponents();
            SetupLayout();
        }
        
        private void InitializeComponents()
        {
            this.BackColor = Color.White;
            
            // Drawing panel
            drawingPanel = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill
            };
            drawingPanel.Paint += DrawingPanel_Paint;
            
            // Control panel
            controlPanel = new Panel
            {
                Width = 300,
                Dock = DockStyle.Right,
                BackColor = SystemColors.Control,
                Padding = new Padding(10),
                AutoScroll = true
            };
            
            // Blade parameters group
            bladeParametersGroup = new GroupBox
            {
                Text = "Blade Parameters",
                Width = 280,
                Height = 220,
                Location = new Point(0, 0)
            };
            
            lblChordLength = new Label { Text = "Chord Length (mm):", Location = new Point(10, 25), Width = 140 };
            nudChordLength = new NumericUpDown { Location = new Point(160, 23), Width = 100, Minimum = 10, Maximum = 1000, Value = 100, DecimalPlaces = 1 };
            
            lblStaggerAngle = new Label { Text = "Stagger Angle (deg):", Location = new Point(10, 55), Width = 140 };
            nudStaggerAngle = new NumericUpDown { Location = new Point(160, 53), Width = 100, Minimum = -90, Maximum = 90, Value = 30, DecimalPlaces = 1 };
            
            lblMaxThickness = new Label { Text = "Max Thickness (%):", Location = new Point(10, 85), Width = 140 };
            nudMaxThickness = new NumericUpDown { Location = new Point(160, 83), Width = 100, Minimum = 1, Maximum = 20, Value = 10, DecimalPlaces = 1 };
            
            lblCamberAngle = new Label { Text = "Camber Angle (deg):", Location = new Point(10, 115), Width = 140 };
            nudCamberAngle = new NumericUpDown { Location = new Point(160, 113), Width = 100, Minimum = 0, Maximum = 60, Value = 20, DecimalPlaces = 1 };
            
            btnCalculateProfile = new Button { Text = "Calculate Profile", Location = new Point(10, 150), Width = 250 };
            btnCalculateProfile.Click += BtnCalculateProfile_Click;
            
            btnClearDrawing = new Button { Text = "Clear Drawing", Location = new Point(10, 180), Width = 250 };
            btnClearDrawing.Click += BtnClearDrawing_Click;
            
            bladeParametersGroup.Controls.AddRange(new Control[] {
                lblChordLength, nudChordLength,
                lblStaggerAngle, nudStaggerAngle,
                lblMaxThickness, nudMaxThickness,
                lblCamberAngle, nudCamberAngle,
                btnCalculateProfile, btnClearDrawing
            });
            
            // Flowpath group
            flowpathGroup = new GroupBox
            {
                Text = "Flowpath Parameters",
                Width = 280,
                Height = 180,
                Location = new Point(0, 230)
            };
            
            lblInletRadius = new Label { Text = "Inlet Radius (mm):", Location = new Point(10, 25), Width = 140 };
            nudInletRadius = new NumericUpDown { Location = new Point(160, 23), Width = 100, Minimum = 50, Maximum = 5000, Value = 200, DecimalPlaces = 1 };
            
            lblOutletRadius = new Label { Text = "Outlet Radius (mm):", Location = new Point(10, 55), Width = 140 };
            nudOutletRadius = new NumericUpDown { Location = new Point(160, 53), Width = 100, Minimum = 50, Maximum = 5000, Value = 250, DecimalPlaces = 1 };
            
            lblAxialLength = new Label { Text = "Axial Length (mm):", Location = new Point(10, 85), Width = 140 };
            nudAxialLength = new NumericUpDown { Location = new Point(160, 83), Width = 100, Minimum = 10, Maximum = 1000, Value = 150, DecimalPlaces = 1 };
            
            btnCalculateFlowpath = new Button { Text = "Calculate Flowpath", Location = new Point(10, 120), Width = 250 };
            btnCalculateFlowpath.Click += BtnCalculateFlowpath_Click;
            
            flowpathGroup.Controls.AddRange(new Control[] {
                lblInletRadius, nudInletRadius,
                lblOutletRadius, nudOutletRadius,
                lblAxialLength, nudAxialLength,
                btnCalculateFlowpath
            });
            
            // Tandem blade group
            tandemGroup = new GroupBox
            {
                Text = "Tandem Blade Configuration",
                Width = 280,
                Height = 150,
                Location = new Point(0, 420)
            };
            
            chkEnableTandem = new CheckBox { Text = "Enable Tandem Blade", Location = new Point(10, 25), Width = 250 };
            chkEnableTandem.CheckedChanged += ChkEnableTandem_CheckedChanged;
            
            lblGapRatio = new Label { Text = "Gap Ratio (%):", Location = new Point(10, 55), Width = 140 };
            nudGapRatio = new NumericUpDown { Location = new Point(160, 53), Width = 100, Minimum = 1, Maximum = 50, Value = 10, DecimalPlaces = 1, Enabled = false };
            
            lblSecondChordRatio = new Label { Text = "2nd Chord Ratio (%):", Location = new Point(10, 85), Width = 140 };
            nudSecondChordRatio = new NumericUpDown { Location = new Point(160, 83), Width = 100, Minimum = 10, Maximum = 100, Value = 80, DecimalPlaces = 1, Enabled = false };
            
            tandemGroup.Controls.AddRange(new Control[] {
                chkEnableTandem,
                lblGapRatio, nudGapRatio,
                lblSecondChordRatio, nudSecondChordRatio
            });
            
            controlPanel.Controls.Add(bladeParametersGroup);
            controlPanel.Controls.Add(flowpathGroup);
            controlPanel.Controls.Add(tandemGroup);
        }
        
        private void SetupLayout()
        {
            this.Controls.Add(drawingPanel);
            this.Controls.Add(controlPanel);
        }
        
        private void ChkEnableTandem_CheckedChanged(object? sender, EventArgs e)
        {
            bool enabled = chkEnableTandem.Checked;
            nudGapRatio.Enabled = enabled;
            nudSecondChordRatio.Enabled = enabled;
        }
        
        private void BtnCalculateProfile_Click(object? sender, EventArgs e)
        {
            CalculateBladeProfile();
            drawingPanel.Invalidate();
        }
        
        private void BtnClearDrawing_Click(object? sender, EventArgs e)
        {
            bladeProfile1Points.Clear();
            bladeProfile2Points.Clear();
            flowpathInnerPoints.Clear();
            flowpathOuterPoints.Clear();
            drawingPanel.Invalidate();
        }
        
        private void BtnCalculateFlowpath_Click(object? sender, EventArgs e)
        {
            CalculateFlowpath();
            drawingPanel.Invalidate();
        }
        
        private void CalculateBladeProfile()
        {
            // Get parameters
            double chordLength = (double)nudChordLength.Value;
            double staggerAngle = (double)nudStaggerAngle.Value * Math.PI / 180.0;
            double maxThickness = (double)nudMaxThickness.Value / 100.0;
            double camberAngle = (double)nudCamberAngle.Value * Math.PI / 180.0;
            
            // Generate first blade profile using NACA-like airfoil
            bladeProfile1Points.Clear();
            int numPoints = 100;
            
            for (int i = 0; i <= numPoints; i++)
            {
                double x = (double)i / numPoints;
                
                // NACA 4-digit style thickness distribution
                double yt = 5 * maxThickness * chordLength * 
                    (0.2969 * Math.Sqrt(x) - 0.1260 * x - 0.3516 * x * x + 
                     0.2843 * x * x * x - 0.1015 * x * x * x * x);
                
                // Camber line
                double yc = 0;
                if (x < 0.5)
                    yc = (camberAngle / (Math.PI * 0.5 * 0.5)) * (2 * 0.5 * x - x * x) * chordLength;
                else
                    yc = (camberAngle / (Math.PI * 0.5 * 0.5)) * ((1 - 2 * 0.5) + 2 * 0.5 * x - x * x) * chordLength;
                
                double xc = x * chordLength;
                
                // Apply stagger angle rotation
                double xRot = xc * Math.Cos(staggerAngle) - (yc + yt) * Math.Sin(staggerAngle);
                double yRot = xc * Math.Sin(staggerAngle) + (yc + yt) * Math.Cos(staggerAngle);
                
                if (i <= numPoints / 2)
                {
                    bladeProfile1Points.Add(new PointF((float)xRot, (float)yRot));
                }
            }
            
            // Add lower surface
            for (int i = numPoints / 2; i >= 0; i--)
            {
                double x = (double)i / numPoints;
                
                double yt = 5 * maxThickness * chordLength * 
                    (0.2969 * Math.Sqrt(x) - 0.1260 * x - 0.3516 * x * x + 
                     0.2843 * x * x * x - 0.1015 * x * x * x * x);
                
                double yc = 0;
                if (x < 0.5)
                    yc = (camberAngle / (Math.PI * 0.5 * 0.5)) * (2 * 0.5 * x - x * x) * chordLength;
                else
                    yc = (camberAngle / (Math.PI * 0.5 * 0.5)) * ((1 - 2 * 0.5) + 2 * 0.5 * x - x * x) * chordLength;
                
                double xc = x * chordLength;
                
                double xRot = xc * Math.Cos(staggerAngle) - (yc - yt) * Math.Sin(staggerAngle);
                double yRot = xc * Math.Sin(staggerAngle) + (yc - yt) * Math.Cos(staggerAngle);
                
                bladeProfile1Points.Add(new PointF((float)xRot, (float)yRot));
            }
            
            // Calculate second blade if tandem is enabled
            if (chkEnableTandem.Checked)
            {
                double gapRatio = (double)nudGapRatio.Value / 100.0;
                double secondChordRatio = (double)nudSecondChordRatio.Value / 100.0;
                double gap = chordLength * gapRatio;
                double secondChord = chordLength * secondChordRatio;
                
                bladeProfile2Points.Clear();
                
                for (int i = 0; i <= numPoints; i++)
                {
                    double x = (double)i / numPoints;
                    
                    double yt = 5 * maxThickness * secondChord * 
                        (0.2969 * Math.Sqrt(x) - 0.1260 * x - 0.3516 * x * x + 
                         0.2843 * x * x * x - 0.1015 * x * x * x * x);
                    
                    double yc = 0;
                    if (x < 0.5)
                        yc = (camberAngle / (Math.PI * 0.5 * 0.5)) * (2 * 0.5 * x - x * x) * secondChord;
                    else
                        yc = (camberAngle / (Math.PI * 0.5 * 0.5)) * ((1 - 2 * 0.5) + 2 * 0.5 * x - x * x) * secondChord;
                    
                    double xc = x * secondChord + chordLength + gap;
                    
                    double xRot = xc * Math.Cos(staggerAngle) - (yc + yt) * Math.Sin(staggerAngle);
                    double yRot = xc * Math.Sin(staggerAngle) + (yc + yt) * Math.Cos(staggerAngle);
                    
                    if (i <= numPoints / 2)
                    {
                        bladeProfile2Points.Add(new PointF((float)xRot, (float)yRot));
                    }
                }
                
                for (int i = numPoints / 2; i >= 0; i--)
                {
                    double x = (double)i / numPoints;
                    
                    double yt = 5 * maxThickness * secondChord * 
                        (0.2969 * Math.Sqrt(x) - 0.1260 * x - 0.3516 * x * x + 
                         0.2843 * x * x * x - 0.1015 * x * x * x * x);
                    
                    double yc = 0;
                    if (x < 0.5)
                        yc = (camberAngle / (Math.PI * 0.5 * 0.5)) * (2 * 0.5 * x - x * x) * secondChord;
                    else
                        yc = (camberAngle / (Math.PI * 0.5 * 0.5)) * ((1 - 2 * 0.5) + 2 * 0.5 * x - x * x) * secondChord;
                    
                    double xc = x * secondChord + chordLength + gap;
                    
                    double xRot = xc * Math.Cos(staggerAngle) - (yc - yt) * Math.Sin(staggerAngle);
                    double yRot = xc * Math.Sin(staggerAngle) + (yc - yt) * Math.Cos(staggerAngle);
                    
                    bladeProfile2Points.Add(new PointF((float)xRot, (float)yRot));
                }
            }
            else
            {
                bladeProfile2Points.Clear();
            }
        }
        
        private void CalculateFlowpath()
        {
            double inletRadius = (double)nudInletRadius.Value;
            double outletRadius = (double)nudOutletRadius.Value;
            double axialLength = (double)nudAxialLength.Value;
            
            flowpathInnerPoints.Clear();
            flowpathOuterPoints.Clear();
            
            int numPoints = 50;
            
            // Linear flowpath for simplicity (can be made more complex)
            for (int i = 0; i <= numPoints; i++)
            {
                double t = (double)i / numPoints;
                double x = t * axialLength;
                
                // Inner flowpath
                double rInner = inletRadius;
                flowpathInnerPoints.Add(new PointF((float)x, (float)rInner));
                
                // Outer flowpath (converging)
                double rOuter = inletRadius + (outletRadius - inletRadius) * t;
                flowpathOuterPoints.Add(new PointF((float)x, (float)rOuter));
            }
        }
        
        private void DrawingPanel_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            
            // Set up transformation for better visualization
            float scale = 1.5f;
            float offsetX = drawingPanel.Width / 4f;
            float offsetY = drawingPanel.Height / 2f;
            
            g.TranslateTransform(offsetX, offsetY);
            g.ScaleTransform(scale, -scale); // Flip Y axis for standard coordinate system
            
            // Draw axes
            using (Pen axisPen = new Pen(Color.LightGray, 0.5f))
            {
                g.DrawLine(axisPen, -100, 0, 400, 0);
                g.DrawLine(axisPen, 0, -200, 0, 200);
            }
            
            // Draw flowpath
            if (flowpathInnerPoints.Count > 1 && flowpathOuterPoints.Count > 1)
            {
                using (Pen flowpathPen = new Pen(Color.Blue, 1.5f))
                {
                    g.DrawLines(flowpathPen, flowpathInnerPoints.ToArray());
                    g.DrawLines(flowpathPen, flowpathOuterPoints.ToArray());
                }
            }
            
            // Draw first blade profile
            if (bladeProfile1Points.Count > 2)
            {
                using (Pen bladePen = new Pen(Color.Red, 2f))
                {
                    g.DrawLines(bladePen, bladeProfile1Points.ToArray());
                }
            }
            
            // Draw second blade profile (tandem)
            if (bladeProfile2Points.Count > 2)
            {
                using (Pen bladePen = new Pen(Color.DarkRed, 2f))
                {
                    g.DrawLines(bladePen, bladeProfile2Points.ToArray());
                }
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                drawingPanel?.Dispose();
                controlPanel?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
