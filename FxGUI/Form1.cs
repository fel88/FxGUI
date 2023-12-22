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
using System.Windows.Forms;
using System.Xml.Linq;

namespace FxGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            glControl = new OpenTK.GLControl(new OpenTK.Graphics.GraphicsMode(32, 24, 0, 4), 3, 3, OpenTK.Graphics.GraphicsContextFlags.Default);
            panel1.Controls.Add(glControl);
            glControl.Dock = DockStyle.Fill;
            glControl.Paint += Gl_Paint;
            te = new ICSharpCode.AvalonEdit.TextEditor();
            te.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinition("XML");
            elementHost1.Child = te;
            te.TextChanged += Te_TextChanged;
            ctx.GameWindow = glControl;


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
                Elements.Add(label);
                var w = item.Attribute("width").Value.ToFloat();
                label.Rect = new GuiBounds(0, 0, w, 30);
                label.Text = item.Attribute("text").Value;

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
            }
            catch (Exception ex)
            {

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

            GL.ClearColor(clearColor[0], clearColor[1], clearColor[2], clearColor[3]);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Enable(EnableCap.DepthTest);

            foreach (var ee in Elements)
            {
                ee.Draw(ctx);
            }



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
<screen backColor=""1f 1f 1f 1f"">
<stackPanel>
<label text=""test"" width=""200""/>
<button text=""button1"" width=""200""/>
</stackPanel>
</screen>
</root>");
        }
    }
}
