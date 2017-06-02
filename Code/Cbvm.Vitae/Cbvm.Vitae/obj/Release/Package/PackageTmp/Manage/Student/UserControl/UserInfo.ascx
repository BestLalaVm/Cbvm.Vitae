<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.ascx.cs" Inherits="Cbvm.Vitae.Manage.Student.UserControl.UserInfo" %>
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
                        <th>学号:
                        </th>
                        <td class="disabled">
                            <asp:TextBox ID="txt_StudentNum_" runat="server" Enabled="false" ReadOnly="True" CssClass="edit-text"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <th>姓名:
                        </th>
                        <td class="disabled">
                            <asp:TextBox ID="txt_NameZh_" runat="server" Enabled="false" CssClass="edit-text" MaxLength="10"></asp:TextBox>
                        </td>

                        <th>级别:
                        </th>
                        <td class="disabled">
                            <asp:DropDownList ID="drp_Period_" runat="server" Enabled="false" Width="182px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>英文名:
                        </th>
                        <td>
                            <asp:TextBox ID="txt_NameEn_" runat="server" CssClass="edit-text" MaxLength="30"></asp:TextBox>
                        </td>
                        <th>所属专业:
                        </th>
                        <td class="disabled">
                            <asp:DropDownList ID="drp_MarjorCode_" runat="server" Width="182px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>性别:
                        </th>
                        <td>
                            <asp:RadioButtonList ID="rdo_Sex_" runat="server" RepeatDirection="Horizontal">
                            </asp:RadioButtonList>
                        </td>
                        <th>班级:
                        </th>
                        <td class="disabled">
                            <asp:TextBox ID="txt_Class_" runat="server" Enabled="false" CssClass="edit-text" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>籍贯:
                        </th>
                        <td>
                            <asp:TextBox ID="txt_NativePlace_" runat="server" Width="158px" CssClass="edit-text" MaxLength="100"></asp:TextBox>
                        </td>
                        <th>政治面貌:
                        </th>
                        <td>
                            <asp:DropDownList ID="drp_Politics_" runat="server" Width="182px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>出生日期:
                        </th>
                        <td>
                            <telerik:RadDatePicker ID="rad_Birthday_" runat="server" Width="180px">
                                <DateInput ID="DateInput1" DateFormat="yyyy-MM-dd" runat="server">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <th>身高:
                        </th>
                        <td>
                            <asp:TextBox ID="txt_Tall_" runat="server" CssClass="edit-text" MaxLength="3" Width="40px"></asp:TextBox>
                            <label class="unit">cm</label>
                        </td>
                    </tr>
                    <tr>
                        <th>个人网站:
                        </th>
                        <td colspan="3">
                            <asp:TextBox ID="txt_WebSite_" runat="server" Width="470px" Height="25px" CssClass="multi-edit-text" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th class="multiLine">描述:
                        </th>
                        <td colspan="4">
                            <asp:TextBox ID="txt_Description_" runat="server" Width="470px" TextMode="MultiLine" CssClass="multi-edit-text" MaxLength="500"
                                Height="122px"></asp:TextBox>
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
    <div class="block persional">
        <div class="caption">
            <h3>个人隐私</h3>
        </div>
        <div class="container">
            <table>
                <tr>
                    <th>身份证:
                    </th>
                    <td>
                        <asp:TextBox ID="txt_IDentityNum_" runat="server" Width="145px" CssClass="edit-text" MaxLength="18"></asp:TextBox>
                    </td>
                    <th>婚姻状况:
                    </th>
                    <td>
                        <asp:RadioButtonList ID="rdo_Married_" runat="server" RepeatDirection="Horizontal">
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <th>Email:
                    </th>
                    <td>
                        <asp:TextBox ID="txt_Email_" runat="server" Width="145px" CssClass="edit-text" MaxLength="50"></asp:TextBox>
                    </td>
                    <th>手机:
                    </th>
                    <td>
                        <asp:TextBox ID="txt_Telephone_" runat="server" Width="176px" CssClass="edit-text" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>通讯地址:
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txt_Address_" runat="server" Width="470px" Height="25px" CssClass="multi-edit-text" MaxLength="300"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th></th>
                    <td colspan="3">
                        <asp:CheckBox ID="chkIsOnline" runat="server" Text="公开个人隐私信息" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="block persional">
        <div class="caption">
            <h3>个人求职意向</h3>
        </div>
        <div class="container">
            <table>
                <tr>
                    <th>工作地点:</th>
                    <td>
                    <%--    <asp:TextBox runat="server" ID="txt_JobAddress_" Width="470px" Height="25px" CssClass="multi-edit-text"></asp:TextBox>--%>
                        <asp:TextBox ID="txt_JobAddress_" runat="server" Width="470px" Height="25px" CssClass="multi-edit-text" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>期望待遇:</th>
                    <td>
                        <asp:DropDownList runat="server" ID="drp_JobSalary_" Width="200px" />
                    </td>
                </tr>
                <tr>
                    <th>可见用户:</th>
                    <td>
                        <asp:CheckBoxList runat="server" ID="chk_OpenType_" RepeatDirection="Horizontal" />
                    </td>
                </tr>
                <tr>
                    <th valign="top">期望岗位:</th>
                    <td>
                        <asp:TextBox runat="server" ID="txt_JobContent_" TextMode="MultiLine" Width="470px" Height="60px" CssClass="multi-edit-text"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th valign="top">其他要求:</th>
                    <td>
                        <asp:TextBox runat="server" ID="txt_JobRequired_" TextMode="MultiLine" Width="470px" Height="60px" CssClass="multi-edit-text"></asp:TextBox>
                    </td>
                </tr>

            </table>
        </div>
    </div>
    <div class="block action">
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" />
    </div>
</div>
<script type="text/javascript">
    $(function () {
        $("input.edit-text").jqxInput({ height: 25, width: 180 });
        $(".multi-edit-text").jqxInput();
        $(".RadInput input.riTextBox").jqxInput();

        $(".upload-edit-text").jqxInput({ height: 25, width: 120 });
    });

    function jqxInputExtand(jqElement, param) {
        if (jqElement.attr("id") == "<%=txt_Tall_.ClientID%>") {
            param.width = "100px";
            jqElement.jqxInput(param);
            return true;
        }
    }

    function initValidation() {
        $("#<%=txt_Tall_.ClientID%>").rules("add", {
            number: true,
            required: true,
            maxlength: 3,
            messages: {
                number: "<%=Resources.ValidateResourceMsg.Number%>",
                required: "<%=Resources.ValidateResourceMsg.Required %>",
                maxlength: jQuery.format("<%=Resources.ValidateResourceMsg.Maxlength%>")
            }
        });
        $("#<%=txt_Email_.ClientID%>").rules("add", {
            required: true,
            email: true,
            messages: {
                required: "<%=Resources.ValidateResourceMsg.Required %>",
                email: "<%=Resources.ValidateResourceMsg.Email%>"
            }
        });

        $("#<%=txt_Telephone_.ClientID%>").rules("add", {
            customPhone: true,
            messages: {
                customPhone: "<%=Resources.ValidateResourceMsg.Telephone%>"
            }
        });

        $("#<%=txt_IDentityNum_.ClientID%>").rules("add", {
            identityNum: true,
            messages: {
                identityNum: "<%=Resources.ValidateResourceMsg.IDentityNum%>"
            }
        });
    }
</script>
