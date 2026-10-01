Imports System.Data
Imports System.Data.SqlClient

Public Class UsuarioRepository

    Public Function ObtenerPorNombreUsuario(
        nombreUsuario As String
    ) As Usuario

        Const sql As String = "
            SELECT
                IdUsuario,
                NombreUsuario,
                PasswordHash,
                NombreCompleto,
                Activo,
                IntentosFallidos,
                BloqueadoHasta,
                FechaCreacion
            FROM dbo.Usuarios
            WHERE NombreUsuario = @NombreUsuario;
        "

        Using connection As SqlConnection =
            ConnectionFactory.CreateConnection()

            Using command As New SqlCommand(sql, connection)

                command.Parameters.Add(
                    "@NombreUsuario",
                    SqlDbType.NVarChar,
                    50
                ).Value = nombreUsuario

                connection.Open()

                Using reader As SqlDataReader =
                    command.ExecuteReader(CommandBehavior.SingleRow)

                    If Not reader.Read() Then
                        Return Nothing
                    End If

                    Return MapearUsuario(reader)

                End Using

            End Using

        End Using

    End Function


    Public Function ObtenerPorId(
        idUsuario As Integer
    ) As Usuario

        Const sql As String = "
            SELECT
                IdUsuario,
                NombreUsuario,
                PasswordHash,
                NombreCompleto,
                Activo,
                IntentosFallidos,
                BloqueadoHasta,
                FechaCreacion
            FROM dbo.Usuarios
            WHERE IdUsuario = @IdUsuario;
        "

        Using connection As SqlConnection =
            ConnectionFactory.CreateConnection()

            Using command As New SqlCommand(sql, connection)

                command.Parameters.Add(
                    "@IdUsuario",
                    SqlDbType.Int
                ).Value = idUsuario

                connection.Open()

                Using reader As SqlDataReader =
                    command.ExecuteReader(CommandBehavior.SingleRow)

                    If Not reader.Read() Then
                        Return Nothing
                    End If

                    Return MapearUsuario(reader)

                End Using

            End Using

        End Using

    End Function


    Public Sub ActualizarIntentosFallidos(
        idUsuario As Integer,
        intentosFallidos As Integer,
        bloqueadoHasta As DateTime?
    )

        Const sql As String = "
            UPDATE dbo.Usuarios
            SET
                IntentosFallidos = @IntentosFallidos,
                BloqueadoHasta = @BloqueadoHasta
            WHERE IdUsuario = @IdUsuario;
        "

        Using connection As SqlConnection =
            ConnectionFactory.CreateConnection()

            Using command As New SqlCommand(sql, connection)

                command.Parameters.Add(
                    "@IntentosFallidos",
                    SqlDbType.Int
                ).Value = intentosFallidos

                command.Parameters.Add(
                    "@BloqueadoHasta",
                    SqlDbType.DateTime2
                ).Value =
                    If(
                        bloqueadoHasta.HasValue,
                        CType(bloqueadoHasta.Value, Object),
                        DBNull.Value
                    )

                command.Parameters.Add(
                    "@IdUsuario",
                    SqlDbType.Int
                ).Value = idUsuario

                connection.Open()

                command.ExecuteNonQuery()

            End Using

        End Using

    End Sub


    Public Sub RestablecerIntentosFallidos(
        idUsuario As Integer
    )

        Const sql As String = "
            UPDATE dbo.Usuarios
            SET
                IntentosFallidos = 0,
                BloqueadoHasta = NULL
            WHERE IdUsuario = @IdUsuario;
        "

        Using connection As SqlConnection =
            ConnectionFactory.CreateConnection()

            Using command As New SqlCommand(sql, connection)

                command.Parameters.Add(
                    "@IdUsuario",
                    SqlDbType.Int
                ).Value = idUsuario

                connection.Open()

                command.ExecuteNonQuery()

            End Using

        End Using

    End Sub


    Private Function MapearUsuario(
        reader As SqlDataReader
    ) As Usuario

        Dim usuario As New Usuario()

        usuario.IdUsuario =
            Convert.ToInt32(reader("IdUsuario"))

        usuario.NombreUsuario =
            Convert.ToString(reader("NombreUsuario"))

        usuario.PasswordHash =
            Convert.ToString(reader("PasswordHash"))

        usuario.NombreCompleto =
            Convert.ToString(reader("NombreCompleto"))

        usuario.Activo =
            Convert.ToBoolean(reader("Activo"))

        usuario.IntentosFallidos =
            Convert.ToInt32(reader("IntentosFallidos"))

        If reader.IsDBNull(reader.GetOrdinal("BloqueadoHasta")) Then
            usuario.BloqueadoHasta = Nothing
        Else
            usuario.BloqueadoHasta =
                Convert.ToDateTime(reader("BloqueadoHasta"))
        End If

        usuario.FechaCreacion =
            Convert.ToDateTime(reader("FechaCreacion"))

        Return usuario

    End Function

End Class