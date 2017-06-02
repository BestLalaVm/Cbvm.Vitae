<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommentWidget.ascx.cs"
    Inherits="Cbvm.Vitae.Template.StudentTemplate.UserControl.CommentWidget" %>
<%@ Import Namespace="LkHelper" %>

<div class="comment">
    <div class="comment-list">
        <div class="caption">
            <h3>评论</h3>
        </div>
        <asp:Repeater ID="rptComment" runat="server">
            <HeaderTemplate>
                <table width="100%" border="1" style="table-layout:fixed;">
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td valign="top">
                        <div class="commentator">
                            <%#Eval("UserName")%>
                        </div>
                    </td>
                    <td>
                        <div class="comment-container">
                            <div class="comment-head">
                                发表于:<%#((DateTime)Eval("CommentTime")).ToCustomerDateString()%>
                            </div>
                            <div class="comment-content">
                                <%#Eval("Comment")%>
                            </div>
                        </div>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div class="new-comment">
        <asp:PlaceHolder runat="server" ID="phContainer">
            <div class="content">
                <telerik:RadTextBox ID="txtComment" runat="server" EmptyMessage="评论" TextMode="MultiLine" MaxLength="2000"
                    Rows="8" Width="586px">
                </telerik:RadTextBox>
                <div>
                    <asp:RequiredFieldValidator ID="rvcComment" runat="server" ControlToValidate="txtComment"
                        ValidationGroup="comment" ErrorMessage="<span class='error'>内容不能为空!</span>" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="action right">
                <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" ValidationGroup="comment" Text="发布" Width="60px" Height="28px" />
            </div>
        </asp:PlaceHolder>
    </div>
</div>
<script type="text/javascript">
    $(function () {
        $("input:submit").jqxButton({
            height: 26
        });
    })
</script>
