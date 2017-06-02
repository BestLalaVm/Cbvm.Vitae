<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/SubMaster.Master" AutoEventWireup="true" CodeBehind="StudentDictoryManage.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.Student.StudentDictoryManage" Theme="StudentManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OpenDictoryDetail() {
            window.parent.ShowIframeForm("创建相册", "StudentDictoryDetail.aspx", "560px", "200px", {
                hideOverFlow: true,
                closeCallBackHandler: function () {
                    window.location.reload();
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dictory-manage">
        <div class="action">
            <a id="lkCreateDictory" onclick="OpenDictoryDetail()"><span>创建相册</span></a>
        </div>
        <div class="container">
            <asp:Repeater ID="rptDictory" runat="server">
                <HeaderTemplate>
                    <ul class="dictory-list">
                </HeaderTemplate>
                <ItemTemplate>
                    <li class="dictory-item">
                        <div class="photo-wrap">
                            <div class="photo-bg">
                                <a href="StudentPhotoManage.aspx?dctId=<%#Eval("ID") %>" title="<%#Eval("Description") %>">
                                    <img src="<%#Eval("Photo") %>" />
                                </a>
                            </div>
                            <div class="photo-info">
                                <div class="info-item"><%#Eval("Title") %></div>
                                <div class="info-item">
                                    <span class="total-page">
                                        <%#Eval("TotalPhotos") %></span>
                                    <div class="dictory-action">
                                        <a href="#"><span>编辑</span></a>
                                        <asp:LinkButton ID="linkDelete" runat="server" OnClick="linkDelete_Click"><span>删除</span></asp:LinkButton>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
