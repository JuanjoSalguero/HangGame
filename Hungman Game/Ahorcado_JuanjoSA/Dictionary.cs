using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ahorcado_JuanjoSA
{
    public struct Dictionary

    {// <----------------------------------------------------- MIEMBROS
       
        private string[] Words; // Miembro donde se guardan las palbras (Diccionario de palabras)

     // <----------------------------------------------------- PROPIEDADES

        public string words // Propiedad Words
        {
            get
            {
                if (Words == null) throw new Exception("* Words no está inicializado."); // Si no está inicializado
                return Words[PalabraRandom()]; // Nos devuelve una palabra aleatoria de las introducidas en el array
            }
            set
            {
                // Pasamos el valor a mayúscula para las validaciones
                value = value.ToUpper();

                // Validación palabras
                Validacíon(value);

                // Inicialización de Words
                if (Words == null) 
                {
                    Words = new string[1]; // Inicializamos el array con un tamaño de 1
                    Words[0] = value; // En la primera posición asignamos value
                }
                else
                {
                    // Array dinámico // Aumentamos el tamaño del array en 1
                    Array.Resize(ref Words, Words.Length + 1);

                    // Validación para no introducir palabras repetidas
                    if (Words.Contains(value)) throw new Exception("* La palabra ya ha sido introducida, introduzca una nueva.");

                    Words[Words.Length - 1] = value; // En la posición del tamaño del array menos uno, le asignamos la palabra introducida. 
                }
            }
        }

     // <----------------------------------------------------- MÉTODOS PRIVADOS

        private int PalabraRandom() // Método para devolver la posición del array para seleccionar la palabra aleatoria
        {
            Random rnd = new Random(); // Creamos el Random
            return rnd.Next(0, Words.Length); // Devolvemos una posición aleatoria
        }

        private void Validacíon(string value) // Método para la validación de las palabras
        {
            // Si no se introduce nada
            if (string.IsNullOrEmpty(value)) throw new Exception("* La palabra no puede estar vacía.");

            // Validación para que la palabra contenga almenos 3 caracteres
            if (value.Length < 3) throw new Exception("* La palabra debe conterner almenos 3 caracteres.");

            // Validación de la palabra introducida
            for (int i = 0; i < value.Length; i++) if (!(value[i] >= 65 && value[i] <= 90))
                    throw new Exception("* La palabra no puede contener espacios, acentos ni símbolos.");
        }
    }
}
