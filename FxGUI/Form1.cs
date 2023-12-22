using FxEngine.Cameras;
using FxEngine.Gui;
using FxEngine.Shaders;
using ICSharpCode.AvalonEdit.Indentation.CSharp;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FxGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            glControl = new OpenTK.GLControl(new OpenTK.Graphics.GraphicsMode(32, 24, 0, 4), 3, 3, OpenTK.Graphics.GraphicsContextFlags.ForwardCompatible);
            panel1.Controls.Add(glControl);
            glControl.Dock = DockStyle.Fill;
            glControl.Paint += Gl_Paint;
            te = new ICSharpCode.AvalonEdit.TextEditor();
            te.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinition("XML");
            elementHost1.Child = te;
            te.TextChanged += Te_TextChanged;
            ctx.GameWindow = glControl;
            glControl.Resize += GlControl_Resize;


        }

        private void GlControl_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
        }

        List<GlGuiElement> Elements = new List<GlGuiElement>();

        void recParse(XElement item, GlGuiElement parent)
        {
            if (item.Name == "screen")
            {
                var spl = item.Attribute("backColor").Value.Split(new char[] { ';', ' ' }).ToArray();
                clearColor = spl.Select(z => z.ToFloat()).ToArray();

            }
            if (item.Name == "label")
            {
                var label = new NativeLabel() { };

                float w = 0;
                float l = 0;
                float t = 0;
                if (item.Attribute("left") != null) l = item.Attribute("left").Value.ToFloat();
                if (item.Attribute("width") != null) w = item.Attribute("width").Value.ToFloat();
                if (item.Attribute("top") != null) t = item.Attribute("top").Value.ToFloat();


                //var h = item.Attribute("height").Value.ToFloat();
                label.Rect = new GuiBounds(l, t, w, 30);
                label.Text = item.Attribute("text").Value;
                Elements.Add(label);

            }
            if (item.Name == "button")
            {
                var btn = new NativeButton() { };

                float w = 0;
                float l = 0;
                float t = 0;
                if (item.Attribute("left") != null) l = item.Attribute("left").Value.ToFloat();
                if (item.Attribute("width") != null) w = item.Attribute("width").Value.ToFloat();
                if (item.Attribute("top") != null) t = item.Attribute("top").Value.ToFloat();


                //var h = item.Attribute("height").Value.ToFloat();
                btn.Rect = new GuiBounds(l, t, w, 30);
                btn.Caption = item.Attribute("text").Value;
                Elements.Add(btn);

            }
            foreach (var citem in item.Elements())
            {

                recParse(citem, parent);
            }
        }
        private void Te_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var doc = XDocument.Parse(te.Text);
                Elements.Clear();
                foreach (var item in doc.Element("root").Elements())
                {
                    recParse(item, null);
                }
                toolStripStatusLabel1.Text = "Good parse";
                toolStripStatusLabel1.BackColor = Color.Green;
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = ex.Message;
                toolStripStatusLabel1.BackColor = Color.Red;
            }
        }

        bool first = true;
        ICSharpCode.AvalonEdit.TextEditor te;
        GLControl glControl;
        private void Gl_Paint(object sender, PaintEventArgs e)
        {
            if (!glControl.Context.IsCurrent)
            {
                glControl.MakeCurrent();
            }
            if (first)
            {
                ctx.TextRoutine = new FxEngine.Fonts.SDF.SdfTextRoutine();
                Bitmap bmp = new Bitmap(10, 10);
                var gr = Graphics.FromImage(bmp);
                ctx.TextRoutine.Init(gr);
                NativeGlGuiElement.Drawer = new NativeDrawProvider(ctx);
                first = false;
            }
            Redraw();

            glControl.SwapBuffers();

        }

        string FormatXml(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                // Handle and throw if fatal exception here; don't just ignore them
                return xml;
            }
        }

        float[] clearColor = new float[] { 0.1f, 0.1f, 0.1f, 1f };
        GlControlDrawingContext ctx = new GlControlDrawingContext();
        void Redraw()
        {
            var pos = ctx.PointToClient(Cursor.Position);
            var ev = new GlGuiEvent() { Position = new System.Drawing.Point(pos.X, pos.Y) };
            
            

            foreach (var item in Elements)
            {
                item.Event(ctx, ev);
                if (ev.Handled) return;
                
            }

            GL.ClearColor(clearColor[0], clearColor[1], clearColor[2], clearColor[3]);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            var o2 = Matrix4.CreateOrthographic(glControl.Width, glControl.Width, -25e4f, 25e4f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref o2);

            GL.MatrixMode(MatrixMode.Modelview);
            var m = Matrix4.LookAt(0, 0, 10, 0, 0, 0, 0, 1, 0);
            GL.LoadMatrix(ref m);
            GL.PushMatrix();
            GL.Translate(-glControl.Width / 2, -glControl.Height / 2, 0);

            GL.Disable(EnableCap.Lighting);
            
            GL.Disable(EnableCap.DepthTest);            
            GL.Color3(Color.Red);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 100, 0);
            GL.End(); 
            GL.Color3(Color.Blue);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(100, 0, 0);
            GL.End();
            foreach (var ee in Elements)
            {
                ee.Draw(ctx);
            }
            GL.PopMatrix();


        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            glControl.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            te.FontSize = 14;
            te.Text = FormatXml(@"
                <root>
<screen backColor=""0.1f 0.1f 0.1f 0.1f"">
<stackPanel>
<label text=""counter: 0"" top=""170"" width=""170""/>
<button text=""+"" left=""30"" top=""200"" width=""30""/>
<button text=""-"" left=""30"" top=""230"" width=""30""/>
</stackPanel>
</screen>
</root>");
        }
    }
}
