<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="StudentPhotoDetail.aspx.cs" Inherits="Cbvm.Vitae.Manage.Student.StudentPhotoDetail" %>

<%@ Register Src="~/Manage/UserControl/CommentList.ascx" TagName="CommentList" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .photo-manage .preview {
            text-align: center;
            vertical-align: middle;
        }

        .body-content .main > .wrap-container .wrap-right .container {
            padding: 0px !important;
        }

        .body-content .main > .wrap-container .wrap-right .photo-manage .photo-list {
            position: relative;
        }

            .body-content .main > .wrap-container .wrap-right .photo-manage .photo-list .container {
                margin-top: 0px;
                display: inline-block;
                overflow: hidden;
                width: 816px;
                margin-left: 24px;
            }

        .photo-manage .photo-list .container ul li {
            width: 50px;
            display: inline-block;
            border: 1px solid #E6D6AB;
        }

            .photo-manage .photo-list .container ul li.selected {
                border-color: #FFB424;
            }

            .photo-manage .photo-list .container ul li:hover {
                border-color: #FFB424;
                border-width: 2px;
            }

            .photo-manage .photo-list .container ul li img {
                max-width: 50px;
                max-height: 80px;
            }

        .photo-manage .photo-list {
            border-bottom: 1px solid #E6D6AB;
        }

        .body-content .main > .wrap-container .wrap-right .comment-list .caption {
            background-color: #fff;
            border-bottom: 1px dashed #E6D6AB;
        }

        .body-content .main > .wrap-container .wrap-right .comment-list .container {
            min-height: 20px;
        }

        .body-content .main .wrap-container .wrap-right .photo-manage .new-comment {
            width: 506px;
            padding-left: 10px;
        }

            .body-content .main .wrap-container .wrap-right .photo-manage .new-comment label {
                top: 8px !important;
            }

            .body-content .main .wrap-container .wrap-right .photo-manage .new-comment .action {
                text-align: right;
            }

        .left-bar, .right-bar {
            height: 58px;
            width: 25px;
            opacity: 0.8;
            background-color: #ddd;
            z-index: 10000;
            position: absolute;
            top: 0px;
        }

        .right-bar {
            right: 0px;
        }

            .left-bar span, .right-bar span {
                width: 25px;
                height: 40px;
                background: url(/Image/scroller-icons.png) 10px center no-repeat;
                display: inline-block;
                left: -5px;
                position: absolute;
                top: 15%;
                cursor: pointer;
            }

            .right-bar span {
                background-position: -46px;
            }
    </style>
    <script type="text/javascript">
        $(function () {
            var picWrapItemWidth = 52;
            var moveWidth = picWrapItemWidth;
            var totalPhotoCount =<%=CurrentDictoryPhotoList.Count%>;
            var picWrapTotalWidth = (picWrapItemWidth+10) * totalPhotoCount;
            var picWrapContainer = $(".photo-manage .photo-list .container ");

            $(".photo-manage .photo-list .container ul").css("width",picWrapTotalWidth);
            var index=$(".photo-manage .photo-list .container ul li.selected").index();
            var scrollLeft = ((index+1)/totalPhotoCount * moveWidth);

            $(".left-bar span").click(function () {
                if(picWrapContainer.scrollLeft() - moveWidth >=0)
                {
                    picWrapContainer.animate({
                        scrollLeft: (picWrapContainer.scrollLeft() - moveWidth)
                    }, 100,AnimateCompleteFunc);
                }else{
                    picWrapContainer.animate({
                        scrollLeft: 0
                    }, 100,AnimateCompleteFunc);
                }
            });

            $(".right-bar span").click(function () {
                if(picWrapContainer.scrollLeft() + moveWidth < picWrapTotalWidth-(totlShownWidth))
                {
                    picWrapContainer.animate({
                        scrollLeft: (picWrapContainer.scrollLeft() + moveWidth)
                    }, 100,AnimateCompleteFunc);
                }else{
                    picWrapContainer.animate({
                        scrollLeft: (picWrapTotalWidth-(totlShownWidth))
                    }, 100,AnimateCompleteFunc);
                }
            });
        });
        function AnimateCompleteFunc() {
             
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <div class="photo-manage">
        <div class="navigate">
            <div class="path">
                <a href="StudentDictoryManage.aspx"><span>个人相册</span></a> > <a href="StudentPhotoManage.aspx?dctId=<%=CurrentDictoryId %>"><span><%=CurrentDictoryName %></span></a> > <span><%=CurrentPicture.Name %></span>
            </div>
        </div>
        <div class="container">
            <div class="photo-list">
                <div class="container">
                    <asp:Repeater ID="rptPhoto" runat="server">
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li class="<%#((bool)Eval("Selected"))?"selected":"" %>">
                                <a href="StudentPhotoDetail.aspx?dctId=<%#Eval("StudentDictoryId") %>&picId=<%#Eval("ID") %>&dctName=<%#Eval("DictoryName") %>">
                                    <img src="<%#Eval("SmallPath") %>" alt="<%#Eval("Name") %>" /></a>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div class="bar">
                    <div class="left-bar">
                        <span></span>
                    </div>
                    <div class="right-bar">
                        <span></span>
                    </div>
                </div>
            </div>
            <div class="preview">
                <img src="<%=CurrentPicture.ThumbPath %>" alt="<%=CurrentPicture.Name %>" />
            </div>
            <div class="comment-list">
                <div class="caption">
                    <span>评论</span>
                    <div style="text-align: right;">
                        <asp:LinkButton ID="linkTop" runat="server" OnClick="linkTop_Click"> <span>设置为封面</span></asp:LinkButton>
                    </div>
                </div>
                <div class="container">
                    <uc:CommentList ID="cmtCommentList" runat="server">
                    </uc:CommentList>
                </div>
            </div>
            <div class="new-comment">
                <div>
                    <asp:TextBox ID="txtComment" runat="server" EmptyMessage="我也说一句" TextMode="MultiLine" Width="500px" Height="50px" MaxLength="1000"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqvComment" runat="server" ErrorMessage="<img src='/Image/invalid.gif' title='内容不能为空!' />" ControlToValidate="txtComment" ValidationGroup="newComment"></asp:RequiredFieldValidator>
                </div>
                <div class="action">
                    <asp:Button ID="btnComment" runat="server" OnClick="btnComment_Click" Text="发表" ValidationGroup="newComment" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
