<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginControl.ascx.cs"
    Inherits="Cbvm.Vitae.Template.UserControl.LoginControl" %>
<div class="login-block widget">
    <div class="caption">
        <h3>登入</h3>
        <span><a href="<%=LkDataContext.AppConfig.HomePageLink %>" target="_self">返回首页</a> </span>
    </div>
    <div class="data-detail">
        <div class="user-type">
            <asp:RadioButtonList ID="rdoUserType" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="学生" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="教师" Value="1"></asp:ListItem>
                <asp:ListItem Text="企业" Value="2"></asp:ListItem>
                <asp:ListItem Text="学校" Value="9"></asp:ListItem>
                <asp:ListItem Text="学院" Value="8"></asp:ListItem>
                <%--                <asp:ListItem Text="管理员" Value="3"></asp:ListItem>--%>
                <asp:ListItem Text="家长" Value="5"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div class="login-textBox">
            <asp:TextBox ID="txt_UserName_" runat="server" Width="200px" EmptyMessage="用户名" CssClass="edit-text"></asp:TextBox>
        </div>
        <div class="login-textBox">
            <asp:TextBox ID="txt_Password_" runat="server" TextMode="Password" Width="200px" CssClass="edit-text"
                EmptyMessage="密码"></asp:TextBox>
        </div>
        <div class="action">
            <asp:Button ID="btn_Login_" runat="server" OnClick="btn_Login_Click" Text="进入系统"
                Width="75px" />
            <asp:HyperLink ID="linkResetPassword" runat="server" NavigateUrl="~/Manage/Student/ResetPassword.aspx"
                Text="忘记密码" Target="_self"></asp:HyperLink>

        </div>
        <div class="enter-block">
            <asp:Button ID="btnLoginWithGuest" runat="server" OnClick="btnLoginWithGuest_Click" Text="以游客身份进入" Width="120px"></asp:Button>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(function () {
        $(".edit-text").jqxInput({ height: 25 });
        $("input:submit").jqxButton({
            height: 26
        });
        $('#<%=rdoUserType.ClientID %> input:radio').click(function () {
            var value = $(this).val();
            passwordLink(value);
        })

        $('#<%=rdoUserType.ClientID %> input:radio').each(function () {
            if (this.checked) {
                passwordLink(this.value);
            }
        })

        function passwordLink(value) {
            var url = "Manage/Student/ResetPassword.aspx";
            switch (value) {
                //Student        
                case "0":
                    url = "Manage/Student/ResetPassword.aspx";
                    break;
                    //Teacher       
                case "1":
                    url = "Manage/Teacher/ResetPassword.aspx";
                    break;
                    //Enterprise       
                case "2":
                    url = "Manage/Enterprise/ResetPassword.aspx";
                    break;
                    //DepartAdmin       
                case "3":
                    url = "Manage/DepartAdmin/ResetPassword.aspx";
                    break;
                case "5":
                    url = "Manage/Family/ResetPassword.aspx";
                    break;
            }
            $('#<%=linkResetPassword.ClientID %>').attr("href", url);
        }
    });
</script>
