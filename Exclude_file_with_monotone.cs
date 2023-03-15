// using System;
// using System.Collections.Generic;
// using System.Drawing;
//
// class Vertex {
//     public int index;
//     public Point P;
//     public int type;
//
//     public Vertex(int index, Point p) {
//         this.index = index;
//         P = p;
//     }
//     public Vertex() {
//         
//     }
// };
//
// class MyClass {
//     static List<Vertex> poly = new List<Vertex>();
//     static List<Vertex> eventQ = new List<Vertex>();
//     static List<KeyValuePair<int, int>> Tree = new List<KeyValuePair<int, int>>();
//     static List<int> helper = new List<int>();
//     static List<KeyValuePair<int, int>> ans = new List<KeyValuePair<int, int>>();
//     
//     static void handleStartVertex(Vertex cur) {
//         Console.Write("\nSTART:" + cur.index + "\n\t");
//         Tree.Add(new KeyValuePair<int, int>(cur.index, cur.P.X));
//         helper[cur.index - 1] = cur.index;
//         Console.Write("Edge" + cur.index + " is inserted and helper(" + cur.index + ")=" + cur.index + "\n");
//     }
//
//     static void handleEndVertex(Vertex cur) {
//         Console.Write("\nEND:" + cur.index + "\n\t");
//         int prev = cur.index - 2;
//         if (cur.index - 2 == -1) {
//             prev = poly.Count - 1;
//         }
//
//         try {
//             if (poly[helper[prev] - 1].type == 3) {
//                 Console.Write("line 60 Insert Diagonal between " + cur.index + " and " + helper[prev] + "\n");
//                 ans.Add(new KeyValuePair<int, int>(cur.index, helper[prev]));
//             }
//         } catch (Exception) {
//             // ignored
//         }
//
//         for (int i = 0; i < Tree.Count; i++) {
//             if (Tree[i].Key == helper[prev]) {
//                 Console.Write("Delete" + Tree[i].Key + "from the tree\n\t");
//                 var k = Tree[i];
//                 Tree[i] = new KeyValuePair<int, int>(-1, k.Value);
//                 Console.WriteLine("test : Tree "+i + " ="+Tree[i].Key+" "+Tree[i].Value);
//             }
//
//             Tree.RemoveAt(i);
//         }
//     }
//
//     static void handleMergeVertex(Vertex cur) {
//         Console.Write("\nMERGE:" + cur.index + "\n\t");
//         int prev = cur.index - 2;
//         if (cur.index - 2 == -1)
//             prev = poly.Count - 1;
//         Console.Write("Prev node:" + (prev + 1) + "\n\t");
//         try {
//             if (poly[helper[prev] - 1].type == 3) {
//                 Console.Write("line 87 is a merge vertex.\n\tInsert Diagonal between " + poly[cur.index].P.X + "," +
//                               poly[cur.index].P.Y + " and " + poly[helper[prev]].P.X + "," +
//                               poly[helper[prev]].P.Y + "\n\t");
//                 ans.Add(new KeyValuePair<int, int>(cur.index, helper[prev]));
//             }
//         } catch (Exception ) {
//             // ignore
//         }
//
//         for (int i = 0; i < Tree.Count; i++) {
//             if (Tree[i].Key == helper[prev]) {
//                 var k = Tree[i];
//                 Tree[i] = new KeyValuePair<int, int>(-1, k.Value);
//                 // Tree[i].Key = -1;
//                 Tree.RemoveAt(i);
//                 Console.WriteLine(
//                     "Edge" + (prev + 1) + "deleted from the tree");
//                 // + "\n\t";
//                 break;
//             }
//         }
//
//         if (Tree.Count != 0) {
//             Console.WriteLine(
//                 "Left neighbor:");
//             Vertex left = new Vertex();
//             if (Tree.Count == 1) {
//                 left = poly[Tree[0].Key - 1];
//             }
//
//             for (int i = 0; i < Tree.Count; i++) {
//                 Console.Write(
//                     "Iterating node:" + Tree[i].Value + " " + Tree[i].Key +"\n");
//                 
//                 if (Tree[i].Value > cur.P.X) {
//                     Console.WriteLine("this is greater");
//                     left = poly[Tree[i - 1].Key - 1];
//                     Console.Write(Tree[i - 1].Key);
//                     break;
//                 }
//                 left = poly[Tree[i].Key - 1];
//                 Console.WriteLine("index " +left.index);
//             }
//
//             // cout+":::";
//             try {
//                 if (poly[helper[left.index - 1] - 1].type == 3) {
//                     Console.Write("is a Merge vertex.\n\t Insert Diagonal between " + cur.index + " and " +
//                                   helper[left.index - 1] + "\n\t");
//                     ans.Add(new KeyValuePair<int, int>(cur.index, helper[left.index - 1]));
//                 }
//             } catch (Exception) {
//                 // ignore 
//             }
//
//             helper[left.index - 1] = cur.index;
//             Console.Write(
//                 "Set helper(" + left.index + ")=" + cur.index + "\n");
//         }
//     }
//
//     static void handleSplitVertex(Vertex current) {
//         Vertex left = new Vertex();
//         Console.Write(
//             "\nSPLIT:" + current.index + "\n\t");
//         if (Tree.Count != 0) {
//             if (Tree.Count == 1) {
//                 left = poly[Tree[0].Key - 1];
//             }
//
//             for (int i = 0; i < Tree.Count; i++) {
//                 if (Tree[i].Value > current.P.X) {
//                     try {
//                         left = poly[Tree[i - 1].Key - 1];
//                         break;
//                     } catch (Exception e) {
//                         // ignore
//                     }
//                 }
//
//                 left = poly[Tree[i].Key - 1];
//             }
//
//             Console.Write("Left neighbor:" + left.index + "\n\t");
//             Console.Write("line 166 Insert Diagonal between " + current.index + " and " + helper[left.index - 1] +
//                           "\n\t");
//             ans.Add(new KeyValuePair<int, int>(current.index, helper[left.index - 1]));
//             helper[left.index - 1] = current.index;
//             Console.Write("Set helper(" + left.index + ")=" + current.index + "\n\t");
//         }
//
//         Tree.Add(new KeyValuePair<int, int>(current.index, current.P.X));
//         Console.Write(
//             "Insert" + current.index + " into the tree ans set helper(" + current.index + ")=" + current.index + "\n");
//         helper[current.index - 1] = current.index;
//     }
//
//     static bool right(Vertex cur, Vertex prev) {
//         if (cur.P.Y < prev.P.Y)
//             return true;
//         if (cur.P.Y == prev.P.Y) {
//             if (cur.P.X < prev.P.X)
//                 return false;
//         }
//
//         return false;
//     }
//
//     static void handleRegularVertex(Vertex cur) {
//         int prev = cur.index - 2;
//         Console.Write("\nREGULAR:" + cur.index + "\n\t");
//
//
//         if (cur.index - 2 < 0 || right(cur, poly[cur.index - 2])) {
//             Console.Write(
//                 "if(right) case:\n\t");
//             if (cur.index - 2 == -1)
//                 prev = poly.Count - 1;
//             if (poly[helper[prev] - 1].type == 3) {
//                 Console.Write("line 199 Insert Diagonal between " + cur.index + " and " + helper[prev] + "\n\t");
//                 ans.Add(new KeyValuePair<int, int>(cur.index, helper[prev]));
//             }
//
//             for (int i = 0; i < Tree.Count; i++) {
//                 if (Tree[i].Key == helper[prev]) {
//                     Console.Write("Delete" + Tree[i].Key + "from the tree\n\t");
//                     var k = Tree[i];
//                     Tree[i] = new KeyValuePair<int, int>(-1, k.Value);
//                     // Tree[i].Key = -1;
//                     Tree.RemoveAt(i);
//                     break;
//                 }
//             }
//
//             Tree.Add(new KeyValuePair<int, int>(cur.index, cur.P.X));
//             helper[cur.index - 1] = cur.index;
//             Console.Write("Insert" + cur.index + " into the tree ans set helper(" + cur.index + ")=" + cur.index +
//                           "\n");
//         } else {
//             Console.Write("Else case:\n\t");
//             Vertex left = new Vertex();
//             if (Tree.Count != 0) {
//                 if (Tree.Count == 1) {
//                     left = poly[Tree[0].Key - 1];
//                 }
//
//                 for (int i = 0; i < Tree.Count; i++) {
//                     try {
//                         if (Tree[i].Value > cur.P.X) {
//                             left = poly[Tree[i - 1].Key - 1];
//                             break;
//                         }
//                     } catch (Exception) {
//                     }
//
//                     left = poly[Tree[i].Key - 1];
//                 }
//
//                 Console.Write("Left neighbor:" + left.index + "\n\t");
//                 try {
//                     if (poly[helper[left.index - 1] - 1].type == 3) {
//                         Console.Write(
//                             "line 245 Insert Diagonal between " + cur.index + " and " + helper[left.index - 1] +
//                             "\n\t");
//                         ans.Add(new KeyValuePair<int, int>(cur.index, helper[left.index - 1]));
//                     }
//                 } catch (Exception e) {
//                     // ignore
//                 }
//
//                 Console.WriteLine("line 253" + (left.index-1));
//                 helper[left.index - 1] = cur.index;
//                 Console.Write("Set helper(" + left.index + ")=" + cur.index + "\n\t");
//             }
//         }
//     }
//
//     static bool angle(Point a, Point b, Point c) {
//         int area = (b.X - a.X) * (c.Y - a.Y) - (c.X - a.X) * (b.Y - a.Y);
//         return area >= 0;
//     }
//
//     static void identifyVertexType() {
//         for (int i = 0; i < poly.Count; i++) {
//             int prev = i - 1, next = i + 1;
//             if (i - 1 == -1)
//                 prev = poly.Count - 1;
//             if (i == poly.Count - 1)
//                 next = 0;
//             if ((poly[prev].P.Y < poly[i].P.Y) && (poly[i].P.Y > poly[next].P.Y)) {
//                 if (angle(poly[i].P, poly[next].P, poly[prev].P)) {
//                     poly[i].type = 1;
//                 } else {
//                     poly[i].type = 4;
//                 }
//             } else if ((poly[prev].P.Y > poly[i].P.Y) && (poly[i].P.Y < poly[next].P.Y)) {
//                 if (angle(poly[i].P, poly[next].P, poly[prev].P)) {
//                     poly[i].type = 5;
//                 } else {
//                     poly[i].type = 3;
//                 }
//             } else {
//                 poly[i].type = 2;
//             }
//         }
//     }
//
//     static void monotonePartition() {
//         int n = poly.Count;
//         identifyVertexType();
//
//         eventQ = new List<Vertex>(poly);
//         
//         // sort(eventQ.begin(), eventQ.end(), comp);
//         eventQ.Sort(((v1, v2) => {
//             if (v1.P.Y == v2.P.Y)
//                 return v1.P.X - v2.P.X;
//             return -v1.P.Y + v2.P.Y;
//         }));
//    
//         Console.Write("Event queue:\n");
//         Console.Write("Xcor Ycor index type\n");
//         for (int i = 0; i < n; i++) {
//             Console.Write(eventQ[i].P.X + " " + eventQ[i].P.Y + " " + eventQ[i].index + "   " + eventQ[i].type +
//                           "\n");
//         }
//
//         int frontQ = 0;
//         while (frontQ != n) {
//             Vertex current = eventQ[frontQ];
//             if (current.type == 1) {
//                 Console.Write("start");
//                 handleStartVertex(current);
//             } else if (current.type == 2) {
//                 Console.Write("reg");
//                 handleRegularVertex(current);
//             } else if (current.type == 3) {
//                 Console.Write("merge");
//                 handleMergeVertex(current);
//             } else if (current.type == 4) {
//                 Console.Write("split");
//                 handleSplitVertex(current);
//             } else if (current.type == 5) {
//                 Console.Write("end");
//                 handleEndVertex(current);
//             }
//
//             // sort(Tree.begin(), Tree.end(), compx);
//             Tree.Sort(((pair1, pair2) => { return pair1.Value - pair2.Value; }));
//             Console.Write("\nTree structure at iteration " + (frontQ + 1) + ":\n\t");
//             for (int i = 0; i < Tree.Count; i++) {
//                 Console.Write(
//                     Tree[i].Key + " ");
//             }
//
//             frontQ++;
//         }
//
//         Console.Write("\nEnd of algorithm. Partitioned into monotone pieces\n\n");
//         Console.Write("No of diagonals inserted:" + ans.Count);
//         Console.Write("\n The diagonals are inserted between:\n");
//         for (int i = 0; i < ans.Count; i++) {
//             Console.Write(ans[i].Key + "\t" + ans[i].Value + "\n");
//         }
//     }
//
//
//     static void PREPARE_DATA(List<KeyValuePair<int,int>> res) {
//         int countv = 0;
//         for (int i = 0; i < 7; i++) {
//             int x = res[i].Key;
//             int y = res[i].Value;
//             Point pt = new Point(x, y);
//             Vertex v = new Vertex(countv + 1, pt);
//             poly.Add(v);
//             countv++;
//             helper.Add(-1);
//         }
//     }
//
//     static void Start() {
//         monotonePartition();
//     }
// }