Imports System

Public Class AuthService

    Private Const MaxIntentosFallidos As Integer = 5
    Private Const MinutosBloqueo As Integer = 10

    Private ReadOnly _usuarioRepository As UsuarioRepository

    Public Sub New()
        _usuarioRepository = New UsuarioRepository()
    End Sub


    Public Function Autenticar(
        nombreUsuario As String,
        password As String
    ) As Usuario

        If String.IsNullOrWhiteSpace(nombreUsuario) Then
            Return Nothing
        End If

        If String.IsNullOrWhiteSpace(password) Then
            Return Nothing
        End If

        nombreUsuario = nombreUsuario.Trim()

        Dim usuario As Usuario =
            _usuarioRepository.ObtenerPorNombreUsuario(
                nombreUsuario
            )

        ' El usuario no existe.
        If usuario Is Nothing Then
            Return Nothing
        End If

        ' El usuario está desactivado.
        If Not usuario.Activo Then
            Return Nothing
        End If

        ' Comprobar bloqueo.
        If EstaBloqueado(usuario) Then
            Return Nothing
        End If

        ' Verificar contraseña.
        Dim passwordCorrecta As Boolean =
            PasswordHasher.VerifyPassword(
                password,
                usuario.PasswordHash
            )

        If passwordCorrecta Then

            _usuarioRepository.RestablecerIntentosFallidos(
                usuario.IdUsuario
            )

            Return usuario

        End If

        ' Contraseña incorrecta.
        Dim nuevosIntentos As Integer =
            usuario.IntentosFallidos + 1

        Dim bloqueadoHasta As DateTime? = Nothing

        If nuevosIntentos >= MaxIntentosFallidos Then

            bloqueadoHasta =
                DateTime.UtcNow.AddMinutes(
                    MinutosBloqueo
                )

        End If

        _usuarioRepository.ActualizarIntentosFallidos(
            usuario.IdUsuario,
            nuevosIntentos,
            bloqueadoHasta
        )

        Return Nothing

    End Function


    Private Function EstaBloqueado(
        usuario As Usuario
    ) As Boolean

        If Not usuario.BloqueadoHasta.HasValue Then
            Return False
        End If

        If usuario.BloqueadoHasta.Value > DateTime.UtcNow Then
            Return True
        End If

        ' El período de bloqueo ya terminó.
        _usuarioRepository.RestablecerIntentosFallidos(
            usuario.IdUsuario
        )

        Return False

    End Function

End Class