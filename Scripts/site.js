document.addEventListener("DOMContentLoaded", function () {

    // ---------------------------------------------
    // Mostrar / ocultar contraseña
    // ---------------------------------------------

    const passwordInput =
        document.getElementById("txtPassword");

    const passwordButton =
        document.getElementById("btnMostrarPassword");

    if (passwordInput && passwordButton) {

        passwordButton.addEventListener("click", function () {

            const mostrando =
                passwordInput.type === "text";

            passwordInput.type =
                mostrando ? "password" : "text";

            const icono =
                passwordButton.querySelector("i");

            if (icono) {

                icono.classList.toggle(
                    "bi-eye",
                    mostrando
                );

                icono.classList.toggle(
                    "bi-eye-slash",
                    !mostrando
                );

            }

            passwordButton.setAttribute(
                "aria-label",
                mostrando
                    ? "Mostrar contraseña"
                    : "Ocultar contraseña"
            );

            passwordButton.setAttribute(
                "title",
                mostrando
                    ? "Mostrar contraseña"
                    : "Ocultar contraseña"
            );

        });

    }

    // ---------------------------------------------
    // Protección contra doble envío
    // ---------------------------------------------

    // ---------------------------------------------
    // Protección contra doble envío
    // ---------------------------------------------

    const formulario =
        document.getElementById("formMaster");

    if (formulario) {

        formulario.addEventListener("submit", function () {

            const botones =
                formulario.querySelectorAll(
                    "[data-single-submit='true']"
                );

            botones.forEach(function (boton) {

                // Esperamos a que Web Forms capture
                // correctamente el submit antes de
                // deshabilitar el botón.
                setTimeout(function () {

                    boton.disabled = true;

                    if (boton.tagName === "INPUT") {
                        boton.value = "Procesando...";
                    }
                    else {
                        boton.textContent = "Procesando...";
                    }

                }, 0);

            });

        });

    }

});


// ---------------------------------------------
// Modal de eliminación
// ---------------------------------------------

let modalEliminar = null;

function mostrarModalEliminar(idCliente) {

    const hiddenField =
        document.getElementById("hfClienteEliminar");

    if (!hiddenField) {
        return;
    }

    hiddenField.value = idCliente;

    const modalElement =
        document.getElementById("modalEliminar");

    if (!modalElement) {
        return;
    }

    modalEliminar =
        bootstrap.Modal.getOrCreateInstance(
            modalElement
        );

    modalEliminar.show();
}


// ---------------------------------------------
// Confirmar eliminación
// ---------------------------------------------

function confirmarEliminacion() {

    const boton =
        document.getElementById("btnConfirmarEliminar");

    if (!boton) {
        return;
    }

    if (modalEliminar) {
        modalEliminar.hide();
    }

    boton.click();

}