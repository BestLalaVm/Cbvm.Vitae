<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserIntroduceInfo.ascx.cs"
    Inherits="Cbvm.Vitae.Manage.Student.UserControl.UserIntroduceInfo" %>
<div class="func-block user-introduce">
    <div class="block">
        <div class="caption">
            <h3>
                <%=GetLocalResourceObject("BaseIntroduce")%>
            </h3>
        </div>
        <div class="container">
            <table>
                <tr>
                    <th>
                        我的兴趣:
                    </th>
                    <td>
                        <asp:TextBox ID="txt_Interest_" runat="server" Width="600px" CssClass="edit-text" MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        喜欢的活动:
                    </th>
                    <td>
                        <asp:TextBox ID="txt_Activity_" runat="server" Width="600px" CssClass="edit-text" MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        喜欢的音乐:
                    </th>
                    <td>
                        <asp:TextBox ID="txt_Music_" runat="server" Width="600px" CssClass="edit-text" MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        喜欢的电影:
                    </th>
                    <td>
                        <asp:TextBox ID="txt_Movie_" runat="server" Width="600px" CssClass="edit-text" MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        喜欢的节目:
                    </th>
                    <td>
                        <asp:TextBox ID="txt_Program_" runat="server" Width="600px" CssClass="edit-text" MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        喜欢的书:
                    </th>
                    <td>
                        <asp:TextBox ID="txt_Book_" runat="server" Width="600px" CssClass="edit-text" MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th class="multiLine">
                        关于我:
                    </th>
                    <td>
                        <asp:TextBox ID="txt_AboutMe_" runat="server" Width="600px" TextMode="MultiLine" CssClass="multi-edit-text" Height="120px" MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="block action">
        <asp:Button ID="btnSave1" runat="server" OnClick="btnSave_Click" Text="保存" UseSubmitBehavior="true"  />
    </div>
</div>
<script type="text/javascript">
    $(function () {
        $("input.edit-text").jqxInput({ height: 25, width: 600 });
        $(".multi-edit-text").jqxInput();
    })
</script>
