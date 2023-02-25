using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;

namespace Test {
    internal class Program {
        public static string path = "C:/Users/Admin/Desktop/COZE/C#/Polygon_Triangulation/log.txt";
        public static StreamWriter w = new StreamWriter(path);
        public static int DELAY=300;
        public static void Main(string[] args) {

            A a = new A();
            a.process();
            w.Close();
        }

        class A {
            
            public const int MAX_DEQUE = 25;


            private void display_message(string err) {
                w.WriteLine(err);
                Console.WriteLine($" log :  {err}");
            }

            public bool is_in_triangle(Vector2 p, Vector2 a, Vector2 b, Vector2 c) {
                Vector2 ab = b - a;
                Vector2 bc = c - b;
                Vector2 ca = c - a;

                Vector2 ap = p - a;
                Vector2 bp = p - a;
                Vector2 cp = p - c;


                display_message("is in triangle");
                if (Cross(ab, ap) > 0f || Cross(bc, bp) > 0f || Cross(ca, cp) > 0f) {
                    return false;
                }

                return true;
            }

            public void process() {
                List<int> indexlist = new List<int>();

                List<Vector2> v2 = new List<Vector2>();
                v2.Add(new Vector2(-4, 6));
                v2.Add(new Vector2(0,2));
                v2.Add(new Vector2(2, 5));
                v2.Add(new Vector2(7, 0));
                v2.Add(new Vector2(5,-6));
                v2.Add(new Vector2(3,3));
                v2.Add(new Vector2(0,-5));
                v2.Add(new Vector2(-6,0));
                v2.Add(new Vector2(-2,1));
                
                display_message($"starting process , indexlist.size = {indexlist.Count}");
                for (int i = 0; i < v2.Count; i++) {
                    indexlist.Add(i);
                }
                
                while (indexlist.Count > 3) {
                    display_message("while loop");
                    for (int i = 0; i < indexlist.Count; i++) {
                        display_message($"for loop, i ={i}");
                        int a = indexlist[i];
                        int b = getItem(indexlist, i - 1);
                        int c = getItem(indexlist, i + 1);

                        Vector2 v_ab = v2[b] - v2[a];
                        Vector2 v_ac = v2[c] - v2[a];
                        
                        if (Cross(v_ab, v_ac) < 0f) {
                            // goc 
                            display_message("cross(ab,ac) < 0 , continue.");
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
                            display_message("is ear");
                            indexlist.RemoveAt(i);
                        }
                    }
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
                    return l[index - l.Count];
                }

                return l[index];
            }
        }
    }
}