<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewStudentResume.aspx.cs"
    Inherits="Cbvm.Vitae.Template.StudentTemplate.ViewStudentResume" Theme="BaseFront" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/UserControl/StudentResumeControl.ascx" TagName="StudentResumeControl"
    TagPrefix="uc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .resume .image-item img {
            max-width: 100px;
            max-height: 500px;
        }

        .resume {
            padding-bottom: 20px;
            padding-top: 20px;
            width: 980px;
            margin: auto;
        }

            .resume .caption {
                text-align: center;
            }

            .resume .block table {
                border-collapse: collapse;
                width: 100%;
            }

            .resume .block .user-image img {
                max-height: 200px;
                max-width: 200px;
            }

            .resume .block > table th {
                width: 100px;
                background-color: rgb(249, 243, 243);
            }

            .resume .block > table .summary {
                text-align: left;
                background-color: rgb(252, 231, 230);
            }

            .resume .block .image-block {
                float: left;
                margin: 5px;
                text-align: center;
            }

            .resume .block .item-block {
                margin: 10px;
                width: 98%;
            }

            .resume .block .grade-item,
            .resume .block .grade-caption {
                float: left;
            }

            .resume .block .grade-caption {
                margin: 0px 10px;
                line-height: 25px;
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

        body {
            background: #fff url() !important;
        }

        .resume .description {
            min-height: 80px;
        }
    </style>
    <script type="text/javascript" src="/Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if (typeof parent.ResetSize == "function") {
                setTimeout(function() {
                    parent.ResetSize();
                }, 1000);
            }
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptManage" runat="server">
        </asp:ScriptManager>
        <uc:StudentResumeControl ID="resumeControl" runat="server">
        </uc:StudentResumeControl>
    </form>
</body>
</html>
