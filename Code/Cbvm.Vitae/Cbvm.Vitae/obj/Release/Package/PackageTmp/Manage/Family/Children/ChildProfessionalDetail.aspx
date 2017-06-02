<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="ChildProfessionalDetail.aspx.cs" 
    Inherits="Cbvm.Vitae.Manage.Family.Children.ChildProfessionalDetail" Theme="FamilyManage"%>
<%@ Register Src="~/Manage/UserControl/EditorControl.ascx" TagName="EditorControl"
    TagPrefix="uc" %>
<%@ Register Src="~/Manage/UserControl/StudentAttachmentControl.ascx" TagName="StudentAttachmentControl"
    TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .attachment-list
        {
            margin-top: 10px;
        }
        .body-content .main .wrap-container .wrap-right .detail .profession-detail table
        {
            width: auto;
        }
        .body-content .main .wrap-container .wrap-right .detail .caption h4, .body-content .main .wrap-container .wrap-right .detail .caption .validate
        {
            display: inline-block;
        }
        .body-content .main .wrap-container .wrap-right .detail .action
        {
            padding-right: 10px;
            margin-top: 10px;
        }
        html body .RadInput .riTextBox, html body .RadInputMgr
        {
            border-style: none !important;
            background: none !important;
        }
        label
        {
            line-height:25px;
         }
         .body-content .main table.wrap-container {
             height: auto !important;
             min-height: 600px;
         }
         .body-content .main > table.wrap-container {
             border: 1px solid #c1c1c1;
         }
    </style>
    <script type="text/javascript">
        $(function() {
            $(window).load(function() {
                parent.ResetSize();
            });
            
            $(".edit-text").jqxInput({ height: 25, disabled: true });
            $(".multi-edit-text").jqxInput({ disabled: true });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <div class="func-block profession-detail">
        <telerik:RadTabStrip ID="radTabs" runat="server" MultiPageID="radMultiPage" Skin="Telerik"
            SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="技能信息" Value="0">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="radMultiPage" runat="server" SelectedIndex="0">
            <telerik:RadPageView ID="PageView1" runat="server">
                <div class="block tab-block">
                    <table>
                        <tr>
                            <th>
                                证书名称:<span class="required">*</span>
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txt_Name_" runat="server" Width="380px" MaxLength="50" Enabled="False" CssClass="edit-text"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                获奖时间:
                            </th>
                            <td colspan="3">
                               <asp:TextBox runat="server" ID="txt_ObtainTime_" Enabled="False" CssClass="edit-text"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="multiLine">
                                描述:
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txt_Description_" runat="server" TextMode="MultiLine" Width="380px" CssClass="multi-edit-text"
                                    Height="100px" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                            </th>
                            <td colspan="3">
                                <asp:CheckBox ID="chk_IsOnline_" runat="server" Text="是否公开" Enabled="False" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="caption">
                    <h4>
                        相关附件</h4>
                </div>
                <uc:studentattachmentcontrol id="grdAttList" runat="server" Enable="False"></uc:studentattachmentcontrol>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
</asp:Content>
