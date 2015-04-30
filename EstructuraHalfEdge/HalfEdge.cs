using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace CGUNS.Doubly_Conected_Edge_List
{
  public  class HalfEdge
    {
      #region ATRIBUTOS
       
        private int verticeOrigen;
        private int edgeOpuesto;
        private int caraIncidente;
        private int edgeSiguiente;
        private int edgePrevio;

      #endregion ATRIBUTOS
        public HalfEdge() {
            verticeOrigen = -1;
            edgeOpuesto = -1;
            caraIncidente = -1;
            edgeSiguiente = -1;
            edgePrevio = -1;
        }
       
        public int getVerticeOrigen() { 
             return verticeOrigen;

         }
        public void  setVerticeOrigen(int value) 
        {
            verticeOrigen = value;
        }

        public int getEdgeOpuesto()
        {
            return edgeOpuesto;

        }
        public void setEdgeOpuesto(int value)
        {
            edgeOpuesto = value;
        }

        public int getCaraIncidente()
        {
            return caraIncidente ;

        }
        public void setCaraIncidente(int value)
        {
            caraIncidente = value;
        }

        public int getEdgeSiguiente()
        {
            return edgeSiguiente;

        }
        public void  setEdgeSiguiente(int value)
        {
           edgeSiguiente = value;
        }

        public int getEdgePrevio()
        {
            return edgePrevio;

        }
        public void setEdgePrevio(int value)
        {
            edgePrevio = value;
        }
    }   
}
