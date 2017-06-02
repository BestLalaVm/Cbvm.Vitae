<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true"
    CodeBehind="StudenyDailyEssayDetail.aspx.cs" Inherits="Cbvm.Vitae.Manage.Student.StudenyDailyEssayDetail"  Theme="StudentManage"%>

<%@ Register Src="~/Manage/UserControl/EditorControl.ascx" TagName="EditorControl"
    TagPrefix="uc" %>
<%@ Register Src="~/Manage/UserControl/CommentList.ascx" TagName="CommentList" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .body-content .main .wrap-container .wrap-right .func-block table th
        {
            width: 60px;
        }
    </style>
    
    <script type="text/javascript">
        //$(function () {
        //    $("input.edit-text").jqxInput({ height: 25, width: 500 });

        //    $(".upload-edit-text").jqxInput({ height: 25, width: 120 });
        //})
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
                                <td style="width:300px;">
                                    <asp:TextBox ID="txt_Title_" runat="server" Width="380px" MaxLength="50" CssClass="edit-text"></asp:TextBox>
                                </td>
                                <td>
                                  <asp:RequiredFieldValidator ID="rqvName" runat="server" ControlToValidate="txt_Title_"
                                    Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>标题不能为空!</label></span>"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <th class="multiLine">
                                    描述:
                                </th>
                                <td colspan="2">
                                    <uc:EditorControl ID="txt_Content_" runat="server" EditorHeight="460" EditorWidth="780">
                                    </uc:EditorControl>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                </th>
                                <td>
                                    <asp:CheckBox ID="chk_IsOnline_" runat="server" Text="是否公开" />
                                </td>
                                <td>
                                
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="block action">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click"
                        Text="保存"></asp:Button>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="PageCommentView" runat="server">
               <uc:CommentList ID="cmtDailyEssayList" runat="server"></uc:CommentList>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
</asp:Content>
