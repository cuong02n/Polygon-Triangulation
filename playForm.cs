using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Polygon_Triangulation {
    public class Vertex {
        public int index;
        public Point P;
        public int type;
        public bool chain; // left or right

        public Vertex(int index, Point p) {
            this.index = index;
            P = p;
        }

        public Vertex(int index, Point p, bool chain) {
            this.index = index;
            this.P = p;
            this.chain = chain;
        }
    };

    public partial class playForm : Form {
        // private static SortedSet<Edge> Tree = new SortedSet<Edge>();
        private static StreamWriter log;
        private List<Point> v = new List<Point>();
        static public Graphics g;
        private List<string> message = new List<string>();
        public const int MAX_DEQUE = 25;

        public GraphicsState firststate;
        public GraphicsState connectedState;
        public GraphicsState tmp;

        public playForm() {
            InitializeComponent();
            g = splitContainer1.Panel1.CreateGraphics();
            firststate = g.Save();
            Show();
            MessageBox.Show("Vẽ hình bằng cách chấm các điểm trên màn hình");
        }

        private async Task delay(int miliseconds) {
            await Task.Delay(miliseconds);
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
            g.FillEllipse(b, e.X - 3, e.Y - 3, 6, 6);
            v.Add(new Point(e.X, e.Y));
            SizeLabel.Text = v.Count.ToString();
            display_message(e.X + " " + e.Y);

            StreamWriter WriteLine = new StreamWriter("C:\\Users\\Admin\\Desktop\\output_C#.txt");
            for (int i = 0; i < v.Count; i++) {
                WriteLine.WriteLine($"{v[i].X} {v[i].Y}");
            }

            WriteLine.WriteLine("999999999");
            WriteLine.Close();
        }


        private void start_MouseClick(object sender, MouseEventArgs e) {
            g.FillPolygon(Brushes.White, v.ToArray());
            if (v.Count < 3) {
                MessageBox.Show("Số đỉnh > 3");
            } else if (main.type == TYPE.EAR_CLIPPING) {
                draw_all_edges(v);
                process_earClip(v);
            } else if (main.type == TYPE.MONOTONE) {
                draw_all_edges(v);
                process_Monotone(v);
            }
        }

        private void clear_MouseClick(object sender, MouseEventArgs e) {
            splitContainer1.Panel1.Refresh();
            g.Restore(firststate);
            v.Clear();
            message.Clear();

            poly.Clear();
            helper.Clear();
            Tree.Clear();
            eventQ.Clear();
            ans.Clear();
            currentPolygon.Clear();
            

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

        public static bool is_in_triangle(Vector2 p, Vector2 p0, Vector2 p1, Vector2 p2) {
            float s = (p0.X - p2.X) * (p.Y - p2.Y) - (p0.Y - p2.Y) * (p.X - p2.X);
            float t = (p1.X - p0.X) * (p.Y - p0.Y) - (p1.Y - p0.Y) * (p.X - p0.X);

            if ((s < 0) != (t < 0) && s != 0 && t != 0)
                return false;

            float d = (p2.X - p1.X) * (p.Y - p1.Y) - (p2.Y - p1.Y) * (p.X - p1.X);
            return d == 0 || (d < 0) == (s + t <= 0);
        }

        private void draw_all_edges(List<Point> v) {
            connectedState = g.Save();
            for (int i = 0; i < v.Count() - 1; i++) {
                g.DrawLine(new Pen(Brushes.Red, 3), v[i], v[i + 1]);
            }

            g.DrawLine(new Pen(Brushes.Red, 3), v.Last(), v.First());
        }

        private void highlight(Point i) {
            tmp = g.Save();
            g.DrawEllipse(new Pen(Brushes.Red, 6), i.X - 3, i.Y - 3, 6, 6);
        }

        // private void unhighlight() {
        //     splitContainer1.Panel1.Refresh();
        //     g.Restore(tmp);
        // }

        private static void drawLine(Point A, Point B) {
            // splitContainer1.Panel1.Refresh();
            // g.Restore(tmp);
            g.DrawLine(new Pen(Brushes.Gold, 3), A, B);
            // tmp = g.Save();
        }

        private static void drawDashedLine(Point A, Point B) {
            Pen p = new Pen(Brushes.Fuchsia, 1);
            p.DashPattern = new float[] { 5, 5 };
            g.DrawLine(p, A, B);
        }

        private async void process_earClip(List<Point> l) {
            display_message("START");

            List<int> indexlist = new List<int>();

            List<Vector2> v2 = new List<Vector2>();

            for (int i = 0; i < v.Count; i++) {
                indexlist.Add(i);
                v2.Add(new Vector2(v[i].X, v[i].Y));
            }

            while (indexlist.Count > 3) {
                for (int i = 0; i < indexlist.Count; i++) {
                    await delay(mainForm.DELAY_MILIS);

                    display_message($"for loop, i= {i}, xét đỉnh {indexlist[i]}");
                    int a = indexlist[i];
                    int b = getItem(indexlist, i - 1);
                    int c = getItem(indexlist, i + 1);

                    // highlight(v[a]);

                    Vector2 v_ab = v2[b] - v2[a];
                    Vector2 v_ac = v2[c] - v2[a];

                    display_message($"point a= {a} , b= {b}, c= {c}");
                    if (Cross(v_ab, v_ac) < 0f) {
                        // goc 
                        display_message("cross < 0,continue ");
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
                        display_message($"Đỉnh {a} này là tai --> xóa bỏ");
                        indexlist.RemoveAt(i);
                        drawLine(v[b], v[c]);
                        break;
                    }
                }
            }

            SizeLabel.Text = indexlist.Count.ToString();
        }

        static List<Vertex> poly = new List<Vertex>();
        static List<Vertex> eventQ = new List<Vertex>();
        static List<KeyValuePair<int, int>> Tree = new List<KeyValuePair<int, int>>();
        static List<int> helper = new List<int>();
        static List<KeyValuePair<int, int>> ans = new List<KeyValuePair<int, int>>();
        static List<Vertex> currentPolygon = new List<Vertex>();

        private void process_Monotone(List<Point> l) {
            PREPARE_DATA(l);
            Start();

            for (int i = 0; i < ans.Count; i++) {
                display_message(ans[i].ToString());
            }
        }

        public float Cross(Vector2 a, Vector2 b) {
            return a.X * b.Y - a.Y * b.X;
        }

        public T getItem<T>(List<T> l, int index) {
            if (index < 0) {
                return l[index + l.Count];
            }

            if (index >= l.Count) {
                return l[index % l.Count];
            }

            return l[index];
        }

        protected override void OnClosing(CancelEventArgs e) {
            Application.Exit();
            base.OnClosing(e);
        }


        static void handleStartVertex(Vertex cur) {
            Tree.Add(new KeyValuePair<int, int>(cur.index, cur.P.X));
            helper[cur.index - 1] = cur.index;
            log.WriteLine("\t Là đỉnh Start, được thêm vào Tree, và Helper(" + cur.index + ")=" + cur.index);
        }

        static void handleEndVertex(Vertex cur) {
            log.WriteLine("\t Là đỉnh End ");
            int prev = cur.index - 2; // prev = 2
            if (cur.index - 2 == -1) {
                prev = poly.Count - 1;
            }

            log.WriteLine("\t Đỉnh trước = " + (prev + 1));

            if (poly[helper[prev] - 1].type == 3) {
                log.WriteLine("\tHelper[ " + (prev + 1) + "] là merge , Thêm đường chéo giữa" + cur.index + " và " +
                              helper[prev]);
                ans.Add(new KeyValuePair<int, int>(cur.index, helper[prev]));
            }


            for (int i = 0; i < Tree.Count; i++) {
                if (Tree[i].Key == helper[prev]) {
                    log.WriteLine("\t Đang xóa bỏ đỉnh" + Tree[i].Key + "khỏi cây , vì có helper[" + prev + "]");
                    log.WriteLine("\t Đã xóa đỉnh : < " + Tree[i].Key + " and " + Tree[i].Value + " >");
                    Tree.RemoveAt(i);
                    break;
                }
            }
        }

        static void handleMergeVertex(Vertex cur) {
            log.WriteLine("\t Là đỉnh Merge");
            int prev = cur.index - 2;
            if (cur.index - 2 == -1)
                prev = poly.Count - 1;
            log.WriteLine("\t Đỉnh prev ( đỉnh trước):" + (prev + 1));
            // try {
            if (poly[helper[prev] - 1].type == 3) {
                log.WriteLine("\t ----- Thêm đỉnh  " + poly[cur.index].P.X + "," +
                              poly[cur.index].P.Y + " and " + poly[helper[prev]].P.X + "," +
                              poly[helper[prev]].P.Y + "làm đường chéo");
                ans.Add(new KeyValuePair<int, int>(cur.index, helper[prev]));
            }
            // } catch (Exception e) {
            // log.WriteLine(e.ToString());
            // }

            for (int i = 0; i < Tree.Count; i++) {
                if (Tree[i].Key == helper[prev]) {
                    log.WriteLine("\t Đã xóa đỉnh " + (prev + 1) + " khỏi Tree");
                    Tree.RemoveAt(i);
                    break;
                }
            }

            if (Tree.Count != 0) {
                log.WriteLine("\t Tìm đỉnh trái :");
                Vertex left = null;
                if (Tree.Count == 1) {
                    left = poly[Tree[0].Key - 1];
                }

                for (int i = 0; i < Tree.Count; i++) {
                    log.WriteLine("\t Đang xét đỉnh :" + Tree[i].Value + " " + Tree[i].Key);

                    if (Tree[i].Value > cur.P.X) {
                        left = poly[Tree[i - 1].Key - 1];
                        log.WriteLine("\t Đỉnh trái là đỉnh : " + (left.index + 1));
                        break;
                    }

                    left = poly[Tree[i].Key - 1];
                }


                if (poly[helper[left.index - 1] - 1].type == 3) {
                    log.WriteLine("\t Đỉnh helper[" + (left.index - 1) + "] -1 là đỉnh " +
                                  poly[helper[left.index - 1] - 1].index + " là Merge");
                    log.WriteLine("\t Thêm đường chéo giữa " + cur.index + " and " +
                                  helper[left.index - 1]);
                    ans.Add(new KeyValuePair<int, int>(cur.index, helper[left.index - 1]));
                }


                helper[left.index - 1] = cur.index;
                log.WriteLine("\t helper(" + left.index + ")=" + cur.index);
            }
        }

        static void handleSplitVertex(Vertex current) {
            Vertex left = null;
            log.WriteLine("\t Là đỉnh SPLIT");
            if (Tree.Count != 0) {
                if (Tree.Count == 1) {
                    left = poly[Tree[0].Key - 1];
                }

                for (int i = 0; i < Tree.Count; i++) {
                    if (Tree[i].Value > current.P.X) {
                        // Tìm cái gần nhất bên trái, ở trong Tree
                        left = poly[Tree[i - 1].Key - 1];
                        break;
                    }

                    left = poly[Tree[i].Key - 1];
                }

                log.WriteLine("\t Hàng xóm bên trái :" + left.index);
                log.WriteLine(
                    "\t ---> Thêm đường chéo giữa đỉnh" + current.index + " và đỉnh " + helper[left.index - 1]);
                ans.Add(new KeyValuePair<int, int>(current.index, helper[left.index - 1]));
                helper[left.index - 1] = current.index;
                log.WriteLine("\t Helper(" + left.index + ")=" + current.index + "\n\t");
            }

            Tree.Add(new KeyValuePair<int, int>(current.index, current.P.X));
            log.WriteLine(
                "\t Thêm vào Tree : " + current.index + "\n\t Helper(" + current.index + ")=" + current.index);
            helper[current.index - 1] = current.index;
        }

        static bool right(Vertex cur, Vertex prev) {
            if (cur.P.Y < prev.P.Y)
                return true;
            if (cur.P.Y == prev.P.Y) {
                if (cur.P.X < prev.P.X)
                    return false;
            }

            return false;
        }

        static void handleRegularVertex(Vertex cur) {
            int prev = cur.index - 2;
            log.WriteLine("\n Là đỉnh REGULAR");
            if (cur.index - 2 == -1)
                prev = poly.Count - 1;
            if (right(cur, poly[prev])) {
                log.WriteLine("if(right) case:\n\t");
                log.WriteLine("\t current index = " + (cur.index + 1));
                log.WriteLine("\t Prev = " + (prev + 1));
                if (poly[helper[prev] - 1].type == 3) {
                    log.WriteLine("\t Thêm đường chéo: giữa 2 đỉnh " + cur.index + ",  " + helper[prev]);
                    ans.Add(new KeyValuePair<int, int>(cur.index, helper[prev]));
                }

                for (int i = 0; i < Tree.Count; i++) {
                    if (Tree[i].Key == helper[prev]) {
                        log.WriteLine("\t Xóa helper[" + (prev + 1) + "] = Đỉnh " + Tree[i].Key + " Khỏi cây");
                        Tree.RemoveAt(i);
                        break;
                    }
                }

                Tree.Add(new KeyValuePair<int, int>(cur.index, cur.P.X));
                helper[cur.index - 1] = cur.index;
                log.WriteLine("\t Thêm " + cur.index + ", " + cur.P.X + " vào cây \n\t Helper(" + cur.index + ")=" +
                              cur.index);
            } else {
                log.WriteLine("\t Else case : \t");
                Vertex left = null;
                if (Tree.Count != 0) {
                    if (Tree.Count == 1) {
                        left = poly[Tree[0].Key - 1];
                    }

                    log.WriteLine("\t Tìm Left : ");
                    for (int i = 0; i < Tree.Count; i++) {
                        if (Tree[i].Value > cur.P.X) {
                            log.WriteLine("\t Tree[" + i + "] có value = " + Tree[i].Value +
                                          " > current.Point.X (" + cur.P.X + ")");
                            log.WriteLine("\t Left = " + poly[Tree[i - 1].Key - 1].index);
                            left = poly[Tree[i - 1].Key - 1];
                            break;
                        }


                        left = poly[Tree[i].Key - 1];
                    }

                    log.WriteLine("\t Left ra ngoài bằng : " + left.index);

                    for (int i = 0; i < Tree.Count; i++) {
                        if (Tree[i].Key == helper[prev]) {
                            log.WriteLine("\t Xóa " + Tree[i].Key + "khỏi cây");
                            var k = Tree[i];
                            Tree.RemoveAt(i);
                            break;
                        }
                    }

                    if (poly[helper[left.index - 1] - 1].type == 3) {
                        log.WriteLine("\t Helper[" + (left.index - 1) + "] =" + helper[left.index - 1] + "poly[" +
                                      (helper[left.index - 1] - 1) + "] = " + (poly[helper[left.index - 1] - 1].index) +
                                      " là Merge");
                        log.WriteLine(
                            "\t Thêm đường chéo giữa " + cur.index + " và " + helper[left.index - 1]);
                        ans.Add(new KeyValuePair<int, int>(cur.index, helper[left.index - 1]));
                    }

                    helper[left.index - 1] = cur.index;
                    log.WriteLine("\t Helper(" + left.index + ")=" + cur.index);
                }
            }
        }

        static bool angle(Point a, Point b, Point c) {
            int area = (b.X - a.X) * (c.Y - a.Y) - (c.X - a.X) * (b.Y - a.Y);
            return area >= 0;
        }

        static void identifyVertexType() {
            for (int i = 0; i < poly.Count; i++) {
                int prev = i - 1, next = i + 1;
                if (i - 1 == -1)
                    prev = poly.Count - 1;
                if (i == poly.Count - 1)
                    next = 0;
                if ((poly[prev].P.Y < poly[i].P.Y) && (poly[i].P.Y > poly[next].P.Y)) {
                    if (angle(poly[i].P, poly[next].P, poly[prev].P)) {
                        poly[i].type = 1;
                    } else {
                        poly[i].type = 4;
                    }
                } else if ((poly[prev].P.Y > poly[i].P.Y) && (poly[i].P.Y < poly[next].P.Y)) {
                    if (angle(poly[i].P, poly[next].P, poly[prev].P)) {
                        poly[i].type = 5;
                    } else {
                        poly[i].type = 3;
                    }
                } else {
                    poly[i].type = 2;
                }
            }

            for (int i = 0; i < poly.Count; i++) {
                g.DrawString((i + 1).ToString(), new Font("Arial", 15), Brushes.Chartreuse, poly[i].P.X - 5,
                    poly[i].P.Y - 5);
            }
        }

        static void monotonePartition() {
            currentPolygon = new List<Vertex>(poly);
            int n = poly.Count;
            identifyVertexType();

            eventQ = new List<Vertex>(poly);

            eventQ.Sort(((v1, v2) => {
                if (v1.P.Y == v2.P.Y)
                    return v1.P.X - v2.P.X;
                return -v1.P.Y + v2.P.Y;
            }));

            log.WriteLine("Event queue:\n");
            log.WriteLine("X\t\tY\t\tindex\t\ttype");
            for (int i = 0; i < n; i++) {
                log.WriteLine(eventQ[i].P.X + "\t\t" + eventQ[i].P.Y + "\t\t" + eventQ[i].index + "\t\t" +
                              eventQ[i].type);
            }

            int frontQ = 0;
            while (frontQ != n) {
                Vertex current = eventQ[frontQ];

                log.WriteLine($"XÉT ĐỈNH {current.index} có {current.P.X} {current.P.Y}");

                if (current.type == 1) {
                    handleStartVertex(current);
                } else if (current.type == 2) {
                    handleRegularVertex(current);
                } else if (current.type == 3) {
                    handleMergeVertex(current);
                } else if (current.type == 4) {
                    handleSplitVertex(current);
                } else if (current.type == 5) {
                    handleEndVertex(current);
                }


                Tree.Sort(((pair1, pair2) => { return pair1.Value - pair2.Value; }));
                log.WriteLine("\nTree Sau khi xét lần " + (frontQ + 1) + ":\n\t");
                for (int i = 0; i < Tree.Count; i++) {
                    log.WriteLine("\t " + Tree[i].Key + ", " + Tree[i].Value);
                }

                log.WriteLine("\t : Helper");

                for (int i = 0; i < helper.Count; i++) {
                    log.WriteLine($"\t helper({i}) = {helper[i]}");
                }

                frontQ++;
            }

            log.WriteLine("\nEnd of algorithm. Partitioned into monotone pieces\n\n");
            log.WriteLine("No of diagonals inserted:" + ans.Count);
            log.WriteLine("\n The diagonals are inserted between:\n");
            for (int i = 0; i < ans.Count; i++) {
                log.WriteLine(ans[i].Key + "\t" + ans[i].Value + "\n");
                Triangulate(splitPolygon(ans[i]));
                g.DrawLine(new Pen(Brushes.Fuchsia,3.0f),poly[ans[i].Key - 1].P, poly[ans[i].Value - 1].P);
            }

            List<Vertex> res = new List<Vertex>();
            for (int i = 0; i < currentPolygon.Count; i++) {
                if (currentPolygon[i] == null) {
                    continue;
                }
                res.Add(currentPolygon[i]);
            }
            Triangulate(res);
        }


        static void PREPARE_DATA(List<Point> res) {
            for (int i = 0; i < res.Count; i++) {
                int x = res[i].X;
                int y = res[i].Y;
                Point pt = new Point(x, y);
                Vertex v = new Vertex(i + 1, pt);
                poly.Add(v);
                helper.Add(-1);
            }
        }


        // // static void get_monotone(List<Vertex> poly, List<KeyValuePair<int, int>> ans) {
        //     // List<Vertex> m_poly = new List<Vertex>(poly);
        //     for (int i = 0; (i < m_poly.Count && m_poly.Count != 0); i++) {
        //         for (int j = 0; j < ans.Count; j++) {
        //             if (ans[j].Key - 1 == i || ans[j].Value - 1 == i) {
        //                 // thoa man 
        //                 m_poly.RemoveAt(i);
        //                 _list_
        //             } else {
        //                 
        //             }
        //         }
        //     }
        // }

        static List<Vertex> splitPolygon(KeyValuePair<int, int> diagonal) {
            int from = Math.Min(diagonal.Key,diagonal.Value)-1;
            int to=  Math.Max(diagonal.Key, diagonal.Value)-1;
            
            List<Vertex> res = new List<Vertex>();
            bool flag_Exception = false;
            currentPolygon[from].type = 2;
            currentPolygon[to].type = 2;
            for (int i = from + 1; i != to; i = (i + 1) % poly.Count) {
                if (currentPolygon[i] == null) {
                    continue;
                }
                if (currentPolygon[i].type == 3 || currentPolygon[i].type == 4) {
                    flag_Exception = true;
                    break;
                }
            }
            if (flag_Exception) {
                res.Add(currentPolygon[to]);
                for (int i = to + 1; i != from; i = (i + 1) % poly.Count) {
                    if (currentPolygon[i] == null) {
                        continue;
                    }
                    res.Add(currentPolygon[i]);
                    currentPolygon[i] = null;
                }
                res.Add(currentPolygon[from]);
            } else {
                res.Add(currentPolygon[from]);
                for (int i = from + 1; i != to; i = (i + 1) % poly.Count) {
                    if (currentPolygon[i] == null) {
                        continue;
                    }
                    res.Add(currentPolygon[i]);
                    currentPolygon[i] = null;
                }
                res.Add(currentPolygon[to]);
            }
            log.WriteLine("the splited monotone polygon");
            
            for (int i = 0; i < res.Count; i++) {
                log.Write(res[i].index.ToString());
            }
            log.WriteLine();
            return res;
        }

        static float areaofTriangle(Point a, Point b, Point c) {
            return ((b.X - a.X) * (c.Y - a.Y) - (c.X - a.X) * (b.Y - a.Y));
        }

        static bool Left(Point a, Point b, Point c) {
            return areaofTriangle(a, b, c) > 0;
        }

        static bool LeftOn(Point a, Point b, Point c) {
            return areaofTriangle(a, b, c) >= 0;
        }

        static bool InCone(List<Vertex> poly, Vertex a, Vertex b) {
            if (LeftOn(a.P, poly[a.index < poly.Count ? a.index : 0].P,
                    poly[(a.index - 2 >= 0) ? a.index - 2 : poly.Count - 1].P)) {
                return (Left(a.P, b.P, poly[(a.index - 2 >= 0) ? a.index - 2 : poly.Count - 1].P) &&
                        Left(b.P, a.P, poly[a.index < poly.Count ? a.index : 0].P));
            }

            return (!(Left(a.P, b.P, poly[a.index < poly.Count ? a.index : 0].P) &&
                      Left(b.P, a.P, poly[(a.index - 2 >= 0) ? a.index - 2 : poly.Count - 1].P)));
        }

        static bool Collinear(Point a, Point b, Point c) {
            return areaofTriangle(a, b, c) == 0;
        }

        static bool Xor(bool x, bool y) {
            return !x ^ !y;
        }

        static bool Intersection(Point a, Point b, Point c, Point d) {
            if (Collinear(a, b, c) || Collinear(a, b, d) || Collinear(c, d, a) || Collinear(c, d, b))
                return false;
            return Xor(Left(a, b, c), Left(a, b, d)) && Xor(Left(c, d, a), Left(c, d, b));
        }

        static bool Between(Point a, Point b, Point c) {
            if (!Collinear(a, b, c))
                return false;
            if (a.X != b.X) {
                return (((a.X <= c.X) && (c.X <= b.X)) || ((a.X >= c.X) && (c.X >= b.X)));
            } else
                return (((a.Y <= c.Y) && (c.Y <= b.Y)) || ((a.Y >= c.Y) && (c.Y >= b.Y)));
        }

        static bool Intersect(Point a, Point b, Point c, Point d) {
            if (Intersection(a, b, c, d)) {
                return true;
            } else if (Between(a, b, c) || Between(a, b, d) || Between(c, d, a) || Between(c, d, b))
                return true;
            else {
                return false;
            }
        }

        static bool isDiagonal(List<Vertex> poly, Vertex a, Vertex b) {
            for (int i = 0; i < poly.Count - 1; i++) {
                if ((poly[i].index != a.index) && (poly[i + 1].index != a.index) && (poly[i].index != b.index) &&
                    (poly[i + 1].index != b.index) && Intersect(a.P, b.P, poly[i].P, poly[i + 1].P)) {
                    return false;
                }
            }

            if ((poly[poly.Count - 1].index != a.index) && (poly[0].index != a.index) &&
                (poly[poly.Count - 1].index != b.index) && (poly[0].index != b.index) &&
                Intersect(a.P, b.P, poly[poly.Count - 1].P, poly[0].P)) {
                return false;
            }

            return true;
        }

        static bool Diagonal(List<Vertex> poly, Vertex a, Vertex b) {
            if (a.index == b.index) {
                return false;
            }

            if (a.index - b.index == 1 || a.index - b.index == -1) {
                return false;
            }

            return (InCone(poly, a, b) && InCone(poly, b, a) && isDiagonal(poly, a, b));
        }


        static void verifyChain(List<Vertex> u) {
            for (int i = 0; i < u.Count; i++) {
                u[i].index = i + 1;
            }

            int Y_MAX = 0;
            int Y_MIN = 0;
            for (int i = 0; i < u.Count; i++) {
                if (u[i].P.Y > u[Y_MAX].P.Y) {
                    Y_MAX = i;
                }

                if (u[i].P.Y < u[Y_MIN].P.Y) {
                    Y_MIN = i;
                }
            }

            log.WriteLine($"i max = {Y_MAX}");
            log.WriteLine($"i min = {Y_MIN}");

            for (int i = Y_MIN; i != Y_MAX; i = (i + 1) % u.Count) {
                u[i].chain = false;
                log.WriteLine($"this chain {i + 1} is false;");
            }

            for (int i = Y_MAX; i != Y_MIN; i = (i + 1) % u.Count) {
                u[i].chain = true;
                log.WriteLine($"this chain {i + 1} is true");
            }
        }

        static void Triangulate(List<Vertex> poly) {
            List<Vertex> u = new List<Vertex>(poly);
            for (int i = 0; i < u.Count; i++) {
                u[i].index = i + 1;
            }
            verifyChain(u);
            u.Sort(((v1, v2) => {
                if (v1.P.Y == v2.P.Y)
                    return v1.P.X - v2.P.X;
                return -v1.P.Y + v2.P.Y;
            }));
            Stack<Vertex> stack = new Stack<Vertex>();
            stack.Push(u[0]);
            stack.Push(u[1]);
            for (int i = 2; i < u.Count - 1; i++) {
                if (u[i].chain != stack.Peek().chain) {
                    log.WriteLine("diff chain");
                    while (stack.Count != 1) {
                        Vertex x = stack.Pop();
                        log.WriteLine($"add diagonal from {x.index} to {u[i].index} ");
                        drawDashedLine(x.P,u[i].P);
                    }

                    while (stack.Count != 0) {
                        stack.Pop();
                    }

                    stack.Push(u[i - 1]);
                    stack.Push(u[i]);
                } else {
                    log.WriteLine("same chain");
                    Vertex x = null;
                    while (stack.Count != 0) {
                        x = stack.Pop();
                        if (Diagonal(poly, x, u[i])) {
                            log.WriteLine($"add diagonal from {x.index} to {u[i].index} ");
                            drawDashedLine(x.P,u[i].P);
                            break;
                        }
                    }

                    stack.Push(x);
                    stack.Push(u[i]);
                }

                Vertex[] k = stack.ToArray();
                log.WriteLine("Stack now  : ");
                for (int j = 0; j < k.Length; j++) {
                    log.Write($"{k[j].index}");
                }

                log.WriteLine("");
            }

            Vertex[] remain = stack.ToArray();
            for (int i = 1; i < remain.Length - 1; i++) {
                log.WriteLine($"add diagonal from {remain[i].index} to last ");
            }
        }

        static void Start() {
            log = new StreamWriter("log.txt");
            monotonePartition();
            log.Close();
        }
    }
}