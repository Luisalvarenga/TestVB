Imports System

Public Class DefaultPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(
        sender As Object,
        e As EventArgs
    ) Handles Me.Load

        If Context.User.Identity.IsAuthenticated Then

            Dim idUsuario As Integer

            If Integer.TryParse(
                Convert.ToString(Session("IdUsuario")),
                idUsuario
            ) AndAlso idUsuario > 0 Then

                Response.Redirect(
                    "~/Clientes.aspx",
                    False
                )

                Context.ApplicationInstance.CompleteRequest()

                Return

            End If

        End If


        Response.Redirect(
            "~/Login.aspx",
            False
        )

        Context.ApplicationInstance.CompleteRequest()

    End Sub

End Class