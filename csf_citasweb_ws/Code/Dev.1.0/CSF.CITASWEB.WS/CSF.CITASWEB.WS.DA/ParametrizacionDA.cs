using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CSF.CITASWEB.WS.BE;
using System.Data;
using System.Configuration;

namespace CSF.CITASWEB.WS.DA
{
    public class ParametrizacionDA
    {
        private SqlDataReader varDataReader;
        
        public ConfiguracionMFABE ObtenerConfiguracionMFA(string codigoAplicacion)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@tvCodigoAplicacion", SqlDbType.VarChar, 50);
                varParametros[0].Value = codigoAplicacion;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("Sp_ConfiguracionMFAPorCodigoAplicacion_Consulta", varParametros, TipoProcesamiento.DataReader, false);

                ConfiguracionMFABE varResultado = null;
                if (varDataReader != null) 
                {
                    if (varDataReader.Read())
                    {
                        string configuracionMFA = varDataReader.GetString(varDataReader.GetOrdinal("ConfiguracionMFA"));
                        string[] aConfiguracionMFA = configuracionMFA.Split('|');
                        varResultado = new ConfiguracionMFABE()
                        {
                            codigoEmpresa = aConfiguracionMFA[0],
                            codigoAplicativo = aConfiguracionMFA[1],
                            claveAplicativo = aConfiguracionMFA[2],
                            codigoDispositivo = aConfiguracionMFA[3]
                        };
                    }
                }
                return varResultado;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }

        public List<ContenidoBE> ObtenerContenidos(string codigos)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@tvLstParametros", SqlDbType.VarChar);
                varParametros[0].Value = codigos;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("Sp_Contenido_ObtenerPorCodigos", varParametros, TipoProcesamiento.DataReader, false);

                List<ContenidoBE> varResultado = new List<ContenidoBE>();
                if (varDataReader != null)
                { 
                    ContenidoBE oContenidoBE;
                    while (varDataReader.Read())
                    {
                        oContenidoBE = new ContenidoBE();
                        oContenidoBE.codigo = varDataReader.GetString(varDataReader.GetOrdinal("Codigo")).Replace("\\n", "\n");
                        oContenidoBE.contenido = varDataReader.GetString(varDataReader.GetOrdinal("Descripcion")).Replace("\\n", "\n");
                        varResultado.Add(oContenidoBE);
                    }
                }
                return varResultado;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }

        //Por el momento se quedarán ObtenerTexto hasta finalizar todo

        public TextoBE ObtenerTexto(string codigo)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@Codigo", SqlDbType.VarChar, 50);
                varParametros[0].Value = codigo;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Texto_ObtenerPorCodigo", varParametros, TipoProcesamiento.DataReader, false);

                TextoBE varResultado = null;
                if (varDataReader.Read())
                {
                    varResultado = new TextoBE()
                    {
                        titulo = varDataReader.GetString(varDataReader.GetOrdinal("Titulo")),
                        texto = varDataReader.GetString(varDataReader.GetOrdinal("Texto")),
                        exportarPDF = varDataReader.GetString(varDataReader.GetOrdinal("ExportarPDF"))
                    };
                    if (varDataReader.NextResult() && varDataReader.HasRows)
                    {
                        varResultado.textosAdicionales = new List<TextoDetalleBE>();
                        TextoDetalleBE oTextoDetalleBE;
                        while (varDataReader.Read())
                        {
                            oTextoDetalleBE = new TextoDetalleBE();
                            oTextoDetalleBE.codigo = varDataReader.GetString(varDataReader.GetOrdinal("CodigoDetalle"));
                            oTextoDetalleBE.texto = varDataReader.GetString(varDataReader.GetOrdinal("Texto"));
                            oTextoDetalleBE.exportarPDF = varDataReader.GetString(varDataReader.GetOrdinal("ExportarPDF"));
                            varResultado.textosAdicionales.Add(oTextoDetalleBE);
                        }
                    }
                    if (varDataReader.NextResult() && varDataReader.HasRows)
                    {
                        int i, nRegistros = varResultado.textosAdicionales.Count;
                        TextoDetalleOpcionBE oTextoDetalleOpcionBE;
                        while (varDataReader.Read())
                        {
                            oTextoDetalleOpcionBE = new TextoDetalleOpcionBE();
                            oTextoDetalleOpcionBE.codigo = varDataReader.GetString(varDataReader.GetOrdinal("Codigo"));
                            oTextoDetalleOpcionBE.descripcion = varDataReader.GetString(varDataReader.GetOrdinal("Descripcion"));
                            oTextoDetalleOpcionBE.codigoDetalle = varDataReader.GetString(varDataReader.GetOrdinal("CodigoDetalle"));
                            oTextoDetalleOpcionBE.tipo = varDataReader.GetString(varDataReader.GetOrdinal("Tipo"));
                            oTextoDetalleOpcionBE.descripcionPDF = varDataReader.GetString(varDataReader.GetOrdinal("DescripcionPDF"));
                            oTextoDetalleOpcionBE.atributos = varDataReader.GetString(varDataReader.GetOrdinal("AtributosBoton"));
                            for (i = 0; i < nRegistros; i++)
                            {
                                if (varResultado.textosAdicionales[i].codigo.Equals(oTextoDetalleOpcionBE.codigoDetalle))
                                {
                                    if (varResultado.textosAdicionales[i].botones == null)
                                    {
                                        varResultado.textosAdicionales[i].botones = new List<TextoDetalleOpcionBE>();
                                    }
                                    varResultado.textosAdicionales[i].botones.Add(oTextoDetalleOpcionBE);
                                    break;
                                }
                            }
                        }
                    }
                }

                return varResultado;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }

        public HODatosBE ObtenerParametrosHospitalizacion()
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_ParametrosHospitalizacion_Listar", null, TipoProcesamiento.DataReader, false);

                HODatosBE varResultado = new HODatosBE();
                while (varDataReader.Read())
                {
                    varResultado.tiposDocumento.Add(new GenericoBE()
                    {
                        codigo = varDataReader.GetString(varDataReader.GetOrdinal("Codigo")),
                        valor = varDataReader.GetString(varDataReader.GetOrdinal("Valor"))
                    });
                }

                varDataReader.NextResult();
                while (varDataReader.Read())
                {
                    varResultado.tiposParentesco.Add(new GenericoBE()
                    {
                        codigo = varDataReader.GetString(varDataReader.GetOrdinal("Codigo")),
                        valor = varDataReader.GetString(varDataReader.GetOrdinal("Valor"))
                    });
                }

                return varResultado;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }

        public ENDatosBE ObtenerParametrosEnlaces()
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_ParametrosEnlaces_Listar", null, TipoProcesamiento.DataReader, false);
                
                ENDatosBE varResultado = new ENDatosBE();
                if (varDataReader != null)
                {
                    string urlBaseIcono = ConfigurationManager.AppSettings["rutaPublicaIcono"].ToString();
                    if (varDataReader.Read())
                    {
                        varResultado.roe = new URLBE()
                        {
                            titulo = varDataReader.GetString(varDataReader.GetOrdinal("Titulo")),
                            URL = varDataReader.GetString(varDataReader.GetOrdinal("URL")),
                            target = varDataReader.GetString(varDataReader.GetOrdinal("Target")),
                            icono = varDataReader.GetString(varDataReader.GetOrdinal("Icono"))
                        };
                        varResultado.roe.icono = !String.IsNullOrEmpty(varResultado.roe.icono) ? urlBaseIcono + varResultado.roe.icono : "";
                    }

                    varDataReader.NextResult();
                    if (varDataReader.Read())
                    {
                        varResultado.contactanos = new URLBE()
                        {
                            titulo = varDataReader.GetString(varDataReader.GetOrdinal("Titulo")),
                            URL = varDataReader.GetString(varDataReader.GetOrdinal("URL")),
                            target = varDataReader.GetString(varDataReader.GetOrdinal("Target")),
                            icono = varDataReader.GetString(varDataReader.GetOrdinal("Icono"))
                        };
                        varResultado.contactanos.icono = !String.IsNullOrEmpty(varResultado.contactanos.icono) ? urlBaseIcono + varResultado.contactanos.icono : "";
                    }
                }

                return varResultado;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }

        public ContenidoBE ObtenerContenido(string codigo)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@Codigo", SqlDbType.VarChar, 50);
                varParametros[0].Value = codigo;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("App_Proc_Contenido_ObtenerPorCodigo", varParametros, TipoProcesamiento.DataReader, false);

                ContenidoBE varResultado = null;
                if (varDataReader.Read())
                {
                    varResultado = new ContenidoBE()
                    {
                        contenido = varDataReader.GetString(varDataReader.GetOrdinal("Descripcion")).Replace("\\n", "\n")
                    };
                }

                return varResultado;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }

        public ParametroSeguridadBE ObtenerParametroSeguridad(string tipoRegistro)
        {
            ConexionUtil varConexion = new ConexionUtil();
            try
            {
                SqlParameter[] varParametros = new SqlParameter[1];
                varParametros[0] = new SqlParameter("@tvTipoRegistro", SqlDbType.VarChar, 10);
                varParametros[0].Value = tipoRegistro;

                varDataReader = (SqlDataReader)varConexion.EjecutarProcedimiento("Sp_ParametroSeguridad_ObtenerPorTipo", varParametros, TipoProcesamiento.DataReader, false);

                ParametroSeguridadBE varResultado = null;
                if (varDataReader.Read())
                {
                    string valor;
                    varResultado = new ParametroSeguridadBE();

                    valor = varDataReader.GetString(varDataReader.GetOrdinal("LongitudMinimaContrasena"));
                    if (valor != "0")
                    {
                        varResultado.longitudMinimaContrasena = new ParametroSeguridadCampoBE();
                        varResultado.longitudMinimaContrasena.valor = valor;
                        varResultado.longitudMinimaContrasena.descripcion = varDataReader.GetString(varDataReader.GetOrdinal("LongitudMinimaContrasenaDes"));
                    }

                    valor = varDataReader.GetString(varDataReader.GetOrdinal("LongitudMaximaContrasena"));
                    if (valor != "0")
                    {
                        varResultado.longitudMaximaContrasena = new ParametroSeguridadCampoBE();
                        varResultado.longitudMaximaContrasena.valor = valor;
                        varResultado.longitudMaximaContrasena.descripcion = varDataReader.GetString(varDataReader.GetOrdinal("LongitudMaximaContrasenaDes"));
                    }

                    valor = varDataReader.GetString(varDataReader.GetOrdinal("IndicadorMinusculas"));
                    if (valor == "1")
                    {
                        varResultado.indicadorMinuscula = new ParametroSeguridadCampoBE();
                        varResultado.indicadorMinuscula.valor = valor;
                        varResultado.indicadorMinuscula.descripcion = varDataReader.GetString(varDataReader.GetOrdinal("IndicadorMinusculasDes"));
                    }

                    valor = varDataReader.GetString(varDataReader.GetOrdinal("IndicadorNumeros"));
                    if (valor == "1")
                    {
                        varResultado.indicadorNumero = new ParametroSeguridadCampoBE();
                        varResultado.indicadorNumero.valor = valor;
                        varResultado.indicadorNumero.descripcion = varDataReader.GetString(varDataReader.GetOrdinal("IndicadorNumerosDes"));
                    }

                    valor = varDataReader.GetString(varDataReader.GetOrdinal("IndicadorMayusculas"));
                    if (valor == "1")
                    {
                        varResultado.indicadorMayuscula = new ParametroSeguridadCampoBE();
                        varResultado.indicadorMayuscula.valor = valor;
                        varResultado.indicadorMayuscula.descripcion = varDataReader.GetString(varDataReader.GetOrdinal("IndicadorMayusculasDes"));
                    }

                    valor = varDataReader.GetString(varDataReader.GetOrdinal("IndicadorCaracteresEspeciales"));
                    if (valor == "1")
                    {
                        varResultado.indicadorCaracterEspecial = new ParametroSeguridadCampoBE();
                        varResultado.indicadorCaracterEspecial.valor = valor;
                        varResultado.indicadorCaracterEspecial.descripcion = varDataReader.GetString(varDataReader.GetOrdinal("IndicadorCaracteresEspecialesDes"));
                    }
                }

                return varResultado;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                varConexion.Desconectar();
            }
        }
    }

}
