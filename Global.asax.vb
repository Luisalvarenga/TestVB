Imports System
Imports System.Web
Imports System.Web.Optimization

Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(
        sender As Object,
        e As EventArgs
    )

        ' Se desencadena al iniciar la aplicación
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)

    End Sub


    Sub Application_Error(
        sender As Object,
        e As EventArgs
    )

        Try

            Dim exception As Exception =
                Server.GetLastError()

            If exception Is Nothing Then
                Return
            End If


            ' Evitar un posible ciclo si la propia
            ' página de error genera una excepción.
            Dim rutaActual As String =
                Request.AppRelativeCurrentExecutionFilePath

            If String.Equals(
                rutaActual,
                "~/Error.aspx",
                StringComparison.OrdinalIgnoreCase
            ) Then

                Return

            End If


            ' Limpiar la excepción para evitar
            ' mostrar detalles técnicos al usuario.
            Server.ClearError()


            ' Redirigir a la página de error.
            Response.Redirect(
                "~/Error.aspx",
                False
            )

            Context.ApplicationInstance.CompleteRequest()

        Catch

            ' No lanzar otra excepción desde
            ' el manejador global de errores.

        End Try

    End Sub

End Class