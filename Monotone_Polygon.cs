using System.Collections.Generic;

namespace Polygon_Triangulation {
    public class Monotone_Polygon {
        public List<Vertex> vertexs = new List<Vertex>();

        public List<Vertex> left_chain = new List<Vertex>();
        public List<Vertex> right_chain = new List<Vertex>();

        public Monotone_Polygon(List<Vertex> vertexs) {
            this.vertexs = vertexs;
            int Y_MAX = 0;
            int Y_MIN = 0;
            for (int i = 1; i < vertexs.Count; i++) {
                if (vertexs[i].P.Y > vertexs[Y_MAX].P.Y) {
                    Y_MAX = i;
                }

                if (vertexs[i].P.Y < vertexs[Y_MIN].P.Y) {
                    Y_MIN = i;
                }
            }
            
            // for(int i =Y_MAX;)
        }
    }
}