<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditorControl.ascx.cs" Inherits="Cbvm.Vitae.Manage.UserControl.EditorControl" %>
<telerik:RadEditor ID="radEditor1" runat="server" Height="600px" Width="800px" CssClass="editor-description" ImageManager-MaxUploadFileSize="10240000" 
    ImageManager-UploadPaths="~/Image/">
    <Modules>
        <telerik:EditorModule Visible="false" />
    </Modules>
    <Tools>
        <telerik:EditorToolGroup>
            <telerik:EditorTool Name="FontName" />
            <telerik:EditorTool Name="FontSize" />
            <telerik:EditorTool Name="ForeColor" />
            <telerik:EditorTool Name="BackColor" />
            <telerik:EditorSeparator />
            <telerik:EditorTool Name="Bold" ShortCut="CTRL+B" />
            <telerik:EditorTool Name="Italic" ShortCut="CTRL+I" />
            <telerik:EditorTool Name="Underline" ShortCut="CTRL+U" />
            <telerik:EditorTool Name="StrikeThrough" />
            <telerik:EditorSeparator />
            <telerik:EditorTool Name="InsertOrderedList" />
            <telerik:EditorTool Name="InsertUnorderedList" />
            <telerik:EditorSeparator />
            <telerik:EditorTool Name="StripAll" />
            <telerik:EditorTool Name="JustifyLeft" />
            <telerik:EditorTool Name="JustifyCenter" />
            <telerik:EditorTool Name="JustifyRight" />
            <telerik:EditorSeparator />
            <telerik:EditorTool Name="InsertTable" />
            <telerik:EditorTool Name="LinkManager"></telerik:EditorTool>
            <telerik:EditorTool Name="Unlink"></telerik:EditorTool>
            <telerik:EditorTool Name="InsertRowAbove" />
            <telerik:EditorTool Name="InsertRowBelow" />
            <telerik:EditorTool Name="DeleteRow" />
        </telerik:EditorToolGroup>
        <telerik:EditorToolGroup Tag="MainToolbar">
            <telerik:EditorTool Name="Cut" />
            <telerik:EditorTool Name="Copy" ShortCut="CTRL+C" />
            <telerik:EditorTool Name="Paste" ShortCut="CTRL+V" />
            <telerik:EditorSeparator />
            <telerik:EditorTool Name="Undo" ShortCut="CTRL+Z" />
            <telerik:EditorTool Name="Redo" ShortCut="CTRL+Y" />
            <telerik:EditorSeparator />
            <telerik:EditorTool Name="FormatStripper" />
            <telerik:EditorTool Name="ToggleScreenMode" ShortCut="F11" />
            <telerik:EditorTool Name="Print" ShortCut="CTRL+P" />
            <telerik:EditorTool Name="ImageManager" ShortCut="CTRL+L" />
            <telerik:EditorTool Name="FlashManager" ShortCut="CTRL+F2" />
            <telerik:EditorTool Name="MediaManager" ShortCut="CTRL+M" />
            <telerik:EditorTool Name="DocumentManager" ShortCut="CTRL+D" />
        </telerik:EditorToolGroup>
    </Tools>
    <MediaManager MaxUploadFileSize="209720000" />
    <FlashManager MaxUploadFileSize="209720000" />
    <ImageManager MaxUploadFileSize="2097200" />
    <DocumentManager MaxUploadFileSize="209720000" SearchPatterns="*.*" />
</telerik:RadEditor>
