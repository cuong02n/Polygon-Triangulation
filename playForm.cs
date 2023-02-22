using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Polygon_Triangulation {
    public partial class playForm : Form {
        public List<Point> v = new List<Point>();

        private Graphics g;
        public List<int> ear_remove = new List<int>();
        private List<string> message = new List<string>();
        public const int MAX_DEQUE = 15;

        public GraphicsState firststate;
        public GraphicsState connectedState;
        public GraphicsState tmp;

        public playForm(string type) {
            main.type = type;
            InitializeComponent();
            g = splitContainer1.Panel1.CreateGraphics();
            firststate = g.Save();
            Show();
            MessageBox.Show("Vẽ hình bằng cách chấm các điểm trên màn hình");
        }

        private void playForm_Click(object sender, EventArgs e) {
        }

        private void playForm_MouseMove(object sender, MouseEventArgs e) {
            mouse_info.Text = $"Vị trí chuột: {e.X} , {e.Y}";
        }

        private void playForm_MouseClick(object sender, MouseEventArgs e) {
            MessageBox.Show($" location : {e.X} , {e.Y}");
        }


        private void Panel1_MouseMove(object sender, MouseEventArgs e) {
            mouse_info.Text = $" Vị trí chuột: {e.X} , {e.Y}";
        }

        private void Panel1_MouseClick(object sender, MouseEventArgs e) {
            Brush b = (Brushes.Aqua);
            g.FillEllipse(b, e.X - 2, e.Y - 2, 3, 3);
            v.Add(new Point(e.X, e.Y));
            SizeLabel.Text = v.Count.ToString();
        }


        private void start_MouseClick(object sender, MouseEventArgs e) {
            g.FillPolygon(Brushes.White, v.ToArray());
            if (v.Count < 3) {
                MessageBox.Show("Số đỉnh > 3");
            } else {
                process(v);
            }
        }

        private void clear_MouseClick(object sender, MouseEventArgs e) {
            splitContainer1.Panel1.Refresh();
            g.Restore(firststate);
            v.Clear();
            message.Clear();
            MessageLabel.Text = "Message";
            SizeLabel.Text = "0";
        }

        private void display_message(string err) {
            message.Add(err);
            if (message.Count >= MAX_DEQUE) {
                message.RemoveAt(0);
            }

            StringBuilder sb = new StringBuilder("");

            for (int i = 0; i < message.Count; i++) {
                sb.Append(message[i] + "\n");
            }

            MessageLabel.Text = sb.ToString();
        }

        public bool is_in_triangle(Vector2 p, Vector2 a, Vector2 b, Vector2 c) {
            Vector2 ab = b - a;
            Vector2 bc = c - b;
            Vector2 ca = c - a;

            Vector2 ap = p - a;
            Vector2 bp = p - a;
            Vector2 cp = p - c;
            
            if (Cross(ab, ap) > 0f || Cross(bc, bp) > 0f || Cross(ca, cp) > 0f) {
                return false;
            }

            return true;
        }

        private void draw_edge() {
            connectedState = g.Save();
            for (int i = 0; i < v.Count()-1; i++) {
                g.DrawLine(new Pen(Brushes.Chartreuse,3),v[i],v[i+1]);
            }
            g.DrawLine(new Pen(Brushes.Chartreuse,3),v.Last(),v.First());
        }

        private void highlight(Point i) {
            tmp = g.Save();
        }
        private void process(List<Point> l) {
            draw_edge();
            
            List<int> indexlist = new List<int>();

            List<Vector2> v2 = new List<Vector2>();
            
            for (int i = 0; i < v.Count; i++) {
                indexlist.Add(i);
                v2.Add(new Vector2(v[i].X, v[i].Y));
            }
            
            while (indexlist.Count > 3) {
                display_message("while loop");
                for (int i = 0; i < indexlist.Count; i++) {
                    int a = indexlist[i];
                    int b = getItem(indexlist, i - 1);
                    int c = getItem(indexlist, i + 1);

                    
                    
                    Vector2 v_ab = v2[b] - v2[a];
                    Vector2 v_ac = v2[c] - v2[a];

                    display_message($"point {a} , {b}, {c}");
                    if (Cross(v_ab, v_ac) < 0f) {
                        // goc 
                        display_message("continue ");
                        continue;
                    }

                    bool isEar = true;

                    for (int j = 0; j < v2.Count; j++) {
                        if (j == a || j == b || j == c) {
                            continue;
                        }

                        if (is_in_triangle(v2[j], v2[a], v2[b], v2[c])) {
                            isEar = false;
                            break;
                        }
                    }

                    if (isEar) {
                        display_message("Đỉnh này là tai --> xóa bỏ");
                        indexlist.RemoveAt(i);
                        break;
                    }
                }
            }

            SizeLabel.Text = indexlist.Count.ToString();
        }


        public float Cross(Vector2 a, Vector2 b) {
            return a.X * b.Y - a.Y * b.X;
        }

        public T getItem<T>(List<T> l, int index) {
            if (index < 0) {
                return l[index + l.Count];
            }

            if (index >= l.Count) {
                return l[index - l.Count];
            }

            return l[index];
        }
    }
}