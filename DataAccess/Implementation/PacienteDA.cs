using CommonMethods;
using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Implementation
{
    public class PacienteDA:IPacienteDA
    {
        private readonly string _connection;
        public readonly Exceptions _exceptions;

        public PacienteDA(IConfiguration configuration, Exceptions exceptions)
        {
            _connection = configuration.GetConnectionString("OracleDb");
            _exceptions = exceptions;
        }

        // =======================================================
        // MÉTODOS PRINCIPALES
        // =======================================================

        public List<Expediente> GetRecordByName(string primerNom, string segundoNom, string primerAp, string segundoAp)
        {
            var lista = new List<Expediente>();

            // Construir el parámetro de búsqueda igual que en VB
            var partes = new List<string>();
            if (!string.IsNullOrEmpty(primerAp)) partes.Add(primerAp);
            if (!string.IsNullOrEmpty(segundoAp)) partes.Add(segundoAp);
            if (!string.IsNullOrEmpty(primerNom)) partes.Add(primerNom);
            if (!string.IsNullOrEmpty(segundoNom)) partes.Add(segundoNom);
            string nomPaciente = "%" + string.Join("% ", partes).ToUpper() + "%";

            try
            {
                using (var connection = new OracleConnection(_connection))
                using (var cmd = new OracleCommand("AGENDA.EXPEDIENTE.exp_consultar_x_nombre", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    cmd.Parameters.Add("pnompaciente", OracleDbType.Varchar2, nomPaciente, ParameterDirection.Input);

                    // Cursor de salida
                    cmd.Parameters.Add("pdatos", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var record = new Expediente
                            {
                                NumExpediente = reader.GetInt32(reader.GetOrdinal("NUM_EXPEDIENTE")),
                                NumCarnet = reader["NUM_CARNET"]?.ToString(),
                                PrimerAp = reader["PRIMER_AP"]?.ToString(),
                                SegundoAp = reader["SEGUNDO_AP"]?.ToString(),
                                PrimerNom = reader["PRIMER_NOM"]?.ToString(),
                                SegundoNom = reader["SEGUNDO_NOM"]?.ToString(),
                                Sexo = reader["SEXO"]?.ToString(),
                                CodTipDoc = reader["COD_TIPDOC"]?.ToString(),
                                NumId = reader["NUM_ID"]?.ToString(),
                                CodClase = reader["COD_CLASE"]?.ToString(),
                                FecNacimiento = reader["FEC_NACIMIENTO"] == DBNull.Value ? null : (DateTime?)reader["FEC_NACIMIENTO"],
                                TelHab = reader["TEL_HAB"] == DBNull.Value ? null : (int?)reader["TEL_HAB"],
                                TelOfic = reader["TEL_OFIC"] == DBNull.Value ? null : (int?)reader["TEL_OFIC"],
                                TelOpc = reader["TEL_OPC"] == DBNull.Value ? null : (int?)reader["TEL_OPC"],
                                Apartado = reader["APARTADO"]?.ToString(),
                                DireccionHab = reader["DIRECCION_HAB"]?.ToString(),
                                CodProvincia = reader["COD_PROVINCIA"]?.ToString(),
                                CodCanton = reader["COD_CANTON"]?.ToString(),
                                CodDistrito = reader["COD_DISTRITO"]?.ToString(),
                                CodBarrio = reader["COD_BARRIO"]?.ToString(),
                                FecExpediente = reader["FEC_EXPEDIENTE"] == DBNull.Value ? null : (DateTime?)reader["FEC_EXPEDIENTE"],
                                EstadoExp = reader["ESTADO_EXP"]?.ToString(),
                                Medico = reader["MEDICO"]?.ToString(),
                                CodMedico = reader["COD_MEDICO"]?.ToString(),
                                Observ = reader["OBSERV"]?.ToString(),
                                NomPaciente = reader["NOM_PACIENTE"]?.ToString(),
                                Usuario = reader["USUARIO"]?.ToString(),
                                NumVisitas = reader["NUM_VISITAS"] == DBNull.Value ? null : (int?)reader["NUM_VISITAS"],
                                CorreoElectronico = reader["CORREO_ELECTRONICO"]?.ToString(),
                                IndDonador = reader["IND_DONADOR"]?.ToString(),
                                NumDonador = reader["NUM_DONADOR"] == DBNull.Value ? null : (int?)reader["NUM_DONADOR"],
                                UserActualiza = reader["USER_ACTUALIZA"]?.ToString(),
                                FecActualiza = reader["FEC_ACTUALIZA"] == DBNull.Value ? null : (DateTime?)reader["FEC_ACTUALIZA"],
                                DscCampoActualizado = reader["DSC_CAMPO_ACTUALIZADO"]?.ToString(),
                                CodGrupo = reader["COD_GRUPO"]?.ToString(),
                                CodSigno = reader["COD_SIGNO"]?.ToString(),
                                IndFallecido = reader["IND_FALLECIDO"]?.ToString(),
                                CodTipoTricare = reader["COD_TIPO_TRICARE"]?.ToString(),
                                SaldoDeducTricare = reader["SALDO_DEDUC_TRICARE"] == DBNull.Value ? null : (decimal?)reader["SALDO_DEDUC_TRICARE"],
                                MonConsumidoTricare = reader["MON_CONSUMIDO_TRICARE"] == DBNull.Value ? null : (decimal?)reader["MON_CONSUMIDO_TRICARE"],
                                CodIdioma = reader["COD_IDIOMA"]?.ToString(),
                                CodEtnia = reader["COD_ETNIA"]?.ToString(),
                                CodReligion = reader["COD_RELIGION"]?.ToString(),
                                NumIdAnterior = reader["NUM_ID_ANTERIOR"]?.ToString(),
                                CodProfesion = reader["COD_PROFESION"]?.ToString(),
                                IndConsEnvioInfo = reader["IND_CONS_ENVIOINFO"]?.ToString(),
                                CodPaisNac = reader["COD_PAIS_NAC"]?.ToString(),
                                CodCategoriaACSocial = reader["COD_CATEGORIA_ACSOCIAL"]?.ToString(),
                                TelCelular = reader["TEL_CELULAR"] == DBNull.Value ? null : (int?)reader["TEL_CELULAR"],
                                IndOrigen = reader["IND_ORIGEN"]?.ToString(),
                                IndVivePais = reader["IND_VIVEPAIS"] != DBNull.Value && Convert.ToInt32(reader["IND_VIVEPAIS"]) == 1,
                                CodPaisVive = reader["COD_PAISVIVE"]?.ToString(),
                                CodEstadoPaisVive = reader["COD_ESTADO_PAISVIVE"]?.ToString(),
                                IndPacienteConvenio = reader["IND_PACIENTE_CONVENIO"] != DBNull.Value && Convert.ToInt32(reader["IND_PACIENTE_CONVENIO"]) == 1,
                                CodConvenio = reader["COD_CONVENIO"]?.ToString(),
                                CorreoElectronicoFE = reader["CORREO_ELECTRONICO_FE"]?.ToString()
                            };

                            lista.Add(record);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
                throw;
            }

            return lista;
        }

        public List<Expediente> GetRecordByIdentification(string pidentificacion, string pcod_tipdoc)
        {
            var lista = new List<Expediente>();

            try
            {
                using (var connection = new OracleConnection(_connection))
                using (var cmd = new OracleCommand("AGENDA.EXPEDIENTE.exp_consultar_x_identificacion", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    cmd.Parameters.Add("pidentificacion", OracleDbType.Varchar2, pidentificacion, ParameterDirection.Input);
                    cmd.Parameters.Add("pcod_tipdoc", OracleDbType.Varchar2, pcod_tipdoc, ParameterDirection.Input);

                    // Cursor de salida
                    cmd.Parameters.Add("pdatos", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var record = new Expediente
                            {
                                NumExpediente = reader.GetInt32(reader.GetOrdinal("NUM_EXPEDIENTE")),
                                NumCarnet = reader["NUM_CARNET"]?.ToString(),
                                PrimerAp = reader["PRIMER_AP"]?.ToString(),
                                SegundoAp = reader["SEGUNDO_AP"]?.ToString(),
                                PrimerNom = reader["PRIMER_NOM"]?.ToString(),
                                SegundoNom = reader["SEGUNDO_NOM"]?.ToString(),
                                Sexo = reader["SEXO"]?.ToString(),
                                CodTipDoc = reader["COD_TIPDOC"]?.ToString(),
                                NumId = reader["NUM_ID"]?.ToString(),
                                CodClase = reader["COD_CLASE"]?.ToString(),
                                FecNacimiento = reader["FEC_NACIMIENTO"] == DBNull.Value ? null : (DateTime?)reader["FEC_NACIMIENTO"],
                                TelHab = reader["TEL_HAB"] == DBNull.Value ? null : (int?)reader["TEL_HAB"],
                                TelOfic = reader["TEL_OFIC"] == DBNull.Value ? null : (int?)reader["TEL_OFIC"],
                                TelOpc = reader["TEL_OPC"] == DBNull.Value ? null : (int?)reader["TEL_OPC"],
                                Apartado = reader["APARTADO"]?.ToString(),
                                DireccionHab = reader["DIRECCION_HAB"]?.ToString(),
                                CodProvincia = reader["COD_PROVINCIA"]?.ToString(),
                                CodCanton = reader["COD_CANTON"]?.ToString(),
                                CodDistrito = reader["COD_DISTRITO"]?.ToString(),
                                CodBarrio = reader["COD_BARRIO"]?.ToString(),
                                FecExpediente = reader["FEC_EXPEDIENTE"] == DBNull.Value ? null : (DateTime?)reader["FEC_EXPEDIENTE"],
                                EstadoExp = reader["ESTADO_EXP"]?.ToString(),
                                Medico = reader["MEDICO"]?.ToString(),
                                CodMedico = reader["COD_MEDICO"]?.ToString(),
                                Observ = reader["OBSERV"]?.ToString(),
                                NomPaciente = reader["NOM_PACIENTE"]?.ToString(),
                                Usuario = reader["USUARIO"]?.ToString(),
                                NumVisitas = reader["NUM_VISITAS"] == DBNull.Value ? null : (int?)reader["NUM_VISITAS"],
                                CorreoElectronico = reader["CORREO_ELECTRONICO"]?.ToString(),
                                IndDonador = reader["IND_DONADOR"]?.ToString(),
                                NumDonador = reader["NUM_DONADOR"] == DBNull.Value ? null : (int?)reader["NUM_DONADOR"],
                                UserActualiza = reader["USER_ACTUALIZA"]?.ToString(),
                                FecActualiza = reader["FEC_ACTUALIZA"] == DBNull.Value ? null : (DateTime?)reader["FEC_ACTUALIZA"],
                                DscCampoActualizado = reader["DSC_CAMPO_ACTUALIZADO"]?.ToString(),
                                CodGrupo = reader["COD_GRUPO"]?.ToString(),
                                CodSigno = reader["COD_SIGNO"]?.ToString(),
                                IndFallecido = reader["IND_FALLECIDO"]?.ToString(),
                                CodTipoTricare = reader["COD_TIPO_TRICARE"]?.ToString(),
                                SaldoDeducTricare = reader["SALDO_DEDUC_TRICARE"] == DBNull.Value ? null : (decimal?)reader["SALDO_DEDUC_TRICARE"],
                                MonConsumidoTricare = reader["MON_CONSUMIDO_TRICARE"] == DBNull.Value ? null : (decimal?)reader["MON_CONSUMIDO_TRICARE"],
                                CodIdioma = reader["COD_IDIOMA"]?.ToString(),
                                CodEtnia = reader["COD_ETNIA"]?.ToString(),
                                CodReligion = reader["COD_RELIGION"]?.ToString(),
                                NumIdAnterior = reader["NUM_ID_ANTERIOR"]?.ToString(),
                                CodProfesion = reader["COD_PROFESION"]?.ToString(),
                                IndConsEnvioInfo = reader["IND_CONS_ENVIOINFO"]?.ToString(),
                                CodPaisNac = reader["COD_PAIS_NAC"]?.ToString(),
                                CodCategoriaACSocial = reader["COD_CATEGORIA_ACSOCIAL"]?.ToString(),
                                TelCelular = reader["TEL_CELULAR"] == DBNull.Value ? null : (int?)reader["TEL_CELULAR"],
                                IndOrigen = reader["IND_ORIGEN"]?.ToString(),
                                IndVivePais = reader["IND_VIVEPAIS"] != DBNull.Value && Convert.ToInt32(reader["IND_VIVEPAIS"]) == 1,
                                CodPaisVive = reader["COD_PAISVIVE"]?.ToString(),
                                CodEstadoPaisVive = reader["COD_ESTADO_PAISVIVE"]?.ToString(),
                                IndPacienteConvenio = reader["IND_PACIENTE_CONVENIO"] != DBNull.Value && Convert.ToInt32(reader["IND_PACIENTE_CONVENIO"]) == 1,
                                CodConvenio = reader["COD_CONVENIO"]?.ToString(),
                                CorreoElectronicoFE = reader["CORREO_ELECTRONICO_FE"]?.ToString()
                            };

                            lista.Add(record);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
                throw;
            }

            return lista;
        }

        public List<string> InsertNewRecord(Expediente pExpediente)
        {
            var outputs = new List<string>();

            try
            {
                using (var scope = new TransactionScope())
                using (var connection = new OracleConnection(_connection))
                using (var cmd = new OracleCommand("AGENDA.EXPEDIENTE.insertar", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    cmd.Parameters.Add("pprimer_ap", OracleDbType.Varchar2).Value = pExpediente.PrimerAp;
                    cmd.Parameters.Add("psegundo_ap", OracleDbType.Varchar2).Value = pExpediente.SegundoAp;
                    cmd.Parameters.Add("pprimer_nom", OracleDbType.Varchar2).Value = pExpediente.PrimerNom;
                    cmd.Parameters.Add("psegundo_nom", OracleDbType.Varchar2).Value = pExpediente.SegundoNom;
                    cmd.Parameters.Add("psexo", OracleDbType.Varchar2).Value = pExpediente.Sexo;
                    cmd.Parameters.Add("pcod_tipdoc", OracleDbType.Varchar2).Value = pExpediente.CodTipDoc;
                    cmd.Parameters.Add("pnum_id", OracleDbType.Varchar2).Value = pExpediente.NumId;
                    cmd.Parameters.Add("pcod_clase", OracleDbType.Varchar2).Value = pExpediente.CodClase;
                    cmd.Parameters.Add("pfec_nacimiento", OracleDbType.Date).Value = (object?)pExpediente.FecNacimiento ?? DBNull.Value;
                    cmd.Parameters.Add("ptel_hab", OracleDbType.Varchar2).Value = pExpediente.TelHab?.ToString() ?? "";
                    cmd.Parameters.Add("ptel_ofic", OracleDbType.Varchar2).Value = pExpediente.TelOfic?.ToString() ?? "";
                    cmd.Parameters.Add("ptel_opc", OracleDbType.Varchar2).Value = pExpediente.TelOpc?.ToString() ?? "";
                    cmd.Parameters.Add("pdireccion_hab", OracleDbType.Varchar2).Value = pExpediente.DireccionHab;
                    cmd.Parameters.Add("pcod_provincia", OracleDbType.Varchar2).Value = pExpediente.CodProvincia;
                    cmd.Parameters.Add("pcod_canton", OracleDbType.Varchar2).Value = pExpediente.CodCanton;
                    cmd.Parameters.Add("pcod_distrito", OracleDbType.Varchar2).Value = pExpediente.CodDistrito;
                    cmd.Parameters.Add("pcod_barrio", OracleDbType.Varchar2).Value = pExpediente.CodBarrio;
                    cmd.Parameters.Add("pfec_expediente", OracleDbType.Date).Value = (object?)pExpediente.FecExpediente ?? DBNull.Value;
                    cmd.Parameters.Add("pestado_exp", OracleDbType.Varchar2).Value = pExpediente.EstadoExp;
                    cmd.Parameters.Add("pobserv", OracleDbType.Varchar2).Value = pExpediente.Observ;
                    cmd.Parameters.Add("pnom_paciente", OracleDbType.Varchar2).Value = pExpediente.NomPaciente;
                    cmd.Parameters.Add("pusuario", OracleDbType.Varchar2).Value = pExpediente.Usuario;
                    cmd.Parameters.Add("pcorreo_electronico", OracleDbType.Varchar2).Value = pExpediente.CorreoElectronico;
                    cmd.Parameters.Add("pind_fallecido", OracleDbType.Varchar2).Value = pExpediente.IndFallecido;
                    cmd.Parameters.Add("pcod_idioma", OracleDbType.Varchar2).Value = pExpediente.CodIdioma;
                    cmd.Parameters.Add("pcod_etnia", OracleDbType.Varchar2).Value = pExpediente.CodEtnia;
                    cmd.Parameters.Add("pcod_religion", OracleDbType.Varchar2).Value = pExpediente.CodReligion;
                    cmd.Parameters.Add("pcod_profesion", OracleDbType.Varchar2).Value = pExpediente.CodProfesion;
                    cmd.Parameters.Add("pind_cons_envioinfo", OracleDbType.Varchar2).Value = pExpediente.IndConsEnvioInfo;
                    cmd.Parameters.Add("pcod_pais_nac", OracleDbType.Varchar2).Value = pExpediente.CodPaisNac;
                    cmd.Parameters.Add("ptel_celular", OracleDbType.Varchar2).Value = pExpediente.TelCelular?.ToString() ?? "";
                    cmd.Parameters.Add("pind_origen", OracleDbType.Varchar2).Value = "AGENDA";
                    cmd.Parameters.Add("pind_vivepais", OracleDbType.Int32).Value = pExpediente.IndVivePais ? 1 : 0;
                    cmd.Parameters.Add("pcod_paisvive", OracleDbType.Varchar2).Value = pExpediente.CodPaisVive;
                    cmd.Parameters.Add("pcod_estado_paisvive", OracleDbType.Varchar2).Value = pExpediente.CodEstadoPaisVive;
                    cmd.Parameters.Add("pind_paciente_convenio", OracleDbType.Int32).Value = pExpediente.IndPacienteConvenio ? 1 : 0;
                    cmd.Parameters.Add("pcod_convenio", OracleDbType.Varchar2).Value = pExpediente.CodConvenio;
                    cmd.Parameters.Add("pcorreo_fe", OracleDbType.Varchar2).Value = pExpediente.CorreoElectronicoFE;

                    // Parámetros de salida
                    var pNumExpediente = new OracleParameter("pnum_expediente", OracleDbType.Int32, ParameterDirection.Output);
                    var pCodError = new OracleParameter("pcoderror", OracleDbType.Int32, ParameterDirection.Output);
                    var pDesError = new OracleParameter("pdeserror", OracleDbType.Varchar2, 4000)
                    {
                        Direction = ParameterDirection.Output
                    };

                    cmd.Parameters.Add(pNumExpediente);
                    cmd.Parameters.Add(pCodError);
                    cmd.Parameters.Add(pDesError);

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    // Leer salidas
                    outputs.Add(pCodError.Value?.ToString() ?? "");
                    outputs.Add(pDesError.Value?.ToString() ?? "");
                    outputs.Add(pNumExpediente.Value?.ToString() ?? "");

                    // Si no hubo error, confirmar transacción
                    if (outputs[0] == "0")
                        scope.Complete();
                }
            }
            catch (Exception ex)
            {
                _exceptions.LogError(ex);
                throw;
            }

            return outputs;
        }
    }
}
