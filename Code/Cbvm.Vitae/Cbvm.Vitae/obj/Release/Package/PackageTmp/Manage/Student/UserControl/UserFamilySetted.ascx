<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserFamilySetted.ascx.cs" Inherits="Cbvm.Vitae.Manage.Student.UserControl.UserFamilySetted" %>
<div class="func-block user-introduce">
    <div class="block">
        <div class="caption">
            <h3>
                <%=GetLocalResourceObject("BaseFamilySetted")%>
            </h3>
        </div>
        <div class="container">
            <table>
                <tr>
                    <th>帐号:</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtUserName" Enabled="False" Width="180px" MaxLength="25"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>称谓:</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtRelationship" Width="180px" MaxLength="40"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>姓名:</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtNameZh" Width="180px" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>性别:</th>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rdoSexType" RepeatDirection="Horizontal" />
                    </td>
                </tr>
                <tr>
                    <th>密码:</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" Width="180px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th></th>
                    <td>
                        <asp:CheckBox runat="server" ID="chkEnabled" Text="启用" />
                    </td>
                </tr>
                <tr>
                    <th></th>
                    <td align="right">
                        <asp:Button runat="server" ID="btnSave" Text="保存" OnClick="btnSave_Click" UseSubmitBehavior="true" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(function () {
        $(".user-introduce textarea").jqTransTextarea();
        $(".user-introduce input:text,input:password").filter(function () {
            return !($(this).css("visibility") == "hidden" || $(this).css("display") == "none");
        }).jqTransInputText();

        $(".user-introduce input:checkbox").jqTransCheckBox();
        $(".user-introduce input:radio").jqTransRadio();

        $(".user-introduce input:submit").jqTransInputButton();
    })
</script>
