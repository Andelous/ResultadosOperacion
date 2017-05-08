using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartamentoServiciosEscolaresCBTis123.Logica.Utilerias
{
    /// <summary>
    /// Representa el resultado de una operación llevada a cabo por la aplicación.
    /// </summary>
    public class ResultadoOperacion
    {
        /// <summary>
        /// Es el estado en el cual terminó la operación realizada.
        /// </summary>
        public EstadoOperacion estadoOperacion { get; }
        /// <summary>
        /// Descripción del resultado de la operación.
        /// </summary>
        public string descripcion { get; }
        /// <summary>
        /// Código de error en caso de haber ocurrido alguno.
        /// </summary>
        public string errCode { get; }

        /// <summary>
        /// Resultado de operación interno a esta operación. Útil si se están llevando a cabo operaciones anidadas.
        /// </summary>
        public ResultadoOperacion resultadoOperacionInterno { get; set; }

        /// <summary>
        /// Constructor general del objeto. Recibe parámetros para poblar las propiedades del objeto.
        /// </summary>
        /// <param name="estadoOperacion">Estado en el cual resultó la operación llevada a cabo.</param>
        /// <param name="descripcion">Descripción de la operación.</param>
        /// <param name="errCode">Código de error.</param>
        /// <param name="resultadoOperacionInterno">Resultado de una operación internaa esta.</param>
        public ResultadoOperacion(EstadoOperacion estadoOperacion, string descripcion = null, string errCode = null, ResultadoOperacion resultadoOperacionInterno = null)
        {
            this.estadoOperacion = estadoOperacion;
            this.descripcion = descripcion;
            this.errCode = errCode;

            this.resultadoOperacionInterno = resultadoOperacionInterno;
        }

        /// <summary>
        /// Devuelve una cadena que representa el resultado de la operación.
        /// </summary>
        /// <returns>Cadena con detalles del resultado de operación.</returns>
        public override string ToString()
        {
            string mensajePredeterminado = this.estadoOperacion.mensajePredeterminado() + "\n\n";

            string detalle = "";
            string estadoOperacion = "";
            string parentesis = descripcion != null ? "(" + descripcion + ")\n" : "";
            string corchetes = "";

            switch (this.estadoOperacion)
            {
                case EstadoOperacion.Correcto:
                case EstadoOperacion.NingunResultado:
                case EstadoOperacion.ErrorCredencialesIncorrectas:
                case EstadoOperacion.ErrorDatosIncorrectos:
                case EstadoOperacion.ErrorDependenciaDeDatos:
                    break;

                case EstadoOperacion.ErrorDesconocido:
                case EstadoOperacion.ErrorAplicacion:
                case EstadoOperacion.ErrorConexionServidor:
                case EstadoOperacion.ErrorAcceso_SintaxisSQL:
                case EstadoOperacion.ErrorEnServidor:
                default:

                    detalle = "Detalle: \n";
                    estadoOperacion = "_" + this.estadoOperacion.ToString() + "_\n";
                    corchetes = errCode != null ? "[ErrCode: " + errCode + "]\n" : "";

                    break;
            }

            string resultadoOperacionInterno =
                this.resultadoOperacionInterno != null ?
                "\n+-------InnerOperation------+\n" +
                this.resultadoOperacionInterno.ToString()
                :
                "";

            return mensajePredeterminado + detalle + estadoOperacion + parentesis + corchetes + resultadoOperacionInterno;
        }
    }

    /// <summary>
    /// Enumeración que representa los posibles estados de la operación.
    /// </summary>
    public enum EstadoOperacion
    {
        // Estados vinculados con el usuario

        /// <summary>
        /// Operación se llevó a cabo de forma correta.
        /// </summary>
        Correcto,
        /// <summary>
        /// La operación no se realizó, ni se encontraron errores.
        /// </summary>
        NingunResultado,
        /// <summary>
        /// Error de credenciales.
        /// </summary>
        ErrorCredencialesIncorrectas,
        /// <summary>
        /// Datos ingresados incorrectos.
        /// </summary>
        ErrorDatosIncorrectos,
        /// <summary>
        /// Error de dependencia de datos. No se pueden modificar datos, ya que existen dependencias con ellos.
        /// </summary>
        ErrorDependenciaDeDatos,

        
        
        // Estados no vinculados con el usuario

        /// <summary>
        /// Error desconocido.
        /// </summary>
        ErrorDesconocido,
        /// <summary>
        /// Error de la aplicación.
        /// </summary>
        ErrorAplicacion,
        /// <summary>
        /// Error al intentar conectarse al servidor.
        /// </summary>
        ErrorConexionServidor,
        /// <summary>
        /// Error en sintaxis SQL.
        /// </summary>
        ErrorAcceso_SintaxisSQL,
        /// <summary>
        /// Error en el servidor, ajeno a la aplicación.
        /// </summary>
        ErrorEnServidor
    }

    /// <summary>
    /// Agrega funcionalidad a los elementos de la enumeración EstadoOperacion.
    /// </summary>
    public static class EstadoOperacionMetodosExtension
    {
        // Metodos de extension para el enum.
        /// <summary>
        /// Devuelve una cadena por defecto de cada uno de los estados de operación conocidos.
        /// </summary>
        /// <param name="estadoOperacion">Estado de operación</param>
        /// <returns>Cadena en lenguaje entendible para el usuario.</returns>
        public static string mensajePredeterminado(this EstadoOperacion estadoOperacion)
        {
            switch (estadoOperacion)
            {
                // Estados vinculados con el usuario
                case EstadoOperacion.Correcto:
                    return "Operación realizada con éxito.";

                case EstadoOperacion.NingunResultado:
                    return "Ninguna operación realizada.";

                case EstadoOperacion.ErrorCredencialesIncorrectas:
                    return "Credenciales incorrectas.";

                case EstadoOperacion.ErrorDatosIncorrectos:
                    return "Datos incorrectos.";

                case EstadoOperacion.ErrorDependenciaDeDatos:
                    return "Error al intentar procesar la operación, existen datos que dependen del elemento que trata de manipular.";

                // Estados no vinculados del usuario.
                case EstadoOperacion.ErrorDesconocido:
                    return "Error desconocido.";

                case EstadoOperacion.ErrorAplicacion:
                    return "Error de la aplicación.";

                case EstadoOperacion.ErrorConexionServidor:
                    return "Error al intentar establecer conexión con el servidor.";

                case EstadoOperacion.ErrorAcceso_SintaxisSQL:
                    return "Error de acceso o sintaxis SQL.";

                case EstadoOperacion.ErrorEnServidor:
                    return "Error en el servidor.";

                default:
                    return "Resultado no reconocido.";
            }
        }
    }
}
