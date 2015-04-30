using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;

namespace CGUNS.Cameras
{
   
    /// Representa una Camara en coordenadas esfericas.
    /// La camara apunta y orbita alrededor del origen de coordenadas (0,0,0).
    /// El vector "up" de la camara es esl eje "Y" (0,1,0).
    /// La posicion de la camara esta dada por 3 valores: Radio, Theta, Phi.
 
    class SphericalCamera
    {
        private const float DEG2RAD = (float)(Math.PI / 180.0); //Para pasar de grados a radianes

        private Matrix4 projMatrix; //Matriz de Proyeccion.

        private float radius; //Distancia al origen.
        private float theta; //Angulo en el plano horizontal (XZ) desde el eje X+ (grados)
        private float phi; //Angulo desde el eje Y+. (0, 180)  menos un epsilon. (grados)

        //Valores necesarios para calcular la Matriz de Vista.
        private Vector3 eye = new Vector3(0.0f, 0.0f, 0.0f);
        private Vector3 target = new Vector3(0, 0, 0);
        private Vector3 up = Vector3.UnitY;
        private float fovy = 50 * DEG2RAD; //50 grados de angulo.
        private float aspectRadio = 1; //Cuadrado
        private float zNear = 0.1f; //Plano Near
        private float zFar = 100f;  //Plano Far
       
        public SphericalCamera()
        {
            //Por ahora la matriz de proyeccion queda fija. :)
           
            projMatrix = Matrix4.CreatePerspectiveFieldOfView(fovy, aspectRadio, zNear, zFar);

            //Posicion inicial de la camara.
            radius = 2.5f;
            theta = 40.0f;
            phi = 65.0f;
        }

        /// <summary>
        /// Retorna la Matriz de Projeccion que esta utilizando esta camara.
        /// </summary>
        /// <returns></returns>
        /// 

        public Matrix4 getProjectionMatrix()
        {
            return projMatrix;
        }
        /// <summary>
        /// Retorna la Matriz de Vista que representa esta camara.
        /// </summary>
        /// <returns></returns>
        public Matrix4 getViewMatrix()
        {   
            //Pasamos de sistema esferico, a sistema cartesiano
            eye.Y = (float)(radius * Math.Cos(phi * DEG2RAD));
            eye.X = (float)(radius * Math.Sin(phi * DEG2RAD) * Math.Cos(theta * DEG2RAD));
            eye.Z = (float)(radius * Math.Sin(phi * DEG2RAD) * Math.Sin(theta * DEG2RAD));
            //Construimos la matriz y la devolvemos.
            return Matrix4.LookAt(eye, target, up);
        }
   

        public void Acercar(float distance)
        {
            if ((distance > 0) && (distance < radius))
            {
                radius = radius - distance;
            }
        }

        public void Alejar(float distance)
        {
            if (distance > 0)
            {
                radius = radius + distance;
            }
        }

        private float deltaTheta = 10;
        private float deltaPhi = 10;

        public void Arriba()
        {
            phi = phi - deltaPhi;
            if (phi < 10)
            {
                phi = 10;
            }
        }

        public void Abajo()
        {
            phi = phi + deltaPhi;
            if (phi > 170)
            {
                phi = 170;
            }
        }

        public void Izquierda()
        {
            theta = theta + deltaTheta;
        }

        public void Derecha()
        {
            theta = theta - deltaTheta;
        }

        /**
         * setea el target de vision, que mira
         * **/
        public void setTarget(Vector3 v) { target = v; }
        /**
         * cambia la oritacion de la camara
         * **/
        public void setUp(Vector3 u) { up = u; }
        /**
         *setea el campo de vision, el x el  y para una proyeccion orthogonal
         * **/
        public void setCampoVisionOrtho(int x1, int x2, int y1, int y2)
        {
            float zNear = 0.1f; //Plano Near
            float zFar = 100f;  //Plano Far
            projMatrix = Matrix4.CreateOrthographicOffCenter(x1, x2, y1, y2, zNear, zFar);
        }
        /**
         * setea el campo de vision para una vision perspectiva
         * **/
    public void setCampoVisionPerspectiva(float angulo ,int aspect )
    {
      fovy=(fovy+angulo)*DEG2RAD;

        projMatrix = Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, zNear, zFar);
        
    }
    /**
     * profundidad: para ello cambiamos el near y el far
     * **/
    public void setProfundidadPerspectiva(float z1, float z2) 
    {
        projMatrix = Matrix4.CreatePerspectiveFieldOfView(fovy, aspectRadio, z1, z2);
    
    }

    }
}
