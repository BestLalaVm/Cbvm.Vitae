<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.ascx.cs" Inherits="Cbvm.Vitae.Manage.Teacher.UserControl.UserInfo" %>
<%@ Register Src="~/Manage/UserControl/UploadControl.ascx" TagName="UploadControl"
    TagPrefix="uc" %>
<div class="func-block user-detail">
    <div class="block user-info">
        <div class="caption">
            <h3>
                <%=GetLocalResourceObject("BaseUserInfo")%>
            </h3>
        </div>
        <div class="container">
            <div class="user-left">
                <table>
                    <tr>
                        <th>工号:
                        </th>
                        <td>
                            <asp:TextBox ID="txt_TeacherNum_" runat="server" Enabled="false" ReadOnly="True" CssClass="edit-text" MaxLength="30"></asp:TextBox>
                        </td>
                        <th>籍贯:
                        </th>
                        <td>
                            <asp:TextBox ID="txt_NativePlace_" runat="server" CssClass="edit-text" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>中文名:
                        </th>
                        <td>
                            <asp:TextBox ID="txt_NameZh_" runat="server" CssClass="edit-text" MaxLength="10"></asp:TextBox>
                        </td>
                        <th>性别:
                        </th>
                        <td>
                            <asp:RadioButtonList ID="rdo_Sex_" runat="server" RepeatDirection="Horizontal">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <th>英文名:
                        </th>
                        <td>
                            <asp:TextBox ID="txt_NameEn_" runat="server" CssClass="edit-text" MaxLength="30"></asp:TextBox>
                        </td>
                        <th>专业:
                        </th>
                        <td>
                            <asp:TextBox ID="txt_MarjorName_" runat="server" CssClass="edit-text" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>毕业学校:
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txt_School_" runat="server" Width="478px" Height="25px" CssClass="multi-edit-text" MaxLength="40"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>常用电话:
                        </th>
                        <td>
                            <asp:TextBox ID="txt_ContactPhone_" runat="server" CssClass="edit-text" MaxLength="20"></asp:TextBox>
                        </td>
                        <th>Email:
                        </th>
                        <td>
                            <asp:TextBox ID="txt_Email_" runat="server" CssClass="edit-text" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th class="multiLine">描述:
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txt_Description_" runat="server" Width="478px" TextMode="MultiLine" MaxLength="500"
                                Height="122px" CssClass="multi-edit-text"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="user-right upload-image">
                <div class="image-container">
                    <asp:Image ID="imgSource" runat="server" />
                </div>
                <div>
                    <uc:UploadControl ID="upLoadControl" runat="server" MaxWidth="210" MaxHeight="200">
                    </uc:UploadControl>
                </div>
            </div>
            <br class="clear" />
        </div>
    </div>
    <div class="block user-info persional">
        <div class="caption">
            <h3>
                <asp:CheckBox ID="chkIsOnline" runat="server" Text="公开个人信息" />
            </h3>
        </div>
        <div class="container">
            <table>
                <tr>
                    <th>学历:
                    </th>
                    <td>
                        <asp:DropDownList ID="drp_EducationCode_" runat="server" Width="150px" CssClass="edit-text">
                        </asp:DropDownList>
                    </td>
                    <th>婚姻状况:
                    </th>
                    <td>
                        <asp:RadioButtonList ID="rdo_Married_" runat="server" RepeatDirection="Horizontal">
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <th>工龄:
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txt_WorkingYear_" MaxLength="10"></asp:TextBox>
                    </td>
                    <th>手机:
                    </th>
                    <td>
                        <asp:TextBox ID="txt_Telephone_" runat="server" Width="175px" Height="25px" CssClass="multi-edit-text" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="block action">
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存"></asp:Button>
    </div>
</div>
<script type="text/javascript">
    setTimeout(function () {
        window.parent.ResetSize();
    }, 500);
</script>
