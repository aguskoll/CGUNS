using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGUNS.Doubly_Conected_Edge_List
{
   public class face
   {
       List<int> vertices;
        int halfEdgeIncidente;
        /**
         * constructor
         */
        public face(int value) 
        {
            halfEdgeIncidente = value;
            vertices = new List<int>();
        }
        public face() { halfEdgeIncidente = -1; }
        /**
         * geters y setters de el half edge incidente
         * **/
        public int getHalfEdgeIncidente() { return halfEdgeIncidente; }
        public void setHalfEdgeIncidente(int value) { halfEdgeIncidente = value; }
        public void setVertices(List<int> entrada) { vertices = entrada; }
        public List<int> getVertices() { return vertices; }
    }
}
