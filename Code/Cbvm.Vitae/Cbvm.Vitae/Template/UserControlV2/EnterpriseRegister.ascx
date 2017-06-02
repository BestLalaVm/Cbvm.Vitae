<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnterpriseRegister.ascx.cs" Inherits="Cbvm.Vitae.Template.UserControlV2.EnterpriseRegister" %>
<%@ Import Namespace="Presentation.Enum" %>
<style type="text/css">
    .regeditForm {
        width: 660px;
        min-height:50px;
        height:auto;
    }

        .regeditForm > label {
            width: 150px;
            display: inline-block;
        }

        .regeditForm input {
            width: 350px;
        }

    .regeditButton {
        margin-left: 460px;
    }

        .regeditButton .regeditUpdate {
            width: 150px !important;
            height: 40px !important;
        }
    .error {
        color:red;
        display:inline-block;
        margin-left:182px;
        font-size:13px;
        line-height:30px;
    }
    .regeditWord {
        font-size:13px !important;
    }
</style>

<div class="qylbContent clearfix">
    <div class="qylbListTit">
        <div class="qylbTitName"><a href="#">企业注册</a></div>
    </div>
    <div class="regeditInfo">
        <h1>单位会员快速注册</h1>
        <p>
            请准确、如实填写，一个单位只能注册一次；为了保证信息的质量，你提交的注册信息后需要经过我们的审核并且提交营业执照有效复印件、单位介绍信原件及经办人身份证复印件和交纳会员费后才能使用。
        </p>
    </div>
    <div class="regeditForm">
        <label>企业名称</label>:<asp:TextBox ID="txt_Name_" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rvcName" ControlToValidate="txt_Name_" runat="server"
                ValidationGroup="register" CssClass="required" Display="Dynamic" ErrorMessage="<div class='error'><i class='msg-ico'></i><label>企业名称不能为空!</label></div>"></asp:RequiredFieldValidator>
    </div>
    <div class="regeditWord">
        请输入单位名称并用全称输入，一旦注册成功后你自己不能修改单位名称，必须提交营业执照复印件盖公章后由本网修改!
    </div>
    <div class="regeditForm">
        <label>营业执照号码</label>:
        <%--<asp:TextBox ID="txt_LicenseNo_" runat="server"></asp:TextBox>--%>
        <asp:FileUpload  ID="txt_LicenseNo_" runat="server"/>
        <asp:RequiredFieldValidator ID="rvcLicenseNo" ControlToValidate="txt_LicenseNo_"
            ValidationGroup="register" CssClass="required" runat="server" Display="Dynamic"
            ErrorMessage="<div class='error'><i class='msg-ico'></i><label>营业执照号码不能为空!</label></div>"></asp:RequiredFieldValidator>
    </div>
    <div class="regeditForm">
        <label>联系地址</label>:<asp:TextBox ID="txt_Address_" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rvcAddress" ControlToValidate="txt_Address_" CssClass="required"
            ValidationGroup="register" runat="server" Display="Dynamic" ErrorMessage="<div class='error'><i class='msg-ico'></i><label>联系地址不能为空!</label></div>"></asp:RequiredFieldValidator>
    </div>
    <div class="regeditForm">
        <label>联系电话</label>:<asp:TextBox ID="txt_ContactPhoto_" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rvcContactName" ControlToValidate="txt_ContactPhoto_"
            ValidationGroup="register" CssClass="required" runat="server" Display="Dynamic"
            ErrorMessage="<div class='error'><i class='msg-ico'></i><label>联系电话不能为空!</label></div>"></asp:RequiredFieldValidator>
    </div>
    <div class="regeditForm">
        <label>联系人姓名</label>:<asp:TextBox ID="txt_ContactName_" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txt_ContactName_"
            ValidationGroup="register" CssClass="required" runat="server" Display="Dynamic"
            ErrorMessage="<div class='error'><i class='msg-ico'></i><label>联系人姓名不能为空!</label></div>"></asp:RequiredFieldValidator>
    </div>
    <div class="regeditForm">
        <label>电子邮件</label>:<asp:TextBox ID="txt_Email_" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rvcEmail" ControlToValidate="txt_Email_" runat="server"
            ValidationGroup="register" CssClass="required" Display="Dynamic" ErrorMessage="<div class='error'><i class='msg-ico'></i><label>电子邮件不能为空!</label></div>"></asp:RequiredFieldValidator>
       <asp:CustomValidator ID="rvcValidEmail" runat="server" Display="Dynamic"  ValidationGroup="register" CssClass="required" 
            ErrorMessage ="<div class='error'><i class='msg-ico'></i><label>电子邮件格式无效!</label></div>" ControlToValidate="txt_Email_" ClientValidationFunction="ValidateEmail"></asp:CustomValidator>
    </div>
    <div class="regeditForm">
        <label>用户名</label>:<asp:TextBox ID="txt_UserName_" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rvcUserName" ControlToValidate="txt_UserName_" runat="server"
            ValidationGroup="register" CssClass="required" Display="Dynamic" ErrorMessage="<div class='error'><i class='msg-ico'></i><label>用户名不能为空!</label></div>"></asp:RequiredFieldValidator>
    </div>
    <div class="regeditForm">
        <label>密码</label>:<asp:TextBox ID="txt_Password_" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rvcPassword" ControlToValidate="txt_Password_" runat="server"
            ValidationGroup="register" CssClass="required" Display="Dynamic" ErrorMessage="<div class='error'><i class='msg-ico'></i><label>密码不能为空!</label></div>"></asp:RequiredFieldValidator>
    </div>
    <div class="regeditForm">
        <label>确认密码</label>:<asp:TextBox ID="txt_Password2_" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txt_Password2_" runat="server"
            ValidationGroup="register" CssClass="required" Display="Dynamic" ErrorMessage="<div class='error'><i class='msg-ico'></i><label>确认密码不能为空!</label></div>"></asp:RequiredFieldValidator>
        <asp:CustomValidator ID="cvcCompare" runat="server" ControlToValidate="txt_Password2_"
            CssClass="required" ValidationGroup="register" Display="Dynamic" ErrorMessage="<div class='error'><i class='msg-ico'></i><label>确认密码与密码不一致!</label></div>"
            ClientValidationFunction="ValidateFunc"></asp:CustomValidator>
    </div>
    <div class="regeditForm">
        <label>验证码</label>:<asp:TextBox ID="txt_CheckCode_" runat="server"></asp:TextBox>
        <a onclick="ReflashCheckCode();" href="#">
            <img id="imgCheckCode" class="checkCode" src='/CheckCode.aspx?UserType=<%=Presentation.Enum.UserType.Enterprise %>' alt="错误" />
        </a>
        <asp:RequiredFieldValidator ID="rqvCheckCode" ControlToValidate="txt_CheckCode_" runat="server"
            ValidationGroup="register" CssClass="required" Display="Dynamic" ErrorMessage="<div class='error'><i class='msg-ico'></i><label>验证码不能为空!</label></div>"></asp:RequiredFieldValidator>
        <asp:CustomValidator runat="server" ID="cvcCheckCode" ControlToValidate="txt_CheckCode_" Display="Dynamic" ValidationGroup="register" CssClass="required"
            OnServerValidate="cvcCheckCode_ServerValidate" ErrorMessage="<div class='error'><i class='msg-ico'></i><label>验证码不匹配!</label></div>"></asp:CustomValidator>
    </div>
    <div class="regeditButton">
        <asp:Button ID="btnRegister" runat="server" CssClass="regeditUpdate" Text="注册提交" OnClick="btnRegister_Click" 
            ValidationGroup="register" Height="30px" Width="80px" />
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

    function ValidateEmail(sender, args) {
        args.IsValid = cbvm.Vitae.Validations.email(args.Value);
    }

    function beforeSubmit() {
        PFullActionRequest();

        return $("form").IsValid();
    }
</script>
