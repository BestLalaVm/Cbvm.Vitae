<%@ control language="C#" autoeventwireup="true" codebehind="EnterpriseRegister.ascx.cs"
    inherits="Cbvm.Vitae.Template.UserControl.EnterpriseRegister" %>
<%@ import namespace="Presentation.Enum" %>
<div class="enterprise-register widget">
    <div class="bar">
        <div class="caption">
            企业注册
        </div>
    </div>
    <div class="container">
        <div class="enterprise-head">
            <div class="row">
                <div class="title">
                    <strong>单位会员快速注册</strong>
                </div>
                <div class="detail">
                    <label>
                        请准确、如实填写，一个单位只能注册一次；为了保证信息的质量，你提交的注册信息后需要经过我们的审核并且提交营业执照有效复印件、单位介绍信原件及经办人身份证复印件和交纳会员费后才能使用。
                    </label>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <asp:PlaceHolder ID="placeRegisterContainer" runat="server">
            <div class="enterprise-content">
                <table width="100%">
                    <tr>
                        <th>企业名称:</th>
                        <td>
                            <asp:TextBox ID="txt_Name_" runat="server" Width="300px" EmptyMessage="企业名称" CssClass="edit-text"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rvcName" ControlToValidate="txt_Name_" runat="server"
                                ValidationGroup="register" CssClass="required" Display="Dynamic" ErrorMessage="<div class='error'><i class='msg-ico'></i><label>企业名称不能为空!</label></div>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div class="tip">
                                请输入单位名称并用全称输入，一旦注册成功后你自己不能修改单位名称，<br />
                                必须提交营业执照复印件盖公章后由本网修改!
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <th>营业执照号码:</th>
                        <td>
                            <asp:TextBox ID="txt_LicenseNo_" runat="server" Width="300px" EmptyMessage="营业执照号码" CssClass="edit-text"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rvcLicenseNo" ControlToValidate="txt_LicenseNo_"
                                ValidationGroup="register" CssClass="required" runat="server" Display="Dynamic"
                                ErrorMessage="<div class='error'><i class='msg-ico'></i><label>营业执照号码不能为空!</label></div>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <th>联系地址:</th>
                        <td>
                            <asp:TextBox ID="txt_Address_" runat="server" Width="300px" EmptyMessage="联系地址" CssClass="edit-text"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rvcAddress" ControlToValidate="txt_Address_" CssClass="required"
                                ValidationGroup="register" runat="server" Display="Dynamic" ErrorMessage="<div class='error'><i class='msg-ico'></i><label>联系地址不能为空!</label></div>"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <th>联系电话:</th>
                        <td>
                            <asp:TextBox ID="txt_ContactPhoto_" runat="server" Width="300px" EmptyMessage="联系电话" CssClass="edit-text"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rvcContactPhoto" ControlToValidate="txt_ContactPhoto_"
                                ValidationGroup="register" CssClass="required" runat="server" Display="Dynamic"
                                ErrorMessage="<div class='error'><i class='msg-ico'></i><label>联系电话不能为空!</label></div>"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <th>联系人姓名:</th>
                        <td>
                            <asp:TextBox ID="txt_ContactName_" runat="server" Width="300px" EmptyMessage="联系人姓名" CssClass="edit-text"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rvcContactName" ControlToValidate="txt_ContactName_"
                                ValidationGroup="register" CssClass="required" runat="server" Display="Dynamic"
                                ErrorMessage="<div class='error'><i class='msg-ico'></i><label>联系人姓名不能为空!</label></div>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <th>电子邮件:</th>
                        <td>
                            <asp:TextBox ID="txt_Email_" runat="server" Width="300px" EmptyMessage="电子邮件" CssClass="edit-text"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rvcEmail" ControlToValidate="txt_Email_" runat="server"
                                ValidationGroup="register" CssClass="required" Display="Dynamic" ErrorMessage="<div class='error'><i class='msg-ico'></i><label>联系人姓名不能为空!</label></div>"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <th>用户名:</th>
                        <td>
                            <asp:TextBox ID="txt_UserName_" runat="server" EmptyMessage="用户名" CssClass="edit-text"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rvcUserName" ControlToValidate="txt_UserName_" runat="server"
                                ValidationGroup="register" CssClass="required" Display="Dynamic" ErrorMessage="<div class='error'><i class='msg-ico'></i><label>用户名不能为空!</label></div>"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <th>密码:</th>
                        <td>
                            <asp:TextBox ID="txt_Password_" runat="server" TextMode="Password" EmptyMessage="密码" CssClass="edit-text"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rvcPassword" ControlToValidate="txt_Password_" runat="server"
                                ValidationGroup="register" CssClass="required" Display="Dynamic" ErrorMessage="<div class='error'><i class='msg-ico'></i><label>密码不能为空!</label></div>"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <th>确认密码:</th>
                        <td>
                            <asp:TextBox ID="txt_Password2_" runat="server" TextMode="Password" EmptyMessage="确认密码" CssClass="edit-text"></asp:TextBox></td>
                        <td>
                            <asp:CustomValidator ID="cvcCompare" runat="server" ControlToValidate="txt_Password2_"
                                CssClass="required" ValidationGroup="register" Display="Dynamic" ErrorMessage="<div class='error'><i class='msg-ico'></i><label>确认密码与密码不一致!</label></div>"
                                ClientValidationFunction="ValidateFunc"></asp:CustomValidator></td>
                    </tr>
                    <tr>
                        <th>验证码:</th>
                        <td>
                            <asp:TextBox ID="txt_CheckCode_" runat="server" Width="230px" EmptyMessage="验证码" CssClass="edit-text"></asp:TextBox>
                                                        <a onclick="ReflashCheckCode();" href="#">
                                <img id="imgCheckCode" class="checkCode" src='../../CheckCode.aspx?UserType=<%=Presentation.Enum.UserType.Enterprise %>'
                                    class="checkCode" alt="错误" />
                            </a>
                        </td>
                        <td>

                            <asp:RequiredFieldValidator ID="rqvCheckCode" ControlToValidate="txt_CheckCode_" runat="server"
                                ValidationGroup="register" CssClass="required" Display="Dynamic" ErrorMessage="<div class='error'><i class='msg-ico'></i><label>验证码不能为空!</label></div>"></asp:RequiredFieldValidator>
                            <asp:CustomValidator runat="server" ID="cvcCheckCode" ControlToValidate="txt_CheckCode_" Display="Dynamic" ValidationGroup="register" CssClass="required"
                                OnServerValidate="cvcCheckCode_ServerValidate" ErrorMessage="<div class='error'><i class='msg-ico'></i><label>验证码不匹配!</label></div>"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr class="action">
                        <th></th>
                        <td>
                            <asp:Button ID="btnRegister" runat="server" Text="注册提交" OnClick="btnRegister_Click"
                                ValidationGroup="register" Height="30px" Width="80px" /></td>
                    </tr>
                </table>
            </div>
        </asp:PlaceHolder>
    </div>
</div>
<script type="text/javascript">
    function ValidateFunc(sender, args) {
        var txt_Password_ = $("#<%=txt_Password_.ClientID %>");
        var txt_Password2_ = $("#<%=txt_Password2_.ClientID %>");
        args.IsValid = true;
        if (txt_Password2_.val() != txt_Password_.val()) {
            args.IsValid = false;
        }

    }
    function ReflashCheckCode() {
        var imgCheckCode = document.getElementById("imgCheckCode");
        imgCheckCode.src = "../../CheckCode.aspx?UserType=" + '<%=UserType.Enterprise.ToString() %>&t=' + (new Date()).getMilliseconds().toString();
    }
    $(function () {
        $(".edit-text").jqxInput({ height: 30 });
        $("input:submit").jqxButton({
            height: 26
        });
    })
</script>
