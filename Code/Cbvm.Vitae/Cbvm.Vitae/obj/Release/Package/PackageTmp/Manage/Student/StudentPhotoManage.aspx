<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="StudentPhotoManage.aspx.cs" Inherits="Cbvm.Vitae.Manage.Student.StudentPhotoManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .body-content .main .wrap-container .wrap-right .photo-manage .action {
            margin-bottom:0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <div class="photo-manage">
        <div class="navigate">
            <div class="path">
                <a href="StudentDictoryManage.aspx"><span>个人相册</span></a> > <span><%=CurrentDictory.Name %></span>
            </div>
        </div>
        <div class="action">
            <input type="button" value="开始上传" style="width: 80px;" onclick="BeginUpload()" />
        </div>
        <div class="container">
            <asp:Repeater ID="rptPhoto" runat="server">
                <HeaderTemplate>
                    <ul class="photo-list">
                </HeaderTemplate>
                <ItemTemplate>
                    <li class="photo-item <%#((bool)Eval("IsDictoryPhoto"))?"selected":"" %>">
                        <div class="photo-wrap">
                            <div class="photo-bg">
                                <a href="StudentPhotoDetail.aspx?picId=<%#Eval("ID") %>&dctId=<%#Eval("DictoryId")%>&dctName=<%=CurrentDictory.Name %>">
                                    <img src="<%#Eval("Photo") %>" alt="<%#Eval("Name") %>" />
                                </a>
                            </div>
                            <div class="photo-info">
                                <div class="info-item"><%#Eval("Title") %></div>
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
    <script type="text/javascript">
        function BeginUpload() {
            window.location.href = "StudentPhotoUpload.aspx?dctId=<%=CurrentDictoryId%>";
        }
    </script>
</asp:Content>
