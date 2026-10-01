<%@ Page Language="VB"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="false"
    CodeBehind="Clientes.aspx.vb"
    Inherits="TestVB.Clientes" %>

<asp:Content
    ID="TitleContent"
    ContentPlaceHolderID="TitleContent"
    runat="server">
    Clientes

</asp:Content>


<asp:Content
    ID="MainContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="d-flex flex-column flex-md-row justify-content-between align-items-md-center gap-3 mb-4">

        <div>

            <h1 class="h2 mb-1">Clientes
            </h1>

            <p class="text-secondary mb-0">

                <asp:Label
                    ID="lblBienvenida"
                    runat="server">
                </asp:Label>

            </p>

        </div>


        <asp:HyperLink
            ID="lnkNuevo"
            runat="server"
            NavigateUrl="~/ClienteForm.aspx"
            CssClass="btn btn-primary">

            + Nuevo cliente

        </asp:HyperLink>

    </div>


    <asp:Label
        ID="lblMensaje"
        runat="server"
        Visible="False"
        CssClass="alert alert-info d-block">
    </asp:Label>


    <div class="card shadow-sm border-0">

        <div class="card-body">

            <div class="row g-2 mb-4">

                <div class="col">

                    <asp:TextBox
                        ID="txtBusqueda"
                        runat="server"
                        MaxLength="150"
                        CssClass="form-control"
                        placeholder="Buscar por nombre, apellido o documento">
                    </asp:TextBox>

                </div>


                <div class="col-auto">

                    <asp:Button
                        ID="btnBuscar"
                        runat="server"
                        Text="Buscar"
                        CssClass="btn btn-primary" />

                </div>


                <div class="col-auto">

                    <asp:Button
                        ID="btnLimpiar"
                        runat="server"
                        Text="Limpiar"
                        CssClass="btn btn-outline-secondary"
                        CausesValidation="False" />

                </div>

            </div>


            <div class="table-responsive">

                <asp:HiddenField
                    ID="hfClienteEliminar"
                    runat="server"
                    ClientIDMode="Static" />

                <asp:LinkButton
                    ID="btnConfirmarEliminar"
                    runat="server"
                    ClientIDMode="Static"
                    data-single-submit="true"
                    Style="display: none;">
                </asp:LinkButton>

                <asp:GridView
                    ID="gvClientes"
                    runat="server"
                    AutoGenerateColumns="False"
                    CssClass="table table-hover align-middle mb-0"
                    GridLines="None"
                    EmptyDataText="No hay clientes registrados."
                    HeaderStyle-CssClass="table-light">

                    <Columns>

                        <asp:BoundField
                            DataField="IdCliente"
                            HeaderText="ID" />

                        <asp:BoundField
                            DataField="Nombres"
                            HeaderText="Nombres" />

                        <asp:BoundField
                            DataField="Apellidos"
                            HeaderText="Apellidos" />

                        <asp:BoundField
                            DataField="Documento"
                            HeaderText="Documento" />

                        <asp:BoundField
                            DataField="Telefono"
                            HeaderText="Teléfono" />

                        <asp:BoundField
                            DataField="Correo"
                            HeaderText="Correo" />


                        <asp:TemplateField
                            HeaderText="Acciones">

                            <ItemTemplate>

                                <div class="d-flex gap-2">

                                    <asp:HyperLink
                                        ID="lnkEditar"
                                        runat="server"
                                        Text="Editar"
                                        CssClass="btn btn-sm btn-outline-primary"
                                        NavigateUrl='<%# "~/ClienteForm.aspx?id=" & Eval("IdCliente") %>'>
                                    </asp:HyperLink>


                                    <button
                                        type="button"
                                        class="btn btn-sm btn-outline-danger"
                                        onclick="mostrarModalEliminar(<%# Eval("IdCliente") %>)">
                                        Eliminar

                                    </button>

                                </div>

                            </ItemTemplate>

                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

            </div>

        </div>

    </div>

    <!-- Modal de confirmación de eliminación -->

    <div
        class="modal fade"
        id="modalEliminar"
        tabindex="-1"
        aria-labelledby="modalEliminarLabel"
        aria-hidden="true">

        <div class="modal-dialog modal-dialog-centered">

            <div class="modal-content">

                <div class="modal-header">

                    <h5
                        class="modal-title"
                        id="modalEliminarLabel">Confirmar eliminación

                    </h5>

                    <button
                        type="button"
                        class="btn-close"
                        data-bs-dismiss="modal"
                        aria-label="Cerrar">
                    </button>

                </div>


                <div class="modal-body">

                    <div class="text-center">

                        <i
                            class="bi bi-exclamation-triangle text-warning"
                            style="font-size: 3rem;"></i>

                        <p class="mt-3 mb-1">
                            ¿Está seguro de eliminar este cliente?

                        </p>

                        <p class="text-secondary small mb-0">
                            El cliente será desactivado y se conservará
                        el registro en la bitácora.

                        </p>

                    </div>

                </div>


                <div class="modal-footer">

                    <button
                        type="button"
                        class="btn btn-outline-secondary"
                        data-bs-dismiss="modal">
                        Cancelar

                    </button>


                    <button
                        type="button"
                        class="btn btn-danger"
                        onclick="confirmarEliminacion()">

                        <i class="bi bi-trash me-1"></i>

                        Sí, eliminar

                    </button>

                </div>

            </div>

        </div>

    </div>

</asp:Content>
