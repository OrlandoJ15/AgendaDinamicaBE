using DataAccess.Interfaces;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DataAccess.Implementation
{
    public class CitaDA : ICitaDA
    {
        private readonly string _connection;

        public CitaDA(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("OracleDb");
        }

        public async Task<List<AppointmentSlot>> ConsultarKeyAsync(int numAgenda, DateTime fecInicial, DateTime fecFinal)
        {
            var result = new List<AppointmentSlot>();

            await using var conn = new OracleConnection(_connection);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand("AGENDA.CITAINSERTAR.consultar_key", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Parámetros de entrada
            cmd.Parameters.Add("pnumagenda", OracleDbType.Int32).Value = numAgenda;
            cmd.Parameters.Add("pfecha_inicial", OracleDbType.Date).Value = fecInicial;
            cmd.Parameters.Add("pfecha_final", OracleDbType.Date).Value = fecFinal;

            // Cursor de salida
            var cursor = new OracleParameter("pdatos", OracleDbType.RefCursor)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(cursor);

            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var cita = new AppointmentSlot
                {
                    NumAgenda = reader["NUM_AGENDA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NUM_AGENDA"]),
                    FecHoraInicial = reader["FEC_HORAINICIAL"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FEC_HORAINICIAL"]),
                    FecHoraFinal = reader["FEC_HORAFINAL"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FEC_HORAFINAL"])
                };

                result.Add(cita);
            }

            return result;
        }

        public async Task<List<AgendaHorario>> ConsultarPorFechaAsync(int numAgenda, DateTime fechaInicial, DateTime fechaFinal)
        {
            var result = new List<AgendaHorario>();

            await using var conn = new OracleConnection(_connection);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand("AGENDA.AGENDAHORARIO.consultar_por_fechas", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Parámetros
            cmd.Parameters.Add("pnum_agenda", OracleDbType.Int32).Value = numAgenda;
            cmd.Parameters.Add("pfecha_inicial", OracleDbType.Varchar2).Value = fechaInicial.ToString("dd/MM/yyyy");
            cmd.Parameters.Add("pfecha_final", OracleDbType.Varchar2).Value = fechaFinal.ToString("dd/MM/yyyy");

            var cursor = new OracleParameter("pdatos", OracleDbType.RefCursor)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(cursor);

            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var horario = new AgendaHorario
                {
                    ID_AGENDAHORARIO_DETALLE = reader["ID_AGENDAHORARIO_DETALLE"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["ID_AGENDAHORARIO_DETALLE"]),
                    NUM_AGENDA = reader["NUM_AGENDA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NUM_AGENDA"]),
                    NUM_DIA = reader["NUM_DIA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NUM_DIA"]),
                    NUM_HORAINICIO = reader["NUM_HORAINICIO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NUM_HORAINICIO"]),
                    NUM_HORAFINAL = reader["NUM_HORAFINAL"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NUM_HORAFINAL"]),
                    NUM_INTERVALOCITA = reader["NUM_INTERVALOCITA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NUM_INTERVALOCITA"]),
                    DESCRIPCION = reader["DESCRIPCION"]?.ToString() ?? string.Empty,
                    IND_LIBRE = reader["IND_LIBRE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IND_LIBRE"]),
                    USR_REGISTRA = reader["USR_REGISTRA"]?.ToString() ?? string.Empty,
                    FEC_REGISTRA = reader["FEC_REGISTRA"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FEC_REGISTRA"]),
                    USR_ACTUALIZA = reader["USR_ACTUALIZA"]?.ToString() ?? string.Empty,
                    FEC_ACTUALIZA = reader["FEC_ACTUALIZA"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FEC_ACTUALIZA"]),
                    NUM_CITAS = reader["NUM_CITAS"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NUM_CITAS"]),
                    NUM_FORMATOHORAINICIO = reader["NUM_FORMATOHORAINICIO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["NUM_FORMATOHORAINICIO"]),
                    NUM_FORMATOHORAFINAL = reader["NUM_FORMATOHORAFINAL"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["NUM_FORMATOHORAFINAL"]),
                    NOM_DIA = reader["NOM_DIA"]?.ToString() ?? string.Empty
                };

                result.Add(horario);
            }

            return result;
        }

        public async Task<List<AgendaHorarioDetalleReceso>> ConsultarRecesosAsync(decimal idAgendaHorarioDetalle)
        {
            var result = new List<AgendaHorarioDetalleReceso>();

            await using var conn = new OracleConnection(_connection);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand("AGENDA.AGENDAHORARIO.consultar_recesos", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Parámetro de entrada
            cmd.Parameters.Add("pid_agendahorario_detalle", OracleDbType.Decimal).Value = idAgendaHorarioDetalle;

            // Cursor de salida
            var cursor = new OracleParameter("pdatos", OracleDbType.RefCursor)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(cursor);

            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var receso = new AgendaHorarioDetalleReceso
                {
                    ID_AGENDAHORARIO_DETALLERECESO = reader["ID_AGENDAHORARIO_DETALLERECESO"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["ID_AGENDAHORARIO_DETALLERECESO"]),
                    ID_AGENDAHORARIO_DETALLE = reader["ID_AGENDAHORARIO_DETALLE"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["ID_AGENDAHORARIO_DETALLE"]),
                    DESCRIPCION = reader["DESCRIPCION"]?.ToString() ?? string.Empty,
                    HORA_INICIO = reader["HORA_INICIO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["HORA_INICIO"]),
                    HORA_FINAL = reader["HORA_FINAL"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["HORA_FINAL"])
                };

                result.Add(receso);
            }

            return result;
        }

        public async Task<Cita?> ConsultarKeyAsync(int numCita)
        {
            await using var conn = new OracleConnection(_connection);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand("AGENDA.CITA.consultar_key", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Parámetros del SP
            cmd.Parameters.Add("pnum_cita", OracleDbType.Int32).Value = numCita;
            var cursor = new OracleParameter("pdatos", OracleDbType.RefCursor)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(cursor);

            await using var reader = await cmd.ExecuteReaderAsync();

            Cita? cita = null;

            if (await reader.ReadAsync())
            {
                cita = new Cita
                {
                    NUM_CITA = reader["NUM_CITA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NUM_CITA"]),
                    NUM_AGENDA = reader["NUM_AGENDA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NUM_AGENDA"]),
                    FEC_HORAINICIAL = reader["FEC_HORAINICIAL"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FEC_HORAINICIAL"]),
                    FEC_HORAFINAL = reader["FEC_HORAFINAL"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FEC_HORAFINAL"]),
                    NUM_TIPOCITA = reader["NUM_TIPOCITA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NUM_TIPOCITA"]),
                    IND_PACIENTERECARGO = reader["IND_PACIENTERECARGO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IND_PACIENTERECARGO"]),
                    NUM_AGENDACOMPARTIDA = reader["NUM_AGENDACOMPARTIDA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NUM_AGENDACOMPARTIDA"]),
                    NUM_EXPEDIENTE = reader["NUM_EXPEDIENTE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NUM_EXPEDIENTE"]),
                    NUM_ORDSERV = reader["NUM_ORDSERV"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NUM_ORDSERV"]),
                    NOM_PACIENTE = reader["NOM_PACIENTE"]?.ToString() ?? string.Empty,
                    FEC_REGISTRA = reader["FEC_REGISTRA"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FEC_REGISTRA"]),
                    USR_REGISTRA = reader["USR_REGISTRA"]?.ToString() ?? string.Empty,
                    IND_ASISTENCIA = reader["IND_ASISTENCIA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IND_ASISTENCIA"]),
                    FEC_ACTUALIZA = reader["FEC_ACTUALIZA"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FEC_ACTUALIZA"]),
                    USR_ACTUALIZA = reader["USR_ACTUALIZA"]?.ToString() ?? string.Empty,
                    DES_RECARGO = reader["DES_RECARGO"]?.ToString() ?? string.Empty,
                    DES_OBSERVACION = reader["DES_OBSERVACION"]?.ToString() ?? string.Empty,
                    IND_REFERENCIAMEDICA = reader["IND_REFERENCIAMEDICA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IND_REFERENCIAMEDICA"]),
                    COD_PROFREFIERE = reader["COD_PROFREFIERE"]?.ToString() ?? string.Empty,
                    DES_REFERENCIA = reader["DES_REFERENCIA"]?.ToString() ?? string.Empty,
                    NUM_CITA_MADRE = reader["NUM_CITA_MADRE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NUM_CITA_MADRE"]),
                    IND_CONFIRMACION = reader["IND_CONFIRMACION"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IND_CONFIRMACION"]),
                    FEC_CONFIRMACION = reader["FEC_CONFIRMACION"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FEC_CONFIRMACION"]),
                    DES_CONFIRMACION = reader["DES_CONFIRMACION"]?.ToString() ?? string.Empty,
                    USR_CONFIRMACION = reader["USR_CONFIRMACION"]?.ToString() ?? string.Empty,
                    HORA_PRESENTARSE = reader["HORA_PRESENTARSE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["HORA_PRESENTARSE"]),
                    DES_TIPOPROC = reader["DES_TIPOPROC"]?.ToString() ?? string.Empty,
                    NUM_TIPOPROC = reader["NUM_TIPOPROC"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NUM_TIPOPROC"]),
                    IND_APLICASEGURO = reader["IND_APLICASEGURO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IND_APLICASEGURO"]),
                    IND_COPAGO = reader["IND_COPAGO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IND_COPAGO"]),
                    IND_DEDUCIBLE = reader["IND_DEDUCIBLE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IND_DEDUCIBLE"]),
                    IND_COBERTURATOTAL = reader["IND_COBERTURATOTAL"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IND_COBERTURATOTAL"]),
                    OBSERVACIONES_SEGURO = reader["OBSERVACIONES_SEGURO"]?.ToString() ?? string.Empty,
                    COD_INSTITUC = reader["COD_INSTITUC"]?.ToString() ?? string.Empty,
                    IND_CITASECUNDARIA = reader["IND_CITASECUNDARIA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IND_CITASECUNDARIA"])
                };
            }

            return cita;
        }

        public async Task<List<CitasCalendario>> ConsultarExisteEnRangoAsync(int numAgenda,DateTime fecInicial,DateTime fecFinal,int numTipoAgenda,int numIntervalo)
        {
            var result = new List<CitasCalendario>();

            await using var conn = new OracleConnection(_connection);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand("AGENDA.CITA.consultar_existe_en_rango", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Parámetros de entrada
            cmd.Parameters.Add("pnum_agenda", OracleDbType.Int32).Value = numAgenda;
            cmd.Parameters.Add("pfecha_incial", OracleDbType.Date).Value = fecInicial;
            cmd.Parameters.Add("pfecha_final", OracleDbType.Date).Value = fecFinal;
            cmd.Parameters.Add("pnum_tipoAgenda", OracleDbType.Decimal).Value = numTipoAgenda;
            cmd.Parameters.Add("pnum_intervalo", OracleDbType.Decimal).Value = numIntervalo;

            // Cursor de salida
            var cursor = new OracleParameter("pdatos", OracleDbType.RefCursor)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(cursor);

            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var cita = new CitasCalendario
                {
                    NUM_AGENDA = reader["NUM_AGENDA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NUM_AGENDA"]),
                    FEC_HORAINICIAL = reader["FEC_HORAINICIAL"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FEC_HORAINICIAL"]),
                    FEC_HORAFINAL = reader["FEC_HORAFINAL"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FEC_HORAFINAL"]),
                    NUM_CITA = reader["NUM_TIPOCITA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NUM_TIPOCITA"]),
                    NOM_PACIENTE = reader["NOM_PACIENTE"]?.ToString() ?? string.Empty,
                    IND_ASISTENCIA = reader["IND_ASISTENCIA"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IND_ASISTENCIA"]),
                    DES_OBSERVACION = reader["DES_OBSERVACION"]?.ToString() ?? string.Empty
                };

                result.Add(cita);
            }

            return result;
        }

        public async Task<List<string>> InsertarBitacoraAsync(CitaBitacoraInsertar bitacora)
        {
            var result = new List<string>();

            await using var conn = new OracleConnection(_connection);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand("AGENDA.CITABITACORA.insertar", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Parámetros de entrada
            cmd.Parameters.Add("pnum_cita", OracleDbType.Int32).Value = bitacora.NUM_CITA;
            cmd.Parameters.Add("pfec_horainicial", OracleDbType.Date).Value = bitacora.FEC_HORAINICIAL;
            cmd.Parameters.Add("pfec_horafinal", OracleDbType.Date).Value = bitacora.FEC_HORAFINAL;
            cmd.Parameters.Add("pind_operacion", OracleDbType.Varchar2, 1).Value = bitacora.IND_OPERACION ?? string.Empty;
            cmd.Parameters.Add("pusr_registra", OracleDbType.Varchar2, 50).Value = bitacora.USR_REGISTRA ?? string.Empty;
            cmd.Parameters.Add("pdes_datoantes", OracleDbType.Clob).Value = bitacora.DES_DATOANTES ?? string.Empty;
            cmd.Parameters.Add("pdes_datodespues", OracleDbType.Clob).Value = bitacora.DES_DATODESPUES ?? string.Empty;
            cmd.Parameters.Add("pnum_agenda", OracleDbType.Int32).Value = bitacora.NUM_AGENDA;

            // Parámetros de salida
            var codError = new OracleParameter("pcoderror", OracleDbType.Int32)
            {
                Direction = ParameterDirection.Output
            };
            var desError = new OracleParameter("pdeserror", OracleDbType.Varchar2, 4000)
            {
                Direction = ParameterDirection.Output
            };

            cmd.Parameters.Add(codError);
            cmd.Parameters.Add(desError);

            // Ejecutar SP
            await cmd.ExecuteNonQueryAsync();

            // Recuperar resultados
            result.Add(codError.Value?.ToString() ?? "0");
            result.Add(desError.Value?.ToString() ?? string.Empty);

            return result;
        }

        public async Task<List<string>> InsertarCitaPacienteAsync(Cita cita, List<CitaProcedimiento> servicios, string bitacoraDatosDespues)
        {
            var result = new List<string>();
            int newNumCita = 0;

            await using var conn = new OracleConnection(_connection);
            await conn.OpenAsync();

            // Validar si existe cita en ese horario (AGENDA.CITAINSERTAR.consultar_key)
            var available = await ConsultarKeyAsync(cita.NUM_AGENDA, cita.FEC_HORAINICIAL, cita.FEC_HORAFINAL);
            if (available.Any()) {
                result.Add("1");
                result.Add("No es posible crear una cita para este horario");
                return result;
            }
            

            // Insertar cita (AGENDA.CITAINSERTAR.Insertar)
            await using (var cmdInsertar = new OracleCommand("AGENDA.CITAINSERTAR.Insertar", conn))
            {
                cmdInsertar.CommandType = CommandType.StoredProcedure;

                cmdInsertar.Parameters.Add("pnum_agenda", OracleDbType.Int32).Value = cita.NUM_AGENDA;
                cmdInsertar.Parameters.Add("pfec_horainicial", OracleDbType.Date).Value = cita.FEC_HORAINICIAL;
                cmdInsertar.Parameters.Add("pfec_horafinal", OracleDbType.Date).Value = cita.FEC_HORAFINAL;

                var codError = new OracleParameter("pcoderror", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };
                var desError = new OracleParameter("pdeserror", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };
                cmdInsertar.Parameters.Add(codError);
                cmdInsertar.Parameters.Add(desError);

                await cmdInsertar.ExecuteNonQueryAsync();

                if (codError.Value?.ToString() != "0")
                {
                    result.Add(codError.Value?.ToString() ?? "1");
                    result.Add(desError.Value?.ToString() ?? "Error al insertar la cita");
                    return result;
                }
            }


            // Insertar la cita (AGENDA.CITA.insertar)
            await using (var cmdInsertarCita = new OracleCommand("AGENDA.CITA.insertar", conn))
            {
                cmdInsertarCita.CommandType = CommandType.StoredProcedure;

                cmdInsertarCita.Parameters.Add("pnum_agenda", OracleDbType.Int32).Value = cita.NUM_AGENDA;
                cmdInsertarCita.Parameters.Add("pfec_horainicial", OracleDbType.Date).Value = cita.FEC_HORAINICIAL;
                cmdInsertarCita.Parameters.Add("pfec_horafinal", OracleDbType.Date).Value = cita.FEC_HORAFINAL;
                cmdInsertarCita.Parameters.Add("pnum_tipocita", OracleDbType.Int32).Value = cita.NUM_TIPOCITA;
                cmdInsertarCita.Parameters.Add("pind_pacienterecargo", OracleDbType.Int32).Value = cita.IND_PACIENTERECARGO;
                cmdInsertarCita.Parameters.Add("pnum_expediente", OracleDbType.Int32).Value = cita.NUM_EXPEDIENTE;
                cmdInsertarCita.Parameters.Add("pnum_ordserv", OracleDbType.Int32).Value = cita.NUM_ORDSERV;
                cmdInsertarCita.Parameters.Add("pnom_paciente", OracleDbType.Varchar2).Value = cita.NOM_PACIENTE;
                cmdInsertarCita.Parameters.Add("pusr_registra", OracleDbType.Varchar2).Value = cita.USR_REGISTRA;
                cmdInsertarCita.Parameters.Add("pdes_recargo", OracleDbType.Varchar2).Value = cita.DES_RECARGO;
                cmdInsertarCita.Parameters.Add("pdes_observacion", OracleDbType.Varchar2).Value = cita.DES_OBSERVACION;
                cmdInsertarCita.Parameters.Add("pind_referenciamedica", OracleDbType.Int32).Value = cita.IND_REFERENCIAMEDICA;
                cmdInsertarCita.Parameters.Add("pcod_profrefiere", OracleDbType.Varchar2).Value = cita.COD_PROFREFIERE;
                cmdInsertarCita.Parameters.Add("pdes_referencia", OracleDbType.Varchar2).Value = cita.DES_REFERENCIA;
                cmdInsertarCita.Parameters.Add("pnum_cita_madre", OracleDbType.Int32).Value = cita.NUM_CITA_MADRE;
                cmdInsertarCita.Parameters.Add("pnum_agendacompartida", OracleDbType.Int32).Value = 0;
                cmdInsertarCita.Parameters.Add("phora_presentarse", OracleDbType.Date).Value = cita.HORA_PRESENTARSE;
                cmdInsertarCita.Parameters.Add("pnum_tipoproc", OracleDbType.Int32).Value = cita.NUM_TIPOPROC == 0 ? DBNull.Value : cita.NUM_TIPOPROC;

                cmdInsertarCita.Parameters.Add("pind_aplicaseguro", OracleDbType.Int16).Value = cita.IND_APLICASEGURO;
                cmdInsertarCita.Parameters.Add("pind_copago", OracleDbType.Int16).Value = cita.IND_COPAGO;
                cmdInsertarCita.Parameters.Add("pind_deducible", OracleDbType.Int16).Value = cita.IND_DEDUCIBLE;
                cmdInsertarCita.Parameters.Add("pind_coberturatotal", OracleDbType.Int16).Value = cita.IND_COBERTURATOTAL;
                cmdInsertarCita.Parameters.Add("pobservaciones_seguro", OracleDbType.Varchar2).Value = cita.OBSERVACIONES_SEGURO;
                cmdInsertarCita.Parameters.Add("pcod_instituc", OracleDbType.Varchar2).Value = cita.COD_INSTITUC;
                cmdInsertarCita.Parameters.Add("pind_citasecundaria", OracleDbType.Int16).Value = cita.IND_CITASECUNDARIA;

                var codErrorCita = new OracleParameter("pcoderror", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };
                var desErrorCita = new OracleParameter("pdeserror", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };
                var numCita = new OracleParameter("pnum_cita", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };

                cmdInsertarCita.Parameters.Add(codErrorCita);
                cmdInsertarCita.Parameters.Add(desErrorCita);
                cmdInsertarCita.Parameters.Add(numCita);

                await cmdInsertarCita.ExecuteNonQueryAsync();

                var codErrorValue = codErrorCita.Value?.ToString() ?? "1";
                var desErrorValue = desErrorCita.Value?.ToString() ?? "Error al insertar la cita";

                if (codErrorValue != "0")
                {
                    result.Add(codErrorValue);
                    result.Add(desErrorValue);
                    return result;
                }
                else
                {
                    newNumCita = numCita.Value == DBNull.Value ? 0 : Convert.ToInt32(numCita.Value);

                }
            }


            // Insertar procedimientos (AGENDA.CITAPROCEDIMIENTO.insertar)
            foreach (var proc in servicios)
            {
                await using var cmdProc = new OracleCommand("AGENDA.CITAPROCEDIMIENTO.insertar", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmdProc.Parameters.Add("pnum_cita", OracleDbType.Int32).Value = newNumCita;
                cmdProc.Parameters.Add("pcod_articulo", OracleDbType.Varchar2, 12).Value = proc.COD_ARTICULO;
                cmdProc.Parameters.Add("pdes_detalle", OracleDbType.Varchar2, 500).Value = proc.DES_DETALLE;

                var codError = new OracleParameter("pcoderror", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };
                var desError = new OracleParameter("pdeserror", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };
                cmdProc.Parameters.Add(codError);
                cmdProc.Parameters.Add(desError);

                await cmdProc.ExecuteNonQueryAsync();

                if (codError.Value?.ToString() != "0")
                {
                    return result;
                }
            }

            // Insertar bitácora
            var bitacora = new CitaBitacoraInsertar
            {
                NUM_CITA = cita.NUM_CITA,
                FEC_HORAINICIAL = cita.FEC_HORAINICIAL,
                FEC_HORAFINAL = cita.FEC_HORAFINAL,
                IND_OPERACION = "I",
                USR_REGISTRA = cita.USR_REGISTRA,
                DES_DATOANTES = string.Empty,
                DES_DATODESPUES = bitacoraDatosDespues,
                NUM_AGENDA = cita.NUM_AGENDA
            };

            var resBitacora = await InsertarBitacoraAsync(bitacora);
            if (resBitacora[0] != "0")
                return result;


            // Ejecutar eliminación (AGENDA.CITAINSERTAR.eliminar)
            await using (var cmdEliminar = new OracleCommand("AGENDA.CITAINSERTAR.eliminar", conn))
            {
                cmdEliminar.CommandType = CommandType.StoredProcedure;

                cmdEliminar.Parameters.Add("pnum_agenda", OracleDbType.Int32).Value = cita.NUM_AGENDA;
                cmdEliminar.Parameters.Add("pfec_horainicial", OracleDbType.Date).Value = cita.FEC_HORAINICIAL;
                cmdEliminar.Parameters.Add("pfec_horafinal", OracleDbType.Date).Value = cita.FEC_HORAFINAL;

                var codError = new OracleParameter("pcoderror", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };
                var desError = new OracleParameter("pdeserror", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };

                cmdEliminar.Parameters.Add(codError);
                cmdEliminar.Parameters.Add(desError);

                await cmdEliminar.ExecuteNonQueryAsync();

                var codErrorValue = codError.Value?.ToString() ?? "1";
                var desErrorValue = desError.Value?.ToString() ?? "Error al eliminar la cita";

                if (codErrorValue != "0")
                {
                    result.Add(codErrorValue);
                    result.Add(desErrorValue);
                    return result;
                }
            }

            result.Add("0");
            result.Add(newNumCita.ToString());
            
            return result;

            
        }

        public async Task<string> NotificaCitaConvenioAsync(string codInstituc, int numAgenda)
        {
            await using var conn = new OracleConnection(_connection);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand("AGENDA.AGAGENDA.CONSULTA_AGENDAEXTERNA", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Parámetros de entrada
            cmd.Parameters.Add("pAgenda", OracleDbType.Int32).Value = numAgenda;
            cmd.Parameters.Add("pConvenio", OracleDbType.Varchar2, 50).Value = codInstituc;

            // Parámetro de salida
            var outputParam = new OracleParameter("pNotificaCitas", OracleDbType.Varchar2, 10)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParam);

            await cmd.ExecuteNonQueryAsync();

            return outputParam.Value?.ToString() ?? string.Empty;
        }

    }
}
