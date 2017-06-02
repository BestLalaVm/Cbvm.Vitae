<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentResume.aspx.cs"
    Inherits="Cbvm.Vitae.Template.StudentResume" %>

<%@ Register Src="~/UserControl/StudentResumeControl.ascx" TagName="StudentResumeControl"
    TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Scripts/jquery-1.9.1.min.js"></script>
    <style type="text/css">
        body {
            margin: 0px;
            font-size: 16px;
            font-family: 华文楷体;
            font-style: normal;
            font-weight: normal;
            font-variant: normal;
            word-wrap: break-word;
            word-spacing: normal;
            white-space: normal;
        }

        .description {
            max-width: 100% !important;
            height: 100px;
        }
        /*简历*/
        .image-item img {
            max-width: 100px;
            max-height: 500px;
        }

        .resume {
            margin-bottom: 130px;
        }

            .resume .caption {
                text-align: center;
                background-color: #fff;
            }

            .resume .block > table {
                border-top: 1px solid #c1c1c1;
                border-bottom: 1px solid #c1c1c1;
                border-left: 0px solid #fff;
                border-bottom: 0px solid #fff;
            }

            .resume .block table {
                border-collapse: collapse;
                width: 100%;
                margin-bottom: 10px;
                border: 1px solid #c1c1c1;
            }

            .resume .block .user-image img {
                max-height: 200px;
                max-width: 200px;
            }

            .resume .block > table th {
                width: 80px;
                background-color: rgb(249, 243, 243);
                text-align: right;
                vertical-align: top;
            }

                .resume .block > table th:first-child {
                    width: 110px;
                }

            .resume .block > table .summary {
                text-align: left;
                background-color: rgb(252, 231, 230);
            }

            .resume .block .image-block {
                float: left;
                margin: 5px;
                text-align: center;
                height: 88px;
                width: 100px;
                position: relative;
                margin-top: 7px;
            }

                .resume .block .image-block .image-item {
                    position: absolute;
                    bottom: 0px;
                }

            .resume .block .item-block {
                margin: 10px;
                width: 98%;
            }

            .resume .block .grade-item, .resume .block .grade-caption {
                float: left;
            }

            .resume .block .grade-caption {
                margin: 0px 10px;
                line-height: 25px;
            }

        .RadRating a, .RadRating a span {
            background-image: url("../Image/Sprite.png");
        }

        .resume .print-container {
            position: relative;
            margin-left: auto;
            width: 140px;
            margin-bottom: 10px;
        }

            .resume .print-container span {
                position: absolute;
                right: 62px;
            }

                .resume .print-container span:hover {
                    text-decoration: underline;
                    cursor: pointer;
                }

            .resume .print-container img {
                margin-left: 44px;
            }
        /*简历*/
    </style>
    <script type="text/javascript">
        $(window).load(function () {
            var index = 0;
            var code = setInterval(function () {
                window.parent.ResetSize();
                index++;
                if (index > 10) {
                    clearInterval(code)
                }
            }, 1000);

            if (typeof window.parent.PFullActionRequestEnd == "function") {
                window.parent.PFullActionRequestEnd();
            }
            if (typeof window.parent.PActionRequestEnd == "function") {
                window.parent.PActionRequestEnd();
            }
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptManage" runat="server">
        </asp:ScriptManager>
        <uc:StudentResumeControl ID="stResume" runat="server">
        </uc:StudentResumeControl>
    </form>
</body>
</html>
