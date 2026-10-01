<%@ Page Language="VB"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="false"
    CodeBehind="Error.aspx.vb"
    Inherits="TestVB.ErrorPage" %>

<asp:Content
    ID="TitleContent"
    ContentPlaceHolderID="TitleContent"
    runat="server">

    Error

</asp:Content>


<asp:Content
    ID="MainContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="row justify-content-center">

        <div class="col-md-8 col-lg-6">

            <div class="card border-0 shadow-sm text-center">

                <div class="card-body p-5">

                    <div class="mb-4">

                        <i
                            class="bi bi-exclamation-triangle text-warning"
                            style="font-size: 4rem;">
                        </i>

                    </div>


                    <h1 class="h3 mb-3">
                        Ocurrió un error
                    </h1>


                    <p class="text-secondary mb-4">
                        No fue posible completar la operación solicitada.
                        Intente nuevamente.
                    </p>


                    <div class="d-flex justify-content-center gap-2">

                        <asp:HyperLink
                            ID="lnkVolver"
                            runat="server"
                            NavigateUrl="~/Clientes.aspx"
                            CssClass="btn btn-primary">

                            Volver a clientes

                        </asp:HyperLink>


                        <asp:HyperLink
                            ID="lnkLogin"
                            runat="server"
                            NavigateUrl="~/Login.aspx"
                            CssClass="btn btn-outline-secondary">

                            Ir al login

                        </asp:HyperLink>

                    </div>

                </div>

            </div>

        </div>

    </div>

</asp:Content>