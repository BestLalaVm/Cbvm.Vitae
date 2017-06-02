<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageBoxItem.aspx.cs" Inherits="Cbvm.Vitae.MessageBoxItem" Theme="BaseManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="/Scripts/WebContentUI.js"></script>
    <style type="text/css">
        .messagebox-container {
            width: 550px;
            margin-left: auto;
            margin-right: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scriptManage">
            <Services>
                <asp:ServiceReference Path="~/Ajax/AjaxService.asmx" />
            </Services>
        </asp:ScriptManager>

        <div class="messagebox-container">
            <table>
                <%--                <tr>
                    <td class="messagebox-title-1">标题:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtSubject" Width="450px" IsRequired="true" refClass="messagebox-title-1"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td valign="top" class="messagebox-title-2">内容:</td>
                    <td>
                        <asp:TextBox runat="server" TextMode="MultiLine" ID="txtMsg" Width="450px" Height="180px" IsRequired="true" refClass="messagebox-title-2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <div class="action">
                            <button id="btnSubmit" type="button" value="提交"></button>
                            <button id="btnClose" type="button" value="关闭"></button>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <script type="text/javascript">
            $(function () {
                $(".messagebox-container textarea").jqTransTextarea();
                $(".messagebox-container input:text").filter(function () {
                    return !($(this).css("visibility") == "hidden" || $(this).css("display") == "none");
                }).jqTransInputText();
                $(".messagebox-container .action button").jqTransInputButton();

                $("#btnSubmit > span").click(function () {
                    submitMsg();
                });

                $("#btnClose > span").click(function () {
                    parent.tb_remove();
                });
            });

            function submitMsg() {
                var result = ContentComponentInstance.Validated();
                if (result == null) {
                    return;
                }
                var subject = "";
                var message = $("#<%=txtMsg.ClientID%>").val();

                Cbvm.Vitae.Ajax.AjaxService.SaveMessageBoxInfo("<%=CurrentAccount.UserName%>", "<%=CurrentAccount.UserType%>", "<%=ReceiverName%>", "<%=ReceiverType%>", subject, message, "<%=RefMessageId%>", function (result) {
                    alert("操作成功!");
                    parent.tb_remove();
                }, function (result) {
                    alert("failure" + result);
                });
            }

            function ComponentValidateEx(control, value, invalidClass) {
                var refClass = control.attr("refClass");
                if (value == "") {
                    $("." + refClass).addClass(invalidClass);
                    return false;
                }
                $("." + refClass).removeClass(invalidClass);
                return true;
            }
        </script>
    </form>
</body>
</html>
