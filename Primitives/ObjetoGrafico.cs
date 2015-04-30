using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using gl = OpenTK.Graphics.OpenGL.GL;
using CGUNS.Shaders;
using CGUNS.Doubly_Conected_Edge_List;

namespace CGUNS.Primitives
{
    public class ObjetoGrafico
    {
        private Vector3[] vPos; //Las posiciones de los vertices.
        private int[] indices;  //Los indices para formar las caras.
        PrimitiveType primitive;
        private Vector3[] colors;
        private DoublyConectedEdgeList estructura;
        public ObjetoGrafico(DoublyConectedEdgeList l,Vector3 c)
        {
            estructura = l;
            Console.Write("hola estoy en obj grafico");
            ConstruirObjeto(l,c);

        }
        public DoublyConectedEdgeList getEstructura() { return estructura; }
        /// <summary>
        /// Construye los Buffers correspondientes de OpenGL para dibujar este objeto.
        /// </summary>
        /// <param name="sProgram"></param>
        public void Build(ShaderProgram sProgram)
        {
            CrearVBOs();
            CrearVAO(sProgram);
        }

        /**
         1 para quads 
         2 para tringles
         3 para tringles fan 
         * 4 para tringles strip
         */
        public void setPrimitive(int opcion)
        {
            switch(opcion){
                case 1:
            primitive = PrimitiveType.Quads;
            break;
                case 2:
            primitive = PrimitiveType.Triangles;
            break;
                case 3:
            primitive = PrimitiveType.TriangleFan;
            break;
                case 4:
            primitive = PrimitiveType.TriangleStrip;
            break;
            }
        }

        /// <summary>
        /// Dibuja el contenido de los Buffers de este objeto.
        /// </summary>
        /// <param name="sProgram"></param>
        /// 
        
        public void Dibujar(ShaderProgram sProgram)
        {
            //Tipo de Primitiva a utilizar (triangulos, strip, fan, quads, ..)
            int offset; // A partir de cual indice dibujamos?
            int count;  // Cuantos?
            DrawElementsType indexType; //Tipo de los indices.

            //primitive = PrimitiveType.Triangles;  //Usamos quads.
            offset = 0;  // A partir del primer indice.
            count = indices.Length; // Todos los indices.
            indexType = DrawElementsType.UnsignedInt; //Los indices son enteros sin signo.

            gl.BindVertexArray(h_VAO); //Seleccionamos el VAO a utilizar.
            gl.DrawElements(primitive, count, indexType, offset); //Dibujamos utilizando los indices del VAO.
            gl.BindVertexArray(0); //Deseleccionamos el VAO
        }

        private int h_VBO; //Handle del Vertex Buffer Object (posiciones de los vertices)
        private int h_EBO; //Handle del Elements Buffer Object (indices)
        private int h_VAO; //Handle del Vertex Array Object (Configuracion de los dos anteriores)
        private int h_color;//handlde de posiciones de los colores
        private void CrearVBOs()
        {   
                
            for (int i = 0; i < indices.Length; i++)
            {


                Console.WriteLine(" Creando VBO Indice" + indices[i]);
               
               
            }
            for (int i = 0; i < vPos.Length; i++)
            {

                Console.WriteLine("EN CREANDO VBO Vertice: " + i + " " + vPos[i].ToString());
            }
            BufferTarget bufferType; //Tipo de buffer (Array: datos, Element: indices)
            IntPtr size;             //Tamanio (EN BYTES!) del buffer.
            //Hint para que OpenGl almacene el buffer en el lugar mas adecuado.
            //Por ahora, usamos siempre StaticDraw (buffer solo para dibujado, que no se modificara)
            BufferUsageHint hint = BufferUsageHint.StaticDraw;

            //VBO con el atributo "posicion" de los vertices.
            bufferType = BufferTarget.ArrayBuffer;
            size = new IntPtr(vPos.Length * Vector3.SizeInBytes);
            h_VBO = gl.GenBuffer();  //Le pido un Id de buffer a OpenGL
            gl.BindBuffer(bufferType, h_VBO); //Lo selecciono como buffer de Datos actual.
            gl.BufferData<Vector3>(bufferType, size, vPos, hint); //Lo lleno con la info.
            gl.BindBuffer(bufferType, 0); // Lo deselecciono (0: ninguno)

            //VBO con otros atributos de los vertices (color, normal, textura, etc).
            //Se pueden hacer en distintos VBOs o en el mismo.
            
            h_color = gl.GenBuffer();
            gl.BindBuffer(bufferType, h_color); //Lo selecciono como buffer de Datos actual.
            gl.BufferData(bufferType, (IntPtr)(colors.Length * Vector3.SizeInBytes), colors, hint);
            gl.BindBuffer(bufferType, 0); 
            //EBO, buffer con los indices.
           
            bufferType = BufferTarget.ElementArrayBuffer;
            size = new IntPtr(indices.Length * sizeof(int));
            h_EBO = gl.GenBuffer();
            gl.BindBuffer(bufferType, h_EBO); //Lo selecciono como buffer de elementos actual.
            gl.BufferData<int>(bufferType, size, indices, hint);
            gl.BindBuffer(bufferType, 0);
        }

        private void CrearVAO(ShaderProgram sProgram)
        {
            // Indice del atributo a utilizar. Este indice se puede obtener de tres maneras:
            // Supongamos que en nuestro shader tenemos un atributo: "in vec3 vPos";
            // 1. Dejar que OpenGL le asigne un indice cualquiera al atributo, y para consultarlo hacemos:
            //    attribIndex = gl.GetAttribLocation(programHandle, "vPos") DESPUES de haberlo linkeado.
            // 2. Nosotros le decimos que indice queremos que le asigne, utilizando:
            //    gl.BindAttribLocation(programHandle, desiredIndex, "vPos"); ANTES de linkearlo.
            // 3. Nosotros de decimos al preprocesador de shader que indice queremos que le asigne, utilizando
            //    layout(location = xx) in vec3 vPos;
            //    En el CODIGO FUENTE del shader (Solo para #version 330 o superior)      
            int attribIndex;
            int cantComponentes; //Cantidad de componentes de CADA dato.
            VertexAttribPointerType attribType; // Tipo de CADA una de las componentes del dato.
            int stride; //Cantidad de BYTES que hay que saltar para llegar al proximo dato. (0: Tightly Packed, uno a continuacion del otro)
            int offset; //Offset en BYTES del primer dato.
            BufferTarget bufferType; //Tipo de buffer.

            // 1. Creamos el VAO
            h_VAO = gl.GenVertexArray(); //Pedimos un identificador de VAO a OpenGL.
            gl.BindVertexArray(h_VAO);   //Lo seleccionamos para trabajar/configurar.

            //2. Configuramos el VBO de posiciones.
            //attribIndex = sProgram.GetVertexAttribLocation("vPos"); //Yo lo saco de mi clase ProgramShader.
            attribIndex = 0;
            cantComponentes = 3;   // 3 componentes (x, y, z)
            attribType = VertexAttribPointerType.Float; //Cada componente es un Float.
            stride = 0;  //Los datos estan uno a continuacion del otro.
            offset = 0;  //El primer dato esta al comienzo. (no hay offset).
            bufferType = BufferTarget.ArrayBuffer; //Buffer de Datos.

            gl.EnableVertexAttribArray(attribIndex); //Habilitamos el indice de atributo.
            gl.BindBuffer(bufferType, h_VBO); //Seleccionamos el buffer a utilizar.
            gl.VertexAttribPointer(attribIndex, cantComponentes, attribType, false, stride, offset);//Configuramos el layout (como estan organizados) los datos en el buffer.

            // 2.a.El bloque anterior se repite para cada atributo del vertice (color, normal, textura..)

            //Yo lo saco de mi clase ProgramShader.
            cantComponentes = 3;   // 3 componentes (x, y, z)
            attribType = VertexAttribPointerType.Float; //Cada componente es un Float.
            stride = 0;  //Los datos estan uno a continuacion del otro.
            offset = 0;  //El primer dato esta al comienzo. (no hay offset).
            bufferType = BufferTarget.ArrayBuffer; //Buffer de Datos.

            //attribIndex = sProgram.GetVertexAttribLocation("colors");
            attribIndex = 1;
            gl.EnableVertexAttribArray(attribIndex); //Habilitamos el indice de atributo.
            gl.BindBuffer(bufferType, h_color); //Seleccionamos el buffer a utilizar.
            gl.VertexAttribPointer(attribIndex, cantComponentes, attribType, false, stride, offset);//Configuramos el layout (como estan organizados) los datos en el buffer.


            // 3. Configuramos el EBO a utilizar. (como son indices, no necesitan info de layout)
            bufferType = BufferTarget.ElementArrayBuffer;
            gl.BindBuffer(bufferType, h_EBO);

            // 4. Deseleccionamos el VAO.
            gl.BindVertexArray(0);
        }



        /**
         * Debe recibir un Doubly conected y rellenar 
         * **/
        public void ConstruirObjeto(DoublyConectedEdgeList l,Vector3 c)
        {
            //rellono los indices

            List<Vertex> vertices = l.getVertices();
            vPos = new Vector3[vertices.Count];
            colors = new Vector3[vertices.Count];
          
            for (int i = 0; i < vertices.Count; i++)
            {
                vPos[i] = vertices[i].getCoordenadas();
                colors[i] = c;
                //  Console.WriteLine("Vertice: " + i + " " + vPos[i].ToString());
            }
           
            List<face> caras = l.getCaras();
            List<int> puntos;
            List<int> aux = new List<int>();
            foreach (face f in caras)
            {
                puntos = f.getVertices();
                for (int i = 0; i < puntos.Count; i++)
                {
                    aux.Add(puntos[i]);

                }
            }
            indices = new int[aux.Count];
            for (int i = 0; i < aux.Count; i++)
            {
                indices[i] = aux[i];
            }

        }


    }

}