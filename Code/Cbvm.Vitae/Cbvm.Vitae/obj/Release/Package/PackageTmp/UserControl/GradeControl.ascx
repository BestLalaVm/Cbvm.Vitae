<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GradeControl.ascx.cs" Inherits="Cbvm.Vitae.UserControl.GradeControl" %>
<div class="item">
    <asp:PlaceHolder ID="placeTitle" runat="server">
        <div class="title">
            <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>:
        </div>
    </asp:PlaceHolder>
    <telerik:RadRating ID="rcRadRate" SelectionMode="Continuous" EnableEmbeddedSkins="false"
        ItemCount="5" runat="server" />
</div>
