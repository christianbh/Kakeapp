<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Cake.UI.Models.Forms.StartPageForm>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <table>
        <tr>
            <th><%:Html.LabelFor(model => model.CakeSchedule.NextDate)%></th>
            <th><%:Html.LabelFor(model => model.NextDepartment.ContactName)%></th>
        </tr>

        <tr>
            <td><%:Model.CakeSchedule.NextDate.ToShortDateString()%></td>
            <td><%:Model.NextDepartment.ContactName%></td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2"><%:Html.ActionLink("Ny kontaktperson", "Create", "Department")%></td>
        </tr>
        <tr>
            <td colspan="2"><%:Html.ActionLink("Ny feriedag", "Create", "CakeSchedule")%></td>
        </tr>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
