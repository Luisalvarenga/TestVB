Imports System.Data
Imports System.Data.SqlClient

Public Class ClienteRepository

    Public Function ObtenerActivos() As List(Of Cliente)

        Const sql As String = "
            SELECT
                IdCliente,
                Nombres,
                Apellidos,
                Documento,
                Telefono,
                Correo,
                Direccion,
                Activo,
                FechaCreacion,
                FechaModificacion,
                RowVersion
            FROM dbo.Clientes
            WHERE Activo = 1
            ORDER BY Apellidos, Nombres;
        "

        Dim clientes As New List(Of Cliente)()

        Using connection As SqlConnection =
            ConnectionFactory.CreateConnection()

            Using command As New SqlCommand(sql, connection)

                connection.Open()

                Using reader As SqlDataReader =
                    command.ExecuteReader()

                    While reader.Read()
                        clientes.Add(MapearCliente(reader))
                    End While

                End Using
            End Using
        End Using

        Return clientes

    End Function

    Public Function BuscarActivos(
    busqueda As String
) As List(Of Cliente)

        Const sql As String = "
        SELECT
            IdCliente,
            Nombres,
            Apellidos,
            Documento,
            Telefono,
            Correo,
            Direccion,
            Activo,
            FechaCreacion,
            FechaModificacion,
            RowVersion
        FROM dbo.Clientes
        WHERE Activo = 1
          AND (
                Nombres LIKE @Busqueda
                OR Apellidos LIKE @Busqueda
                OR Documento LIKE @Busqueda
              )
        ORDER BY Apellidos, Nombres;
    "

        Dim clientes As New List(Of Cliente)()

        Using connection As SqlConnection =
        ConnectionFactory.CreateConnection()

            Using command As New SqlCommand(
            sql,
            connection
        )

                command.Parameters.Add(
                "@Busqueda",
                SqlDbType.NVarChar,
                150
            ).Value = "%" & busqueda.Trim() & "%"

                connection.Open()

                Using reader As SqlDataReader =
                command.ExecuteReader()

                    While reader.Read()

                        clientes.Add(
                        MapearCliente(reader)
                    )

                    End While

                End Using

            End Using

        End Using

        Return clientes

    End Function

    Public Function ObtenerPorId(idCliente As Integer) As Cliente

        Const sql As String = "
            SELECT
                IdCliente,
                Nombres,
                Apellidos,
                Documento,
                Telefono,
                Correo,
                Direccion,
                Activo,
                FechaCreacion,
                FechaModificacion,
                RowVersion
            FROM dbo.Clientes
            WHERE IdCliente = @IdCliente;
        "

        Using connection As SqlConnection =
            ConnectionFactory.CreateConnection()

            Using command As New SqlCommand(sql, connection)

                command.Parameters.Add(
                    "@IdCliente",
                    SqlDbType.Int
                ).Value = idCliente

                connection.Open()

                Using reader As SqlDataReader =
                    command.ExecuteReader(CommandBehavior.SingleRow)

                    If Not reader.Read() Then
                        Return Nothing
                    End If

                    Return MapearCliente(reader)

                End Using
            End Using
        End Using

    End Function

    Public Function ExisteDocumento(
    documento As String,
    idClienteExcluir As Integer
) As Boolean

        Const sql As String = "
        SELECT COUNT(1)
        FROM dbo.Clientes
        WHERE Documento = @Documento
          AND Activo = 1
          AND IdCliente <> @IdClienteExcluir;
    "

        Using connection As SqlConnection =
        ConnectionFactory.CreateConnection()

            Using command As New SqlCommand(
            sql,
            connection
        )

                command.Parameters.Add(
                "@Documento",
                SqlDbType.NVarChar,
                30
            ).Value = documento.Trim()

                command.Parameters.Add(
                "@IdClienteExcluir",
                SqlDbType.Int
            ).Value = idClienteExcluir

                connection.Open()

                Return Convert.ToInt32(
                command.ExecuteScalar()
            ) > 0

            End Using
        End Using

    End Function

    Public Function Insertar(
        cliente As Cliente,
        connection As SqlConnection,
        transaction As SqlTransaction
    ) As Integer

        Const sql As String = "
            INSERT INTO dbo.Clientes
            (
                Nombres,
                Apellidos,
                Documento,
                Telefono,
                Correo,
                Direccion,
                Activo
            )
            VALUES
            (
                @Nombres,
                @Apellidos,
                @Documento,
                @Telefono,
                @Correo,
                @Direccion,
                1
            );

            SELECT CAST(SCOPE_IDENTITY() AS INT);
        "

        Using command As New SqlCommand(
            sql,
            connection,
            transaction
        )

            AgregarParametros(command, cliente)

            Return Convert.ToInt32(
                command.ExecuteScalar()
            )

        End Using

    End Function


    Public Function Actualizar(
        cliente As Cliente,
        connection As SqlConnection,
        transaction As SqlTransaction
    ) As Boolean

        Const sql As String = "
            UPDATE dbo.Clientes
            SET
                Nombres = @Nombres,
                Apellidos = @Apellidos,
                Documento = @Documento,
                Telefono = @Telefono,
                Correo = @Correo,
                Direccion = @Direccion,
                FechaModificacion = SYSUTCDATETIME()
            WHERE
                IdCliente = @IdCliente
                AND Activo = 1;
        "

        Using command As New SqlCommand(
            sql,
            connection,
            transaction
        )

            command.Parameters.Add(
                "@IdCliente",
                SqlDbType.Int
            ).Value = cliente.IdCliente

            AgregarParametros(command, cliente)

            Dim filasAfectadas As Integer =
                command.ExecuteNonQuery()

            Return filasAfectadas = 1

        End Using

    End Function


    Public Function EliminarLogicamente(
        idCliente As Integer,
        connection As SqlConnection,
        transaction As SqlTransaction
    ) As Boolean

        Const sql As String = "
            UPDATE dbo.Clientes
            SET
                Activo = 0,
                FechaModificacion = SYSUTCDATETIME()
            WHERE
                IdCliente = @IdCliente
                AND Activo = 1;
        "

        Using command As New SqlCommand(
            sql,
            connection,
            transaction
        )

            command.Parameters.Add(
                "@IdCliente",
                SqlDbType.Int
            ).Value = idCliente

            Dim filasAfectadas As Integer =
                command.ExecuteNonQuery()

            Return filasAfectadas = 1

        End Using

    End Function


    Private Sub AgregarParametros(
        command As SqlCommand,
        cliente As Cliente
    )

        command.Parameters.Add(
            "@Nombres",
            SqlDbType.NVarChar,
            100
        ).Value = cliente.Nombres.Trim()

        command.Parameters.Add(
            "@Apellidos",
            SqlDbType.NVarChar,
            100
        ).Value = cliente.Apellidos.Trim()

        command.Parameters.Add(
            "@Documento",
            SqlDbType.NVarChar,
            30
        ).Value = cliente.Documento.Trim()

        command.Parameters.Add(
            "@Telefono",
            SqlDbType.NVarChar,
            30
        ).Value =
            If(
                String.IsNullOrWhiteSpace(cliente.Telefono),
                CType(DBNull.Value, Object),
                cliente.Telefono.Trim()
            )

        command.Parameters.Add(
            "@Correo",
            SqlDbType.NVarChar,
            150
        ).Value =
            If(
                String.IsNullOrWhiteSpace(cliente.Correo),
                CType(DBNull.Value, Object),
                cliente.Correo.Trim()
            )

        command.Parameters.Add(
            "@Direccion",
            SqlDbType.NVarChar,
            250
        ).Value =
            If(
                String.IsNullOrWhiteSpace(cliente.Direccion),
                CType(DBNull.Value, Object),
                cliente.Direccion.Trim()
            )

    End Sub


    Private Function MapearCliente(
        reader As SqlDataReader
    ) As Cliente

        Dim cliente As New Cliente()

        cliente.IdCliente =
            Convert.ToInt32(reader("IdCliente"))

        cliente.Nombres =
            Convert.ToString(reader("Nombres"))

        cliente.Apellidos =
            Convert.ToString(reader("Apellidos"))

        cliente.Documento =
            Convert.ToString(reader("Documento"))

        If reader.IsDBNull(
            reader.GetOrdinal("Telefono")
        ) Then
            cliente.Telefono = Nothing
        Else
            cliente.Telefono =
                Convert.ToString(reader("Telefono"))
        End If

        If reader.IsDBNull(
            reader.GetOrdinal("Correo")
        ) Then
            cliente.Correo = Nothing
        Else
            cliente.Correo =
                Convert.ToString(reader("Correo"))
        End If

        If reader.IsDBNull(
            reader.GetOrdinal("Direccion")
        ) Then
            cliente.Direccion = Nothing
        Else
            cliente.Direccion =
                Convert.ToString(reader("Direccion"))
        End If

        cliente.Activo =
            Convert.ToBoolean(reader("Activo"))

        cliente.FechaCreacion =
            Convert.ToDateTime(reader("FechaCreacion"))

        If reader.IsDBNull(
            reader.GetOrdinal("FechaModificacion")
        ) Then
            cliente.FechaModificacion = Nothing
        Else
            cliente.FechaModificacion =
                Convert.ToDateTime(reader("FechaModificacion"))
        End If

        If reader.IsDBNull(
            reader.GetOrdinal("RowVersion")
        ) Then
            cliente.RowVersion = Nothing
        Else
            cliente.RowVersion =
                DirectCast(
                    reader("RowVersion"),
                    Byte()
                )
        End If

        Return cliente

    End Function

End Class