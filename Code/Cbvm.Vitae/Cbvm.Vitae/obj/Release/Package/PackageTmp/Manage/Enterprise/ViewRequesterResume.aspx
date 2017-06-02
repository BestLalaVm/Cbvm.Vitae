<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/SubMaster.Master" AutoEventWireup="true" CodeBehind="ViewRequesterResume.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.Enterprise.ViewRequesterResume" Theme="EnterpriseManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .body-content .main {
            width: 990px !important;
            margin-left: auto;
            margin-right: auto;
        }

            .body-content .main .action {
                border-bottom: 1px solid #c1c1c1;
                margin-top: 2px !important;
                padding-right: 10px;
                padding-bottom: 10px;
            }
    </style>
    <script type="text/javascript">
        var FFextraHeight = 0;
        function dynIframeSize(iframename) {

            var pTar = null;
            var h = 400; if (document.getElementById) {
                             pTar = document.getElementById(iframename);
                         }
            else {
                             eval('pTar = ' + iframename + ';');
                         }
            if (pTar) {
                //begin resizing iframe   
                pTar.style.display = "block"
                if (pTar.contentDocument && pTar.contentDocument.body && pTar.contentDocument.body.offsetHeight) {
                    //ns6 syntax
                    h = pTar.contentDocument.body.offsetHeight + FFextraHeight;
                }
                else if (pTar.Document && pTar.Document.body && pTar.Document.body.scrollHeight) {
                    //ie5+ syntax  
                    h = pTar.Document.body.scrollHeight + FFextraHeight;
                }
                if (h > 400) {
                    pTar.height = h + 20;
                }
                else {
                    pTar.height = 400;
                }
            }
        }

        /*End Iframe loading*/
        function ResetSize() {
            dynIframeSize("frame");
        }
    </script>
    
    <script type="text/javascript">
        function denyFunc(parameters) {
            Cbvm.Vitae.Ajax.AjaxService.DenyRequest(<%=JobRequestID%>, function(result) {
                if (result.IsSucess) {
                    alert("拒绝成功!");
                }
            }, function(result) {
                alert("操作失败!");
            });
        }
        
        function closeFunc() {
            window.close();
            //parent.removeFrameDialog();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="action">
        <input width="100px" type="button" value="拒绝" d="btnDeny" onclick="denyFunc();"/>       
        <input width="100px" type="button" value="关闭" d="btnClose" onclick="closeFunc()"/>       
    </div>
    <iframe id="frame" src="../../Template/StudentTemplate/ViewStudentResume.aspx?StudentNum=<%=StudentNum %>" frameborder="0" scrolling="yes" width="100%" height="100%"></iframe>
    
    
</asp:Content>
