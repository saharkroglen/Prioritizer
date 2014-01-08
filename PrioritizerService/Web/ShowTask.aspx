<%@ Page Language="C#" MasterPageFile="~/web/SiteStruct.Master" AutoEventWireup="true"
    CodeBehind="ShowTask.aspx.cs" Inherits="PrioritizerService.ShowTask" %>

<asp:Content ID="Login" runat="server" ContentPlaceHolderID="cphSiteContent">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Gray" />
    <%@ import namespace="System.Threading" %>
    <%@ register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>
    <script type="text/javascript">
        var showNotification = function (btn) {
            //Ext.Msg.notify("Button Click", "You clicked the " + btn + " button");
        };

//        var showResultText = function (btn, text) {
//            Ext.Msg.notify("Button Click", "You clicked the " + btn + 'button and entered the text "' + text + '".');
//        };
    </script>
    <table>
        <tr><%=kk %>
            <td style="width: 200px; vertical-align: top">
                <ext:Panel runat="server" Region="West" Split="true" Title="Menu" Width="200" Collapsible="true" />
            </td>
            <td>
                <ext:Panel runat="server" Region="Center" Title="Task Viewer" LabelWidth="110" Width=""
                    Height="" Icon="find" BodyPadding="5" Layout="Form" >
                    <Items>
                        <ext:TextField ID="TaskName"  runat="server" FieldLabel="Name" ReadOnly="true" Vtype="" />
                        <ext:TextField ID="EstimatedHours" runat="server" FieldLabel="Estimated Hours" ReadOnly="true" Text=""
                            Vtype="" />
                        <ext:TextField ID="RequestedBy" runat="server" FieldLabel="Requested By" ReadOnly="true" Text=""
                            Vtype="" />
                        <ext:TextField ID="CompletionPercentage" runat="server" FieldLabel="%Completed" ReadOnly="true" Text="" Vtype="" />
                        <ext:TextField ID="Project" runat="server" FieldLabel="Project" Text="" ReadOnly="true" Vtype="" />
                        <ext:TextField ID="Defect" runat="server" FieldLabel="Defect" Text="" ReadOnly="true" Vtype="" />
                        <ext:TextField ID="ActualWork" runat="server" FieldLabel="Actual Work (Hours)" ReadOnly="true" Text=""
                            Vtype="" />
                        <ext:TextField ID="DueDate" runat="server" FieldLabel="Due Date" Text="" ReadOnly="true" Vtype="" />
                        <ext:FieldContainer ID="FieldContainer1" runat="server" Layout="HBoxLayout">
                            <FieldDefaults LabelAlign="Top" />
                            <Items>
                                <ext:TextArea ID="Body" runat="server" FieldLabel="Body" LabelAlign="Top" ReadOnly="true" Height="200px" Flex="1"
                                    Margins="0" />
                                <%--<ext:TextArea ID="ChangeLog" runat="server" FieldLabel="Change Log" LabelAlign="Top" Flex="1" Margins="0" />--%>
                            </Items>
                        </ext:FieldContainer>
                    </Items>
                </ext:Panel>
            </td>
        </tr>
    </table>
    <%--</Items>
    </ext:Viewport>--%>
</asp:Content>
