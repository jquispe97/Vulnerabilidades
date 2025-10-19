<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CSF.CITASWEB.WS.BackEnd.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>[Sanna App] Backend</title>
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $(".divInput .input").focusout(function () {
                if ($(this).val() != "" && $(this).val() != null) {
                    $(this).addClass("has-content");
                } else {
                    $(this).removeClass("has-content");
                }
            });

            $(".divInput .input").each(function () {
                if ($(this).val() != '' && $(this).val() != null) {
                    $(this).addClass("has-content");
                    if ($(this).hasClass("imagen")) {
                        $(this).parent().find('.botonLink').show(1000);
                    }
                }
            });

            $('#txtUsuario').focus();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divLogin">
        <div id="divLoginContenido">
            <div class="divInput">
                <asp:TextBox ID="txtUsuario" runat="server" autocomplete="off" CssClass="input obligatorio" placeholder=""></asp:TextBox>
                <label>
                    Usuario</label>
                <span class="focus-border"></span>
            </div>
            <div class="divInput">
                <asp:TextBox ID="txtPassword" runat="server" autocomplete="off" TextMode="Password" CssClass="input obligatorio"
                    placeholder=""></asp:TextBox>
                <label>
                    Password</label>
                <span class="focus-border"></span>
            </div>
            <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" OnClientClick="return validarFormulario('obligatorio');"
                OnClick="btnIngresar_Click" />
        </div>
    </div>
    </form>
</body>
</html>
