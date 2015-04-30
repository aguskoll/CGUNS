using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
namespace CGUNS.Doubly_Conected_Edge_List
{
   public  class Vertex
    {
        private Vector3  coordenadas ;
        private int edgeIncidente;

        public Vertex(float x, float y, float z) 
        {
            coordenadas[0] = x;
            coordenadas[1] = y;
            coordenadas[2] = z;
            edgeIncidente = -1;

        }
        public Vector3 getCoordenadas ()
        {
           return coordenadas;
            
        }
        public void setCoordenadas(Vector3 v) 
        {
            coordenadas = v;

        }
        public void setEdgeIncidente(int e) 
        {
            edgeIncidente = e;
        }

        public int getEdgeIncidente() { return edgeIncidente; }
        
    }

}
