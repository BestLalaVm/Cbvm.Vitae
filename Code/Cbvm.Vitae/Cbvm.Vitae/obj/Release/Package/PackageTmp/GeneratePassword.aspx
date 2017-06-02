<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneratePassword.aspx.cs" Inherits="Cbvm.Vitae.GeneratePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <asp:TextBox runat="server" ID="txtPassword"></asp:TextBox>
        <asp:Button runat="server" ID="btnGenerate" Text="Generate" OnClick="btnGenerate_Click"/>
    </div>
     <div>
         <span>Output:</span>
         <asp:TextBox runat="server" ID="txtOutput"></asp:TextBox>
     </div>
    </form>
</body>
</html>
