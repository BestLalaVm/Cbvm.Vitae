<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="ChildDailyEssayDetail.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.Family.Children.ChildDailyEssayDetail" Theme="FamilyManage" %>

<%@ Register Src="~/Manage/UserControl/EditorControl.ascx" TagName="EditorControl"
    TagPrefix="uc" %>
<%@ Register Src="~/Manage/UserControl/CommentList.ascx" TagName="CommentList" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .body-content .main .wrap-container .wrap-right .func-block table th {
            width: 60px;
        }
        .body-content .main table.wrap-container {
            height: auto !important;
        }
        .body-content .main .wrap-container .wrap-right .detail .func-block .tab-block .container > table th {
            width: 60px !important;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $(window).load(function () {
                parent.ResetSize();
            });
            
            $(".edit-text").jqxInput({ height: 25, disabled: true });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <div class="func-block">
        <telerik:RadTabStrip ID="radTabStrip" runat="server" OnClientTabSelected="TabSelected" MultiPageID="radMultipage">
            <Tabs>
                <telerik:RadTab Text="基本信息" Value="0" runat="server" Selected="true">
                </telerik:RadTab>
                <telerik:RadTab Text="相关评论" Value="1" runat="server">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="radMultipage" runat="server">
            <telerik:RadPageView ID="PageView1" runat="server" Selected="true">
                <div class="block tab-block">
                    <div class="container">
                        <table>
                            <tr>
                                <th>
                                    <span class="required">标题</span>:
                                </th>
                                <td style="width: 300px;">
                                    <asp:TextBox ID="txt_Title_" runat="server" Width="380px" MaxLength="50" Enabled="False" CssClass="edit-text"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <th class="multiLine">描述:
                                </th>
                                <td colspan="2">
                                    <uc:EditorControl ID="txt_Content_" runat="server" EditorHeight="460" EditorWidth="750" EditorEnabled="False">
                                    </uc:EditorControl>
                                </td>
                            </tr>
                            <tr>
                                <th></th>
                                <td>
                                    <asp:CheckBox ID="chk_IsOnline_" runat="server" Text="是否公开" Enabled="False" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="PageCommentView" runat="server">
                <uc:CommentList ID="cmtDailyEssayList" runat="server"></uc:CommentList>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
</asp:Content>
