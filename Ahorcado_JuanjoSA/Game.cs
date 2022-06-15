using System;
using System.Collections.Generic;
using System.Text;

namespace Ahorcado_JuanjoSA
{
    // <----------------------------------------------------- MIEMBROS
    public struct Game
    {
        // CONSTANTES
        private const byte CHANCES = 7;     // Número de intentos que tiene el jugador
        // MIEMBROS
        private string RndWord;             // Palabra aleatoria procedente del diccionario
        private int Errors;                 // Contador errores
        private char[] Character;           // Almacén para los caracteres introducidos incorrectamente

        // <----------------------------------------------------- PROPIEDADES DE LOS MIEMBROS
        public byte chances // Propiedad CHANCES
        {
            get { return CHANCES; }
        }

        public string rndWord // Propiedad RndWord
        {
            get { return RndWord; }
        }

        public int errors // Propiedad Errors
        {
            get
            {
                return Errors;
            }
        }

        public char character // Propiedad Character
        {
            get
            {
                if (Character == null)
                {
                    Character = new char[1];
                    Array.Resize(ref Character, Character.Length + 1);
                }
                return Character[Character.Length - 1];
            }
            set
            {
                if (Character == null)
                {
                    Character = new char[1];
                }
                Array.Resize(ref Character, Character.Length + 1);

                // Validación para no introducir palabras repetidas
                if (character.ToString().Contains(value)) throw new Exception("* La letra ya ha sido introducida, introduzca una nueva.");

                // Validación de la palabra introducida
                if (!(value >= 65 && value <= 90))
                    throw new Exception("* El caracter debe estar comprendido entre la A y la Z sin incluir la Ñ.");

                Character[Character.Length - 1] = value;
            }
        }

        // <----------------------------------------------------- MÉTODOS PÚBLICOS

        public char[] getCharacter() // Método para poder imprimir los caracteres erroneos en el Program.cs
        {
            if (Character == null)
            {
                Character = new char[1];
            }

            return Character;
        } 

        public void Increase() // Método para incrementar los errores en uno
        {
            Errors++; // Incrementamos en uno los errores
        }

        public string WordAssignament(Dictionary dictionary) // Método para asignar una palabra aleatoria al juego
        {
            if (RndWord == null) RndWord = dictionary.words;

            return RndWord;
        }
    }

}
