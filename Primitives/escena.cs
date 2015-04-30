using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  CGUNS.Shaders;
using CGUNS.Doubly_Conected_Edge_List;
using CGUNS;
using OpenTK;
namespace CGUNS.Primitives
{
	public class escena
	{
        public  List<ObjetoGrafico> objetos;
        private ShaderProgram sProgram;
        private List<Vector3> traslaciones;
        private List<Vector3> rotaciones;
        public escena(ShaderProgram p) 
        {
            sProgram = p;
            objetos = new List<ObjetoGrafico>();
            traslaciones = new List<Vector3>();
            rotaciones = new List<Vector3>();
        }

        public void iniciarEscena() 
        {
            //cada objeto que queremos en la escena se agrega a la lista de objetos
            objetos.Add(crearCuboBase());
            //-------------------ubicacion-------------------------------- posicion(traslacion)------------ rotacion,--------------------------------color
            objetos.Add(crear("C:/Users/Agus/Documents/Ejercicio1/cubo.obj", new Vector3(0.0f, 0.0f, 0.0f), new Vector3( 0.0f,0.0f,0.0f),      new Vector3(0.0f, 1.0f, 0.0f)));
            objetos.Add(crear("C:/Users/Agus/Documents/Ejercicio1/cubo2.obj", new Vector3(0.0f, 1f, 1f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f)));
            objetos.Add(crear("C:/Users/Agus/Documents/Ejercicio1/IcoEsfera.obj", new Vector3(0.5f, 0.5f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 1.0f)));
            objetos.Add(crear("C:/Users/Agus/Documents/Ejercicio1/UV.obj", new Vector3(1.5f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.5f, 1.0f, 0.0f)));
            objetos.Add(crear("C:/Users/Agus/Documents/Ejercicio1/toroide.obj", new Vector3(0f, 1.3f, 0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 0.0f)));
            objetos.Add(crear("C:/Users/Agus/Documents/Ejercicio1/cuboRec.obj", new Vector3(1.2f, 1.2f, 1.2f), new Vector3(0.0f, 45.0f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f)));
             objetos.Add(crear("C:/Users/Agus/Documents/Ejercicio1/cilindro.obj", new Vector3(0f, 0f, 1.5f), new Vector3(0.0f, 90.0f, 0.0f), new Vector3(0.0f, 0.5f, 0.5f)));

            objetos.Add(crear("C:/Users/Agus/Documents/Ejercicio1/cono.obj", new Vector3(0.0f, 0.0f, 0.3f), new Vector3(90f, 0f, 0f), new Vector3(1.0f, 0.0f, 0.0f)));
            objetos.Add(crear("C:/Users/Agus/Documents/Ejercicio1/cono.obj", new Vector3(0f, 0.3f, 0f), new Vector3(0.0f, 0f, 0.0f), new Vector3(0.0f, 1.0f, 0.5f)));
            objetos.Add(crear("C:/Users/Agus/Documents/Ejercicio1/cono.obj", new Vector3(0.3f, 0.0f, 0.0f), new Vector3(0f, 0f, 270f), new Vector3(0.5f, 0.0f, 0.5f)));
            //_______________________________________________________________________________________________________________________
            foreach(ObjetoGrafico o in objetos)
              {
                 o.setPrimitive(2);
                 o.Build(sProgram);
                                    
               }
            Console.WriteLine("cantidad de objetos a crear "+objetos.Count);
        }
        #region crearParticulares
        //___________________________________________________________________
        #region Cubos
        public ObjetoGrafico crearCuboBase()
        {
            DoublyConectedEdgeList l;
            String ubicacion;
            ObjFileParser parserPrueba;
            ObjetoGrafico obj;
          
            l = new DoublyConectedEdgeList();
           
           Vector3 traslacion = new Vector3(0.0f, 0.0f, 0.0f);
            traslaciones.Add(traslacion);
          
           ubicacion = "C:/Users/Agus/Documents/Ejercicio1/cubo.obj";
            parserPrueba = new ObjFileParser(l, ubicacion);
            
            Vector3 color = new Vector3(1.0f, 0.0f, 0.0f);
           obj = new ObjetoGrafico(l,color);
            return obj;
        }
      
#endregion 
       //___________________________________________________________________
       #region cilindro
       public  ObjetoGrafico crearCilindro()
        {
            DoublyConectedEdgeList l;
            String ubicacion;
            ObjFileParser parserPrueba;
            ObjetoGrafico obj;
             Vector3 traslacion = new Vector3(0.0f,1.0f,1.0f);
             traslaciones.Add(traslacion);
            l = new DoublyConectedEdgeList();
            ubicacion = "C:/Users/Agus/Documents/Ejercicio1/cubo2.obj";
            
            parserPrueba = new ObjFileParser(l, ubicacion);
            l.mostrarfigura();
            Vector3 color = new Vector3(0.0f, 0.0f, 1.0f);
            obj = new ObjetoGrafico(l,color);
            return obj;
        }
       #endregion 
       //___________________________________________________________________
        public ObjetoGrafico crearIcoesfera()
        {
            DoublyConectedEdgeList l;
            String ubicacion;
            ObjFileParser parserPrueba;
            ObjetoGrafico obj;
            
            Vector3 traslacion = new Vector3(1.0f, 1.0f, 0.0f);
            traslaciones.Add(traslacion);
           
            l = new DoublyConectedEdgeList();
            ubicacion = "C:/Users/Agus/Documents/Ejercicio1/Icoesfera.obj";

            parserPrueba = new ObjFileParser(l, ubicacion);
            l.mostrarfigura();
            Vector3 color = new Vector3(1.0f, 1.0f, 0.0f);
            obj = new ObjetoGrafico(l, color);
            return obj;
        }

        public ObjetoGrafico crearUV()
        {
            DoublyConectedEdgeList l;
            String ubicacion;
            ObjFileParser parserPrueba;
            ObjetoGrafico obj;

            Vector3 traslacion = new Vector3(1.0f, 0.0f, 0.0f);
            traslaciones.Add(traslacion);

            l = new DoublyConectedEdgeList();
            ubicacion = "C:/Users/Agus/Documents/Ejercicio1/Octoesfera.obj";

            parserPrueba = new ObjFileParser(l, ubicacion);
            l.mostrarfigura();
            Vector3 color = new Vector3(0.0f, 1.0f, 0.0f);
            l.setColor(color);
            obj = new ObjetoGrafico(l, color);
            return obj;
        }
        #endregion 
        public ObjetoGrafico crear(string u,Vector3 posicion,Vector3 rotacion,Vector3 c)
        {
            DoublyConectedEdgeList l;
            String ubicacion;
            ObjFileParser parserPrueba;
            ObjetoGrafico obj;

            Vector3 traslacion = posicion;
        
            l = new DoublyConectedEdgeList();
            ubicacion = u;

            parserPrueba = new ObjFileParser(l,u);
            l.setTraslacion(posicion);
            l.setRotacion(rotacion);
            Vector3 color = c;
            l.setColor(color);
            obj = new ObjetoGrafico(l, color);
            return obj;
        }
        
        public List<ObjetoGrafico> getObjetos() 
        {
            return objetos;
        
        }
        public List<Vector3> getTraslaciones()
        {
            return traslaciones;

        }

        public List<Vector3> getRotaciones()
        {
            return rotaciones;

        }
    
    }
}
