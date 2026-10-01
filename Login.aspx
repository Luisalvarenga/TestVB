<%@ Page Language="VB"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="false"
    CodeBehind="Login.aspx.vb"
    Inherits="TestVB.Login" %>


<asp:Content
    ID="TitleContent"
    ContentPlaceHolderID="TitleContent"
    runat="server">
    Iniciar sesión

</asp:Content>


<asp:Content
    ID="MainContent"
    ContentPlaceHolderID="MainContent"
    runat="server">


    <div class="row justify-content-center">

        <div class="col-md-6 col-lg-4">


            <div class="text-center mb-4">

                <h1 class="h3 fw-bold">Iniciar sesión
                </h1>

                <p class="text-secondary">
                    Gestión de Clientes
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


                    <div class="mb-3">

                        <label
                            for="txtUsuario"
                            class="form-label">
                            Usuario

                        </label>


                        <asp:TextBox
                            ID="txtUsuario"
                            runat="server"
                            MaxLength="50"
                            CssClass="form-control"
                            autocomplete="username">
                        </asp:TextBox>

                    </div>


                    <div class="mb-4">

                        <label
                            for="txtPassword"
                            class="form-label">
                            Contraseña

   
                        </label>

                        <div class="input-group">

                            <asp:TextBox
                                ID="txtPassword"
                                runat="server"
                                TextMode="Password"
                                CssClass="form-control"
                                ClientIDMode="Static"
                                autocomplete="current-password">
                            </asp:TextBox>

                            <button
                                type="button"
                                class="btn btn-outline-secondary"
                                id="btnMostrarPassword"
                                aria-label="Mostrar contraseña"
                                title="Mostrar contraseña">

                                <i class="bi bi-eye"></i>

                            </button>

                        </div>

                    </div>


                    <div class="d-grid">

                        <asp:Button
                            ID="btnIngresar"
                            runat="server"
                            Text="Ingresar"
                            CssClass="btn btn-primary btn-lg" />

                    </div>


                </div>

            </div>


        </div>

    </div>

</asp:Content>
