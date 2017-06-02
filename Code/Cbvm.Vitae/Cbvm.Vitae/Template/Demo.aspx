<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Master.Master" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="Cbvm.Vitae.Template.Demo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="server">
   

    <br />
    <h3><strong>第一步：</strong> 请登入之后，点击获取Token信息</h3>
    <div>
        <asp:Button ID="btnGenerate" runat="server" OnClick="btnGenerate_Click" Text="点击获取当前用户的Token信息"  Width="250px" Height="30px"/>
    <table>
        <tr>
            <th>Token</th>
            <th>
                <asp:TextBox ID="txtToken" runat="server" ReadOnly="true" Width="250px" Height="30px"></asp:TextBox></th>
        </tr>
        <tr>
            <th>UserName</th>
            <th>
                <asp:TextBox ID="txtUserName" runat="server" ReadOnly="true" Width="250px" Height="30px"></asp:TextBox></th>
        </tr>
        <tr>
            <th>UserType</th>
            <th>
                <asp:TextBox ID="txtUserType" runat="server" ReadOnly="true" Width="250px" Height="30px"></asp:TextBox></th>
        </tr>
    </table>
    </div>
    <p>
    <h3><strong>第二步：</strong> 获取登入信息或者退出</h3>
     <input type="button" value="Auto Login Test" onclick="autoLogin()" style="width: 150px;" />
    <input type="button" value="Logout Test" onclick="autoLogout()" style="width: 150px;" />

    <div>    <br />
        <span>响应数据:</span>
    <textarea id="txtResponse" rows="10" cols="100" readonly="readonly"></textarea>
    </div>
    </p>

    <script type="text/javascript">
        function autoLogin() {
            var data = JSON.stringify({ token: $("#<%=txtToken.ClientID%>").val(), userName: $("#<%=txtUserName.ClientID%>").val(), userType: $("#<%=txtUserType.ClientID%>").val(),                ipaddress:"<%=Request.UserHostAddress%>" });
            var jhr = $.ajax({
                url: "/Ajax/Authenticate.asmx/Login",
                type: "POST",
                data: data,
                dataType: "json",
                contentType: "application/json",
                success: function (response, status, jqXHR) {
                    $("#txtResponse").val("");
                    if (response.d.Success) {
                        $("#txtResponse").val(JSON.stringify(response.d));
                        alert("成功获取数据!");
                    } else {
                        alert(response.d.Message);
                    }
                },
                error: function (jqXHR, status, errowThrow) {
                    $("#txtResponse").val("");
                }
            });
            jhr.always(function (data, status, error) {
                if (status) {

                }
            });
        }

        function autoLogout() {
            var data = JSON.stringify({ token: $("#<%=txtToken.ClientID%>").val(), userName: $("#<%=txtUserName.ClientID%>").val(), userType: $("#<%=txtUserType.ClientID%>").val(), ipaddress: "<%=Request.UserHostAddress%>" });
            var jhr = $.ajax({
                url: "/Ajax/Authenticate.asmx/Logout",
                type: "POST",
                data: data,
                contentType: "application/json",
                dataType: "json",
                success: function (response, status, jqXHR) {
                    if (response.d.Success) {
                        alert("成功注销!");
                        window.location.reload();
                    } else {
                        alert(response.d.Message);
                    }
                },
                error: function (jqXHR, status, errowThrow) {

                }
            });

            jhr.always(function (data, status, error) {
                if (status) {

                }
            });
        }
    </script>
</asp:Content>
