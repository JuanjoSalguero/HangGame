using System;
using System.Linq;

namespace Ahorcado_JuanjoSA
{   

    internal class Program
    {// <----------------------------------------------------- METODOS MENÚ
        
        static public void Welcome()  // Método de bienvenida
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n\t-----------------------------------------------------------------------------------");
            Console.WriteLine("\t|                         Bienvenido al Juego del Ahorcado                        |");
            Console.WriteLine("\t-----------------------------------------------------------------------------------\n");
            Console.ResetColor();
        }

        static public void Menu() //Método del Menú 
        {
            // CONSTANTES
            // VARIABLES
            Dictionary dictionary = new Dictionary(); // Instanciación estructura Dictionary
            byte menuOption; // Opciones del menú
            string message; // String para mensajes de error
            bool check = false;

            // ENTRADA
            do
            {
                try
                {
                    Console.Clear();
                    Welcome(); // Bienvenida

                    // Solicitud de la acción a realizar por el usuario
                    Console.WriteLine("\t¿Qué acción desea realizar el Usuario?\n");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\t\t1-. Añadir palabras." +
                        "            \n\t\t2-. Iniciar partida." +
                        "            \n\t\t3-. Salir.");
                    Console.ResetColor();
                    menuOption = Byte.Parse(Console.ReadLine());

                    // Si no se introduce una opción válida
                    if (menuOption < 1 || menuOption > 3) throw new Exception("* La opcion debe estar comprendida entre 1 y 3");

                    // Opciones del Menú
                    switch (menuOption)
                    {
                        case 1: // ------------------------------------------- 1. Añadir palabras al diccionario
                            dictionary = SolicitarPalabras(dictionary);
                            break;

                        case 2: // ------------------------------------------- 2. Iniciar partida del Ahorcado
                            JuegoAhorcado(dictionary);
                            break;

                        case 3: // -------------------------------------------  3. Salir del juego
                            check = true;
                            SalidaFinal();
                            break;
                    }
                }
                catch (FormatException) // Excepción de formato erroneo
                {
                    message = "* El formato de la opción no es correcto.";
                    MetodoFinally(message);
                }
                catch (Exception e)
                {
                    MetodoFinally(e.Message);
                }

            } while (!check);
        }

        static public void SalidaFinal() // Método de salida que finaliza el juego
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n\t------------------------------------------------------------------");
            Console.WriteLine("\t|                                                                |");
            Console.WriteLine("\t|          Espero que se haya divertido. ¡Hasta pronto!          |");
            Console.WriteLine("\t|                                                                |");
            Console.WriteLine("\t------------------------------------------------------------------");
            Console.ResetColor();
            Console.ReadKey();
        }


     // <----------------------------------------------------- METODOS DICCIONARIO

        static public Dictionary SolicitarPalabras(Dictionary dictionary) // Método para solicitar las palabras y almacenarlas en el diccionario 
        {
            // VARIABLES
            bool finish = false; // Booleana para salir del bucle de la introducción de palabras
            string aux; // Auxiliar para salir del bucle de la introducción de palabras

            // ENTRADA
            do
            {
                try
                {
                    // Lectura de las palabras
                    Console.Write("\n\tIntroduzca una palabra: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n\t   S para salir"); 
                    Console.ResetColor();
                    aux = Console.ReadLine(); // Almacenamos la palabra en una variable provisional
                    if (aux == "S" || aux == "s") finish = true; // Si esta no es ni S ni s
                    else dictionary.words = aux; // Almacenamos la palabra en dictionary.words
                }
                catch (Exception e)
                {
                    MetodoFinally(e.Message);
                }

            } while (!finish);

            // SALIDA
            return dictionary;
        }


     // <----------------------------------------------------- METODOS GAME 

        static public void ComienzoJuego() // Método bienvenida de comienza juego
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("|                   ¡Comenzamos!      Adivina la palabra                 |");
            Console.WriteLine("--------------------------------------------------------------------------\n");
            Console.ResetColor();
        }

        static public void JuegoAhorcado(Dictionary dictionary) // Método para empezar el Juego del Ahorcado
        {
            // CONSTANTES
            // VARIABLES
            Game game = new Game(); // Instanciación estructura Game
            char[] linesAndChars = new char[game.WordAssignament(dictionary).Length]; // Array que almacena lineas y que almacenará las letras que contiene la palabra random
            bool finish = false; // Booleana para finalizar el programa
            bool victory = false; // Booleana para controlar la victorio cuando se introduzcan todas las letras correctamente
            string letterOrWord=""; // Variable para almacenar momentaneamente el caracter/palabra introducida por el usuario
            byte i; // Indice i

            // Inicialización de la palabra con rayas en lugar de letras
            for (i = 0; i < game.WordAssignament(dictionary).Length; i++) linesAndChars[i] = '_'; // Rellenamos con "_"

            // ENTRADA
            do 
            {
                try
                {
                    Console.Clear();
                    // Cabecera juego
                    ComienzoJuego();
                    // Letras erroneas
                    ImprimirLetrasErroneas(game,dictionary);
                    // Número de Errores restantes + gráfico ahorcado
                    ErroresAhorcado(game);
                    // Imprimir las lineas y letras que formen parte de la palabra
                    ImprimirLetrasPalabra(linesAndChars);

                    // Solicitud de letra o palabra
                    Console.Write("\n\n\n\nIntroduzca una letra o inserte la palabra a adivinar: ");
                    letterOrWord = Console.ReadLine(); // Lectura de la palabra o letra
                    letterOrWord = letterOrWord.ToUpper(); // Pasamos la palabra o la letra introducida a mayúscula

                    // Validacion letras/palabras introducidas
                    ValidacionLectura(letterOrWord);

                    if (letterOrWord.Length == 1) // Si la longitud de lo introducido es uno, esto quiere decir que es un caracter
                    {
                        // Validación para no introducir caracteres repetidos
                        for (i = 0; i < game.getCharacter().Length; i++)
                        {
                            if (game.getCharacter()[i]==letterOrWord[0]) throw new Exception("* La letra ya ha sido introducida, introduzca una nueva.");
                        }                    

                        // Si la palabra rndWord no contiene el caracter introducido
                        if (!(game.WordAssignament(dictionary).Contains(letterOrWord)))
                        {
                            game.character = letterOrWord[0]; // Almacenamos el caracter erroneo en su propiedad
                            game.Increase(); // Incrementamos los errores
                        }
                        else
                        {
                            // Si la letra ya ha sido introducida
                            if (linesAndChars.Contains(letterOrWord[0])) throw new Exception("* La letra ya ha sido introducida, introduzca una nueva."); 

                            // Comprobación de si la letra introducida coincide con alguna de la palabra aleatoria
                            for (i = 0; i < game.WordAssignament(dictionary).Length; i++)
                            {
                                if (letterOrWord[0] == game.WordAssignament(dictionary)[i]) // Si el caracter introducido coincide con alguno de la palabra aleatoria
                                {
                                    linesAndChars[i] = letterOrWord[0]; // Lo almacenamos en su posición en el array que habiamos creado de rayas
                                }
                            }
                        }
                    }
                    else // Si no
                    {
                        if (letterOrWord == game.WordAssignament(dictionary)) // Si la palabra introducida es igual a la palabra random
                        {
                            finish = true; // Finalizar true para que salga del bucle
                            victory = true; // Victoria true para que nos muestre la salida de victoria
                        }
                        else
                        {
                            game.Increase(); // Si la palabra introducida no es correcta, incrementamos el error
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("\n* La palabra introducida no es la palabra a adivinar.");
                            Console.WriteLine("Presione ENTER para continuar...");
                            Console.ReadLine();
                            Console.ResetColor();
                        }
                        
                    }

                    if (game.errors == game.chances) finish = true; // Si el número de errores es igual al numero de intentos, finalizamos el juego (Derrota)
                }
                catch (Exception e)
                {
                    MetodoFinally(e.Message);
                }
                
            } while (!finish); //Realizar el bucle mientras finish sea false

            // PROCESO
            // SALIDA
            SalidaJuego(game, dictionary, victory); // Salida de victoria o derrota
        }

        static public void ValidacionLectura(string letterOrWord) // Método para la validación de lectura de la palabra
        {
            // Validación para que no acepte palabras vacias
            if (string.IsNullOrEmpty(letterOrWord)) throw new Exception("* No ha introducido ninguna palabra.");

            // Validación para no introducir palabras de menos de 3 palabras
            if (letterOrWord.Length == 2) throw new Exception("* La palabra debe ser almenos de 3 letras.");

            // Validación de la palabra introducida
            for (int i = 0; i < letterOrWord.Length; i++) if (!(letterOrWord[i] >= 65 && letterOrWord[i] <= 90))
                    throw new Exception("* La palabra no puede contener espacios, acentos ni símbolos.");
        }

        static public void ImprimirLetrasErroneas(Game game, Dictionary dictionary) // Método para imprimir las letras erroneas
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t\t\t     Letras erroneas:");
            Console.ResetColor();
            Console.Write("\t\t\t     ");

            //Impresión de letras erroneas
            if (!(game.WordAssignament(dictionary).Contains(game.character))) // Si la palabra a adivinar no contiene el caracter introducido
            {
                for (int i = 1; i < game.getCharacter().Length; i++)
                {
                    Console.Write(game.getCharacter()[i] + " "); // Imprimimos los caracteres almacenados más el último 
                }
            }
            Console.WriteLine(" ");
        }

        static public void ImprimirLetrasPalabra(char[] linesAndChars) // Método para imprimir las lineas y letras que formen parte de la palabra
        {
            Console.Write("\t\t");
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            for (int i = 0; i < linesAndChars.Length; i++) // Bucle para imprimir las rayas y las letras almacenadas
            {
                Console.Write(linesAndChars[i] + " "); // Imprimimos la palabra con sus rayas o letras acertadas correspondientes
            }
            Console.ResetColor();
        }
       
        static public void ErroresAhorcado(Game game) // Método para imprimir el grafico del ahorcado
        {
            switch (game.errors)
            {
                case 0: // 7 intentos
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("          ____   ");
                    Console.WriteLine("         |       ");
                    Console.WriteLine("         |       ");
                    Console.WriteLine("         |       ");
                    Console.WriteLine("         |       ");
                    Console.WriteLine("       __|___    ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"\n    Tiene {game.chances} intentos.");
                    Console.ResetColor();
                    break;
                case 1: // 6 intentos
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("          ____   ");
                    Console.WriteLine("         |    |  ");
                    Console.WriteLine("         |       ");
                    Console.WriteLine("         |       ");
                    Console.WriteLine("         |       ");
                    Console.WriteLine("      ___|___    ");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write($"\n    Tiene {game.chances - game.errors} intentos.");
                    Console.ResetColor();
                    break;
                case 2: // 5 intentos
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("          ____   ");
                    Console.WriteLine("         |    |  ");
                    Console.WriteLine("         |    O  ");
                    Console.WriteLine("         |       ");
                    Console.WriteLine("         |       ");
                    Console.WriteLine("      ___|___    ");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write($"\n    Tiene {game.chances - game.errors} intentos.");
                    Console.ResetColor();
                    break;
                case 3: // 4 intentos
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("          ____   ");
                    Console.WriteLine("         |    |  ");
                    Console.WriteLine("         |    O  ");
                    Console.WriteLine("         |   /  ");
                    Console.WriteLine("         |       ");
                    Console.WriteLine("      ___|___    ");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write($"\n    Tiene {game.chances - game.errors} intentos.");
                    Console.ResetColor();
                    break;
                case 4: // 3 intentos
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("          ____   ");
                    Console.WriteLine("         |    |  ");
                    Console.WriteLine("         |    O  ");
                    Console.WriteLine(@"         |   / \ ");
                    Console.WriteLine("         |       ");
                    Console.WriteLine("      ___|___    ");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write($"\n    Tiene {game.chances - game.errors} intentos.");
                    Console.ResetColor();
                    break;
                case 5: // 2 intentos
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("          ____   ");
                    Console.WriteLine("         |    |  ");
                    Console.WriteLine("         |    O  ");
                    Console.WriteLine(@"         |   /|\ ");
                    Console.WriteLine("         |       ");
                    Console.WriteLine("      ___|___    ");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write($"\n    Tiene {game.chances - game.errors} intentos.");
                    Console.ResetColor();
                    break;
                case 6: // 1 intento
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("          ____   ");
                    Console.WriteLine("         |    |  ");
                    Console.WriteLine("         |    O  ");
                    Console.WriteLine(@"         |   /|\ ");
                    Console.WriteLine("         |   /   ");
                    Console.WriteLine("      ___|___    ");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write($"\n    Tiene {game.chances - game.errors} intentos.");
                    Console.ResetColor();
                    break;
            }
        }

        static public void Derrota(string rndWord) // Método para la derrota
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("|                        H A S      P E R D I D O                        |");
            Console.WriteLine("--------------------------------------------------------------------------\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                 ____   ");
            Console.WriteLine("                |    |  ");
            Console.Write    ("                |    O   ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\t La palabra era " + rndWord);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"                |   /|\ ");
            Console.WriteLine(@"                |   / \ ");
            Console.WriteLine("             ___|___    ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\n--------------------------------------------------------------------------\n");
            Console.ResetColor();

        }

        static public void Victoria() // Método para la victoria
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("|                                                                        |");
            Console.WriteLine("|                   ¡  H A S      A C E R T A D O  !                     |");
            Console.WriteLine("|                                                                        |");
            Console.WriteLine("|         ______________________________________________________         |");
            Console.WriteLine("|                                                                        |");
            Console.WriteLine("|                     ¡  E N H O R A B U E N A  !                        |");
            Console.WriteLine("|                                                                        |");
            Console.WriteLine("--------------------------------------------------------------------------\n");
            Console.ResetColor();
        }
        
        static public void SalidaJuego(Game game, Dictionary dictionary, bool victory) // Metodo para la victoria/derrota del juego
        {
            if (game.errors == game.chances) Derrota(game.WordAssignament(dictionary)); // Si los errores es igual que al de las oportunidaddes (7) Finaliza el juego
            else if (victory) Victoria(); // Si acertamos la palabra
            Console.WriteLine("\t Presione ENTER para volver al menú.");
            Console.ReadLine();
        } 


     // <----------------------------------------------------- METODOS GENERALES

        static public void MetodoFinally(string message) // Método finally
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\n" + message);
            Console.WriteLine("Presione ENTER para continuar...");
            Console.ReadLine();
            Console.Clear();
            Console.ResetColor();
        }

     // <----------------------------------------------------- MAIN
        static void Main(string[] args)
        {
            // CONSTANTES
            // VARIABLES
            // ENTRADA
            Menu();
            
            // PROCESO
            // SALIDA
        }
    }
}
