using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace CGUNS.Doubly_Conected_Edge_List
{
    public class DoublyConectedEdgeList
    {
        #region atributos
        private List<Vertex> vertices;          //Lista de vertices.
        private List<face> caras;              //Lista de caras.
        private List<HalfEdge> lados;      //Lista de aristas.
        private Vector3 rotar;
        private Vector3 traslacion;
        private Vector3 color;
        #endregion

        public DoublyConectedEdgeList()
        {
            vertices = new List<Vertex>();
            caras = new List<face>();
            lados = new List<HalfEdge>();


        }
        /**geters**/
        #region getters
        public List<Vertex> getVertices() { return vertices; }
        public List<face> getCaras() { return caras; }
        public List<HalfEdge> getLados() { return lados; }

        #endregion

        #region consultasGenerales

        public int getCantCaras() { return caras.Count; }
        public int getCantVertices() { return vertices.Count; }
        public int getCantLados() { return lados.Count; }

        #endregion

        #region consultasVertices

        public Vector3 coordenadas(int indice)
        {
            return vertices[indice].getCoordenadas();
        }

        public int halfEdgeIncidente(int indice)
        {
            return vertices[indice].getEdgeIncidente();

        }
        public void setEdgeIncidente(int v, int e)
        {
            vertices[v].setEdgeIncidente(e);
        }
        public Vertex getVertex(int indice) { return vertices[indice]; }
        #endregion
        #region consultaCaras

        public int contornoCara(int f)
        {
            return caras[f].getHalfEdgeIncidente();
        }
        public void setContornoCara(int f, int e)
        {


            caras[f].setHalfEdgeIncidente(e);
        }
        #endregion

        #region consultaHalfEdge
        public int Verticeorigen(int e)
        {
            return lados[e].getVerticeOrigen();
        }

        public int edgeOpuesto(int e) { return lados[e].getEdgeOpuesto(); }

        public void setCaraIncidente(int e, int f)
        {
            lados[e].setCaraIncidente(f);

        }

        public void setPrevSig(int e1, int e2)
        {
            lados[e1].setEdgeSiguiente(e2);

            lados[e2].setEdgePrevio(e1);


        }
        public void setEdgePrev(int e, int n)
        {
            lados[e].setEdgePrevio(n);

        }
        public void setEdgeSig(int e, int n)
        {
            lados[e].setEdgeSiguiente(n);

        }

        //continuar

        #endregion

        #region AgregarElementos
        public int addVertice(Vertex v)
        {
            vertices.Add(v);

            return vertices.Count - 1;
        }
        /**
         * se le dan las coordenadas y requiero el half edge incidente
         * **/

        public int CrearVertice(Vector3 coord)
        {
            Vertex v = new Vertex(coord[0], coord[1], coord[2]);
            vertices.Add(v);
            //faltaria lo del el edge incidente 
            return vertices.Count - 1;
        }



        public int addCara(face f)
        {
            caras.Add(f);
            return caras.Count - 1;
        }
      /*  public int CrearCara()
        {
            face f = new face();
            caras.Add(f);
            return caras.Count - 1;
        }
*/
        public int crearCara(List<int> Indices)
        {
            int faceIndex;
          
            face newFace = new face();
            newFace.setVertices(Indices);
            caras.Add(newFace);
            faceIndex = caras.Count - 1;

         
            return faceIndex;
        }





        public int addLado(HalfEdge he)
        {
            lados.Add(he);
            return lados.Count - 1;
        }

        public int crearLado(int v1, int v2)
        {
            int toReturn = -1;

            int i = 0; Boolean encontre = false;
            while (i < lados.Count && !encontre) //busco si el que se va a crear ya existe
            {
                if (lados[i].getVerticeOrigen() == v1)
                {
                    toReturn = i;
                    encontre = true;
                    // Console.WriteLine(" Ya existe con "+v1+" "+v2);
                }
                i++;
            }
            if (toReturn > 0)
                return toReturn;
            //creao el par correspondiente
            HalfEdge e = new HalfEdge();
            e.setVerticeOrigen(v1);
            e.setEdgeOpuesto(lados.Count + 1);
            lados.Add(e);
            toReturn = lados.Count - 1;


            HalfEdge twin = new HalfEdge();
            twin.setVerticeOrigen(v2);
            // twin.set
            twin.setEdgeOpuesto(toReturn);
            lados.Add(twin);
            //si los vertices no tienen lado saliente seteado, lo seteo
            if (vertices[v1].getEdgeIncidente() == -1)
                vertices[v1].setEdgeIncidente(toReturn);

            if (vertices[v2].getEdgeIncidente() == -1)
                vertices[v2].setEdgeIncidente(toReturn + 1);

            return toReturn;
        }

        #endregion

  



        public void mostrarfigura()
        {
            Console.WriteLine("Vertices: ");
            Console.WriteLine("Vertice      Coordenadas                     LadoIncidente");
            for (int i = 0; i < vertices.Count; i++)
            {
                Console.Write(i);
                Console.Write("             ");
                Console.Write((vertices[i].getCoordenadas()[0]));
                Console.Write(",");
                Console.Write((vertices[i].getCoordenadas()[1]));
                Console.Write(" ");
                Console.Write((vertices[i].getCoordenadas()[2]));
                Console.Write("             ");
                Console.Write(vertices[i].getEdgeIncidente());
                Console.WriteLine();
            }
            Console.WriteLine(); Console.WriteLine(); Console.WriteLine();
            Console.WriteLine("CARAS      LADO INCIDENTE       ");
            for (int i = 0; i < caras.Count; i++)
            {
                Console.Write(i);
                Console.Write("             ");
                Console.Write(caras[i].getHalfEdgeIncidente());
                Console.WriteLine();
            }
            Console.WriteLine(); Console.WriteLine(); Console.WriteLine();
            Console.WriteLine(" LADO      ORIGEN     OPUESTO   CARAINC           SIG        PREV     ");
            for (int i = 0; i < lados.Count(); i++)
            {
                Console.Write(i); Console.Write("          ");
                Console.Write(lados[i].getVerticeOrigen());
                Console.Write("              ");
                Console.Write(lados[i].getEdgeOpuesto());
                Console.Write("             ");
                Console.Write(lados[i].getCaraIncidente());
                Console.Write("           ");
                Console.Write(lados[i].getEdgeSiguiente());
                Console.Write("          ");
                Console.Write(lados[i].getEdgePrevio());
                Console.WriteLine();
            }


        }
        public void setRotacion(Vector3 r) { rotar = r; }
        public Vector3 getRotacion() { return rotar; }
        public void setTraslacion(Vector3 r) { traslacion= r; }
        public Vector3 getTraslacion() { return traslacion; }
        public void setColor(Vector3 r) { color= r; }
        public Vector3 getColor() { return color; }
    }
}