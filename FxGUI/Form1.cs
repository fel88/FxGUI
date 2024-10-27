using FxEngine.Gui;
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml.Linq;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;

namespace FxGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            //new OpenTK.Graphics.GraphicsMode(32, 24, 0, 4), 3, 3, OpenTK.Graphics.GraphicsContextFlags.ForwardCompatible
            GLControlSettings settings = new GLControlSettings();
            settings.NumberOfSamples = 8;
            settings.Profile = OpenTK.Windowing.Common.ContextProfile.Compatability;
            glControl = new GLControl(settings);
            glControl.MouseDown += GlControl_MouseDown;
            panel1.Controls.Add(glControl);
            glControl.Dock = DockStyle.Fill;
            glControl.Paint += Gl_Paint;
            te = new ICSharpCode.AvalonEdit.TextEditor();
            te.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinition("XML");
            elementHost1.Child = te;
            te.TextChanged += Te_TextChanged;
            ctx.GameWindow = glControl;
            glControl.Resize += GlControl_Resize;
            te.FontSize = 14;
            glControl.MouseEnter += GlControl_MouseEnter;
            glControl.KeyDown += GlControl_KeyDown;
        }

        private void GlControl_MouseEnter(object sender, EventArgs e)
        {
            glControl.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, System.Windows.Forms.Keys keyData)
        {
            if (glControl.ClientRectangle.Contains(glControl.PointToClient(System.Windows.Forms.Cursor.Position)))
            {
                KeyGlGuiEvent ev = new KeyGlGuiEvent
                {
                    Key = (Keys)keyData
                };
                if (keyData == System.Windows.Forms.Keys.Delete)
                {
                    ev.Key = Keys.Delete;
                }
                if (keyData == System.Windows.Forms.Keys.Back)
                {
                    ev.Key = Keys.Backspace;
                }
                if (keyData == System.Windows.Forms.Keys.Space)
                {
                    ev.Key = Keys.Space;
                }
                if (keyData >= System.Windows.Forms.Keys.D0 && keyData <= System.Windows.Forms.Keys.D9)
                {
                    ev.Key = Keys.KeyPad0+(keyData- System.Windows.Forms.Keys.D0);
                }

                foreach (var item in Elements)
                {
                    item.Event(ctx, ev);
                    if (ev.Handled)
                        break;
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern short GetKeyState(int keyCode);
        public const int KEY_PRESSED = 0x8000;

        public static bool IsKeyDown(Keys key)
        {
            return Convert.ToBoolean(GetKeyState((int)key) & KEY_PRESSED);
        }

        public static bool IsKeyPressed(Keys key)
        {
            return IsKeyDown(key);
        }
        internal static Tuple<Keys[], Keys[]> UpdateLatches()
        {
            List<Keys> pressed = new List<Keys>();
            List<Keys> pressed2 = new List<Keys>();
            for (int i = 0; i < 150; i++)
            {
                var key = (Keys)i;
                if (IsKeyPressed(key))
                {
                    pressed2.Add(key);
                    if (!latches[i])
                    {
                        latches[i] = true;
                        pressed.Add(key);
                    }
                }
                else
                {
                    latches[i] = false;
                }
            }
            return new Tuple<Keys[], Keys[]>(pressed.ToArray(), pressed2.ToArray());
        }

        public static bool[] latches = new bool[256];
        private void GlControl_KeyDown(object sender, KeyEventArgs e)
        {

            // var tuple = UpdateLatches();
            //  var pressed = tuple.Item1;
            //  var pressed2 = tuple.Item2;
            //  if (pressed2.Any())
            {
                KeyGlGuiEvent ev = new KeyGlGuiEvent();
                //ev.Key = pressed2.First();
                ev.Key = (Keys)e.KeyValue;


                foreach (var item in Elements)
                {
                    item.Event(ctx, ev);
                    if (ev.Handled) break;
                }
            }
        }

        private void GlControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var pos = ctx.PointToClient(System.Windows.Forms.Cursor.Position);
            var ev = new MouseClickGlGuiEvent() { Position = new System.Drawing.Point(pos.X, pos.Y) };

            foreach (var item in Elements)
            {
                item.Event(ctx, ev);
                if (ev.Handled)
                    break;
            }
        }

        private void GlControl_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
        }

        List<GlGuiElement> Elements = new List<GlGuiElement>();

        void recParse(XElement item, GlGuiElement parent)
        {
            float w = 100;
            float l = 0;
            float t = 0;
            float h = 30;
            if (item.Attribute("left") != null) l = item.Attribute("left").Value.ToFloat();
            if (item.Attribute("width") != null) w = item.Attribute("width").Value.ToFloat();
            if (item.Attribute("top") != null) t = item.Attribute("top").Value.ToFloat();
            if (item.Attribute("height") != null) h = item.Attribute("height").Value.ToFloat();


            if (item.Name == "screen")
            {
                var spl = item.Attribute("backColor").Value.Split(new char[] { ';', ' ' }).ToArray();
                clearColor = spl.Select(z => z.ToFloat()).ToArray();

            }
            if (item.Name == "label")
            {
                var label = new NativeLabel() { };


                //var h = item.Attribute("height").Value.ToFloat();
                label.Rect = new GuiBounds(l, t, w, h);
                label.Text = item.Attribute("text").Value;
                Elements.Add(label);

            }
            if (item.Name == "panel")
            {
                var label = new NativePanel() { };

                parent = label;
                //var h = item.Attribute("height").Value.ToFloat();
                label.Rect = new GuiBounds(l, t, w, h);
                label.Title = item.Attribute("title").Value;
                label.titleH = 30;
                Elements.Add(label);

            }
            if (item.Name == "button")
            {
                var btn = new NativeButton() { };



                //var h = item.Attribute("height").Value.ToFloat();
                btn.Rect = new GuiBounds(l, t, w, h);
                btn.Caption = item.Attribute("text").Value;
                if (parent is NativePanel np)
                {
                    np.Childs.Add(btn);
                }
                else
                    Elements.Add(btn);

            }
            if (item.Name == "textBox")
            {
                var btn = new NativeTextBox() { };

                //var h = item.Attribute("height").Value.ToFloat();
                btn.Rect = new GuiBounds(l, t, w, h);
                btn.Text = "0";
                Elements.Add(btn);

            }
            if (item.Name == "trackBar")
            {
                var btn = new NativeTrackBar() { };

                //var h = item.Attribute("height").Value.ToFloat();
                btn.Rect = new GuiBounds(l, t, w, h);

                Elements.Add(btn);

            }
            if (item.Name == "checkBox")
            {
                var btn = new NativeCheckBox() { };




                //var h = item.Attribute("height").Value.ToFloat();
                btn.Rect = new GuiBounds(l, t, w, h);
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
            var pos = ctx.PointToClient(System.Windows.Forms.Cursor.Position);
            var ev = new GlGuiEvent() { Position = new System.Drawing.Point(pos.X, pos.Y) };

            foreach (var item in Elements)
            {
                item.Event(ctx, ev);
                if (ev.Handled)
                    break;
            }


            GL.ClearColor(clearColor[0], clearColor[1], clearColor[2], clearColor[3]);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            var aspect = glControl.Width / (float)glControl.Height;
            var o2 = Matrix4.CreateOrthographic(glControl.Width, glControl.Width / aspect, -25e4f, 25e4f);

            //var o2 = Matrix4.CreateOrthographic(glControl.Width, glControl.Width, -25e4f, 25e4f);

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


        private void timer1_Tick(object sender, EventArgs e)
        {
            glControl.Invalidate();
        }


        private void sample1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

        private void sample2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            te.Text = FormatXml(@"
                <root>
<screen backColor=""0.1f 0.1f 0.1f 0.1f"">
<stackPanel>
<label text=""checked: false"" top=""170"" width=""170""/>
<checkBox text=""test"" left=""30"" top=""200"" width=""25"" height=""25""/>

</stackPanel>
</screen>
</root>");
        }

        private void sample3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            te.Text = FormatXml(@"
                <root>
<screen backColor=""0.1f 0.1f 0.1f 0.1f"">
<stackPanel>

<trackBar  left=""30"" top=""200"" width=""125"" height=""25""/>

</stackPanel>
</screen>
</root>");
        }

        private void sample4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            te.Text = FormatXml(@"
                <root>
<screen backColor=""0.1f 0.1f 0.1f 0.1f"">
<stackPanel>

<textBox  left=""30"" top=""200"" width=""125"" height=""25""/>

</stackPanel>
</screen>
</root>");
        }

        private void sample5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            te.Text = FormatXml(@"
                <root>
<screen backColor=""0.1f 0.1f 0.1f 0.1f"">
<panel title=""menu"" left=""30"" top=""200"" width=""155"" height=""155"">

<button text=""button 1""  left=""10"" top=""50"" width=""125"" height=""25""/>
<button text=""button 1""  left=""10"" top=""80"" width=""125"" height=""25""/>
<button text=""button 1""  left=""10"" top=""110"" width=""125"" height=""25""/>

</panel>
</screen>
</root>");
        }

        private void Form1_Leave(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var mf = new MessageFilter();
            System.Windows.Forms.Application.AddMessageFilter(mf);
        }
    }
}
