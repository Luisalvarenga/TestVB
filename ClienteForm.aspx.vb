Imports System

Public Class ClienteForm
    Inherits BasePage

    Private Property IdClienteActual As Integer
        Get
            Return Convert.ToInt32(
                If(ViewState("IdClienteActual"), 0)
            )
        End Get

        Set(value As Integer)
            ViewState("IdClienteActual") = value
        End Set
    End Property


    Protected Sub Page_Load(
        sender As Object,
        e As EventArgs
    ) Handles Me.Load

        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("~/Login.aspx")
            Return
        End If

        If Not IsPostBack Then
            CargarCliente()

        End If

    End Sub


    Private Sub CargarCliente()

        Dim idCliente As Integer = ObtenerIdClienteDesdeQueryString()

        If idCliente = 0 Then

            IdClienteActual = 0
            lblTitulo.Text = "Nuevo cliente"

            Return

        End If


        Dim service As New ClienteService()

        Dim cliente As Cliente =
            service.ObtenerCliente(idCliente)

        If cliente Is Nothing OrElse
           Not cliente.Activo Then

            MostrarMensaje(
                "El cliente solicitado no existe."
            )

            btnGuardar.Enabled = False

            Return

        End If


        IdClienteActual = cliente.IdCliente

        lblTitulo.Text = "Editar cliente"

        txtNombres.Text = cliente.Nombres
        txtApellidos.Text = cliente.Apellidos
        txtDocumento.Text = cliente.Documento
        txtTelefono.Text = cliente.Telefono
        txtCorreo.Text = cliente.Correo
        txtDireccion.Text = cliente.Direccion

    End Sub


    Protected Sub btnGuardar_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnGuardar.Click

        lblMensaje.Visible = False

        If Not Page.IsValid Then
            Return
        End If


        Try

            Dim cliente As New Cliente() With {
                .IdCliente = IdClienteActual,
                .Nombres = txtNombres.Text.Trim(),
                .Apellidos = txtApellidos.Text.Trim(),
                .Documento = txtDocumento.Text.Trim(),
                .Telefono = txtTelefono.Text.Trim(),
                .Correo = txtCorreo.Text.Trim(),
                .Direccion = txtDireccion.Text.Trim()
            }


            Dim idUsuario As Integer =
                Convert.ToInt32(
                    Session("IdUsuario")
                )

            Dim nombreUsuario As String =
                Convert.ToString(
                    Session("NombreUsuario")
                )


            Dim service As New ClienteService()


            If IdClienteActual = 0 Then

                Dim nuevoId As Integer =
                    service.AgregarCliente(
                        cliente,
                        idUsuario,
                        nombreUsuario
                    )

            Else

                service.EditarCliente(
                    cliente,
                    idUsuario,
                    nombreUsuario
                )

            End If


            Response.Redirect(
                "~/Clientes.aspx",
                False
            )

            Context.ApplicationInstance.CompleteRequest()

        Catch ex As ArgumentException

            MostrarMensaje(
                ex.Message
            )

        Catch ex As InvalidOperationException

            MostrarMensaje(
                ex.Message
            )

        Catch ex As Exception

            MostrarMensaje(
                "Ocurrió un error al guardar el cliente."
            )

        End Try

    End Sub


    Private Function ObtenerIdClienteDesdeQueryString() As Integer

        Dim valor As String =
            Request.QueryString("id")

        If String.IsNullOrWhiteSpace(valor) Then
            Return 0
        End If


        Dim idCliente As Integer

        If Not Integer.TryParse(
            valor,
            idCliente
        ) Then

            Return 0

        End If


        If idCliente <= 0 Then
            Return 0
        End If


        Return idCliente

    End Function


    Private Sub MostrarMensaje(
        mensaje As String
    )

        lblMensaje.Text =
            Server.HtmlEncode(mensaje)

        lblMensaje.Visible = True

    End Sub

End Class