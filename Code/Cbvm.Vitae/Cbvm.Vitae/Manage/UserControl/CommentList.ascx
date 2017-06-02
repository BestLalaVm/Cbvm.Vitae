<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommentList.ascx.cs"
    Inherits="Cbvm.Vitae.Manage.UserControl.CommentList" %>
<div class="comment">
    <div class="comment-list">
        <asp:Repeater ID="rptComment" runat="server">
            <HeaderTemplate>
                <table width="100%" border="1">
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
                                发表于:<%#Eval("CommentTime")%>
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
        <asp:PlaceHolder ID="phCommentEmpty" runat="server" Visible="false">
          <div class="empty-list">
            没有任何评论!
          </div>
        </asp:PlaceHolder>
    </div>
</div>
