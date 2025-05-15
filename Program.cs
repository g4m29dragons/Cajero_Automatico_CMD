//Nombre del Autor: Jesus Augusto Chacon Corredor
//Codigo del curso: 213023
//Grupo: 16
//Carrera: Ingenieria en sistemas


/*
 * Cuentas Validas para el acceso al Cajero Automatico UNAD:
1. cuentas disponibles para acceder al cajero:
    *Cuenta N° = 85150769 - Contraseña = 1984
    *Cuenta N° = 36550310 - Contraseña = 2024
*/

using System;
using System.Collections.Generic;

namespace CajeroAutomaticoSimulacion
{
    //El programa tiene varias clases: Usuario, Cuenta, CajeroAutomatico y Program.

    //esta clase representa a un usuario del cajero.
    public class UsuarioCajero 
    {
        //esta propiedad pide datos y no es modificable sus datos a pedir.
        public string Numeros_de_Cuentas { get; private set; }

        //esta propiedad pide datos y no es modificable sus datos a pedir.
        public string GuardarContraseña { get; private set; }

        //este metodo funciona como constructor tanto para numeroCuenta como para contraseña.
        public UsuarioCajero(string numero_de_Cuenta, string contraseña)
        {
            //Asigna el número de cuenta a la propiedad NumeroCuenta.
            Numeros_de_Cuentas = numero_de_Cuenta;

            // Asigna la contraseña a la propiedad Contraseña.
            GuardarContraseña = contraseña;
        }

        // Método que verifica si el número de cuenta y la contraseña coinciden con las propiedades del usuario.
        public bool ValidarCuentas(string numeroCuenta, string contraseña) =>
            Numeros_de_Cuentas == numeroCuenta && GuardarContraseña == contraseña;//conversion a objetos.
    }

    //Clase que muestra en pantalla la fecha actual.
    public class FechaHoraSistema
    {
        public void ImprimirFechaHora()
        {
            // Obtener la fecha y hora actual del sistema
            DateTime fechaHoraActual = DateTime.Now;

            // Imprimir la fecha y hora en la consola
            Console.WriteLine("|Fecha Actual:                    |\n"
                +"|"+fechaHoraActual.ToString()+"         |");
        }
    }

    //esta clase representa una cuenta bancaria.
    public class Cuentas_Usuarios
    {

        // Propiedad que almacena el saldo de la cuenta y solo permite lectura.
        public decimal Saldo { get; private set; }

        //esta propiedad pide datos y no es modificable sus datos a pedir y tiene una
        //suma limite de dos millones, lo cual no permitira retiros superiores a esta suma.
        public decimal Tope_retiros { get; private set; } = 2000000;

        // Propiedad que almacena la cantidad retirada en el día, inicializada en cero, y solo permite lectura.
        public decimal Retiros_diarios { get; private set; } = 0;

        // Propiedad que almacena los puntos ViveColombia y solo permite lectura.
        public int Puntos_vive_colombia { get; private set; }

        //Este constructor inicializa las propiedades Saldo y Puntos_vive_colombia.
        public Cuentas_Usuarios(decimal saldoInicial, int puntosIniciales)
        {
            Saldo = saldoInicial;
            Puntos_vive_colombia = puntosIniciales;
        }

        // Método que muestra el saldo actual de la cuenta.
        public void ConsultarSaldo() => Console.WriteLine("-----------------------------------\n" +
        "|         Recibo-Consulta         |\n" + "|                                 |"+$"\n|Saldo actual: {Saldo:C}     |");
        
        //Con este método podemos retirar dinero de la cuenta y verifica que el retiro sea válido.
        public bool RetirarDinero(decimal monto)
        {

            //Aqui se verifica que el saldo sea suficiente y que el monto a retirar no exceda el tope diario.
            if (Saldo >= monto && (Retiros_diarios + monto) <= Tope_retiros)
            {
                //el monto le reste al saldo
                Saldo -= monto;

                //el monto le suma a los retiros diarios que se pueden hacer.
                Retiros_diarios += monto;

                FechaHoraSistema fechaHora = new FechaHoraSistema();

                //aqui nos muestra en pantalla lo operacion realizada si es exitosa.
                Console.WriteLine(" ---------------------------------");
                Console.WriteLine("|             Recibo:             |");
                Console.WriteLine("|                                 |");
                Console.WriteLine("|Retiro exitoso.                  |");
                Console.WriteLine("|                                 |");
                Console.WriteLine($"|Saldo restante: {Saldo:C}   |");
                Console.WriteLine($"|Valor Retiro:   $ {monto}         |");
                Console.WriteLine("|                                 |");
                fechaHora.ImprimirFechaHora();
                Console.WriteLine(" ---------------------------------");
                return true;
            }

            //Muestra en pantalla un mensaje si es fellida la operacion.
            else
            {
                Console.WriteLine("\nRetiro fallido. \nFondos insuficientes o tope diario excedido.\n");
                return true;
            }
        }

        //Usaremos este método para transferir dinero a otra cuenta.
        public bool TransferirDinero(Cuentas_Usuarios cuentaDestino, decimal monto)
        {
            //Aqui se verifica que el saldo sea suficiente.
            if (Saldo >= monto)
            {
                //se le resta el monto del saldo.
                Saldo -= monto; cuentaDestino.RecibirTransferencia(monto);

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                //Aqui se le agrega el monto al saldo de la cuenta destino.
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("|              Recibo             |");
                Console.WriteLine("|                                 |");
                Console.WriteLine($"|Transferencia exitosa.           |");
                Console.WriteLine("|                                 |");
                Console.WriteLine($"|Saldo restante: {Saldo:C}   |");
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                //en caso de fallar las transferencia, dira que no hay fondos.
                Console.WriteLine("\nTransferencia fallida. \nFondos insuficientes.\n\n");
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            }
        }

        // Método que aumenta el saldo al recibir una transferencia.
        public void RecibirTransferencia(decimal monto) => Saldo += monto;

        // Método que muestra los puntos ViveColombia.
        public void ConsultarPuntos() => Console.WriteLine($"Puntos ViveColombia: {Puntos_vive_colombia}");
       
        //con este metodo podremos realizar la accion de cangear puntos
        public bool CanjearPuntos(int puntos)
        {
            //con esta condicional podremos realizar el canje de puntos.
            if (Puntos_vive_colombia >= puntos)
            {
                //aqui los puntos de le restan a puntos_vive_colombia.
                Puntos_vive_colombia -= puntos;

                //aqui toma los puntos y los multiplica *7 y se los agrega al saldo.
                Saldo += puntos * 7;

                FechaHoraSistema fechaHora = new FechaHoraSistema();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                //si el canje es exitoso nos mostrara en pantalla los puntos cambiados.
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("|    Canje puntos VivaColombia.   |");
                Console.WriteLine("|                                 |");
                Console.WriteLine("|Canje exitoso.                   |");
                Console.WriteLine("|                                 |");
                Console.WriteLine($"|Puntos restantes: {Puntos_vive_colombia}             |" +
                    $"\n|Saldo: {Saldo:C}            |");
                Console.WriteLine("|                                 |");
                fechaHora.ImprimirFechaHora();
                Console.WriteLine("-----------------------------------");
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\nCanje fallido. \nPuntos insuficientes.");
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            }
        }
    }

    //Aqui definimos la clase CajeroAutomatico
    public class CajeroAutomatico
    {
        // Diccionario para almacenar usuarios, utilizando el número de cuenta como clave
        private Dictionary<string, UsuarioCajero> usuarios = new Dictionary<string, UsuarioCajero>();

        // Diccionario para almacenar cuentas, utilizando el número de cuenta como clave
        private Dictionary<string, Cuentas_Usuarios> cuentas = new Dictionary<string, Cuentas_Usuarios>();

        // Variable para almacenar el usuario actualmente autenticado
        private UsuarioCajero usuarioActual;

        //Este es el método para registrar un nuevo usuario y su cuenta asociada
        public void RegistrarUsuario(string numeroCuenta, string contraseña, decimal saldoInicial, int puntosIniciales)
        {
            // Añadir el nuevo usuario al diccionario de usuarios
            usuarios[numeroCuenta] = new UsuarioCajero(numeroCuenta, contraseña);

            // Añadir la nueva cuenta al diccionario de cuentas
            cuentas[numeroCuenta] = new Cuentas_Usuarios(saldoInicial, puntosIniciales);
        }

        // Método para autenticar a un usuario
        public bool AutenticarUsuario(string numeroCuenta, string contraseña)
        {
            // Verificar si el número de cuenta existe y si la autenticación es exitosa.
            if (usuarios.ContainsKey(numeroCuenta) && usuarios[numeroCuenta].ValidarCuentas(numeroCuenta, contraseña))
            {
                // Asignar el usuario autenticado a la variable usuarioActual.
                usuarioActual = usuarios[numeroCuenta];
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n \n Autenticación exitosa.\n");
                Console.ForegroundColor = ConsoleColor.White;
                // Devolver true si la autenticación es exitosa.
                return true;
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\n\nNumero de cuenta o contraseña erronea.");
            Console.ForegroundColor = ConsoleColor.White;
            // Devolver false si la autenticación falla.
            return false;
        }

        //con este metodo se opera el cajero automático.
        public void OperarCajero()
        {
            // Verificar si hay un usuario autenticado
            if (usuarioActual == null) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Debe autenticarse antes de realizar operaciones.");
                Console.ForegroundColor = ConsoleColor.White;
                // Salir del método si no hay un usuario autenticado
                return; 
            }

            // Obtener la cuenta actual del usuario autenticado
            Cuentas_Usuarios cuentaActual = cuentas[usuarioActual.Numeros_de_Cuentas];

            //Con esta variable se controla el bucle de operaciones.
            bool continuar = true;

            //este Bucle permite realizar múltiples operaciones.
            while (continuar)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                //con esta linea de codigo muestra el menú de operaciones.
                Console.WriteLine("\nSeleccione una operación:\n \n1. Consultar saldo\n2. Retirar dinero\n3. Transferir dinero\n4. Consultar puntos ViveColombia\n5. Canjear puntos ViveColombia\n6. Salir");
                Console.ForegroundColor = ConsoleColor.White;
                //en este switch leera la opción seleccionada por el usuario.
                switch (Console.ReadLine())
                {
                    // Caso para consultar el saldo
                    case "1":
                        cuentaActual.ConsultarSaldo();
                        if (true) 
                        {
                            FechaHoraSistema fechaHora = new FechaHoraSistema();
                            Console.WriteLine("|                                 |");
                            fechaHora.ImprimirFechaHora();
                            Console.WriteLine("-----------------------------------");
                        }
                        
                        break;

                    // Caso para retirar dinero
                    case "2":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Ingrese el monto a retirar:");
                        Console.ForegroundColor = ConsoleColor.White;

                        if (decimal.TryParse(Console.ReadLine(), out decimal montoRetiro)) cuentaActual.RetirarDinero(montoRetiro);
                        
                        break;

                    // Caso para transferir dinero
                    case "3":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Ingrese el número de cuenta destino:"); 
                        string cuentaDestinoNumero = Console.ReadLine();
                        if (cuentas.ContainsKey(cuentaDestinoNumero))
                        {
                            Console.WriteLine("Ingrese el monto a transferir:");
                            if (decimal.TryParse(Console.ReadLine(), out decimal montoTransferencia)) cuentaActual.TransferirDinero(cuentas[cuentaDestinoNumero], montoTransferencia);

                            if (cuentaDestinoNumero == "36550310")
                            {
                                FechaHoraSistema fechaHora = new FechaHoraSistema();
                                Console.WriteLine("|                                 |");
                                Console.WriteLine($"|Valor Transferido: {montoTransferencia}        |");
                                Console.WriteLine("|                                 |");
                                Console.WriteLine("|Nombre usuario:                  |\n" +
                                    "|Mariela Corredor Corredor        |");
                                Console.WriteLine("|                                 |");
                                fechaHora.ImprimirFechaHora();
                                Console.WriteLine("-----------------------------------");
                            }

                            if (cuentaDestinoNumero == "85150769")
                            {
                                FechaHoraSistema fechaHora = new FechaHoraSistema();
                                Console.WriteLine("|                                 |");
                                Console.WriteLine($"|Valor Transferido: {montoTransferencia}        |");
                                Console.WriteLine("|                                 |");
                                Console.WriteLine("|Nombre usuario:                  |\n" +
                                    "|Jesus Augusto Chacon Corredor    |");
                                Console.WriteLine("|                                 |");
                                fechaHora.ImprimirFechaHora();
                                Console.WriteLine("-----------------------------------");
                                OperarCajero();
                            }

                            if (montoTransferencia >= 2000000)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Monto inválido.");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }

                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("La cuenta destino no existe.");
                            Console.ForegroundColor = ConsoleColor.White;
                            OperarCajero();
                        }
                        break;

                    // Caso para consultar los puntos ViveColombia
                    case "4": cuentaActual.ConsultarPuntos(); 
                        break;

                    // Caso para canjear puntos ViveColombia
                    case "5":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Ingrese la cantidad de puntos a canjear:");
                        if (int.TryParse(Console.ReadLine(), out int puntosCanje)) cuentaActual.CanjearPuntos(puntosCanje);

                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Cantidad de puntos inválida.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;

                    // Caso para salir del menú de operaciones
                    case "6":
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        continuar = false; Console.WriteLine("Gracias por usar el cajero automático UNAD.");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    // Caso por defecto para opciones no válidas
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción no válida.");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
        }
    }

    //public void Recibo(usuarioActual) 
    //{

    //}

    //Esta es la Clase principal del programa.
    class Program
    {
        //Este es el método principal donde inicia la ejecución del programa.
        static void Main(string[] args)
        {
            FechaHoraSistema fechaHora = new FechaHoraSistema();

            //Aqui se crea una instancia de la clase CajeroAutomatico.
            CajeroAutomatico cajero = new CajeroAutomatico();

            //Este es el registro de los usuarios en el cajero automático.
            cajero.RegistrarUsuario("85150769", "1984", 5000000, 1900);
            cajero.RegistrarUsuario("36550310", "2024", 3000000, 2000);

            //Aqui se le solicita al usuario que ingrese el número de cuenta.
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Ingrese el número de cuenta:");
            Console.ForegroundColor = ConsoleColor.White;
            string numeroCuenta = Console.ReadLine();

            //Aqui se le solicita al usuario que ingrese el número de contraseña.
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Ingrese la contraseña:");

            //aqui se inicializa la variable para almacenar la contraseña ingresada.
            String contraseña = null;

            //este bucle es para leer la contraseña de manera segura (ocultando los caracteres).
            while (true)
            {
                //Lee una tecla sin mostrarla en la consola
                var tecla = Console.ReadKey(true);

                //Aqui termina el bucle cuando se presiona Enter.
                if (tecla.Key == ConsoleKey.Enter) 
                {
                    //termina el bucle cuando presionas Enter.
                    break;
                }

                // Concatenar el carácter ingresado a la variable contraseña
                contraseña = contraseña + (Convert.ToString(tecla.KeyChar));

                //al presionar cualquier tecla mostrara un asterisco en lugar del carácter real.
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("*");
            }

            //Validador de usuarios.
            if (numeroCuenta == "85150769")
            {
                // Mostrara un mensaje de bienvenida personalizado para el usuario 85150769
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("\nBienvenido al cajero UNAD señor: \nJesus Augusto Chacon Corredor");
            }

            //Validador de usuarios.
            if (numeroCuenta == "36550310")
            {
                // Mostrara un mensaje de bienvenida personalizado para el usuario 36550310
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("\nBienvenida al cajero UNAD señora: \n Mariela Corredor Corredor");
            }

            //Autenticador de usuarios.
            if (cajero.AutenticarUsuario(numeroCuenta, contraseña))
            {
                //Si la autenticación del usuario es exitosa, permitir al usuario operar el cajero.
                cajero.OperarCajero();
            }

            //Si la autenticacion no es exitosa mostrar.
            else
            {
                // Si la autenticación falla, mostrar mensaje de error y finalizar el programa
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAutenticación fallida. \n\nFin del programa.\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}