<%@ Page Language="VB"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="false"
    CodeBehind="ClienteForm.aspx.vb"
    Inherits="TestVB.ClienteForm" %>


<asp:Content
    ID="TitleContent"
    ContentPlaceHolderID="TitleContent"
    runat="server">

    Cliente

</asp:Content>


<asp:Content
    ID="MainContent"
    ContentPlaceHolderID="MainContent"
    runat="server">


    <div class="row justify-content-center">

        <div class="col-lg-8">


            <div class="mb-4">

                <h1 class="h2 mb-1">

                    <asp:Label
                        ID="lblTitulo"
                        runat="server"
                        Text="Nuevo cliente">
                    </asp:Label>

                </h1>

                <p class="text-secondary mb-0">
                    Complete la información del cliente.
                </p>

            </div>


            <div class="card shadow-sm border-0">

                <div class="card-body p-4">


                    <asp:Label
                        ID="lblMensaje"
                        runat="server"
                        Visible="False"
                        CssClass="alert alert-danger d-block">
                    </asp:Label>


                    <div class="row g-3">


                        <!-- Nombres -->

                        <div class="col-md-6">

                            <label
                                for="txtNombres"
                                class="form-label">

                                Nombres <span class="text-danger">*</span>

                            </label>


                            <asp:TextBox
                                ID="txtNombres"
                                runat="server"
                                MaxLength="100"
                                CssClass="form-control">
                            </asp:TextBox>


                            <asp:RequiredFieldValidator
                                ID="rfvNombres"
                                runat="server"
                                ControlToValidate="txtNombres"
                                ErrorMessage="Los nombres son obligatorios."
                                CssClass="text-danger small"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>

                        </div>


                        <!-- Apellidos -->

                        <div class="col-md-6">

                            <label
                                for="txtApellidos"
                                class="form-label">

                                Apellidos <span class="text-danger">*</span>

                            </label>


                            <asp:TextBox
                                ID="txtApellidos"
                                runat="server"
                                MaxLength="100"
                                CssClass="form-control">
                            </asp:TextBox>


                            <asp:RequiredFieldValidator
                                ID="rfvApellidos"
                                runat="server"
                                ControlToValidate="txtApellidos"
                                ErrorMessage="Los apellidos son obligatorios."
                                CssClass="text-danger small"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>

                        </div>


                        <!-- Documento -->

                        <div class="col-md-6">

                            <label
                                for="txtDocumento"
                                class="form-label">

                                Documento <span class="text-danger">*</span>

                            </label>


                            <asp:TextBox
                                ID="txtDocumento"
                                runat="server"
                                MaxLength="30"
                                CssClass="form-control">
                            </asp:TextBox>


                            <asp:RequiredFieldValidator
                                ID="rfvDocumento"
                                runat="server"
                                ControlToValidate="txtDocumento"
                                ErrorMessage="El documento es obligatorio."
                                CssClass="text-danger small"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>

                        </div>


                        <!-- Teléfono -->

                        <div class="col-md-6">

                            <label
                                for="txtTelefono"
                                class="form-label">

                                Teléfono

                            </label>


                            <asp:TextBox
                                ID="txtTelefono"
                                runat="server"
                                MaxLength="30"
                                CssClass="form-control">
                            </asp:TextBox>

                        </div>


                        <!-- Correo -->

                        <div class="col-12">

                            <label
                                for="txtCorreo"
                                class="form-label">

                                Correo electrónico

                            </label>


                            <asp:TextBox
                                ID="txtCorreo"
                                runat="server"
                                MaxLength="150"
                                TextMode="Email"
                                CssClass="form-control">
                            </asp:TextBox>

                        </div>


                        <!-- Dirección -->

                        <div class="col-12">

                            <label
                                for="txtDireccion"
                                class="form-label">

                                Dirección

                            </label>


                            <asp:TextBox
                                ID="txtDireccion"
                                runat="server"
                                MaxLength="250"
                                TextMode="MultiLine"
                                Rows="4"
                                CssClass="form-control">
                            </asp:TextBox>

                        </div>


                    </div>


                    <hr class="my-4" />


                    <div class="d-flex gap-2">

                        <asp:Button
                            ID="btnGuardar"
                            runat="server"
                            Text="Guardar"
                            CssClass="btn btn-primary"
                            CausesValidation="True"/>


                        <asp:HyperLink
                            ID="lnkCancelar"
                            runat="server"
                            NavigateUrl="~/Clientes.aspx"
                            CssClass="btn btn-outline-secondary">

                            Cancelar

                        </asp:HyperLink>

                    </div>


                </div>

            </div>

        </div>

    </div>

    <!-- Modal de reactivación -->

<div
    class="modal fade"
    id="modalReactivar"
    tabindex="-1"
    aria-labelledby="modalReactivarLabel"
    aria-hidden="true">

    <div class="modal-dialog modal-dialog-centered">

        <div class="modal-content">

            <div class="modal-header">

                <h5
                    class="modal-title"
                    id="modalReactivarLabel">

                    Cliente inactivo encontrado

                </h5>

                <button
                    type="button"
                    class="btn-close"
                    data-bs-dismiss="modal"
                    aria-label="Cerrar">
                </button>

            </div>


            <div class="modal-body">

                <p class="mb-2">
                    Ya existe un cliente registrado con el documento:
                </p>

                <p class="fw-bold mb-3">

                    <asp:Label
                        ID="lblDocumentoReactivar"
                        runat="server">
                    </asp:Label>

                </p>

                <p class="mb-0">
                    Este cliente se encuentra inactivo.
                    ¿Desea reactivarlo?
                </p>

            </div>


            <div class="modal-footer">

                <button
                    type="button"
                    class="btn btn-outline-secondary"
                    data-bs-dismiss="modal">

                    Cancelar

                </button>


                <asp:Button
                    ID="btnReactivar"
                    runat="server"
                    Text="Reactivar cliente"
                    CssClass="btn btn-primary"
                    CausesValidation="False" />

            </div>

        </div>

    </div>

</div>

</asp:Content>