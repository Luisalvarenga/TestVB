Imports System
Imports System.Web.Security
Imports System.Web.UI

Public Class BasePage
    Inherits Page

    Protected Overrides Sub OnInit(e As EventArgs)

        MyBase.OnInit(e)

        If Context Is Nothing Then
            Return
        End If


        If Context.Session IsNot Nothing Then

            ViewStateUserKey =
                Context.Session.SessionID

        End If


        ' -------------------------------------------------
        ' Validar autenticación y sesión del usuario.
        ' -------------------------------------------------

        If Not Context.User.Identity.IsAuthenticated Then

            Response.Redirect(
                "~/Login.aspx",
                False
            )

            Context.ApplicationInstance.CompleteRequest()

            Return

        End If


        Dim idUsuario As Integer

        If Not Integer.TryParse(
            Convert.ToString(Session("IdUsuario")),
            idUsuario
        ) OrElse idUsuario <= 0 Then

            ' La cookie de autenticación existe, pero
            ' la sesión del usuario ya no existe.
            ' Cerramos también la autenticación para
            ' evitar estados inconsistentes.

            FormsAuthentication.SignOut()

            Response.Redirect(
                "~/Login.aspx",
                False
            )

            Context.ApplicationInstance.CompleteRequest()

            Return

        End If

    End Sub

End Class