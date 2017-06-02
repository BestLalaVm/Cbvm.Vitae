<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.ascx.cs" Inherits="Cbvm.Vitae.Manage.Family.UserControl.UserInfo" %>
<%@ Register Src="~/Manage/UserControl/UploadControl.ascx" TagName="UploadControl"
    TagPrefix="uc" %>
<div class="func-block user-info">
    <div class="block">
        <div class="caption">
            <h3>
                <%=GetLocalResourceObject("BaseUserInfo")%>
            </h3>
        </div>
        <div class="container">
            <div class="user-left">
                <table>
                    <tr>
                        <th>帐号:
                        </th>
                        <td class="disabled">
                            <asp:TextBox ID="txt_UserName_" runat="server" Enabled="false" ReadOnly="True" CssClass="edit-text" MaxLength="25"></asp:TextBox>
                        </td>
                        <th>性别:
                        </th>
                        <td>
                            <asp:RadioButtonList ID="rdo_Sex_" runat="server" RepeatDirection="Horizontal">
                            </asp:RadioButtonList>
                        </td>
                        <td rowspan="5">
                            <div class="user-right upload-image">
                                <div class="image-container">
                                    <asp:Image ID="imgSource" runat="server" />
                                </div>
                                <div>
                                    <uc:UploadControl ID="upLoadControl" runat="server" MaxWidth="210" MaxHeight="200">
                                    </uc:UploadControl>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <th>中文姓名:
                        </th>
                        <td>
                            <asp:TextBox ID="txt_NameZh_" runat="server" CssClass="edit-text" MaxLength="20"></asp:TextBox>
                        </td>
                        <th>英文姓名:
                        </th>
                        <td>
                            <asp:TextBox ID="txt_NameEn_" runat="server" CssClass="edit-text" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>子女学号:
                        </th>
                        <td class="disabled">
                            <asp:TextBox ID="txt_StudentNum_" runat="server" Enabled="false" CssClass="edit-text" ReadOnly="True"></asp:TextBox>
                        </td>
                        <th>子女姓名:</th>
                        <td class="disabled">
                            <asp:TextBox ID="txt_StudentName_" runat="server" Enabled="false" CssClass="edit-text" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>邮箱:
                        </th>
                        <td>
                            <asp:TextBox ID="txt_Email_" runat="server" CssClass="edit-text" MaxLength="50"></asp:TextBox>
                        </td>
                        <th>联系电话:</th>
                        <td>
                            <asp:TextBox ID="txt_Telephone_" runat="server" CssClass="edit-text" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th class="multiLine">兴趣爱好:
                        </th>
                        <td colspan="4">
                            <asp:TextBox ID="txt_Interested_" runat="server" Width="470px" TextMode="MultiLine" CssClass="multi-edit-text"
                                Height="100px" MaxLength="500"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th class="multiLine">个人描述:
                        </th>
                        <td colspan="4">
                            <asp:TextBox ID="txt_AboutMe_" runat="server" Width="470px" TextMode="MultiLine" CssClass="multi-edit-text"
                                Height="100px" MaxLength="500"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>

            <br class="clear" />
        </div>
    </div>
    <div class="block action">
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" />
    </div>
</div>
<script type="text/javascript">
    //$(function () {
    //    $(".user-info .user-left select").jqTransSelect();
    //    $(".user-info .user-left textarea").jqTransTextarea();
    //    $(".user-info .user-left input:text").filter(function () {
    //        return !($(this).css("visibility") == "hidden" || $(this).css("display") == "none");
    //    }).jqTransInputText();
    //    $(".user-info .user-left input:radio").jqTransRadio();
    //    $(".user-info .user-left input:checkbox").jqTransCheckBox();

    //    $(".user-info .persional input:radio").jqTransRadio();
    //    $(".user-info .persional input:text").jqTransInputText();
    //    $(".user-info input:submit").jqTransInputButton();
    //})
    $(function () {
        $("input.edit-text").jqxInput({ height: 25, width: 180 });
        $(".multi-edit-text").jqxInput();
        $(".RadInput input.riTextBox").jqxInput();

        $(".upload-edit-text").jqxInput({ height: 25, width: 120 });
    })
</script>
