Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub btnIngresar_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnIngresar.Click

        lblMensaje.Visible = False

        Dim nombreUsuario As String =
            txtUsuario.Text.Trim()

        Dim password As String =
            txtPassword.Text

        If String.IsNullOrWhiteSpace(nombreUsuario) OrElse
           String.IsNullOrWhiteSpace(password) Then

            MostrarMensaje(
                "Ingrese usuario y contraseña."
            )

            Return
        End If

        Try

            Dim authService As New AuthService()

            Dim usuario As Usuario =
                authService.Autenticar(
                    nombreUsuario,
                    password
                )

            If usuario Is Nothing Then

                MostrarMensaje(
                    "Usuario o contraseña incorrectos."
                )

                Return

            End If

            ' -------------------------------------------------
            ' Limpiar cualquier dato de sesión previo antes
            ' de almacenar la identidad del usuario autenticado.
            ' -------------------------------------------------
            Session.Clear()

            Session("IdUsuario") = usuario.IdUsuario

            Session("NombreUsuario") = usuario.NombreUsuario

            Session("NombreCompleto") = usuario.NombreCompleto

            ' -------------------------------------------------
            ' Crear ticket de Forms Authentication.
            ' -------------------------------------------------

            FormsAuthentication.SetAuthCookie(
                usuario.NombreUsuario,
                False
            )

            Response.Redirect(
                "~/Clientes.aspx",
                False
            )

            Context.ApplicationInstance.CompleteRequest()

        Catch ex As Exception

            ' No mostrar detalles internos de la excepción
            ' al usuario.
            MostrarMensaje(
                "Ocurrió un error al iniciar sesión."
            )

            ' El error real se registrará posteriormente
            ' mediante nuestro manejo global de errores.

        End Try

    End Sub


    Private Sub MostrarMensaje(
        mensaje As String
    )

        lblMensaje.Text =
            Server.HtmlEncode(mensaje)

        lblMensaje.Visible = True

    End Sub

End Class